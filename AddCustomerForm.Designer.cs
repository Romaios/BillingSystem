namespace BillingSystem
{
    partial class AddCustomerForm
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
            lbltitle = new Label();
            lblFullname = new Label();
            lbladdress = new Label();
            txtFullName = new TextBox();
            txtAddress = new TextBox();
            lblcontact = new Label();
            txtContact = new TextBox();
            lblemail = new Label();
            txtEmail = new TextBox();
            lblbalance = new Label();
            txtBalance = new TextBox();
            btnsave = new Button();
            btnclear = new Button();
            btnback = new Button();
            SuspendLayout();
            // 
            // lbltitle
            // 
            lbltitle.AutoSize = true;
            lbltitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbltitle.Location = new Point(89, 44);
            lbltitle.Name = "lbltitle";
            lbltitle.Size = new Size(146, 20);
            lbltitle.TabIndex = 0;
            lbltitle.Text = "Add New Customer\r\n";
            // 
            // lblFullname
            // 
            lblFullname.AutoSize = true;
            lblFullname.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFullname.Location = new Point(25, 79);
            lblFullname.Name = "lblFullname";
            lblFullname.Size = new Size(84, 20);
            lblFullname.TabIndex = 1;
            lblFullname.Text = "Full Name:";
            // 
            // lbladdress
            // 
            lbladdress.AutoSize = true;
            lbladdress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbladdress.Location = new Point(25, 111);
            lbladdress.Name = "lbladdress";
            lbladdress.Size = new Size(70, 20);
            lbladdress.TabIndex = 2;
            lbladdress.Text = "Address:\r\n";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(116, 79);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(220, 27);
            txtFullName.TabIndex = 3;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(116, 112);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(220, 27);
            txtAddress.TabIndex = 4;
            // 
            // lblcontact
            // 
            lblcontact.AutoSize = true;
            lblcontact.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblcontact.Location = new Point(23, 155);
            lblcontact.Name = "lblcontact";
            lblcontact.Size = new Size(92, 20);
            lblcontact.TabIndex = 5;
            lblcontact.Text = "Contact No.";
            // 
            // txtContact
            // 
            txtContact.Location = new Point(116, 155);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(220, 27);
            txtContact.TabIndex = 6;
            // 
            // lblemail
            // 
            lblemail.AutoSize = true;
            lblemail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblemail.Location = new Point(23, 196);
            lblemail.Name = "lblemail";
            lblemail.Size = new Size(51, 20);
            lblemail.TabIndex = 7;
            lblemail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(116, 196);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(220, 27);
            txtEmail.TabIndex = 8;
            // 
            // lblbalance
            // 
            lblbalance.AutoSize = true;
            lblbalance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblbalance.Location = new Point(25, 240);
            lblbalance.Name = "lblbalance";
            lblbalance.Size = new Size(79, 20);
            lblbalance.TabIndex = 9;
            lblbalance.Text = "Initial Bal.";
            // 
            // txtBalance
            // 
            txtBalance.Location = new Point(121, 240);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(215, 27);
            txtBalance.TabIndex = 10;
            txtBalance.Text = "0.00";
            // 
            // btnsave
            // 
            btnsave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnsave.Location = new Point(51, 289);
            btnsave.Name = "btnsave";
            btnsave.Size = new Size(94, 29);
            btnsave.TabIndex = 11;
            btnsave.Text = "Save";
            btnsave.UseVisualStyleBackColor = true;
            btnsave.Click += btnsave_Click;
            // 
            // btnclear
            // 
            btnclear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnclear.Location = new Point(162, 289);
            btnclear.Name = "btnclear";
            btnclear.Size = new Size(94, 29);
            btnclear.TabIndex = 12;
            btnclear.Text = "Clear";
            btnclear.UseVisualStyleBackColor = true;
            // 
            // btnback
            // 
            btnback.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnback.Location = new Point(276, 289);
            btnback.Name = "btnback";
            btnback.Size = new Size(94, 29);
            btnback.TabIndex = 13;
            btnback.Text = "Back";
            btnback.UseVisualStyleBackColor = true;
            // 
            // AddCustomerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 373);
            Controls.Add(btnback);
            Controls.Add(btnclear);
            Controls.Add(btnsave);
            Controls.Add(txtBalance);
            Controls.Add(lblbalance);
            Controls.Add(txtEmail);
            Controls.Add(lblemail);
            Controls.Add(txtContact);
            Controls.Add(lblcontact);
            Controls.Add(txtAddress);
            Controls.Add(txtFullName);
            Controls.Add(lbladdress);
            Controls.Add(lblFullname);
            Controls.Add(lbltitle);
            Name = "AddCustomerForm";
            Text = "Billing System - Add Customer";
            Load += AddCustomerForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbltitle;
        private Label lblFullname;
        private Label lbladdress;
        private TextBox txtFullName;
        private TextBox txtAddress;
        private Label lblcontact;
        private TextBox txtContact;
        private Label lblemail;
        private TextBox txtEmail;
        private Label lblbalance;
        private TextBox txtBalance;
        private Button btnsave;
        private Button btnclear;
        private Button btnback;
    }
}