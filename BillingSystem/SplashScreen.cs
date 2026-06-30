using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingSystem.Utils
{
    public enum SplashScreenMode
    {
        Startup,
        Exit
    }

    public partial class SplashScreen : Form
    {
        private const int SplashSoundDurationMs = 5000;
        private const double FadeStep = 0.08d;
        private const int EntranceOffset = 18;
        private readonly SplashScreenMode _mode;
        private int _frameIndex;
        private int _introOffset;
        private bool _isFadingOut;
        private bool _transitionPending;
        private LoginForm? _preparedLoginForm;
        private Point _appNameBaseLocation;
        private Point _taglineBaseLocation;
        private Point _spinnerBaseLocation;
        private Point _gifBaseLocation;
        private Point _statusIconBaseLocation;
        private Point _loadingBaseLocation;
        private SoundPlayer? _splashPlayer;

        public SplashScreen(SplashScreenMode mode = SplashScreenMode.Startup)
        {
            InitializeComponent();
            _mode = mode;
            FormClosed += SplashScreen_FormClosed;
            DoubleBuffered = true;
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            BackColor = Color.Black;
            ConfigureSplashVisual();
            CacheBaseLocations();
            Opacity = 0d;
            _introOffset = EntranceOffset;
            ApplyEntranceOffset();
            ConfigureForMode();
            animationTimer.Start();
            splashTimer.Start();
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            _frameIndex = (_frameIndex + 1) % 12;
            AdvanceFormAnimation();

            if (pnlSpinner.Visible)
            {
                pnlSpinner.Invalidate();
            }
        }

        private async void splashTimer_Tick(object sender, EventArgs e)
        {
            splashTimer.Stop();
            pnlSpinner.Visible = false;
            picSplashGif.Visible = false;
            lblStatusIcon.Visible = true;
            lblLoading.ForeColor = Color.LightGreen;
            lblLoading.Text = _mode == SplashScreenMode.Startup ? "Ready" : "Goodbye";

            PrepareNextForm();
            await Task.Delay(450);
            BeginFadeOut();
        }

        private void ConfigureForMode()
        {
            if (_mode == SplashScreenMode.Startup)
            {
                lblTagline.Text = "Water Billing Management System";
                lblLoading.ForeColor = Color.LightBlue;
                lblLoading.Text = "Loading...";
                splashTimer.Interval = SplashSoundDurationMs;
                PlaySound("SplashSound.wav");
                return;
            }

            lblTagline.Text = "Thank you for using the Billing System";
            lblLoading.ForeColor = Color.LightBlue;
            lblLoading.Text = "Closing...";
            splashTimer.Interval = SplashSoundDurationMs;
            PlaySound("Close.wav", "SplashSound.wav");
        }

        private void PlaySound(params string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                string soundPath = Path.Combine(AppContext.BaseDirectory, "Resources", fileName);
                if (!File.Exists(soundPath))
                    continue;

                try
                {
                    _splashPlayer = new SoundPlayer(soundPath);
                    _splashPlayer.Load();
                    _splashPlayer.Play();
                    return;
                }
                catch
                {
                    _splashPlayer?.Dispose();
                    _splashPlayer = null;
                }
            }

            // Ignore audio failures so the splash screen still works.
        }

        private void CacheBaseLocations()
        {
            _appNameBaseLocation = lblAppName.Location;
            _taglineBaseLocation = lblTagline.Location;
            _spinnerBaseLocation = pnlSpinner.Location;
            _gifBaseLocation = picSplashGif.Location;
            _statusIconBaseLocation = lblStatusIcon.Location;
            _loadingBaseLocation = lblLoading.Location;
        }

        private void ApplyEntranceOffset()
        {
            lblAppName.Location = OffsetPoint(_appNameBaseLocation, _introOffset);
            lblTagline.Location = OffsetPoint(_taglineBaseLocation, _introOffset);
            pnlSpinner.Location = OffsetPoint(_spinnerBaseLocation, _introOffset);
            picSplashGif.Location = OffsetPoint(_gifBaseLocation, _introOffset);
            lblStatusIcon.Location = OffsetPoint(_statusIconBaseLocation, _introOffset);
            lblLoading.Location = OffsetPoint(_loadingBaseLocation, _introOffset);
        }

        private void ConfigureSplashVisual()
        {
            string gifPath = Path.Combine(AppContext.BaseDirectory, "Resources", "WaterDrop.gif");

            if (File.Exists(gifPath))
            {
                picSplashGif.ImageLocation = gifPath;
                picSplashGif.Visible = true;
                pnlSpinner.Visible = false;
                return;
            }

            picSplashGif.Visible = false;
            pnlSpinner.Visible = true;
        }

        private static Point OffsetPoint(Point point, int verticalOffset)
        {
            return new Point(point.X, point.Y + verticalOffset);
        }

        private void AdvanceFormAnimation()
        {
            if (_isFadingOut)
            {
                Opacity = Math.Max(0d, Opacity - FadeStep);

                if (Opacity <= 0d)
                {
                    animationTimer.Stop();
                    CompleteTransition();
                }

                return;
            }

            if (Opacity < 1d)
            {
                Opacity = Math.Min(1d, Opacity + FadeStep);
            }

            if (_introOffset <= 0)
                return;

            _introOffset = Math.Max(0, _introOffset - 2);
            ApplyEntranceOffset();
        }

        private void BeginFadeOut()
        {
            _transitionPending = true;
            _isFadingOut = true;
        }

        private void PrepareNextForm()
        {
            if (_mode != SplashScreenMode.Startup || _preparedLoginForm != null)
                return;

            _preparedLoginForm = new LoginForm();

            // Keep the splash visible on top while the login form finishes loading behind it.
            _preparedLoginForm.Show();
            _preparedLoginForm.SendToBack();
            Activate();
        }

        private void CompleteTransition()
        {
            if (!_transitionPending)
                return;

            _transitionPending = false;

            if (_mode == SplashScreenMode.Exit)
            {
                Close();
                return;
            }

            var loginForm = _preparedLoginForm ?? new LoginForm();
            _preparedLoginForm = null;

            // When login form actually closes, close the hidden splash so the app can exit cleanly.
            loginForm.FormClosed += (s, args) => Close();

            if (!loginForm.Visible)
            {
                loginForm.Show();
            }

            Hide();
            loginForm.BringToFront();
            loginForm.Activate();
        }

        private void pnlSpinner_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int spokeCount = 12;
            float centerX = pnlSpinner.Width / 2f;
            float centerY = pnlSpinner.Height / 2f;
            float innerRadius = 18f;
            float outerRadius = 34f;
            float penWidth = 10f;

            for (int i = 0; i < spokeCount; i++)
            {
                int relative = (i - _frameIndex + spokeCount) % spokeCount;
                int alpha = Math.Max(35, 255 - (relative * 18));
                double angle = (Math.PI * 2d * i / spokeCount) - (Math.PI / 2d);

                float startX = centerX + (float)(Math.Cos(angle) * innerRadius);
                float startY = centerY + (float)(Math.Sin(angle) * innerRadius);
                float endX = centerX + (float)(Math.Cos(angle) * outerRadius);
                float endY = centerY + (float)(Math.Sin(angle) * outerRadius);

                using (var pen = new Pen(Color.FromArgb(alpha, Color.White), penWidth))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    e.Graphics.DrawLine(pen, startX, startY, endX, endY);
                }
            }
        }

        private void SplashScreen_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _splashPlayer?.Stop();
            _splashPlayer?.Dispose();
            _splashPlayer = null;
        }
    }
}
