using System.Drawing;
using System.Windows.Forms;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class AppMessageBox : Form
    {
        public AppMessageBox(
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            InitializeComponent();

            Text = caption;
            lblCaption.Text = caption;
            lblMessage.Text = text;
            picIcon.Image = GetIconBitmap(icon);

            ConfigureButtons(buttons);
            PlaySoundForIcon(icon);
        }

        public static DialogResult Show(string text)
        {
            return Show(text, "Message", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static DialogResult Show(string text, string caption)
        {
            return Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static DialogResult Show(
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            using var dialog = new AppMessageBox(text, caption, buttons, icon);
            return dialog.ShowDialog();
        }

        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            using var dialog = new AppMessageBox(text, caption, buttons, icon);
            return dialog.ShowDialog(owner);
        }

        private void ConfigureButtons(MessageBoxButtons buttons)
        {
            btnPrimary.Visible = true;
            btnSecondary.Visible = false;

            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                    btnPrimary.Text = "Yes";
                    btnPrimary.DialogResult = DialogResult.Yes;
                    btnSecondary.Text = "No";
                    btnSecondary.DialogResult = DialogResult.No;
                    btnSecondary.Visible = true;
                    AcceptButton = btnPrimary;
                    CancelButton = btnSecondary;
                    break;
                default:
                    btnPrimary.Text = "OK";
                    btnPrimary.DialogResult = DialogResult.OK;
                    AcceptButton = btnPrimary;
                    CancelButton = btnPrimary;
                    break;
            }
        }

        private static Bitmap? GetIconBitmap(MessageBoxIcon icon)
        {
            Icon? systemIcon = icon switch
            {
                MessageBoxIcon.Error => SystemIcons.Error,
                MessageBoxIcon.Warning => SystemIcons.Warning,
                MessageBoxIcon.Information => SystemIcons.Information,
                MessageBoxIcon.Question => SystemIcons.Question,
                _ => null
            };

            return systemIcon?.ToBitmap();
        }

        private static void PlaySoundForIcon(MessageBoxIcon icon)
        {
            if (icon == MessageBoxIcon.Error || icon == MessageBoxIcon.Warning)
            {
                UiSoundPlayer.PlayError();
            }
        }
    }
}
