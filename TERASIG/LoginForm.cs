using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsigurariApp
{
    // Formularul de logare (modal). Oferă buton și link pentru înregistrare.
    public class LoginForm : Form
    {
        private Label lblTitle;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private LinkLabel linkRegister;
        private Label lblUser, lblPass;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form styling
            this.Text = "Autentificare - Asigurări";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(420, 260);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblTitle = new Label
            {
                Text = "Aplicație Asigurări - Autentificare",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };
            this.Controls.Add(lblTitle);

            lblUser = new Label { Text = "Utilizator:", Left = 40, Top = 70, Width = 80 };
            this.Controls.Add(lblUser);
            txtUsername = new TextBox { Left = 130, Top = 66, Width = 230, Name = "txtUsername" };
            this.Controls.Add(txtUsername);

            lblPass = new Label { Text = "Parolă:", Left = 40, Top = 110, Width = 80 };
            this.Controls.Add(lblPass);
            txtPassword = new TextBox { Left = 130, Top = 106, Width = 230, UseSystemPasswordChar = true, Name = "txtPassword" };
            this.Controls.Add(txtPassword);

            btnLogin = new Button { Text = "Autentificare", Left = 130, Top = 150, Width = 110 };
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            btnExit = new Button { Text = "Ieșire", Left = 250, Top = 150, Width = 110 };
            btnExit.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnExit);

            linkRegister = new LinkLabel { Text = "Înregistrare utilizator nou", Left = 130, Top = 190, Width = 230 };
            linkRegister.Click += LinkRegister_Click;
            this.Controls.Add(linkRegister);
        }

        private void LinkRegister_Click(object sender, EventArgs e)
        {
            using (var reg = new RegisterForm())
            {
                var dr = reg.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    MessageBox.Show("Înregistrare reușită. Te poți autentifica acum.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Text = reg.RegisteredUsername;
                    txtPassword.Focus();
                }
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Completează utilizator și parolă.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DataStore.ValidateCredentials(user, pass))
            {
                // autentificare reușită
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Utilizator sau parolă incorectă.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}