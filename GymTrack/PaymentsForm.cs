using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GymTrack
{
    public partial class PaymentsForm : Form
    {
        public PaymentsForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private DataGridView dgv;
        private ComboBox cmbMember;
        private TextBox txtAmount;
        private DateTimePicker dtpPayment;
        private ComboBox cmbPlan;

        private void BuildUI()
        {
            this.Text = "GymTrack - Payments";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // HEADER
            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 70;
            header.BackColor = Color.FromArgb(26, 26, 46);
            this.Controls.Add(header);

            Label lblTitle = new Label();
            lblTitle.Text = "Payment Management";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(25, 13);
            header.Controls.Add(lblTitle);

            Label lblSub = new Label();
            lblSub.Text = "Record and manage member payments";
            lblSub.Font = new Font("Arial", 9);
            lblSub.ForeColor = Color.FromArgb(150, 150, 180);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(25, 42);
            header.Controls.Add(lblSub);

            // MAIN LAYOUT
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.RowCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            mainLayout.Padding = new Padding(15);
            mainLayout.BackColor = Color.FromArgb(245, 247, 250);
            this.Controls.Add(mainLayout);

            // Fix render order
            this.Controls.SetChildIndex(mainLayout, 0);
            this.Controls.SetChildIndex(header, 1);

            // ═══════════════════════
            // LEFT PANEL — Add Payment
            // ═══════════════════════
            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.BackColor = Color.White;
            leftPanel.Margin = new Padding(0, 0, 10, 0);
            mainLayout.Controls.Add(leftPanel, 0, 0);

            Label lblFormTitle = new Label();
            lblFormTitle.Text = "Record Payment";
            lblFormTitle.Font = new Font("Arial", 13, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(26, 26, 46);
            lblFormTitle.AutoSize = true;
            lblFormTitle.Location = new Point(20, 20);
            leftPanel.Controls.Add(lblFormTitle);

            Panel accentLine = new Panel();
            accentLine.Size = new Size(300, 3);
            accentLine.Location = new Point(20, 48);
            accentLine.BackColor = Color.FromArgb(247, 127, 0);
            leftPanel.Controls.Add(accentLine);

            // Member
            AddFormLabel(leftPanel, "Select Member *", 20, 65);
            cmbMember = new ComboBox();
            cmbMember.Location = new Point(20, 88);
            cmbMember.Size = new Size(295, 30);
            cmbMember.Font = new Font("Arial", 10);
            cmbMember.DropDownStyle = ComboBoxStyle.DropDownList;
            leftPanel.Controls.Add(cmbMember);

            // Plan
            AddFormLabel(leftPanel, "Membership Plan *", 20, 130);
            cmbPlan = new ComboBox();
            cmbPlan.Location = new Point(20, 153);
            cmbPlan.Size = new Size(295, 30);
            cmbPlan.Font = new Font("Arial", 10);
            cmbPlan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlan.Items.AddRange(new string[] {
                "Monthly - ₱500",
                "Quarterly - ₱1,200",
                "Annual - ₱4,000"
            });
            cmbPlan.SelectedIndex = 0;
            leftPanel.Controls.Add(cmbPlan);

            // Amount
            AddFormLabel(leftPanel, "Amount Paid (₱) *", 20, 195);
            txtAmount = new TextBox();
            txtAmount.Location = new Point(20, 218);
            txtAmount.Size = new Size(295, 30);
            txtAmount.Font = new Font("Arial", 10);
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Text = "500";
            leftPanel.Controls.Add(txtAmount);

            // Auto-fill amount when plan changes
            cmbPlan.SelectedIndexChanged += (s, e) =>
            {
                string[] amounts = { "500", "1200", "4000" };
                txtAmount.Text = amounts[cmbPlan.SelectedIndex];
            };

            // Payment Date
            AddFormLabel(leftPanel, "Payment Date *", 20, 260);
            dtpPayment = new DateTimePicker();
            dtpPayment.Location = new Point(20, 283);
            dtpPayment.Size = new Size(295, 30);
            dtpPayment.Font = new Font("Arial", 10);
            dtpPayment.Format = DateTimePickerFormat.Short;
            dtpPayment.Value = DateTime.Today;
            leftPanel.Controls.Add(dtpPayment);

            // Save Button
            Button btnSave = new Button();
            btnSave.Text = "Record Payment";
            btnSave.Location = new Point(20, 340);
            btnSave.Size = new Size(200, 42);
            btnSave.BackColor = Color.FromArgb(247, 127, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Arial", 11, FontStyle.Bold);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += BtnSavePayment_Click;
            leftPanel.Controls.Add(btnSave);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(230, 340);
            btnClose.Size = new Size(85, 42);
            btnClose.BackColor = Color.FromArgb(230, 57, 70);
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Arial", 10);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();
            leftPanel.Controls.Add(btnClose);

            // ═══════════════════════
            // RIGHT PANEL — Payment History
            // ═══════════════════════
            Panel rightPanel = new Panel();
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.BackColor = Color.White;
            rightPanel.Margin = new Padding(10, 0, 0, 0);
            mainLayout.Controls.Add(rightPanel, 1, 0);

            Label lblHistory = new Label();
            lblHistory.Text = "Payment History";
            lblHistory.Font = new Font("Arial", 13, FontStyle.Bold);
            lblHistory.ForeColor = Color.FromArgb(26, 26, 46);
            lblHistory.AutoSize = true;
            lblHistory.Location = new Point(20, 20);
            rightPanel.Controls.Add(lblHistory);

            Panel accentLine2 = new Panel();
            accentLine2.Size = new Size(200, 3);
            accentLine2.Location = new Point(20, 48);
            accentLine2.BackColor = Color.FromArgb(67, 97, 238);
            rightPanel.Controls.Add(accentLine2);

            dgv = new DataGridView();
            dgv.Location = new Point(20, 60);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right;
            dgv.Size = new Size(rightPanel.Width - 40, rightPanel.Height - 80);
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 26, 46);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 42;
            dgv.RowTemplate.Height = 36;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.GridColor = Color.FromArgb(230, 230, 240);
            dgv.DefaultCellStyle.Font = new Font("Arial", 10);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 255);
            rightPanel.Controls.Add(dgv);

            // Load data
            LoadMembers();
            LoadPaymentHistory();
        }

        private void AddFormLabel(Panel parent, string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Arial", 9);
            lbl.ForeColor = Color.FromArgb(80, 80, 100);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            parent.Controls.Add(lbl);
        }

        private void LoadMembers()
        {
            cmbMember.Items.Clear();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT MemberID, FirstName + ' ' + LastName AS FullName FROM Members ORDER BY FirstName";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cmbMember.Items.Add(new MemberItem(
                            Convert.ToInt32(reader["MemberID"]),
                            reader["FullName"].ToString()
                        ));
                    }
                    if (cmbMember.Items.Count > 0)
                        cmbMember.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message);
            }
        }

        private void LoadPaymentHistory()
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("Member", "Member Name");
            dgv.Columns.Add("Plan", "Plan");
            dgv.Columns.Add("Amount", "Amount");
            dgv.Columns.Add("Date", "Payment Date");

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT m.FirstName + ' ' + m.LastName AS FullName,
                               p.PlanName,
                               pay.AmountPaid,
                               pay.PaymentDate
                        FROM Payments pay
                        JOIN Subscriptions s ON pay.SubID = s.SubID
                        JOIN Members m ON s.MemberID = m.MemberID
                        JOIN MembershipPlans p ON s.PlanID = p.PlanID
                        ORDER BY pay.PaymentDate DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        dgv.Rows.Add(
                            reader["FullName"],
                            reader["PlanName"],
                            "₱" + Convert.ToDecimal(reader["AmountPaid"]).ToString("N2"),
                            Convert.ToDateTime(reader["PaymentDate"]).ToString("MM/dd/yyyy")
                        );
                        count++;
                    }
                    if (count == 0)
                        dgv.Rows.Add("No payment records yet", "", "", "");
                }
            }
            catch { }
        }

        private void BtnSavePayment_Click(object sender, EventArgs e)
        {
            if (cmbMember.SelectedItem == null)
            {
                MessageBox.Show("Please select a member!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MemberItem member = (MemberItem)cmbMember.SelectedItem;

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Get SubID
                    string getSubSql = "SELECT TOP 1 SubID FROM Subscriptions WHERE MemberID = @mid ORDER BY SubID DESC";
                    SqlCommand getSubCmd = new SqlCommand(getSubSql, conn);
                    getSubCmd.Parameters.AddWithValue("@mid", member.ID);
                    object subResult = getSubCmd.ExecuteScalar();

                    if (subResult == null)
                    {
                        MessageBox.Show("No subscription found for this member!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int subId = Convert.ToInt32(subResult);

                    // Insert Payment
                    string sql = @"INSERT INTO Payments (SubID, AmountPaid, PaymentDate)
                                   VALUES (@sid, @amt, @dt)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@sid", subId);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@dt", dtpPayment.Value.Date);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"Payment of ₱{amount:N2} recorded successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadPaymentHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving payment: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Helper class for ComboBox
    public class MemberItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MemberItem(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString() => Name;
    }
}