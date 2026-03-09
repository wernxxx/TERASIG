using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AsigurariApp
{
    // Form principal MDI
    public class MainForm : Form
    {
        private MenuStrip menu;
        private ToolStrip tool;
        private StatusStrip status;
        private ToolStripStatusLabel statusLabel;
        private ToolStripButton btnNewPolicy;
        private ToolStripButton btnViewPolicies;
        private ToolStripButton btnLogout;

        public MainForm()
        {
            InitializeComponent();
            UpdateStatus();
        }

        private void InitializeComponent()
        {
            this.Text = "Asigurări - Aplicație Desktop";
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;

            // MenuStrip
            menu = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("Fișier");
            var newPolicyMenu = new ToolStripMenuItem("Poliță nouă", null, NewPolicy_Click) { ShortcutKeys = Keys.Control | Keys.N };
            var viewPoliciesMenu = new ToolStripMenuItem("Vizualizează polițele", null, ViewPolicies_Click) { ShortcutKeys = Keys.Control | Keys.P };
            var exitMenu = new ToolStripMenuItem("Ieșire", null, Exit_Click) { ShortcutKeys = Keys.Alt | Keys.F4 };
            fileMenu.DropDownItems.Add(newPolicyMenu);
            fileMenu.DropDownItems.Add(viewPoliciesMenu);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitMenu);

            var windowMenu = new ToolStripMenuItem("Ferestre");
            // MdiWindowListItem permite listarea ferestrelor copil
            menu.MdiWindowListItem = windowMenu;

            var accountMenu = new ToolStripMenuItem("Cont");
            var logoutMenu = new ToolStripMenuItem("Deconectare", null, Logout_Click);
            accountMenu.DropDownItems.Add(logoutMenu);

            menu.Items.Add(fileMenu);
            menu.Items.Add(windowMenu);
            menu.Items.Add(accountMenu);

            this.MainMenuStrip = menu;
            this.Controls.Add(menu);

            // ToolStrip (toolbar)
            tool = new ToolStrip();
            btnNewPolicy = new ToolStripButton("Poliță nouă");
            btnNewPolicy.Click += NewPolicy_Click;
            btnViewPolicies = new ToolStripButton("Vezi polițele");
            btnViewPolicies.Click += ViewPolicies_Click;
            btnLogout = new ToolStripButton("Deconectare");
            btnLogout.Click += Logout_Click;

            tool.Items.Add(btnNewPolicy);
            tool.Items.Add(btnViewPolicies);
            tool.Items.Add(new ToolStripSeparator());
            tool.Items.Add(btnLogout);

            tool.Dock = DockStyle.Top;
            this.Controls.Add(tool);

            // StatusStrip
            status = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            status.Items.Add(statusLabel);
            this.Controls.Add(status);

            // un panou lateral simplu cu butoane pentru tipuri de polițe (opțional)
            var sidePanel = new Panel { Dock = DockStyle.Left, Width = 180, BackColor = Color.FromArgb(240, 240, 245) };
            var lbl = new Label { Text = "Tipuri asigurări", Font = new Font("Segoe UI", 9, FontStyle.Bold), Dock = DockStyle.Top, Height = 30, TextAlign = ContentAlignment.MiddleCenter };
            sidePanel.Controls.Add(lbl);

            int top = 40;
            foreach (var t in Enum.GetValues(typeof(PolicyType)).Cast<PolicyType>())
            {
                var b = new Button { Text = t.ToString(), Left = 10, Top = top, Width = 160, Height = 30, Tag = t };
                b.Click += (s, e) =>
                {
                    // la click deschidem o fereastră de creare poliță preselectată tipul respectiv
                    var pf = new PolicyForm((PolicyType)((Button)s).Tag);
                    pf.MdiParent = this;
                    pf.Show();
                };
                sidePanel.Controls.Add(b);
                top += 36;
            }
            this.Controls.Add(sidePanel);

            // On load, mărim zona centrală
            this.Load += (s, e) => { /* poate încărca/detecta setări */ };
        }

        private void UpdateStatus()
        {
            var user = DataStore.CurrentUser != null ? DataStore.CurrentUser.FullName : "N/A";
            statusLabel.Text = $"Utilizator: {user} | Polițe înregistrate: {DataStore.Policies.Count}";
        }

        private void NewPolicy_Click(object sender, EventArgs e)
        {
            var form = new PolicyForm();
            form.MdiParent = this;
            form.Show();
        }

        private void ViewPolicies_Click(object sender, EventArgs e)
        {
            // Verificăm dacă formularul e deja deschis
            var existing = this.MdiChildren.OfType<PolicyListForm>().FirstOrDefault();
            if (existing != null)
            {
                existing.BringToFront();
            }
            else
            {
                var listForm = new PolicyListForm();
                listForm.MdiParent = this;
                listForm.FormClosed += (s, ev) => UpdateStatus();
                listForm.Show();
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Sigur vrei să te deconectezi?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                DataStore.CurrentUser = null;
                // Închidem toate ferestrele și revenim la login
                foreach (var f in this.MdiChildren.ToArray()) f.Close();
                this.Hide();
                using (var login = new LoginForm())
                {
                    var dr = login.ShowDialog();
                    if (dr == DialogResult.OK && DataStore.CurrentUser != null)
                    {
                        this.UpdateStatus();
                        this.Show();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}