using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GymTrack
{
    public partial class ViewMembersForm : Form
    {
        public ViewMembersForm()
        {
            InitializeComponent();
            BuildUI();
            LoadMembers("");
        }

        private DataGridView dgv;

        private void BuildUI()
        {
            this.Text = "GymTrack - View Members";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // HEADER
            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 70;
            header.BackColor = Color.FromArgb(26, 26, 46);
            this.Controls.Add(header);

            Label lblTitle = new Label();
            lblTitle.Text = "Member List";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(25, 13);
            header.Controls.Add(lblTitle);

            Label lblSub = new Label();
            lblSub.Text = "View and manage all gym members";
            lblSub.Font = new Font("Arial", 9);
            lblSub.ForeColor = Color.FromArgb(150, 150, 180);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(25, 42);
            header.Controls.Add(lblSub);

            // TOOLBAR
            Panel toolbar = new Panel();
            toolbar.Dock = DockStyle.Top;
            toolbar.Height = 55;
            toolbar.BackColor = Color.White;
            this.Controls.Add(toolbar);

            Label lblSearch = new Label();
            lblSearch.Text = "Search:";
            lblSearch.Font = new Font("Arial", 10);
            lblSearch.ForeColor = Color.FromArgb(80, 80, 100);
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(20, 17);
            toolbar.Controls.Add(lblSearch);

            TextBox txtSearch = new TextBox();
            txtSearch.Location = new Point(75, 14);
            txtSearch.Size = new Size(280, 30);
            txtSearch.Font = new Font("Arial", 10);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            toolbar.Controls.Add(txtSearch);

            Button btnSearch = new Button();
            btnSearch.Text = "Search";
            btnSearch.Location = new Point(365, 12);
            btnSearch.Size = new Size(90, 32);
            btnSearch.BackColor = Color.FromArgb(67, 97, 238);
            btnSearch.ForeColor = Color.White;
            btnSearch.Font = new Font("Arial", 9, FontStyle.Bold);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Click += (s, e) => LoadMembers(txtSearch.Text);
            toolbar.Controls.Add(btnSearch);

            Button btnRefresh = new Button();
            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(465, 12);
            btnRefresh.Size = new Size(90, 32);
            btnRefresh.BackColor = Color.FromArgb(46, 196, 182);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Font = new Font("Arial", 9, FontStyle.Bold);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadMembers(""); };
            toolbar.Controls.Add(btnRefresh);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(565, 12);
            btnClose.Size = new Size(90, 32);
            btnClose.BackColor = Color.FromArgb(230, 57, 70);
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Arial", 9, FontStyle.Bold);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();
            toolbar.Controls.Add(btnClose);

            // Search on Enter key
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoadMembers(txtSearch.Text);
            };

            // TABLE
            Panel tablePanel = new Panel();
            tablePanel.Dock = DockStyle.Fill;
            tablePanel.Padding = new Padding(20, 15, 20, 20);
            tablePanel.BackColor = Color.FromArgb(245, 247, 250);
            this.Controls.Add(tablePanel);

            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 46);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.ColumnHeadersHeight = 42;
            dgv.RowTemplate.Height = 36;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.GridColor = Color.FromArgb(230, 230, 240);
            dgv.DefaultCellStyle.Font = new Font("Arial", 10);
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 255);

            // Alternating row colors
            dgv.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex % 2 == 0)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                else
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 255);
            };

            tablePanel.Controls.Add(dgv);

            // Fix render order
            this.Controls.SetChildIndex(tablePanel, 0);
            this.Controls.SetChildIndex(toolbar, 1);
            this.Controls.SetChildIndex(header, 2);
        }

        private void LoadMembers(string search)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("MemberID", "ID");
            dgv.Columns.Add("Name", "Full Name");
            dgv.Columns.Add("Phone", "Phone");
            dgv.Columns.Add("Email", "Email");
            dgv.Columns.Add("Plan", "Plan");
            dgv.Columns.Add("StartDate", "Start Date");
            dgv.Columns.Add("EndDate", "End Date");
            dgv.Columns.Add("Status", "Status");

            dgv.Columns["MemberID"].Visible = false;
            dgv.Columns["Name"].FillWeight = 20;
            dgv.Columns["Phone"].FillWeight = 12;
            dgv.Columns["Email"].FillWeight = 18;
            dgv.Columns["Plan"].FillWeight = 12;
            dgv.Columns["StartDate"].FillWeight = 12;
            dgv.Columns["EndDate"].FillWeight = 12;
            dgv.Columns["Status"].FillWeight = 10;

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        SELECT m.MemberID,
                               m.FirstName + ' ' + m.LastName AS FullName,
                               m.Phone, m.Email,
                               p.PlanName,
                               s.StartDate, s.EndDate, s.Status
                        FROM Members m
                        LEFT JOIN Subscriptions s ON m.MemberID = s.MemberID
                        LEFT JOIN MembershipPlans p ON s.PlanID = p.PlanID
                        WHERE m.FirstName + ' ' + m.LastName 
                              LIKE @search
                        ORDER BY m.MemberID DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        string status = reader["Status"].ToString();
                        int row = dgv.Rows.Add(
                            reader["MemberID"],
                            reader["FullName"],
                            reader["Phone"],
                            reader["Email"],
                            reader["PlanName"],
                            Convert.ToDateTime(reader["StartDate"]).ToString("MM/dd/yyyy"),
                            Convert.ToDateTime(reader["EndDate"]).ToString("MM/dd/yyyy"),
                            status
                        );

                        // Color status
                        Color statusColor = status == "Active" ?
                            Color.FromArgb(46, 196, 182) :
                            status == "Expired" ?
                            Color.FromArgb(230, 57, 70) :
                            Color.FromArgb(247, 127, 0);

                        dgv.Rows[row].Cells["Status"].Style.ForeColor = statusColor;
                        dgv.Rows[row].Cells["Status"].Style.Font =
                            new Font("Arial", 10, FontStyle.Bold);
                        count++;
                    }

                    if (count == 0)
                        dgv.Rows.Add("", "No members found", "", "", "", "", "", "");

                    this.Text = $"GymTrack - View Members ({count} records)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}