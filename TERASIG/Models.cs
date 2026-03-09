using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace AsigurariApp
{
    // Tipuri posibile de asigurări
    public enum PolicyType
    {
        Auto,
        Viata,
        Medical,
        Calatorie,
        Locuinta,
        Business,
        AlteAsigurari
    }

    // Model utilizator (stocat în memorie)
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; } // stocăm hash, nu parola în clar (demo)
        public string FullName { get; set; }

        public override string ToString() => $"{FullName} ({Username})";
    }

    // Model poliță
    public class Policy
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public PolicyType Type { get; set; }
        public string HolderName { get; set; } = "";
        public decimal SumInsured { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1);
        public string Description { get; set; } = "";
        public string CreatedBy { get; set; } = ""; // username creator
    }

    // DataStore static — stocare în memorie (fără DB)
    public static class DataStore
    {
        // Lista de utilizatori (poate fi BindingList dacă vrem data-binding)
        public static List<User> Users { get; } = new List<User>();

        // Listele de polițe — BindingList ajută la legare la DataGridView
        public static BindingList<Policy> Policies { get; } = new BindingList<Policy>();

        // Utilizatorul curent (după logare)
        public static User CurrentUser { get; set; }

        // Hashing simplu SHA256 pentru parole (demo)
        public static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Verifică credențiale; întoarce true dacă există utilizatorul
        public static bool ValidateCredentials(string username, string password)
        {
            var hash = HashPassword(password);
            var user = Users.Find(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user != null && user.PasswordHash == hash)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        // Înregistrează un utilizator nou; întoarce false dacă username deja există
        public static bool RegisterUser(string username, string password, string fullName)
        {
            if (Users.Exists(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return false;
            Users.Add(new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                FullName = fullName
            });
            return true;
        }

        // Date demo pentru pornire
        public static void InitializeDemoData()
        {
            Users.Clear();
            Policies.Clear();
            // user admin demo
            Users.Add(new User
            {
                Username = "admin",
                PasswordHash = HashPassword("admin123"),
                FullName = "Administrator Demo"
            });

            // două polițe demo
            Policies.Add(new Policy
            {
                Type = PolicyType.Auto,
                HolderName = "Ion Popescu",
                SumInsured = 15000m,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1),
                Description = "Asigurare CASCO - auto mică",
                CreatedBy = "admin"
            });

            Policies.Add(new Policy
            {
                Type = PolicyType.Viata,
                HolderName = "Maria Ionescu",
                SumInsured = 50000m,
                StartDate = DateTime.Today.AddDays(-30),
                EndDate = DateTime.Today.AddYears(10),
                Description = "Asigurare de viață pe termen lung",
                CreatedBy = "admin"
            });
        }
    }
}