using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BillingSystem.Database;
using BillingSystem.Utils;
using MySql.Data.MySqlClient;

namespace BillingSystem
{
    public partial class ArchivedCustomerForm : Form
    {
        private int _selectedCustomerId;
        private bool _ignoreSelection = true;

        public ArchivedCustomerForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            FormResizer.Enable(this);
            Shown += ArchivedCustomerForm_Shown;
        }

        private void ArchivedCustomerForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadArchivedCustomers(showEmptyMessage: true);
        }

        private void ArchivedCustomerForm_Shown(object? sender, EventArgs e)
        {
            ClearArchivedSelection();
            _ignoreSelection = false;
        }

        private void ConfigureDataGridView()
        {
            dgvArchivedLIst.AutoGenerateColumns = false;
            dgvArchivedLIst.AllowUserToAddRows = false;
            dgvArchivedLIst.AllowUserToDeleteRows = false;
            dgvArchivedLIst.MultiSelect = true;
            dgvArchivedLIst.ReadOnly = true;
            colCustomerID.DataPropertyName = "CustomerID";
            colFullName.DataPropertyName = "FullName";
            colAddress.DataPropertyName = "Address";
            colContactNumber.DataPropertyName = "ContactNumber";
            colEmail.DataPropertyName = "Email";
            colBalance.DataPropertyName = "Balance";
            colStatus.DataPropertyName = "Status";
        }

        private void LoadArchivedCustomers(bool showEmptyMessage)
        {
            try
            {
                CustomerArchiveService.EnsureArchiveColumnExists();

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT CustomerID,
                                          FullName,
                                          Address,
                                          ContactNumber,
                                          Email,
                                          Balance,
                                          Status
                                   FROM   Customers
                                   WHERE  IsArchived = 1
                                   ORDER  BY CustomerID ASC;";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvArchivedLIst.DataSource = dt;
                        ApplyArchivedGridSort();
                        lblTitle.Text = $"Archived Customers ({dt.Rows.Count} record(s))";

                        if (showEmptyMessage && dt.Rows.Count == 0)
                        {
                            AppMessageBox.Show(
                                "No archived customer records were found.",
                                "Archived Customers",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error loading archived customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearArchivedSelection()
        {
            dgvArchivedLIst.ClearSelection();
            dgvArchivedLIst.CurrentCell = null;
            _selectedCustomerId = 0;
        }

        private void ApplyArchivedGridSort()
        {
            if (dgvArchivedLIst.Rows.Count == 0 || !dgvArchivedLIst.Columns.Contains("colCustomerID"))
                return;

            dgvArchivedLIst.Sort(dgvArchivedLIst.Columns["colCustomerID"], ListSortDirection.Ascending);
        }

        private void dgvArchivedLIst_SelectionChanged(object sender, EventArgs e)
        {
            if (_ignoreSelection) return;
            if (dgvArchivedLIst.CurrentRow == null) return;

            object? idValue = dgvArchivedLIst.CurrentRow.Cells["colCustomerID"].Value;
            if (idValue != null && int.TryParse(idValue.ToString(), out int customerId))
            {
                _selectedCustomerId = customerId;
            }
            else
            {
                _selectedCustomerId = 0;
            }
        }

        private List<int> GetSelectedArchivedCustomerIds()
        {
            return dgvArchivedLIst.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(row => row.Cells["colCustomerID"].Value?.ToString())
                .Where(value => int.TryParse(value, out _))
                .Select(value => int.Parse(value!))
                .Distinct()
                .OrderBy(id => id)
                .ToList();
        }

        private void UnarchiveCustomers(List<int> customerIds)
        {
            try
            {
                CustomerArchiveService.EnsureArchiveColumnExists();

                int restoredCount = 0;

                foreach (int customerId in customerIds)
                {
                    if (CustomerArchiveService.SetArchivedState(customerId, false))
                    {
                        restoredCount++;
                        AuditLogger.Log("UNARCHIVE_CUSTOMER",
                            $"Customer ID {customerId} restored by {AppSession.CurrentUsername}.");
                    }
                }

                if (restoredCount > 0)
                {
                    string message = restoredCount == 1
                        ? "1 customer restored successfully."
                        : $"{restoredCount} customers restored successfully.";

                    AppMessageBox.Show(message,
                        "Unarchived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadArchivedCustomers(showEmptyMessage: false);
                    ClearArchivedSelection();
                }
                else
                {
                    AppMessageBox.Show("No selected customers could be restored.",
                        "Unarchive Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error restoring customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnarchive_Click(object sender, EventArgs e)
        {
            List<int> selectedCustomerIds = GetSelectedArchivedCustomerIds();

            if (selectedCustomerIds.Count == 0)
            {
                AppMessageBox.Show("Please select at least one customer to unarchive.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmMessage = selectedCustomerIds.Count == 1
                ? "Are you sure you want to restore this customer?"
                : $"Are you sure you want to restore these {selectedCustomerIds.Count} customers?";

            DialogResult confirm = AppMessageBox.Show(
                confirmMessage,
                "Confirm Unarchive",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            UnarchiveCustomers(selectedCustomerIds);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplyTheme()
        {
            BackColor = AppTheme.BackgroundColor;
            pnlTop.BackColor = AppTheme.PrimaryColor;
            pnlBottom.BackColor = Color.FromArgb(242, 242, 242);

            btnUnarchive.BackColor = AppTheme.SuccessColor;
            btnUnarchive.ForeColor = Color.White;
            btnClose.BackColor = AppTheme.SecondaryColor;
            btnClose.ForeColor = Color.White;

            dgvArchivedLIst.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            dgvArchivedLIst.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvArchivedLIst.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvArchivedLIst.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridRowAlt;
        }
    }
}
