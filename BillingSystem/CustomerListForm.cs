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
using System.Security.Cryptography;
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

        // The grid auto-selects its first row when data is bound and again during
        // the form's first render. We ignore those automatic selections so the
        // user must deliberately click a row before _selectedCustomerId is set
        // (this keeps the "no customer selected" check / Popup 1 reachable).
        private bool _ignoreSelection = true;
        private const int SidePanelExpandedWidth = 255;
        private const int SidePanelCollapsedWidth = 140;
        private bool _isSidePanelCollapsed;
        private readonly Dictionary<Button, Rectangle> _expandedSideButtonBounds = new();
        private readonly Dictionary<Button, string> _expandedSideButtonTexts = new();
        private readonly Dictionary<Button, Font> _expandedSideButtonFonts = new();
        private readonly Dictionary<Button, string> _collapsedSideButtonIcons = new();
        private readonly List<Button> _sideActionButtons = new();

        // The login form that opened this window. Kept hidden while the user
        // is logged in so logout can re-show it instead of re-creating it.
        private readonly LoginForm? _loginForm;
        private bool _isLoggingOut;
        private bool _returningToLogin;
        private bool _canEditCustomer;

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            // Ignore the grid's automatic selection until the form is shown.
            if (_ignoreSelection) return;

            //If no row is selected (e.g., grid is empty), do nothing
            if (dgvCustomers.CurrentRow == null) return;

            //Read the CustomerID value from the selected row
            var idCell = dgvCustomers.CurrentRow.Cells["CustomerID"].Value;

            if (idCell != null && int.TryParse(idCell.ToString(), out int id))
            {
                _selectedCustomerId = id;
            }
            else
            {
                // Blank/new-row placeholder (or non-numeric) — there is no valid
                // customer here, so forget any previously selected one.
                _selectedCustomerId = 0;
            }
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // e.RowIndex is -1 when the header row is double-clicked - ignore it
            if (e.RowIndex < 0) return;

            if (!_canEditCustomer)
            {
                AppMessageBox.Show("You do not have permission to edit customers.",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenEditForm();
        }

        public CustomerListForm(LoginForm? loginForm = null)
        {
            InitializeComponent();
            InitializeCollapsibleSidePanel();
            ConfigureDataGridView();
            WireSelectionClearBehavior();
            _loginForm = loginForm;
            FormResizer.Enable(this);

            // Clear the grid's automatic selection AFTER the form is fully shown
            // (Load runs too early — the grid re-selects row 0 during first paint).
            this.Shown += CustomerListForm_Shown;
            this.FormClosing += CustomerListForm_FormClosing;
            this.FormClosed += CustomerListForm_FormClosed;
            this.Resize += CustomerListForm_Resize;
        }

        private void InitializeCollapsibleSidePanel()
        {
            _sideActionButtons.AddRange(new[]
            {
                btnAdd,
                btnDelete,
                btnAnalytics,
                btnAuditLog,
                btnChangePassword,
                btnManagePermissions,
                btnUserManagement,
                btnExportExcel,
                btnViewBilling,
                btnExportPdf,
                btnLogout
            });

            foreach (Button button in _sideActionButtons)
            {
                _expandedSideButtonBounds[button] = button.Bounds;
                _expandedSideButtonTexts[button] = button.Text;
                _expandedSideButtonFonts[button] = button.Font;
                sidePanelToolTip.SetToolTip(button, button.Text);
            }

            _collapsedSideButtonIcons[btnAdd] = "➕";
            _collapsedSideButtonIcons[btnDelete] = "🗑";
            _collapsedSideButtonIcons[btnAnalytics] = "📈";
            _collapsedSideButtonIcons[btnAuditLog] = "🧾";
            _collapsedSideButtonIcons[btnChangePassword] = "🔑";
            _collapsedSideButtonIcons[btnManagePermissions] = "🛡";
            _collapsedSideButtonIcons[btnUserManagement] = "👥";
            _collapsedSideButtonIcons[btnExportExcel] = "📊";
            _collapsedSideButtonIcons[btnViewBilling] = "💧";
            _collapsedSideButtonIcons[btnExportPdf] = "📄";
            _collapsedSideButtonIcons[btnLogout] = "↩";

            _collapsedSideButtonIcons[btnAdd] = "\u2795";
            _collapsedSideButtonIcons[btnDelete] = "\uD83D\uDDD1";
            _collapsedSideButtonIcons[btnAnalytics] = "\uD83D\uDCC8";
            _collapsedSideButtonIcons[btnAuditLog] = "\uD83D\uDCCB";
            _collapsedSideButtonIcons[btnChangePassword] = "\uD83D\uDD11";
            _collapsedSideButtonIcons[btnManagePermissions] = "\uD83D\uDEE1";
            _collapsedSideButtonIcons[btnUserManagement] = "\uD83D\uDC65";
            _collapsedSideButtonIcons[btnExportExcel] = "\uD83D\uDCCA";
            _collapsedSideButtonIcons[btnViewBilling] = "\uD83D\uDCB3";
            _collapsedSideButtonIcons[btnExportPdf] = "\uD83D\uDCC4";
            _collapsedSideButtonIcons[btnLogout] = "\u21A9";

            sidePanelToolTip.SetToolTip(btnToggleSidePanel, "Collapse actions");
            UpdateMainLayout();
        }

        private void CustomerListForm_Shown(object? sender, EventArgs e)
        {
            ClearCustomerSelection();

            // From now on, honor genuine user-initiated selections.
            _ignoreSelection = false;
        }

        private void CustomerListForm_Resize(object? sender, EventArgs e)
        {
            UpdateMainLayout();
        }

        // Resets the grid to "no customer selected" so the next action must
        // start from a deliberate row click (don't remember the last pick).
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
        }

        private void UpdateMainLayout()
        {
            const int outerMargin = 12;
            const int gap = 6;

            pnlBottom.Left = ClientSize.Width - outerMargin - pnlBottom.Width;
            pnlBottom.Height = statusStrip1.Top - pnlBottom.Top;
            pnlTop.Width = pnlBottom.Left - gap - pnlTop.Left;
            dgvCustomers.Width = pnlTop.Width;
            dgvCustomers.Height = statusStrip1.Top - dgvCustomers.Top - 3;
            LayoutTopPanelControls();
            LayoutSidePanelButtons();
        }

        private void LayoutTopPanelControls()
        {
            const int rightMargin = 8;
            const int controlGap = 6;
            const int minimumSearchWidth = 140;
            const int titleGap = 20;

            btnSearch.Top = (pnlTop.Height - btnSearch.Height) / 2;
            btnSearch.Left = pnlTop.Width - rightMargin - btnSearch.Width;

            txtSearch.Top = (pnlTop.Height - txtSearch.Height) / 2;

            int availableSearchWidth = btnSearch.Left - controlGap - (lblTitle.Right + titleGap);
            int searchWidth = Math.Max(minimumSearchWidth, Math.Min(292, availableSearchWidth));

            txtSearch.Width = searchWidth;
            txtSearch.Left = btnSearch.Left - controlGap - txtSearch.Width;
        }

        private void LayoutSidePanelButtons()
        {
            const int topMargin = 5;
            const int afterToggleGap = 10;
            const int rowGap = 6;
            const int bottomMargin = 8;

            btnToggleSidePanel.SetBounds(5, topMargin, pnlBottom.Width - 10, 28);

            int topRow1 = btnToggleSidePanel.Bottom + afterToggleGap;
            int topRow2 = topRow1 + btnAdd.Height + rowGap;
            int topRow3 = topRow2 + btnAnalytics.Height + rowGap;
            int topRow4 = topRow3 + btnChangePassword.Height + rowGap;

            btnAdd.Top = topRow1;
            btnDelete.Top = topRow1;
            btnAnalytics.Top = topRow2;
            btnAuditLog.Top = topRow2;
            btnChangePassword.Top = topRow3;
            btnManagePermissions.Top = topRow3;
            btnUserManagement.Top = topRow4;

            int bottomRow2 = pnlBottom.Height - bottomMargin - btnLogout.Height;
            int bottomRow1 = bottomRow2 - rowGap - btnExportExcel.Height;
            int minimumBottomRow1 = btnUserManagement.Bottom + rowGap;

            if (bottomRow1 < minimumBottomRow1)
            {
                bottomRow1 = minimumBottomRow1;
                bottomRow2 = bottomRow1 + btnExportExcel.Height + rowGap;
            }

            btnExportExcel.Top = bottomRow1;
            btnViewBilling.Top = bottomRow1;
            btnExportPdf.Top = bottomRow2;
            btnLogout.Top = bottomRow2;
        }

        private void ApplySidePanelState(bool collapsed)
        {
            _isSidePanelCollapsed = collapsed;
            pnlBottom.Width = collapsed ? SidePanelCollapsedWidth : SidePanelExpandedWidth;
            btnToggleSidePanel.Text = collapsed ? "▶" : "◀";
            sidePanelToolTip.SetToolTip(btnToggleSidePanel,
                collapsed ? "Expand actions" : "Collapse actions");
            btnToggleSidePanel.Text = "☰";
            btnToggleSidePanel.Width = pnlBottom.Width - 10;
            btnToggleSidePanel.Left = 5;
            btnToggleSidePanel.Text = "\u2630";

            foreach (Button button in _sideActionButtons)
            {
                Rectangle expandedBounds = _expandedSideButtonBounds[button];

                if (collapsed)
                {
                    bool rightColumn = expandedBounds.Left >= 129;
                    int collapsedLeft = rightColumn ? 74 : 8;
                    button.Bounds = new Rectangle(
                        collapsedLeft,
                        expandedBounds.Top,
                        58,
                        expandedBounds.Height);
                    button.Text = _collapsedSideButtonIcons[button];
                    button.Font = new Font("Segoe UI Emoji", 14f, FontStyle.Regular);
                }
                else
                {
                    button.Bounds = expandedBounds;
                    button.Text = _expandedSideButtonTexts[button];
                    button.Font = _expandedSideButtonFonts[button];
                }

                button.TextAlign = ContentAlignment.MiddleCenter;
            }

            UpdateMainLayout();
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

            if (e.Button == MouseButtons.Right && hit.RowIndex >= 0)
            {
                int columnIndex = hit.ColumnIndex >= 0 ? hit.ColumnIndex : 0;
                bool clickedRowAlreadySelected = dgvCustomers.Rows[hit.RowIndex].Selected;
                List<int> selectedIdsBeforeRightClick = GetSelectedCustomerIds();
                bool preserveMultiSelection =
                    clickedRowAlreadySelected && selectedIdsBeforeRightClick.Count > 1;

                if (!clickedRowAlreadySelected)
                {
                    if (dgvCustomers.SelectedRows.Count <= 1)
                    {
                        dgvCustomers.ClearSelection();
                    }

                    dgvCustomers.Rows[hit.RowIndex].Selected = true;
                }

                dgvCustomers.CurrentCell = dgvCustomers.Rows[hit.RowIndex].Cells[columnIndex];

                if (preserveMultiSelection)
                {
                    ReselectCustomers(selectedIdsBeforeRightClick);
                    dgvCustomers.CurrentCell = dgvCustomers.Rows[hit.RowIndex].Cells[columnIndex];
                }

                dgvCustomers.Focus();

                object? idValue = dgvCustomers.Rows[hit.RowIndex].Cells["CustomerID"].Value;
                if (idValue != null && int.TryParse(idValue.ToString(), out int customerId))
                {
                    _selectedCustomerId = customerId;
                }
                else
                {
                    _selectedCustomerId = 0;
                }

                return;
            }

            if (hit.Type == DataGridViewHitTestType.None && e.Button == MouseButtons.Left)
            {
                ClearCustomerSelection();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var addCustomerForm = new AddCustomerForm())
            {
                if (addCustomerForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
        }

        private void LoadCustomers()
        {
            try
            {
                CustomerArchiveService.EnsureArchiveColumnExists();

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // SELECT all active customers ordered by ID
                    string sql = @"SELECT CustomerID,
                                  FullName,
                                  Address,
                                  ContactNumber,
                                  Email,
                                  Balance,
                                  Status
                           FROM   Customers
                           WHERE  IsArchived = 0
                           ORDER  BY CustomerID ASC;";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the DataTable to the grid
                        dgvCustomers.DataSource = dt;
                        ApplyCustomerGridSort();

                        // Improve column headers for readability
                        if (dgvCustomers.Columns.Count > 0)
                        {
                            dgvCustomers.Columns["CustomerID"].HeaderText = "ID";
                            dgvCustomers.Columns["FullName"].HeaderText = "Full Name";
                            dgvCustomers.Columns["ContactNumber"].HeaderText = "Contact No.";
                            dgvCustomers.Columns["Balance"].HeaderText = "Balance (?)";
                        }

                        lblTitle.Text = $"Customer List  ({dt.Rows.Count} record(s))";
                    }
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error loading customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomerListForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadCustomers();
            ApplyPermissions();
            InitStatusStrip();
            // Note: the initial-selection reset happens in CustomerListForm_Shown,
            // because the grid re-selects row 0 during the first paint after Load.
        }

        private void SearchCustomers(string keyword)
        {
            try
            {
                CustomerArchiveService.EnsureArchiveColumnExists();

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
                           WHERE  IsArchived = 0
                             AND (FullName      LIKE @keyword
                              OR  Address       LIKE @keyword
                              OR  ContactNumber LIKE @keyword)
                            ORDER  BY CustomerID ASC;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        // %keyword% matches the search text anywhere in the column
                        cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvCustomers.DataSource = dt;
                            ApplyCustomerGridSort();
                            lblTitle.Text = $"Customer List  ({dt.Rows.Count} result(s))";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error searching customers:\n{ex.Message}",
                    "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // Empty search box ? show all customers again
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
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.MultiSelect = true;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.Columns["CustomerID"].DataPropertyName = "CustomerID";
            dgvCustomers.Columns["FullName"].DataPropertyName = "FullName";
            dgvCustomers.Columns["Address"].DataPropertyName = "Address";
            dgvCustomers.Columns["ContactNumber"].DataPropertyName = "ContactNumber";
            dgvCustomers.Columns["Email"].DataPropertyName = "Email";
            dgvCustomers.Columns["Balance"].DataPropertyName = "Balance";
        }

        private void ApplyCustomerGridSort()
        {
            if (dgvCustomers.Rows.Count == 0 || !dgvCustomers.Columns.Contains("CustomerID"))
                return;

            dgvCustomers.Sort(dgvCustomers.Columns["CustomerID"], ListSortDirection.Ascending);
        }

        private void OpenEditForm()
        {
            if (_selectedCustomerId == 0)
            {
                AppMessageBox.Show("Please select a customer to edit.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open AddCustomerForm in EDIT mode, passing the selected CustomerID
            using (var editForm = new AddCustomerForm(_selectedCustomerId))
            {
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
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
                            AppMessageBox.Show("Customer deleted successfully.",
                                "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AuditLogger.Log("DELETE_CUSTOMER",
                                $"Customer ID {customerId} deleted by {AppSession.CurrentUsername}.");


                            LoadCustomers();   // Refresh the grid
                            _selectedCustomerId = 0;   // Clear selection tracker
                        }
                        else
                        {
                            AppMessageBox.Show("Customer could not be deleted. It may no longer exist.",
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error deleting customer:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArchiveCustomer(int customerId)
        {
            try
            {
                CustomerArchiveService.EnsureArchiveColumnExists();

                bool archived = CustomerArchiveService.SetArchivedState(customerId, true);

                if (archived)
                {
                    AppMessageBox.Show("Customer archived successfully.",
                        "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AuditLogger.Log("ARCHIVE_CUSTOMER",
                        $"Customer ID {customerId} archived by {AppSession.CurrentUsername}.");
                    LoadCustomers();
                    ClearCustomerSelection();
                }
                else
                {
                    AppMessageBox.Show("Customer could not be archived. It may no longer exist.",
                        "Archive Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error archiving customer:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<int> GetSelectedCustomerIds()
        {
            return dgvCustomers.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(row => row.Cells["CustomerID"].Value?.ToString())
                .Where(value => int.TryParse(value, out _))
                .Select(value => int.Parse(value!))
                .Distinct()
                .OrderBy(id => id)
                .ToList();
        }

        private void ReselectCustomers(List<int> customerIds)
        {
            HashSet<int> selectedIdSet = customerIds.ToHashSet();

            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                object? idValue = row.Cells["CustomerID"].Value;
                bool shouldSelect = idValue != null
                    && int.TryParse(idValue.ToString(), out int customerId)
                    && selectedIdSet.Contains(customerId);

                row.Selected = shouldSelect;
            }
        }

        private void ArchiveCustomers(List<int> customerIds)
        {
            try
            {
                CustomerArchiveService.EnsureArchiveColumnExists();

                int archivedCount = 0;

                foreach (int customerId in customerIds)
                {
                    if (CustomerArchiveService.SetArchivedState(customerId, true))
                    {
                        archivedCount++;
                        AuditLogger.Log("ARCHIVE_CUSTOMER",
                            $"Customer ID {customerId} archived by {AppSession.CurrentUsername}.");
                    }
                }

                if (archivedCount > 0)
                {
                    string message = archivedCount == 1
                        ? "1 customer archived successfully."
                        : $"{archivedCount} customers archived successfully.";

                    AppMessageBox.Show(message,
                        "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                    ClearCustomerSelection();
                }
                else
                {
                    AppMessageBox.Show("No selected customers could be archived.",
                        "Archive Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error archiving customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void archiveCustomerToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            List<int> selectedCustomerIds = GetSelectedCustomerIds();

            if (selectedCustomerIds.Count == 0)
            {
                AppMessageBox.Show("Please select at least one customer to archive.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmMessage = selectedCustomerIds.Count == 1
                ? "Are you sure you want to archive this customer?"
                : $"Are you sure you want to archive these {selectedCustomerIds.Count} customers?";

            DialogResult confirm = AppMessageBox.Show(
                confirmMessage,
                "Confirm Archive",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                ArchiveCustomers(selectedCustomerIds);
            }
        }

        private void viewArchivedListToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using (var form = new ArchivedCustomerForm())
            {
                form.ShowDialog(this);
            }

            LoadCustomers();
            ClearCustomerSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Step 1: Make sure a customer is selected
            if (_selectedCustomerId == 0)
            {
                AppMessageBox.Show("Please select a customer to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Step 2: Confirm before deleting — this cannot be undone
            DialogResult confirm = AppMessageBox.Show(
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export
            if (dgvCustomers.Rows.Count == 0)
            {
                AppMessageBox.Show("There are no records to export.",
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

                    AppMessageBox.Show("Customer list exported successfully to Excel.",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AuditLogger.Log("EXPORT_EXCEL",
                        $"{AppSession.CurrentUsername} exported customer list to Excel.");

                }
                catch (Exception ex)
                {
                    AppMessageBox.Show($"Error exporting to Excel:\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void btnCloseAnalytics_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export
            if (dgvCustomers.Rows.Count == 0)
            {
                AppMessageBox.Show("There are no records to export.",
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

                    AppMessageBox.Show("Customer list exported successfully to PDF.",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AuditLogger.Log("EXPORT_PDF",
                     $"{AppSession.CurrentUsername} exported customer list to PDF.");
                }
                catch (Exception ex)
                {
                    AppMessageBox.Show($"Error exporting to PDF:\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Audit the logout BEFORE clearing the session, so the entry
            // still records who logged out.
            AuditLogger.Log("LOGOUT",
                $"{AppSession.CurrentFullName} ({AppSession.CurrentRole}) logged out.");
            AppSession.Clear();
            _isLoggingOut = true;

            // Return to the Login screen and close this window.
            if (_loginForm != null && !_loginForm.IsDisposed)
            {
                _loginForm.ResetForLogin();
                _loginForm.Show();
            }
            else
            {
                // Fallback: no reference available — start a fresh Login form.
                new LoginForm().Show();
            }

            this.Close();
        }

        private void CustomerListForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isLoggingOut || _returningToLogin)
                return;

            if (e.CloseReason != CloseReason.UserClosing)
                return;

            AuditLogger.Log("RETURN_TO_LOGIN",
                $"{AppSession.CurrentFullName} ({AppSession.CurrentRole}) returned to the login form from Customer List.");

            AppSession.Clear();
            _returningToLogin = true;

            if (_loginForm != null && !_loginForm.IsDisposed)
            {
                _loginForm.ResetForLogin();
                _loginForm.Show();
            }
            else
            {
                new LoginForm().Show();
            }
        }

        private void CustomerListForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // If the main window closes unexpectedly while the login form is
            // still hidden, close that hidden login too so the app exits cleanly
            // instead of leaving a background process with no visible windows.
            if (_isLoggingOut || _returningToLogin)
                return;

            if (_loginForm != null && !_loginForm.IsDisposed)
            {
                _loginForm.Close();
            }
            else
            {
                Application.ExitThread();
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

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword form = new frmChangePassword();
            form.ShowDialog(this);
        }

        private void btnViewBilling_Click(object sender, EventArgs e)
        {
            // Popup 1 — a customer must be selected first. The form does NOT open.


            if (_selectedCustomerId == 0)
            {
                AppMessageBox.Show("Please select a customer to view billing records.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pass the selected customer's ID (never a name) to the history form.


            frmBillingHistory form = new frmBillingHistory(_selectedCustomerId);
            form.ShowDialog(this);

            // Clear the selection afterwards so the next View Billing requires a
            // fresh pick instead of reusing this customer.


            ClearCustomerSelection();
        }

        private void ApplyPermissions()
        {
            try
            {
                PermissionInitializer.EnsureManageUsersPermissionExists();

                btnUserManagement.Enabled = false;

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
                                        _canEditCustomer = isAllowed;
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
                                    case "ManageUsers":
                                        btnUserManagement.Enabled = isAllowed; break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppMessageBox.Show($"Error loading permissions:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            btnUserManagement.BackColor = AppTheme.PrimaryColor;
            btnUserManagement.ForeColor = Color.White;
            btnToggleSidePanel.BackColor = Color.White;
            btnToggleSidePanel.ForeColor = AppTheme.PrimaryColor;

            // DataGridView header colors
            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);

            // Alternating row colors
            dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridRowAlt;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission(AppSession.CurrentRole, "ManageUsers"))
            {
                AuditLogger.Log("ACCESS_DENIED_USER_MANAGEMENT",
                    $"{AppSession.CurrentUsername} was denied access from the Customer List User Management button.");
                AppMessageBox.Show("You do not have permission to open User Management.",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmUserListForm form = new FrmUserListForm();
            form.ShowDialog(this);
        }

        private void btnToggleSidePanel_Click(object sender, EventArgs e)
        {
            ApplySidePanelState(!_isSidePanelCollapsed);
        }
    }
}

