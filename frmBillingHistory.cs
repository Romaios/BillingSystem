using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class frmBillingHistory : Form
    {
        // The customer whose billing history is shown. Passed as an ID (not a
        // name) so the form looks everything up safely from the database.
        private readonly int _customerId;

        public frmBillingHistory(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
        }

        private void frmBillingHistory_Load(object sender, EventArgs e)
        {
            LoadCustomerName();
            LoadBillingRecords();
        }

        // Retrieves the customer's full name FROM the database (§3.4 — never
        // trust a name passed in as plain text) and shows it at the top.
        private void LoadCustomerName()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = "SELECT FullName FROM Customers WHERE CustomerID = @CustomerID;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", _customerId);

                        object result = cmd.ExecuteScalar();
                        string fullName = result?.ToString() ?? "(unknown customer)";

                        lblCustomerName.Text = $"Billing History — {fullName}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer name:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Loads ONLY the selected customer's billing records (parameterized) and
        // binds them to the grid, then renames the headers for readability.
        private void LoadBillingRecords()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Oldest -> newest. Ordered by BillingDate because BillingMonth
                    // is stored as text (e.g. "January 2025") and would not sort
                    // chronologically on its own.
                    string sql = @"SELECT BillingMonth,
                                          PreviousReading,
                                          PresentReading,
                                          Consumption,
                                          RatePerCubic,
                                          TotalAmount,
                                          Status
                                   FROM   Billing
                                   WHERE  CustomerID = @CustomerID
                                   ORDER  BY BillingDate ASC;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", _customerId);

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvBilling.DataSource = dt;

                            // Friendly column headers
                            if (dgvBilling.Columns.Count > 0)
                            {
                                dgvBilling.Columns["BillingMonth"].HeaderText = "Billing Month";
                                dgvBilling.Columns["PreviousReading"].HeaderText = "Previous Reading";
                                dgvBilling.Columns["PresentReading"].HeaderText = "Present Reading";
                                dgvBilling.Columns["Consumption"].HeaderText = "Consumption";
                                dgvBilling.Columns["RatePerCubic"].HeaderText = "Rate Per Cubic";
                                dgvBilling.Columns["TotalAmount"].HeaderText = "Total Amount";
                                dgvBilling.Columns["Status"].HeaderText = "Status";
                            }

                            // Popup 2 — the customer exists but has no billing records.
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show(
                                    "No billing records were found for this customer.",
                                    "Billing History",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading billing records:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvBilling_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}