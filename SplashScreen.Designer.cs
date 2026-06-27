namespace BillingSystem
{
    partial class SplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblAppName = new Label();
            lblTagline = new Label();
            lblLoading = new Label();
            splashTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblAppName
            // 
            lblAppName.Font = new Font("Segoe UI", 24F);
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(-50, -3);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(577, 76);
            lblAppName.TabIndex = 0;
            lblAppName.Text = " BILLING SYSTEM";
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTagline
            // 
            lblTagline.AutoSize = true;
            lblTagline.Font = new Font("Segoe UI", 12F);
            lblTagline.ForeColor = Color.FromArgb(189, 215, 238);
            lblTagline.Location = new Point(93, 91);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(321, 28);
            lblTagline.TabIndex = 1;
            lblTagline.Text = " Water Billing Management System ";
            lblTagline.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLoading
            // 
            lblLoading.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblLoading.ForeColor = Color.LightBlue;
            lblLoading.Location = new Point(93, 167);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(103, 25);
            lblLoading.TabIndex = 2;
            lblLoading.Text = "Loading... ";
            lblLoading.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // splashTimer
            // 
            splashTimer.Interval = 2500;
            splashTimer.Tick += splashTimer_Tick;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 36, 126);
            ClientSize = new Size(482, 253);
            Controls.Add(lblLoading);
            Controls.Add(lblTagline);
            Controls.Add(lblAppName);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashScreen";
            Text = "SplashScreen";
            Load += SplashScreen_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAppName;
        private Label lblTagline;
        private Label lblLoading;
        private System.Windows.Forms.Timer splashTimer;
    }
}