using System;
using System.Drawing;
using System.Windows.Forms;

namespace GymTrack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupLoginForm();
        }

        private void SetupLoginForm()
        {
            this.Text = "GymTrack - Login";
            this.Size = new Size(420, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 244, 255);

            // Logo Panel
            Panel topPanel = new Panel();
            topPanel.Size = new Size(420, 160);
            topPanel.Location = new Point(0, 0);
            topPanel.BackColor = Color.FromArgb(26, 26, 46);
            this.Controls.Add(topPanel);

            Label lblLogo = new Label();
            lblLogo.Text = "GYMTRACK";
            lblLogo.Font = new Font("Arial", 24, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.AutoSize = true;
            lblLogo.Location = new Point(110, 50);
            topPanel.Controls.Add(lblLogo);

            Label lblSub = new Label();
            lblSub.Text = "MEMBERSHIP SYSTEM";
            lblSub.Font = new Font("Arial", 9, FontStyle.Regular);
            lblSub.ForeColor = Color.FromArgb(170, 170, 170);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(125, 90);
            topPanel.Controls.Add(lblSub);

            // Username
            Label lblUser = new Label();
            lblUser.Text = "Username";
            lblUser.Font = new Font("Arial", 9);
            lblUser.ForeColor = Color.FromArgb(80, 80, 80);
            lblUser.Location = new Point(60, 190);
            lblUser.AutoSize = true;
            this.Controls.Add(lblUser);

            TextBox txtUsername = new TextBox();
            txtUsername.Name = "txtUsername";
            txtUsername.Location = new Point(60, 210);
            txtUsername.Width = 280;
            txtUsername.Height = 30;
            txtUsername.Font = new Font("Arial", 11);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(txtUsername);

            // Password
            Label lblPass = new Label();
            lblPass.Text = "Password";
            lblPass.Font = new Font("Arial", 9);
            lblPass.ForeColor = Color.FromArgb(80, 80, 80);
            lblPass.Location = new Point(60, 260);
            lblPass.AutoSize = true;
            this.Controls.Add(lblPass);

            TextBox txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Location = new Point(60, 280);
            txtPassword.Width = 280;
            txtPassword.Font = new Font("Arial", 11);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(txtPassword);

            // Login Button
            Button btnLogin = new Button();
            btnLogin.Text = "LOGIN";
            btnLogin.Location = new Point(60, 340);
            btnLogin.Size = new Size(280, 42);
            btnLogin.BackColor = Color.FromArgb(67, 97, 238);
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Arial", 11, FontStyle.Bold);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            TextBox txtUsername = (TextBox)this.Controls["txtUsername"];
            TextBox txtPassword = (TextBox)this.Controls["txtPassword"];

            if (txtUsername.Text == "admin" && txtPassword.Text == "admin")
            {
                DashboardForm dashboard = new DashboardForm();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!",
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}