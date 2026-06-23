namespace BillingSystem
{
    partial class frmChangePassword
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblNew = new System.Windows.Forms.Label();
            this.lblRetype = new System.Windows.Forms.Label();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.txtRetype = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(186, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Change Password";
            //
            // lblCurrent
            //
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(20, 72);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(133, 20);
            this.lblCurrent.TabIndex = 1;
            this.lblCurrent.Text = "Current Password:";
            //
            // lblNew
            //
            this.lblNew.AutoSize = true;
            this.lblNew.Location = new System.Drawing.Point(20, 112);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(107, 20);
            this.lblNew.TabIndex = 3;
            this.lblNew.Text = "New Password:";
            //
            // lblRetype
            //
            this.lblRetype.AutoSize = true;
            this.lblRetype.Location = new System.Drawing.Point(20, 152);
            this.lblRetype.Name = "lblRetype";
            this.lblRetype.Size = new System.Drawing.Size(126, 20);
            this.lblRetype.TabIndex = 5;
            this.lblRetype.Text = "Retype Password:";
            //
            // txtCurrent
            //
            this.txtCurrent.Location = new System.Drawing.Point(180, 69);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(220, 27);
            this.txtCurrent.TabIndex = 2;
            this.txtCurrent.UseSystemPasswordChar = true;
            //
            // txtNew
            //
            this.txtNew.Location = new System.Drawing.Point(180, 109);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(220, 27);
            this.txtNew.TabIndex = 4;
            this.txtNew.UseSystemPasswordChar = true;
            //
            // txtRetype
            //
            this.txtRetype.Location = new System.Drawing.Point(180, 149);
            this.txtRetype.Name = "txtRetype";
            this.txtRetype.Size = new System.Drawing.Size(220, 27);
            this.txtRetype.TabIndex = 6;
            this.txtRetype.UseSystemPasswordChar = true;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(110, 200);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save Password";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(260, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // frmChangePassword
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 270);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRetype);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.txtCurrent);
            this.Controls.Add(this.lblRetype);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Label lblRetype;
        private System.Windows.Forms.TextBox txtCurrent;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.TextBox txtRetype;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
