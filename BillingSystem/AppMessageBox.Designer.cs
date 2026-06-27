namespace BillingSystem
{
    partial class AppMessageBox
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
            pnlTop = new Panel();
            lblCaption = new Label();
            pnlBody = new Panel();
            lblMessage = new Label();
            picIcon = new PictureBox();
            pnlButtons = new FlowLayoutPanel();
            btnSecondary = new Button();
            btnPrimary = new Button();
            pnlTop.SuspendLayout();
            pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(31, 78, 121);
            pnlTop.Controls.Add(lblCaption);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(430, 50);
            pnlTop.TabIndex = 0;
            // 
            // lblCaption
            // 
            lblCaption.Dock = DockStyle.Fill;
            lblCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCaption.ForeColor = Color.White;
            lblCaption.Location = new Point(0, 0);
            lblCaption.Name = "lblCaption";
            lblCaption.Padding = new Padding(16, 12, 16, 12);
            lblCaption.Size = new Size(430, 50);
            lblCaption.TabIndex = 0;
            lblCaption.Text = "Message";
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.White;
            pnlBody.Controls.Add(lblMessage);
            pnlBody.Controls.Add(picIcon);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 50);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(18, 16, 18, 12);
            pnlBody.Size = new Size(430, 108);
            pnlBody.TabIndex = 1;
            // 
            // lblMessage
            // 
            lblMessage.Location = new Point(90, 16);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(330, 70);
            lblMessage.TabIndex = 1;
            lblMessage.Text = "Message text";
            lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picIcon
            // 
            picIcon.Location = new Point(12, 16);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(68, 70);
            picIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            picIcon.TabIndex = 0;
            picIcon.TabStop = false;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.White;
            pnlButtons.Controls.Add(btnSecondary);
            pnlButtons.Controls.Add(btnPrimary);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.FlowDirection = FlowDirection.RightToLeft;
            pnlButtons.Location = new Point(0, 158);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(0, 8, 10, 8);
            pnlButtons.Size = new Size(430, 56);
            pnlButtons.TabIndex = 2;
            // 
            // btnSecondary
            // 
            btnSecondary.BackColor = Color.FromArgb(31, 78, 121);
            btnSecondary.DialogResult = DialogResult.No;
            btnSecondary.FlatStyle = FlatStyle.Flat;
            btnSecondary.ForeColor = Color.White;
            btnSecondary.Location = new Point(328, 11);
            btnSecondary.Margin = new Padding(8, 3, 0, 3);
            btnSecondary.Name = "btnSecondary";
            btnSecondary.Size = new Size(92, 32);
            btnSecondary.TabIndex = 1;
            btnSecondary.Text = "No";
            btnSecondary.UseVisualStyleBackColor = false;
            // 
            // btnPrimary
            // 
            btnPrimary.BackColor = Color.FromArgb(31, 78, 121);
            btnPrimary.DialogResult = DialogResult.OK;
            btnPrimary.FlatStyle = FlatStyle.Flat;
            btnPrimary.ForeColor = Color.White;
            btnPrimary.Location = new Point(228, 11);
            btnPrimary.Margin = new Padding(8, 3, 0, 3);
            btnPrimary.Name = "btnPrimary";
            btnPrimary.Size = new Size(92, 32);
            btnPrimary.TabIndex = 0;
            btnPrimary.Text = "OK";
            btnPrimary.UseVisualStyleBackColor = false;
            // 
            // AppMessageBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(430, 214);
            Controls.Add(pnlBody);
            Controls.Add(pnlButtons);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AppMessageBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            pnlTop.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblCaption;
        private Panel pnlBody;
        private Label lblMessage;
        private PictureBox picIcon;
        private FlowLayoutPanel pnlButtons;
        private Button btnSecondary;
        private Button btnPrimary;
    }
}
