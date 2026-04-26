using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GymTrack
{
    public partial class MemberRegistrationForm : Form
    {
        public MemberRegistrationForm()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "GymTrack - Register Member";
            this.Size = new Size(650, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // HEADER
            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 70;
            header.BackColor = Color.FromArgb(26, 26, 46);
            this.Controls.Add(header);

            Label lblTitle = new Label();
            lblTitle.Text = "Register New Member";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(25, 13);
            header.Controls.Add(lblTitle);

            Label lblSub = new Label();
            lblSub.Text = "Fill in the member information below";
            lblSub.Font = new Font("Arial", 9);
            lblSub.ForeColor = Color.FromArgb(150, 150, 180);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(25, 42);
            header.Controls.Add(lblSub);

            // FORM BODY
            Panel body = new Panel();
            body.Location = new Point(0, 70);
            body.Size = new Size(650, 480);
            body.BackColor = Color.FromArgb(245, 247, 250);
            this.Controls.Add(body);

            // ROW 1: First Name | Last Name
            AddLabel(body, "First Name *", 20, 20);
            TextBox txtFirstName = AddTextBox(body, "txtFirstName", 20, 45);

            AddLabel(body, "Last Name *", 330, 20);
            TextBox txtLastName = AddTextBox(body, "txtLastName", 330, 45);

            // ROW 2: Phone | Email
            AddLabel(body, "Phone Number", 20, 100);
            TextBox txtPhone = AddTextBox(body, "txtPhone", 20, 125);

            AddLabel(body, "Email Address", 330, 100);
            TextBox txtEmail = AddTextBox(body, "txtEmail", 330, 125);

            // ROW 3: Address
            AddLabel(body, "Address", 20, 180);
            TextBox txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Location = new Point(20, 205);
            txtAddress.Size = new Size(590, 60);
            txtAddress.Font = new Font("Arial", 10);
            txtAddress.Multiline = true;
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.BackColor = Color.White;
            body.Controls.Add(txtAddress);

            // ROW 4: Plan | Start Date
            AddLabel(body, "Membership Plan *", 20, 280);
            ComboBox cmbPlan = new ComboBox();
            cmbPlan.Name = "cmbPlan";
            cmbPlan.Location = new Point(20, 305);
            cmbPlan.Size = new Size(270, 30);
            cmbPlan.Font = new Font("Arial", 10);
            cmbPlan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlan.Items.AddRange(new string[] {
                "Monthly - ₱500",
                "Quarterly - ₱1,200",
                "Annual - ₱4,000"
            });
            cmbPlan.SelectedIndex = 0;
            body.Controls.Add(cmbPlan);

            AddLabel(body, "Start Date *", 330, 280);
            DateTimePicker dtpStart = new DateTimePicker();
            dtpStart.Name = "dtpStart";
            dtpStart.Location = new Point(330, 305);
            dtpStart.Size = new Size(280, 30);
            dtpStart.Font = new Font("Arial", 10);
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Value = DateTime.Today;
            body.Controls.Add(dtpStart);

            // ROW 5: End Date | Status
            AddLabel(body, "End Date", 20, 355);
            DateTimePicker dtpEnd = new DateTimePicker();
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Location = new Point(20, 380);
            dtpEnd.Size = new Size(270, 30);
            dtpEnd.Font = new Font("Arial", 10);
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Value = DateTime.Today.AddMonths(1);
            body.Controls.Add(dtpEnd);

            // Auto-calculate end date when plan changes
            cmbPlan.SelectedIndexChanged += (s, e) =>
            {
                int months = cmbPlan.SelectedIndex == 0 ? 1 :
                             cmbPlan.SelectedIndex == 1 ? 3 : 12;
                dtpEnd.Value = dtpStart.Value.AddMonths(months);
            };

            dtpStart.ValueChanged += (s, e) =>
            {
                int months = cmbPlan.SelectedIndex == 0 ? 1 :
                             cmbPlan.SelectedIndex == 1 ? 3 : 12;
                dtpEnd.Value = dtpStart.Value.AddMonths(months);
            };

            AddLabel(body, "Status", 330, 355);
            ComboBox cmbStatus = new ComboBox();
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Location = new Point(330, 380);
            cmbStatus.Size = new Size(280, 30);
            cmbStatus.Font = new Font("Arial", 10);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new string[] { "Active", "Pending", "Expired" });
            cmbStatus.SelectedIndex = 0;
            body.Controls.Add(cmbStatus);

            // BUTTONS
            Button btnSave = new Button();
            btnSave.Text = "Save Member";
            btnSave.Size = new Size(180, 42);
            btnSave.Location = new Point(20, 435);
            btnSave.BackColor = Color.FromArgb(67, 97, 238);
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Arial", 11, FontStyle.Bold);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += (s, e) =>
            {
                SaveMember(txtFirstName, txtLastName, txtPhone,
                          txtEmail, txtAddress, cmbPlan,
                          dtpStart, dtpEnd, cmbStatus);
            };
            body.Controls.Add(btnSave);

            Button btnClear = new Button();
            btnClear.Text = "Clear Form";
            btnClear.Size = new Size(130, 42);
            btnClear.Location = new Point(215, 435);
            btnClear.BackColor = Color.FromArgb(150, 150, 170);
            btnClear.ForeColor = Color.White;
            btnClear.Font = new Font("Arial", 10);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Cursor = Cursors.Hand;
            btnClear.Click += (s, e) =>
            {
                txtFirstName.Clear(); txtLastName.Clear();
                txtPhone.Clear(); txtEmail.Clear();
                txtAddress.Clear(); cmbPlan.SelectedIndex = 0;
                dtpStart.Value = DateTime.Today;
                cmbStatus.SelectedIndex = 0;
            };
            body.Controls.Add(btnClear);

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Size = new Size(100, 42);
            btnClose.Location = new Point(360, 435);
            btnClose.BackColor = Color.FromArgb(230, 57, 70);
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Arial", 10);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();
            body.Controls.Add(btnClose);
        }

        private Label AddLabel(Panel parent, string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Arial", 9);
            lbl.ForeColor = Color.FromArgb(80, 80, 100);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            parent.Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(Panel parent, string name, int x, int y)
        {
            TextBox txt = new TextBox();
            txt.Name = name;
            txt.Location = new Point(x, y);
            txt.Size = new Size(270, 30);
            txt.Font = new Font("Arial", 10);
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = Color.White;
            parent.Controls.Add(txt);
            return txt;
        }

        private void SaveMember(TextBox txtFirst, TextBox txtLast,
            TextBox txtPhone, TextBox txtEmail, TextBox txtAddress,
            ComboBox cmbPlan, DateTimePicker dtpStart,
            DateTimePicker dtpEnd, ComboBox cmbStatus)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtFirst.Text) ||
                string.IsNullOrWhiteSpace(txtLast.Text))
            {
                MessageBox.Show("First Name and Last Name are required!",
                    "Validation Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int planId = cmbPlan.SelectedIndex + 1;

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Insert Member
                    string memberSql = @"
                        INSERT INTO Members 
                        (FirstName, LastName, Phone, Email, Address, DateJoined)
                        VALUES (@fn, @ln, @ph, @em, @ad, @dj);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(memberSql, conn);
                    cmd.Parameters.AddWithValue("@fn", txtFirst.Text.Trim());
                    cmd.Parameters.AddWithValue("@ln", txtLast.Text.Trim());
                    cmd.Parameters.AddWithValue("@ph", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@em", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@ad", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@dj", DateTime.Today);

                    int memberId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insert Subscription
                    string subSql = @"
                        INSERT INTO Subscriptions
                        (MemberID, PlanID, StartDate, EndDate, Status)
                        VALUES (@mid, @pid, @sd, @ed, @st)";

                    SqlCommand subCmd = new SqlCommand(subSql, conn);
                    subCmd.Parameters.AddWithValue("@mid", memberId);
                    subCmd.Parameters.AddWithValue("@pid", planId);
                    subCmd.Parameters.AddWithValue("@sd", dtpStart.Value.Date);
                    subCmd.Parameters.AddWithValue("@ed", dtpEnd.Value.Date);
                    subCmd.Parameters.AddWithValue("@st", cmbStatus.Text);
                    subCmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    $"Member '{txtFirst.Text} {txtLast.Text}' registered successfully!",
                    "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Clear form
                txtFirst.Clear(); txtLast.Clear();
                txtPhone.Clear(); txtEmail.Clear();
                txtAddress.Clear();
                cmbPlan.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving member: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}