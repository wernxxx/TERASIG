using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsigurariApp
{
    // Formularul de înregistrare (modal)
    public class RegisterForm : Form
    {
        private TextBox txtUsername, txtPassword, txtConfirm, txtFullName;
        private Button btnRegister, btnCancel;
        public string RegisteredUsername { get; private set; } = "";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Înregistrare utilizator";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(420, 300);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblTitle = new Label
            {
                Text = "Creează cont nou",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 48
            };
            this.Controls.Add(lblTitle);

            Label l1 = new Label { Text = "Nume complet:", Left = 30, Top = 70, Width = 100 };
            this.Controls.Add(l1);
            txtFullName = new TextBox { Left = 140, Top = 66, Width = 220 };
            this.Controls.Add(txtFullName);

            Label l2 = new Label { Text = "Utilizator:", Left = 30, Top = 110, Width = 100 };
            this.Controls.Add(l2);
            txtUsername = new TextBox { Left = 140, Top = 106, Width = 220 };
            this.Controls.Add(txtUsername);

            Label l3 = new Label { Text = "Parolă:", Left = 30, Top = 150, Width = 100 };
            this.Controls.Add(l3);
            txtPassword = new TextBox { Left = 140, Top = 146, Width = 220, UseSystemPasswordChar = true };
            this.Controls.Add(txtPassword);

            Label l4 = new Label { Text = "Confirmare:", Left = 30, Top = 190, Width = 100 };
            this.Controls.Add(l4);
            txtConfirm = new TextBox { Left = 140, Top = 186, Width = 220, UseSystemPasswordChar = true };
            this.Controls.Add(txtConfirm);

            btnRegister = new Button { Text = "Înregistrare", Left = 140, Top = 230, Width = 100 };
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);

            btnCancel = new Button { Text = "Anulează", Left = 260, Top = 230, Width = 100 };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancel);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;
            string conf = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Completează toate câmpurile.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (pass != conf)
            {
                MessageBox.Show("Parolele nu coincid.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = DataStore.RegisterUser(user, pass, fullName);
            if (!ok)
            {
                MessageBox.Show("Numele de utilizator există deja. Alege altul.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Înregistrare reușită
            RegisteredUsername = user;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}