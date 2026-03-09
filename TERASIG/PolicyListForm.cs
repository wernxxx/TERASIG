using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AsigurariApp
{
    // Form pentru afisarea tuturor politelor intr-un DataGridView
    // Comentarii in romana fara diacritice
    public class PolicyListForm : Form
    {
        private DataGridView dgv;
        private Button btnRefresh, btnEdit, btnDelete, btnNew;
        private BindingSource bs; // sursa de legare pentru DataGridView

        public PolicyListForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Lista politelor";
            this.ClientSize = new Size(860, 480);

            bs = new BindingSource();

            // DataGridView configurat pentru afisare de randuri
            dgv = new DataGridView
            {
                Left = 10,
                Top = 10,
                Width = 840,
                Height = 380,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Coloane utile - DataPropertyName trebuie sa corespunda proprietatilor din clasa Policy
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tip", DataPropertyName = "Type", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Titular", DataPropertyName = "HolderName", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Suma", DataPropertyName = "SumInsured", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Incepe", DataPropertyName = "StartDate", Width = 90 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Termina", DataPropertyName = "EndDate", Width = 90 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Creat de", DataPropertyName = "CreatedBy", Width = 100 });

            dgv.CellDoubleClick += Dgv_CellDoubleClick;

            // Butoane plasate jos. Le ancoram pentru a ramane in coltul din stanga jos la redimensionare
            btnRefresh = new Button { Text = "Reimprospateaza", Left = 10, Top = 400, Width = 120, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            btnRefresh.Click += (s, e) => LoadData();

            btnNew = new Button { Text = "Polita noua", Left = 150, Top = 400, Width = 100, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            btnNew.Click += (s, e) =>
            {
                // Deschidem formularul de creare. Folosim Show pentru a-l integra in MDI daca exista.
                var pf = new PolicyForm();
                pf.MdiParent = this.MdiParent;
                pf.Show();
            };

            btnEdit = new Button { Text = "Editeaza", Left = 270, Top = 400, Width = 100, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button { Text = "Sterge", Left = 390, Top = 400, Width = 100, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            btnDelete.Click += BtnDelete_Click;

            this.Controls.AddRange(new Control[] { dgv, btnRefresh, btnNew, btnEdit, btnDelete });
        }

        // Incarcam datele in BindingSource; daca DataStore.Policies e null, cream o lista goala pentru siguranta
        private void LoadData()
        {
            try
            {
                bs.DataSource = DataStore.Policies ?? new BindingList<Policy>();
                dgv.DataSource = bs;
                bs.ResetBindings(false);
            }
            catch (Exception ex)
            {
                // Afisam o eroare simpla daca ceva nu merge
                MessageBox.Show("Eroare la incarcare date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eveniment la dublu click pe un rand: afisam detaliile politiei
        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var p = dgv.Rows[e.RowIndex].DataBoundItem as Policy;
            if (p == null) return;

            MessageBox.Show(
                $"ID: {p.Id}\nTip: {p.Type}\nTitular: {p.HolderName}\nSuma: {p.SumInsured}\nPerioada: {p.StartDate:d} - {p.EndDate:d}\nDescriere: {p.Description}\nCreat de: {p.CreatedBy}",
                "Detalii polita",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Buton edit: verificam selectie si deschidem formularul de editare cu obiectul existent
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteaza o polita pentru editare.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var p = dgv.SelectedRows[0].DataBoundItem as Policy;
            if (p == null) return;

            var pf = new PolicyForm(null, p);
            pf.MdiParent = this.MdiParent;
            pf.Show();
        }

        // Buton sterge: confirmare si apoi eliminare din colectie
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecteaza o polita pentru stergere.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var p = dgv.SelectedRows[0].DataBoundItem as Policy;
            if (p == null) return;

            var conf = MessageBox.Show($"Stergi polita {p.Id}?\nActiunea e ireversibila.", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (conf == DialogResult.Yes)
            {
                // Incercam sa eliminam si sa actualizam vizualizarea
                DataStore.Policies?.Remove(p);
                LoadData();
            }
        }
    }
}