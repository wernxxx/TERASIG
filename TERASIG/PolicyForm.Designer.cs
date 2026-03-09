using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AsigurariApp
{
    partial class PolicyForm
    {
        // control declarations (designer)
        private ComboBox cmbType;
        private TextBox txtHolder;
        private TextBox txtSum;
        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;
        private RichTextBox rtbDesc;
        private Button btnSave, btnCancel;
        private Label lType, lHolder, lSum, lStart, lEnd, lDesc;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = "Poliță nouă";
            this.ClientSize = new Size(560, 420);
            this.BackColor = Color.FromArgb(15, 32, 64); // dark blue background
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // Labels
            lType = new Label { Text = "Tip poliță:", Left = 20, Top = 20, Width = 100, ForeColor = Color.White, BackColor = Color.Transparent };
            lHolder = new Label { Text = "Nume titular:", Left = 20, Top = 60, Width = 100, ForeColor = Color.White, BackColor = Color.Transparent };
            lSum = new Label { Text = "Suma asigurată:", Left = 20, Top = 100, Width = 100, ForeColor = Color.White, BackColor = Color.Transparent };
            lStart = new Label { Text = "Începe la:", Left = 20, Top = 140, Width = 100, ForeColor = Color.White, BackColor = Color.Transparent };
            lEnd = new Label { Text = "Se termină la:", Left = 20, Top = 180, Width = 100, ForeColor = Color.White, BackColor = Color.Transparent };
            lDesc = new Label { Text = "Descriere:", Left = 20, Top = 220, Width = 100, ForeColor = Color.White, BackColor = Color.Transparent };

            // ComboBox
            cmbType = new ComboBox
            {
                Left = 130,
                Top = 16,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(20, 38, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cmbType.Items.AddRange(Enum.GetValues(typeof(PolicyType)).Cast<object>().ToArray());
            if (cmbType.Items.Count > 0) cmbType.SelectedIndex = 0;

            // TextBoxes
            txtHolder = new TextBox { Left = 130, Top = 56, Width = 400, BackColor = Color.FromArgb(20, 38, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            txtSum = new TextBox { Left = 130, Top = 96, Width = 200, BackColor = Color.FromArgb(20, 38, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            // DateTimePickers
            dtStart = new DateTimePicker { Left = 130, Top = 136, Width = 200, Format = DateTimePickerFormat.Short, CalendarForeColor = Color.White, CalendarMonthBackground = Color.FromArgb(20, 38, 70) };
            dtEnd = new DateTimePicker { Left = 130, Top = 176, Width = 200, Format = DateTimePickerFormat.Short, CalendarForeColor = Color.White, CalendarMonthBackground = Color.FromArgb(20, 38, 70) };

            // RichTextBox
            rtbDesc = new RichTextBox { Left = 130, Top = 216, Width = 400, Height = 120, BackColor = Color.FromArgb(20, 38, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            // Buttons
            btnSave = new Button
            {
                Text = "Salvează",
                Left = 130,
                Top = 350,
                Width = 100,
                BackColor = Color.FromArgb(10, 70, 140),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Anulează",
                Left = 250,
                Top = 350,
                Width = 100,
                BackColor = Color.FromArgb(60, 60, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => this.Close();

            // Add controls
            this.Controls.AddRange(new Control[] {
                lType, cmbType,
                lHolder, txtHolder,
                lSum, txtSum,
                lStart, dtStart,
                lEnd, dtEnd,
                lDesc, rtbDesc,
                btnSave, btnCancel
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
