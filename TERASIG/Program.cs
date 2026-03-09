using System;
using System.Windows.Forms;

namespace AsigurariApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inițializare WinForms (pentru .NET 6+)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Pregătim datele inițiale în memorie (un utilizator admin și câteva polițe bune pentru demo)
            DataStore.InitializeDemoData();

            // Afișăm formularul de login ca modal
            using (var login = new LoginForm())
            {
                var dr = login.ShowDialog();
                if (dr == DialogResult.OK && DataStore.CurrentUser != null)
                {
                    // Dacă login reușește, pornim formularul principal (MDI)
                    Application.Run(new MainForm());
                }
                else
                {
                    // Altceva -> închidem aplicația
                    Application.Exit();
                }
            }
        }
    }
}