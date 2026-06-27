using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BillingSystem.Utils
{
    /// <summary>
    /// Proportionally resizes every child control — size, position AND font —
    /// as the window size changes, so the layout scales to fill the window
    /// (e.g. when the user maximizes or drags an edge) instead of staying a
    /// fixed size. The form keeps its normal design-time startup size.
    ///
    /// Usage: call <see cref="Enable"/> right after InitializeComponent(),
    /// while the form is still at its design-time size (that size becomes the
    /// baseline everything is scaled from).
    /// </summary>
    public class FormResizer
    {
        private readonly Form _form;
        private Size _baseSize;

        // Original design-time bounds and font sizes, captured once and used as
        // the baseline for every subsequent resize.
        private readonly Dictionary<Control, Rectangle> _bounds =
            new Dictionary<Control, Rectangle>();
        private readonly Dictionary<Control, float> _fontSizes =
            new Dictionary<Control, float>();

        private FormResizer(Form form)
        {
            _form = form;
        }

        public static void Enable(Form form)
        {
            var resizer = new FormResizer(form);
            resizer._baseSize = form.ClientSize;          // design size = baseline
            resizer.Store(form);                          // remember original layout

            // Normalize every grid: clean white background, and auto-size rows so
            // their height follows the (scaled) cell font instead of clipping.
            NormalizeGrids(form);
            WireButtonClickSounds(form);

            // Re-flow the controls every time the window size changes
            // (maximize, restore, or drag-resize). The form keeps its normal
            // startup size — we do not force it to open maximized.
            form.Resize += (s, e) => resizer.Apply();
        }

        // Prepares every DataGridView on the form: a clean white background, and
        // rows that auto-size to their content so they grow with the cell font
        // (otherwise the enlarged text is clipped into the fixed design height).
        private static void NormalizeGrids(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
                if (c.HasChildren)
                    NormalizeGrids(c);
            }
        }

        private static void WireButtonClickSounds(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button button)
                {
                    button.MouseDown -= Button_MouseDownPlaySound;
                    button.MouseDown += Button_MouseDownPlaySound;
                }

                if (c.HasChildren)
                    WireButtonClickSounds(c);
            }
        }

        private static void Button_MouseDownPlaySound(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            UiSoundPlayer.PlayClick();
        }

        // Recursively records the design-time bounds and font size of every control.
        private void Store(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                _bounds[c] = c.Bounds;
                _fontSizes[c] = c.Font.Size;
                if (c.HasChildren)
                    Store(c);
            }
        }

        // Recursively rescales every control relative to the original baseline.
        private void Apply()
        {
            if (_baseSize.Width <= 0 || _baseSize.Height <= 0)
                return;
            if (_form.ClientSize.Width <= 0 || _form.ClientSize.Height <= 0)
                return;

            float scaleX = (float)_form.ClientSize.Width / _baseSize.Width;
            float scaleY = (float)_form.ClientSize.Height / _baseSize.Height;
            // Fonts scale uniformly (use the smaller factor) so text never distorts.
            float fontScale = Math.Min(scaleX, scaleY);

            _form.SuspendLayout();
            Scale(_form, scaleX, scaleY, fontScale);
            _form.ResumeLayout(true);
        }

        private void Scale(Control parent, float scaleX, float scaleY, float fontScale)
        {
            foreach (Control c in parent.Controls)
            {
                if (_bounds.TryGetValue(c, out Rectangle b))
                {
                    c.Bounds = new Rectangle(
                        (int)Math.Round(b.X * scaleX),
                        (int)Math.Round(b.Y * scaleY),
                        (int)Math.Round(b.Width * scaleX),
                        (int)Math.Round(b.Height * scaleY));
                }

                if (_fontSizes.TryGetValue(c, out float baseFont))
                {
                    float newSize = Math.Max(1f, baseFont * fontScale);
                    if (Math.Abs(c.Font.Size - newSize) > 0.1f)
                        c.Font = new Font(c.Font.FontFamily, newSize, c.Font.Style);
                }

                // Keep a themed column-header font in proportion with the cells.
                // Headers that inherit dgv.Font (null style font) already scale.
                if (c is DataGridView dgv)
                {
                    var headerStyle = dgv.ColumnHeadersDefaultCellStyle;
                    if (headerStyle.Font != null &&
                        Math.Abs(headerStyle.Font.Size - dgv.Font.Size) > 0.1f)
                    {
                        headerStyle.Font = new Font(headerStyle.Font.FontFamily,
                            dgv.Font.Size, headerStyle.Font.Style);
                    }
                }

                if (c.HasChildren)
                    Scale(c, scaleX, scaleY, fontScale);
            }
        }
    }
}
