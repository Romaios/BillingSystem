using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            FormResizer.Enable(this);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Step 1: Make sure both fields are filled
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                AppMessageBox.Show("Please enter your username.",
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                AppMessageBox.Show("Please enter your password.",
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Step 2: Query the Users table to check credentials
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Parameterized query � safe from SQL injection
                    string sql = @"SELECT UserID, FullName, Role
                           FROM   Users
                           WHERE  Username = @Username
                             AND  Password = @Password;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate AppSession with the logged-in user's details
                                AppSession.CurrentUserID = reader.GetInt32("UserID");
                                AppSession.CurrentUsername = txtUsername.Text.Trim();
                                AppSession.CurrentFullName = reader.GetString("FullName");
                                AppSession.CurrentRole = reader.GetString("Role");

                                // Write a LOGIN audit log entry
                                AuditLogger.Log("LOGIN",
                                    $"{AppSession.CurrentFullName} ({AppSession.CurrentRole}) logged in.");

                                // Open the Customer List Form
                                CustomerListForm listForm = new CustomerListForm(this);
                                listForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                // No match found � wrong credentials
                                AppMessageBox.Show(
                                    "Invalid username or password.\nPlease try again.",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                txtPassword.Clear();
                                txtPassword.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show an error if the database cannot be reached
                AppMessageBox.Show(
                    "Database error:\n" + ex.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Resets the login form for re-use after a logout: clears the
        /// password (and username) and puts focus back on the username box.
        /// </summary>
        public void ResetForLogin()
        {
            txtPassword.Clear();
            txtUsername.Clear();
            txtUsername.Focus();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Test the database connection when the form opens.
            // This gives a clear warning if MySQL is not running.
            if (!DatabaseConnection.TestConnection())
            {
                AppMessageBox.Show(
                    "Cannot connect to the database.\n\n" +
                    "Please make sure:\n" +
                    "  1. MySQL Server is running.\n" +
                    "  2. BillingDB database exists.\n" +
                    "  3. The password in DatabaseConnection.cs is correct.",
                    "Database Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            txtUsername.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult confirm = AppMessageBox.Show(
                "Are you sure you want to exit the application?",
                "Exit Application",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                AuditLogger.Log("LOGIN_CANCEL_EXIT",
                    "The application was closed from the login form.");
                Hide();

                using (var exitSplash = new SplashScreen(SplashScreenMode.Exit))
                {
                    exitSplash.ShowDialog(this);
                }

                Close();
                return;
            }

            AuditLogger.Log("LOGIN_CANCEL_ABORTED",
                "Exit from the login form was cancelled.");
        }



        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }
    }
}

