using BillingSystem.Database;
using BillingSystem.Utils;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace BillingSystem
{
    public partial class CustomerListForm : Form
    {
        // Stores the CustomerID of the currently selected row.
        // 0 means no customer is currently selected.
        private int _selectedCustomerId = 0;
        private bool _ignoreSelection = true;
        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            // If no row is selected (e.g., grid is empty), do nothing
            if (_ignoreSelection) return;
            if (dgvCustomers.CurrentRow == null) return;

            // Read the CustomerID value from the selected row
            var idCell = dgvCustomers.CurrentRow.Cells["CustomerID"].Value;

            if (idCell != null && int.TryParse(idCell.ToString(), out int id))
            {
                _selectedCustomerId = id;
            }
            else
                _selectedCustomerId = 0;
        }
        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // e.RowIndex is -1 when the header row is double-clicked — ignore it
            if (e.RowIndex < 0) return;

            OpenEditForm();
        }

        public CustomerListForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            WireSelectionClearBehavior();
            this.Shown += CustomerListForm_Shown;
        }
        private void CustomerListForm_Shown(object? sender, EventArgs e)
        {
            ClearCustomerSelection();
            _ignoreSelection = false;
        }

        private void ClearCustomerSelection()
        {
            dgvCustomers.ClearSelection();
            dgvCustomers.CurrentCell = null;
            _selectedCustomerId = 0;
        }

        private void WireSelectionClearBehavior()
        {
            this.MouseDown += ClearSelectionSurface_MouseDown;
            pnlTop.MouseDown += ClearSelectionSurface_MouseDown;
            pnlBottom.MouseDown += ClearSelectionSurface_MouseDown;
            statusStrip1.MouseDown += ClearSelectionSurface_MouseDown;
            lblTitle.MouseDown += ClearSelectionSurface_MouseDown;
            txtSearch.MouseDown += ClearSelectionSurface_MouseDown;
            dgvCustomers.MouseDown += dgvCustomers_MouseDown;
        }

        private void ClearSelectionSurface_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_ignoreSelection) return;
            ClearCustomerSelection();
        }

        private void dgvCustomers_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_ignoreSelection) return;

            var hit = dgvCustomers.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.None)
            {
                ClearCustomerSelection();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)

        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.ShowDialog();
            this.Close();
        }

        private void LoadCustomers()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // SELECT all customers, most recently added first
                    string sql = @"SELECT CustomerID,
                                  FullName,
                                  Address,
                                  ContactNumber,
                                  Email,
                                  Balance,
                                  Status
                           FROM   Customers
                           ORDER  BY FullName ASC;";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the DataTable to the grid
                        dgvCustomers.DataSource = dt;

                        // Improve column headers for readability
                        if (dgvCustomers.Columns.Count > 0)
                        {
                            dgvCustomers.Columns["CustomerID"].HeaderText = "ID";
                            dgvCustomers.Columns["FullName"].HeaderText = "Full Name";
                            dgvCustomers.Columns["ContactNumber"].HeaderText = "Contact No.";
                            dgvCustomers.Columns["Balance"].HeaderText = "Balance (₱)";
                        }

                        lblTitle.Text = $"Customer List  ({dt.Rows.Count} record(s))";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void CustomerListForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadCustomers();
            ApplyPermissions();
            InitStatusStrip();
        }


        // Call this inside CustomerListForm_Load after ApplyPermissions()
        private void InitStatusStrip()
        {
            lblStatusUser.Text =
                $"User: {AppSession.CurrentFullName}  |  Role: {AppSession.CurrentRole}";
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblStatusTime.Text = DateTime.Now.ToString("dddd, MMMM dd yyyy   hh:mm:ss tt");
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }



        private void SearchCustomers(string keyword)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Parameterized SELECT with WHERE ... LIKE
                    string sql = @"SELECT CustomerID,
                                  FullName,
                                  Address,
                                  ContactNumber,
                                  Email,
                                  Balance,
                                  Status
                           FROM   Customers
                           WHERE  FullName      LIKE @keyword
                              OR  Address       LIKE @keyword
                              OR  ContactNumber LIKE @keyword
                           ORDER  BY FullName ASC;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        // %keyword% matches the search text anywhere in the column
                        cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvCustomers.DataSource = dt;
                            lblTitle.Text = $"Customer List  ({dt.Rows.Count} result(s))";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customers:\n{ex.Message}",
                    "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // Empty search box → show all customers again
                LoadCustomers();
            }
            else
            {
                SearchCustomers(keyword);
            }

        }


        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns["CustomerID"].DataPropertyName = "CustomerID";
            dgvCustomers.Columns["FullName"].DataPropertyName = "FullName";
            dgvCustomers.Columns["Address"].DataPropertyName = "Address";
            dgvCustomers.Columns["ContactNumber"].DataPropertyName = "ContactNumber";
            dgvCustomers.Columns["Email"].DataPropertyName = "Email";
            dgvCustomers.Columns["Balance"].DataPropertyName = "Balance";
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // Empty search box → show all customers again
                LoadCustomers();
            }
            else
            {
                SearchCustomers(keyword);

            }
        }

        private void OpenEditForm()
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to edit.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open AddCustomerForm in EDIT mode, passing the selected CustomerID
            AddCustomerForm editForm = new AddCustomerForm(_selectedCustomerId);

            // Refresh the grid automatically once the edit form closes
            editForm.FormClosed += (s, args) => LoadCustomers();

            editForm.ShowDialog(this);
        }
        private void DeleteCustomer(int customerId)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Parameterized DELETE — removes exactly one row
                    string sql = "DELETE FROM Customers WHERE CustomerID = @CustomerID;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer deleted successfully.",
                                "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadCustomers();   // Refresh the grid
                            _selectedCustomerId = 0;   // Clear selection tracker
                        }
                        else
                        {
                            MessageBox.Show("Customer could not be deleted. It may no longer exist.",
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Step 1: Make sure a customer is selected
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Step 2: Confirm before deleting — this cannot be undone
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this customer?\n" +
                "All billing records for this customer will also be deleted.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            // Step 3: Only delete if the user clicked Yes
            if (confirm == DialogResult.Yes)
            {
                DeleteCustomer(_selectedCustomerId);
            }
            // If the user clicked No, do nothing — the record is preserved
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            // Open the Analytics Dashboard as a dialog so the
            // Customer List stays open in the background
            frmAnalytics analyticsForm = new frmAnalytics();
            AuditLogger.Log("VIEW_ANALYTICS",
    $"{AppSession.CurrentUsername} opened the Analytics Dashboard.");


            analyticsForm.ShowDialog(this);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export
            if (dgvCustomers.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.",
                    "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Let the user choose where to save the file
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName = "CustomerList.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Customers");

                        // Write column headers in row 1
                        for (int col = 0; col < dgvCustomers.Columns.Count; col++)
                        {
                            worksheet.Cell(1, col + 1).Value = dgvCustomers.Columns[col].HeaderText;
                            worksheet.Cell(1, col + 1).Style.Font.Bold = true;
                        }

                        // Write each data row starting from row 2
                        for (int row = 0; row < dgvCustomers.Rows.Count; row++)
                        {
                            for (int col = 0; col < dgvCustomers.Columns.Count; col++)
                            {
                                var cellValue = dgvCustomers.Rows[row].Cells[col].Value;
                                worksheet.Cell(row + 2, col + 1).Value = cellValue?.ToString() ?? "";
                            }
                        }

                        // Auto-adjust column widths to fit the content
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show("Customer list exported successfully to Excel.",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AuditLogger.Log("EXPORT_EXCEL",
    $"{AppSession.CurrentUsername} exported customer list to Excel.");


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting to Excel:\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export
            if (dgvCustomers.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.",
                    "Export to PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF Document (*.pdf)|*.pdf";
                saveDialog.FileName = "CustomerList.pdf";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (PdfDocument document = new PdfDocument())
                    {
                        // Create a new page set to Landscape orientation
                        PdfPage page = document.AddPage();
                        page.Orientation = PdfSharpCore.PageOrientation.Landscape;

                        using (XGraphics gfx = XGraphics.FromPdfPage(page))
                        {
                            XFont titleFont = new XFont("Arial", 16, XFontStyle.Bold);
                            XFont headerFont = new XFont("Arial", 10, XFontStyle.Bold);
                            XFont cellFont = new XFont("Arial", 9, XFontStyle.Regular);

                            // Title
                            gfx.DrawString("Customer List Report", titleFont, XBrushes.Black,
                                new XRect(0, 20, page.Width, 30), XStringFormats.TopCenter);

                            int columnCount = dgvCustomers.Columns.Count;
                            double margin = 30;
                            double tableWidth = page.Width - (margin * 2);
                            double colWidth = tableWidth / columnCount;
                            double rowHeight = 22;
                            double y = 60;

                            // Draw column headers
                            double x = margin;
                            for (int col = 0; col < columnCount; col++)
                            {
                                gfx.DrawString(dgvCustomers.Columns[col].HeaderText, headerFont,
                                    XBrushes.Black, new XRect(x, y, colWidth, rowHeight),
                                    XStringFormats.CenterLeft);
                                x += colWidth;
                            }

                            y += rowHeight;
                            gfx.DrawLine(XPens.Black, margin, y, page.Width - margin, y);

                            // Draw each data row
                            foreach (DataGridViewRow row in dgvCustomers.Rows)
                            {
                                x = margin;
                                y += rowHeight;

                                // Start a new page if we run out of vertical space
                                if (y > page.Height - margin)
                                {
                                    page = document.AddPage();
                                    page.Orientation = PdfSharpCore.PageOrientation.Landscape;
                                    gfx.Dispose();
                                    y = 40;
                                }

                                for (int col = 0; col < columnCount; col++)
                                {
                                    string text = row.Cells[col].Value?.ToString() ?? "";
                                    gfx.DrawString(text, cellFont, XBrushes.Black,
                                        new XRect(x, y, colWidth, rowHeight),
                                        XStringFormats.CenterLeft);
                                    x += colWidth;
                                }
                            }
                        }

                        document.Save(saveDialog.FileName);
                    }

                    MessageBox.Show("Customer list exported successfully to PDF.",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AuditLogger.Log("EXPORT_PDF",
    $"{AppSession.CurrentUsername} exported customer list to PDF.");



                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting to PDF:\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AuditLogger.Log("LOGOUT",
    $"{AppSession.CurrentFullName} ({AppSession.CurrentRole}) logged out.");
            AppSession.Clear();


        }
        private void ApplyPermissions()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT PermissionName, IsAllowed
                           FROM   UserPermissions
                           WHERE  Role = @Role;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Role", AppSession.CurrentRole);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string permName = reader.GetString("PermissionName");
                                bool isAllowed = reader.GetBoolean("IsAllowed");

                                switch (permName)
                                {
                                    case "AddCustomer":
                                        btnAdd.Enabled = isAllowed; break;
                                    case "EditCustomer":
                                        // Edit is triggered by double-click — disable
                                        // it by blocking the CellDoubleClick handler
                                        dgvCustomers.ReadOnly = !isAllowed;
                                        break;
                                    case "DeleteCustomer":
                                        btnDelete.Enabled = isAllowed; break;
                                    case "Analytics":
                                        btnAnalytics.Enabled = isAllowed; break;
                                    case "ExportExcel":
                                        btnExportExcel.Enabled = isAllowed; break;
                                    case "ExportPdf":
                                        btnExportPdf.Enabled = isAllowed; break;
                                    case "AuditLogs":
                                        btnAuditLog.Enabled = isAllowed; break;
                                    case "ManagePermissions":
                                        btnManagePermissions.Enabled = isAllowed; break;
                                    case "ChangePassword":
                                        btnChangePassword.Enabled = isAllowed; break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading permissions:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAuditLog_Click(object sender, EventArgs e)
        {

            frmAuditLogs auditForm = new frmAuditLogs();
            auditForm.ShowDialog(this);


        }

        private void btnManagePermissions_Click(object sender, EventArgs e)
        {
            frmManagePermissions permForm = new frmManagePermissions();
            permForm.ShowDialog(this);
        }


        private void btnViewBillng_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please Select a Customer to view billing records.",
                    "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmBillingHistory form = new frmBillingHistory(_selectedCustomerId);
            form.ShowDialog(this);

            ClearCustomerSelection();
        }
        private void ApplyTheme()
        {
            // Form background
            this.BackColor = AppTheme.BackgroundColor;

            // Top panel (header bar)
            pnlTop.BackColor = AppTheme.PrimaryColor;
            pnlBottom.BackColor = Color.FromArgb(242, 242, 242);

            // Action buttons
            btnAdd.BackColor = AppTheme.SuccessColor;
            btnAdd.ForeColor = Color.White;
            btnDelete.BackColor = AppTheme.DangerColor;
            btnDelete.ForeColor = Color.White;
            btnAnalytics.BackColor = AppTheme.PrimaryColor;
            btnAnalytics.ForeColor = Color.White;
            btnExportExcel.BackColor = AppTheme.SecondaryColor;
            btnExportExcel.ForeColor = Color.White;
            btnExportPdf.BackColor = AppTheme.SecondaryColor;
            btnExportPdf.ForeColor = Color.White;
            btnAuditLog.BackColor = AppTheme.SecondaryColor;
            btnAuditLog.ForeColor = Color.White;
            btnManagePermissions.BackColor = AppTheme.DangerColor;
            btnManagePermissions.ForeColor = Color.White;

            // DataGridView header colors
            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);

            // Alternating row colors
            dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridRowAlt;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword();


            frm.ShowDialog();
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnusermanagement_Click(object sender, EventArgs e)
        {
            UserListForm frm = new UserListForm();
            frm.ShowDialog(this);
        }
    }
}


