using System;
using System.Globalization;
using System.Windows.Forms;

namespace AsigurariApp
{
    // Parțial: logică și validări; layout și control declarations se află în Designer
    public partial class PolicyForm : Form
    {
        private Policy editingPolicy = null;

        // Constructor pentru creare nouă; se poate specifica tipul preselectat
        public PolicyForm(PolicyType? preselect = null, Policy editing = null)
        {
            editingPolicy = editing;
            InitializeComponent();

            if (preselect.HasValue)
                cmbType.SelectedItem = preselect.Value;

            if (editingPolicy != null)
                LoadEditingPolicy();
        }

        private void LoadEditingPolicy()
        {
            cmbType.SelectedItem = editingPolicy.Type;
            txtHolder.Text = editingPolicy.HolderName;
            txtSum.Text = editingPolicy.SumInsured.ToString(CultureInfo.InvariantCulture);
            dtStart.Value = editingPolicy.StartDate;
            dtEnd.Value = editingPolicy.EndDate;
            rtbDesc.Text = editingPolicy.Description;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validări simple
            if (string.IsNullOrWhiteSpace(txtHolder.Text))
            {
                MessageBox.Show("Completează numele titularului.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Conversia șir -> decimal (safe)
            if (!DecimalTryParseInvariant(txtSum.Text.Trim(), out decimal sum) || sum <= 0)
            {
                MessageBox.Show("Introdu o sumă validă (număr pozitiv). Folosește '.' ca separator zecimal.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtEnd.Value.Date <= dtStart.Value.Date)
            {
                MessageBox.Show("Data de sfârșit trebuie să fie după data de început.", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (editingPolicy == null)
            {
                // creăm poliță nouă și adăugăm în DataStore
                var p = new Policy
                {
                    Type = (PolicyType)cmbType.SelectedItem,
                    HolderName = txtHolder.Text.Trim(),
                    SumInsured = sum,
                    StartDate = dtStart.Value.Date,
                    EndDate = dtEnd.Value.Date,
                    Description = rtbDesc.Text,
                    CreatedBy = DataStore.CurrentUser != null ? DataStore.CurrentUser.Username : "unknown"
                };
                DataStore.Policies.Add(p);
                MessageBox.Show("Poliță creată cu succes.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                // editare
                editingPolicy.Type = (PolicyType)cmbType.SelectedItem;
                editingPolicy.HolderName = txtHolder.Text.Trim();
                editingPolicy.SumInsured = sum;
                editingPolicy.StartDate = dtStart.Value.Date;
                editingPolicy.EndDate = dtEnd.Value.Date;
                editingPolicy.Description = rtbDesc.Text;
                MessageBox.Show("Poliță actualizată.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        // Conversie sigură a string -> decimal folosind InvariantCulture
        private bool DecimalTryParseInvariant(string s, out decimal value)
        {
            return decimal.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out value);
        }
    }
}
