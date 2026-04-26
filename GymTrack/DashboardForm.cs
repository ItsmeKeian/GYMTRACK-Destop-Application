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
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            this.Text = "GymTrack - Dashboard";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.MinimumSize = new Size(1100, 650);

            // ═══════════════════════════
            // SIDEBAR
            // ═══════════════════════════
            Panel sidebar = new Panel();
            sidebar.Size = new Size(220, 650);
            sidebar.Location = new Point(0, 0);
            sidebar.BackColor = Color.FromArgb(26, 26, 46);
            this.Controls.Add(sidebar);

            // Sidebar Logo
            Label lblLogo = new Label();
            lblLogo.Text = "GYMTRACK";
            lblLogo.Font = new Font("Arial", 16, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.AutoSize = true;
            lblLogo.Location = new Point(30, 35);
            sidebar.Controls.Add(lblLogo);

            Label lblLogoSub = new Label();
            lblLogoSub.Text = "Management System";
            lblLogoSub.Font = new Font("Arial", 8);
            lblLogoSub.ForeColor = Color.FromArgb(150, 150, 180);
            lblLogoSub.AutoSize = true;
            lblLogoSub.Location = new Point(30, 58);
            sidebar.Controls.Add(lblLogoSub);

            // Divider
            Panel div = new Panel();
            div.Size = new Size(180, 1);
            div.Location = new Point(20, 85);
            div.BackColor = Color.FromArgb(60, 60, 90);
            sidebar.Controls.Add(div);

            // Sidebar Menu Items
            AddSidebarItem(sidebar, "Dashboard", 110, true);
            AddSidebarItem(sidebar, "Register Member", 160, false);
            AddSidebarItem(sidebar, "View Members", 210, false);
            AddSidebarItem(sidebar, "Payments", 260, false);
            AddSidebarItem(sidebar, "Reports", 310, false);

            // Sidebar bottom
            Panel divBottom = new Panel();
            divBottom.Size = new Size(180, 1);
            divBottom.Location = new Point(20, 530);
            divBottom.BackColor = Color.FromArgb(60, 60, 90);
            sidebar.Controls.Add(divBottom);

            Label lblAdmin = new Label();
            lblAdmin.Text = "Admin";
            lblAdmin.Font = new Font("Arial", 10, FontStyle.Bold);
            lblAdmin.ForeColor = Color.White;
            lblAdmin.AutoSize = true;
            lblAdmin.Location = new Point(30, 545);
            sidebar.Controls.Add(lblAdmin);

            Label lblRole = new Label();
            lblRole.Text = "Administrator";
            lblRole.Font = new Font("Arial", 8);
            lblRole.ForeColor = Color.FromArgb(150, 150, 180);
            lblRole.AutoSize = true;
            lblRole.Location = new Point(30, 565);
            sidebar.Controls.Add(lblRole);

            Button btnLogout = new Button();
            btnLogout.Text = "Logout";
            btnLogout.Size = new Size(160, 32);
            btnLogout.Location = new Point(30, 590);
            btnLogout.BackColor = Color.FromArgb(230, 57, 70);
            btnLogout.ForeColor = Color.White;
            btnLogout.Font = new Font("Arial", 9, FontStyle.Bold);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Click += BtnLogout_Click;
            sidebar.Controls.Add(btnLogout);

            // ═══════════════════════════
            // MAIN CONTENT AREA
            // ═══════════════════════════
            Panel mainArea = new Panel();
            mainArea.Size = new Size(880, 650);
            mainArea.Location = new Point(220, 0);
            mainArea.BackColor = Color.FromArgb(245, 247, 250);
            this.Controls.Add(mainArea);

            // Top bar
            Panel topBar = new Panel();
            topBar.Size = new Size(880, 65);
            topBar.Location = new Point(0, 0);
            topBar.BackColor = Color.White;
            mainArea.Controls.Add(topBar);

            Label lblPage = new Label();
            lblPage.Text = "Dashboard";
            lblPage.Font = new Font("Arial", 16, FontStyle.Bold);
            lblPage.ForeColor = Color.FromArgb(26, 26, 46);
            lblPage.AutoSize = true;
            lblPage.Location = new Point(30, 18);
            topBar.Controls.Add(lblPage);

            Label lblDate = new Label();
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd yyyy");
            lblDate.Font = new Font("Arial", 9);
            lblDate.ForeColor = Color.Gray;
            lblDate.AutoSize = true;
            lblDate.Location = new Point(650, 23);
            topBar.Controls.Add(lblDate);

            // ═══════════════════════════
            // STAT CARDS ROW
            // ═══════════════════════════
            AddStatCard(mainArea, "Total Members", "0",
                Color.FromArgb(67, 97, 238), 30, 90);
            AddStatCard(mainArea, "Active Members", "0",
                Color.FromArgb(46, 196, 182), 240, 90);
            AddStatCard(mainArea, "Expired", "0",
                Color.FromArgb(230, 57, 70), 450, 90);
            AddStatCard(mainArea, "This Month", "0",
                Color.FromArgb(247, 127, 0), 660, 90);

            // ═══════════════════════════
            // QUICK ACTIONS
            // ═══════════════════════════
            Label lblQuick = new Label();
            lblQuick.Text = "Quick Actions";
            lblQuick.Font = new Font("Arial", 12, FontStyle.Bold);
            lblQuick.ForeColor = Color.FromArgb(26, 26, 46);
            lblQuick.AutoSize = true;
            lblQuick.Location = new Point(30, 230);
            mainArea.Controls.Add(lblQuick);

            AddActionCard(mainArea, "Register Member",
                "Add a new gym member", 30, 265,
                Color.FromArgb(67, 97, 238), BtnRegister_Click);
            AddActionCard(mainArea, "View Members",
                "Browse all members", 240, 265,
                Color.FromArgb(46, 196, 182), BtnView_Click);
            AddActionCard(mainArea, "Payments",
                "Manage payments", 450, 265,
                Color.FromArgb(247, 127, 0), BtnPayments_Click);

            // ═══════════════════════════
            // RECENT SECTION
            // ═══════════════════════════
            Label lblRecent = new Label();
            lblRecent.Text = "Recent Members";
            lblRecent.Font = new Font("Arial", 12, FontStyle.Bold);
            lblRecent.ForeColor = Color.FromArgb(26, 26, 46);
            lblRecent.AutoSize = true;
            lblRecent.Location = new Point(30, 420);
            mainArea.Controls.Add(lblRecent);

            Panel tablePanel = new Panel();
            tablePanel.Size = new Size(820, 170);
            tablePanel.Location = new Point(30, 450);
            tablePanel.BackColor = Color.White;
            mainArea.Controls.Add(tablePanel);

            DataGridView dgv = new DataGridView();
            dgv.Size = new Size(820, 170);
            dgv.Location = new Point(0, 0);
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

            dgv.Columns.Add("Name", "Member Name");
            dgv.Columns.Add("Plan", "Plan");
            dgv.Columns.Add("Start", "Start Date");
            dgv.Columns.Add("Status", "Status");
            dgv.Columns[0].Width = 250;
            dgv.Columns[1].Width = 150;
            dgv.Columns[2].Width = 180;
            dgv.Columns[3].Width = 120;

            dgv.Rows.Add("No records yet", "", "", "");
            tablePanel.Controls.Add(dgv);
        }

        private void AddSidebarItem(Panel sidebar, string text, int y, bool active)
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

            if (active)
            {
                btn.BackColor = Color.FromArgb(67, 97, 238);
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.FromArgb(180, 180, 210);
            }
            sidebar.Controls.Add(btn);
        }

        private void AddStatCard(Panel parent, string label,
            string value, Color color, int x, int y)
        {
            Panel card = new Panel();
            card.Size = new Size(190, 110);
            card.Location = new Point(x, y);
            card.BackColor = color;
            parent.Controls.Add(card);

            Label lblVal = new Label();
            lblVal.Text = value;
            lblVal.Font = new Font("Arial", 28, FontStyle.Bold);
            lblVal.ForeColor = Color.White;
            lblVal.AutoSize = true;
            lblVal.Location = new Point(20, 18);
            card.Controls.Add(lblVal);

            Label lblLbl = new Label();
            lblLbl.Text = label;
            lblLbl.Font = new Font("Arial", 9);
            lblLbl.ForeColor = Color.FromArgb(220, 220, 255);
            lblLbl.AutoSize = true;
            lblLbl.Location = new Point(20, 72);
            card.Controls.Add(lblLbl);
        }

        private void AddActionCard(Panel parent, string title,
            string desc, int x, int y, Color color, EventHandler handler)
        {
            Panel card = new Panel();
            card.Size = new Size(190, 130);
            card.Location = new Point(x, y);
            card.BackColor = Color.White;
            card.Cursor = Cursors.Hand;
            parent.Controls.Add(card);

            Panel accent = new Panel();
            accent.Size = new Size(190, 5);
            accent.Location = new Point(0, 0);
            accent.BackColor = color;
            card.Controls.Add(accent);

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(26, 26, 46);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(15, 20);
            card.Controls.Add(lblTitle);

            Label lblDesc = new Label();
            lblDesc.Text = desc;
            lblDesc.Font = new Font("Arial", 8);
            lblDesc.ForeColor = Color.Gray;
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(15, 45);
            card.Controls.Add(lblDesc);

            Button btn = new Button();
            btn.Text = "Open";
            btn.Size = new Size(75, 28);
            btn.Location = new Point(15, 80);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Arial", 8, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Click += handler;
            card.Controls.Add(btn);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Member Registration - Coming Soon!");
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            MessageBox.Show("View Members - Coming Soon!");
        }

        private void BtnPayments_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payments - Coming Soon!");
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }
    }
}