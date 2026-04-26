using System;
using System.Drawing;
using System.Windows.Forms;

namespace GymTrack
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "GymTrack - Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ROOT LAYOUT: Sidebar | Main
            TableLayoutPanel root = new TableLayoutPanel();
            root.Dock = DockStyle.Fill;
            root.ColumnCount = 2;
            root.RowCount = 1;
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Padding = new Padding(0);
            root.Margin = new Padding(0);
            this.Controls.Add(root);

            // ═══════════════════════
            // SIDEBAR
            // ═══════════════════════
            Panel sidebar = new Panel();
            sidebar.Dock = DockStyle.Fill;
            sidebar.BackColor = Color.FromArgb(26, 26, 46);
            sidebar.Margin = new Padding(0);
            root.Controls.Add(sidebar, 0, 0);

            Label lblLogo = new Label();
            lblLogo.Text = "GYMTRACK";
            lblLogo.Font = new Font("Arial", 16, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.AutoSize = true;
            lblLogo.Location = new Point(25, 35);
            sidebar.Controls.Add(lblLogo);

            Label lblSub = new Label();
            lblSub.Text = "Management System";
            lblSub.Font = new Font("Arial", 8);
            lblSub.ForeColor = Color.FromArgb(150, 150, 180);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(25, 60);
            sidebar.Controls.Add(lblSub);

            Panel divLine = new Panel();
            divLine.Size = new Size(180, 1);
            divLine.Location = new Point(20, 88);
            divLine.BackColor = Color.FromArgb(60, 60, 90);
            sidebar.Controls.Add(divLine);

            AddSidebarBtn(sidebar, "Dashboard", 110, true);
            AddSidebarBtn(sidebar, "Register Member", 155, false);
            AddSidebarBtn(sidebar, "View Members", 200, false);
            AddSidebarBtn(sidebar, "Payments", 245, false);
            AddSidebarBtn(sidebar, "Reports", 290, false);

            // Admin info at bottom
            Label lblAdmin = new Label();
            lblAdmin.Text = "Admin";
            lblAdmin.Font = new Font("Arial", 10, FontStyle.Bold);
            lblAdmin.ForeColor = Color.White;
            lblAdmin.AutoSize = true;
            lblAdmin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAdmin.Location = new Point(25, 560);
            sidebar.Controls.Add(lblAdmin);

            Label lblRole = new Label();
            lblRole.Text = "Administrator";
            lblRole.Font = new Font("Arial", 8);
            lblRole.ForeColor = Color.FromArgb(150, 150, 180);
            lblRole.AutoSize = true;
            lblRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblRole.Location = new Point(25, 582);
            sidebar.Controls.Add(lblRole);

            Button btnLogout = new Button();
            btnLogout.Text = "Logout";
            btnLogout.Size = new Size(170, 35);
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogout.Location = new Point(25, 608);
            btnLogout.BackColor = Color.FromArgb(230, 57, 70);
            btnLogout.ForeColor = Color.White;
            btnLogout.Font = new Font("Arial", 9, FontStyle.Bold);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Click += (s, e) => { this.Close(); new Form1().Show(); };
            sidebar.Controls.Add(btnLogout);

            // ═══════════════════════
            // MAIN CONTENT
            // ═══════════════════════
            TableLayoutPanel main = new TableLayoutPanel();
            main.Dock = DockStyle.Fill;
            main.ColumnCount = 1;
            main.RowCount = 5;
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));   // topbar
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 130));  // stats
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));   // label
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 160));  // actions
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // table
            main.Padding = new Padding(0);
            main.Margin = new Padding(0);
            main.BackColor = Color.FromArgb(245, 247, 250);
            root.Controls.Add(main, 1, 0);

            // TOP BAR
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Fill;
            topBar.BackColor = Color.White;
            topBar.Margin = new Padding(0);
            main.Controls.Add(topBar, 0, 0);

            Label lblPage = new Label();
            lblPage.Text = "Dashboard";
            lblPage.Font = new Font("Arial", 16, FontStyle.Bold);
            lblPage.ForeColor = Color.FromArgb(26, 26, 46);
            lblPage.AutoSize = true;
            lblPage.Location = new Point(25, 18);
            topBar.Controls.Add(lblPage);

            Label lblDate = new Label();
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd yyyy");
            lblDate.Font = new Font("Arial", 9);
            lblDate.ForeColor = Color.Gray;
            lblDate.AutoSize = true;
            lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDate.Location = new Point(600, 22);
            topBar.Controls.Add(lblDate);

            // STAT CARDS using TableLayoutPanel
            TableLayoutPanel statPanel = new TableLayoutPanel();
            statPanel.Dock = DockStyle.Fill;
            statPanel.ColumnCount = 4;
            statPanel.RowCount = 1;
            statPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            statPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            statPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            statPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            statPanel.Padding = new Padding(20, 15, 20, 5);
            statPanel.BackColor = Color.FromArgb(245, 247, 250);
            statPanel.Margin = new Padding(0);
            main.Controls.Add(statPanel, 0, 1);

            string[] statTitles = { "Total Members", "Active Members", "Expired", "This Month" };
            Color[] statColors = {
                Color.FromArgb(67, 97, 238),
                Color.FromArgb(46, 196, 182),
                Color.FromArgb(230, 57, 70),
                Color.FromArgb(247, 127, 0)
            };

            for (int i = 0; i < 4; i++)
            {
                Panel card = new Panel();
                card.Dock = DockStyle.Fill;
                card.BackColor = statColors[i];
                card.Margin = new Padding(5, 0, 5, 0);

                Label val = new Label();
                val.Text = "0";
                val.Font = new Font("Arial", 26, FontStyle.Bold);
                val.ForeColor = Color.White;
                val.AutoSize = true;
                val.Location = new Point(15, 10);
                card.Controls.Add(val);

                Label lbl = new Label();
                lbl.Text = statTitles[i];
                lbl.Font = new Font("Arial", 9);
                lbl.ForeColor = Color.FromArgb(220, 220, 255);
                lbl.AutoSize = true;
                lbl.Location = new Point(15, 60);
                card.Controls.Add(lbl);

                statPanel.Controls.Add(card, i, 0);
            }

            // QUICK ACTIONS LABEL
            Panel lblActionPanel = new Panel();
            lblActionPanel.Dock = DockStyle.Fill;
            lblActionPanel.BackColor = Color.FromArgb(245, 247, 250);
            lblActionPanel.Margin = new Padding(0);
            main.Controls.Add(lblActionPanel, 0, 2);

            Label lblActions = new Label();
            lblActions.Text = "Quick Actions";
            lblActions.Font = new Font("Arial", 11, FontStyle.Bold);
            lblActions.ForeColor = Color.FromArgb(26, 26, 46);
            lblActions.AutoSize = true;
            lblActions.Location = new Point(25, 5);
            lblActionPanel.Controls.Add(lblActions);

            // ACTION CARDS
            TableLayoutPanel actionPanel = new TableLayoutPanel();
            actionPanel.Dock = DockStyle.Fill;
            actionPanel.ColumnCount = 3;
            actionPanel.RowCount = 1;
            actionPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            actionPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            actionPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            actionPanel.Padding = new Padding(20, 0, 20, 5);
            actionPanel.BackColor = Color.FromArgb(245, 247, 250);
            actionPanel.Margin = new Padding(0);
            main.Controls.Add(actionPanel, 0, 3);

            string[] actTitles = { "Register Member", "View Members", "Payments" };
            string[] actDescs = { "Add a new gym member", "Browse all members", "Manage payments" };
            Color[] actColors = {
                Color.FromArgb(67, 97, 238),
                Color.FromArgb(46, 196, 182),
                Color.FromArgb(247, 127, 0)
            };
            EventHandler[] handlers = { BtnRegister_Click, BtnView_Click, BtnPayments_Click };

            for (int i = 0; i < 3; i++)
            {
                Panel card = new Panel();
                card.Dock = DockStyle.Fill;
                card.BackColor = Color.White;
                card.Margin = new Padding(5, 0, 5, 0);

                Panel accent = new Panel();
                accent.Dock = DockStyle.Top;
                accent.Height = 4;
                accent.BackColor = actColors[i];
                card.Controls.Add(accent);

                Label t = new Label();
                t.Text = actTitles[i];
                t.Font = new Font("Arial", 10, FontStyle.Bold);
                t.ForeColor = Color.FromArgb(26, 26, 46);
                t.AutoSize = true;
                t.Location = new Point(15, 18);
                card.Controls.Add(t);

                Label d = new Label();
                d.Text = actDescs[i];
                d.Font = new Font("Arial", 8);
                d.ForeColor = Color.Gray;
                d.AutoSize = true;
                d.Location = new Point(15, 42);
                card.Controls.Add(d);

                Button btn = new Button();
                btn.Text = "Open";
                btn.Size = new Size(80, 28);
                btn.Location = new Point(15, 100);
                btn.BackColor = actColors[i];
                btn.ForeColor = Color.White;
                btn.Font = new Font("Arial", 8, FontStyle.Bold);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
                btn.Click += handlers[i];
                card.Controls.Add(btn);

                actionPanel.Controls.Add(card, i, 0);
            }

            // TABLE
            Panel tableContainer = new Panel();
            tableContainer.Dock = DockStyle.Fill;
            tableContainer.BackColor = Color.FromArgb(245, 247, 250);
            tableContainer.Padding = new Padding(20, 5, 20, 20);
            tableContainer.Margin = new Padding(0);
            main.Controls.Add(tableContainer, 0, 4);

            Label lblRecent = new Label();
            lblRecent.Text = "Recent Members";
            lblRecent.Font = new Font("Arial", 11, FontStyle.Bold);
            lblRecent.ForeColor = Color.FromArgb(26, 26, 46);
            lblRecent.AutoSize = true;
            lblRecent.Location = new Point(20, 5);
            tableContainer.Controls.Add(lblRecent);

            DataGridView dgv = new DataGridView();
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right;
            dgv.Location = new Point(20, 30);
            dgv.Size = new Size(tableContainer.Width - 40, tableContainer.Height - 50);
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 46);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 36;
            dgv.RowTemplate.Height = 32;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.GridColor = Color.FromArgb(230, 230, 240);
            dgv.DefaultCellStyle.Font = new Font("Arial", 9);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Columns.Add("Name", "Member Name");
            dgv.Columns.Add("Plan", "Plan");
            dgv.Columns.Add("Start", "Start Date");
            dgv.Columns.Add("Status", "Status");
            dgv.Rows.Add("No records yet", "", "", "");
            tableContainer.Controls.Add(dgv);
        }

        private void AddSidebarBtn(Panel sidebar, string text, int y, bool active)
        {
            Button btn = new Button();
            btn.Text = "  " + text;
            btn.Size = new Size(220, 40);
            btn.Location = new Point(0, y);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Font = new Font("Arial", 10);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.BackColor = active ? Color.FromArgb(67, 97, 238) : Color.Transparent;
            btn.ForeColor = active ? Color.White : Color.FromArgb(180, 180, 210);
            sidebar.Controls.Add(btn);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            MemberRegistrationForm regForm = new MemberRegistrationForm();
            regForm.ShowDialog();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            MessageBox.Show("View Members - Coming Soon!");
        }

        private void BtnPayments_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payments - Coming Soon!");
        }
    }
}