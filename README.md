Mai jos ai un **README.md profesional pentru GitHub**, cu structură clară, badges, secțiuni moderne și loc pentru **screenshots**. Poți copia direct în **README.md** în repository.

---

# 📊 AsigurariApp – Insurance Policy Management (C# Windows Forms)

![C#](https://img.shields.io/badge/C%23-.NET-blue)
![.NET](https://img.shields.io/badge/.NET-Windows%20Forms-purple)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey)
![License](https://img.shields.io/badge/license-MIT-green)
![Status](https://img.shields.io/badge/status-educational-orange)

Aplicație desktop realizată în **C# folosind Windows Forms**, care permite **gestionarea polițelor de asigurare** printr-o interfață grafică intuitivă.

Proiectul demonstrează utilizarea componentelor grafice din **.NET WinForms**, implementarea unui sistem simplu de autentificare și manipularea datelor în memorie.

---

# 🚀 Funcționalități

Aplicația permite:

✔ autentificarea utilizatorilor
✔ înregistrarea unui cont nou
✔ crearea unei polițe de asigurare
✔ vizualizarea polițelor existente
✔ editarea polițelor
✔ ștergerea polițelor
✔ interfață **MDI (Multiple Document Interface)**
✔ validarea datelor introduse

---

# 🖥 Screenshots

## 🔐 Login Window

<img src="screenshots/login.png" width="600">

---

## 🏠 Main Application Window

<img src="screenshots/main.png" width="700">

---

## 📋 Policy List

<img src="screenshots/policy-list.png" width="700">

---

## ➕ Create Policy

<img src="screenshots/create-policy.png" width="600">

---

# 🏗 Arhitectura aplicației

Aplicația este organizată pe mai multe componente:

```
AsigurariApp
│
├── Program.cs
├── MainForm.cs
├── LoginForm.cs
├── RegisterForm.cs
│
├── Models.cs
│
├── PolicyForm.cs
├── PolicyForm.Designer.cs
│
└── PolicyListForm.cs
```

---

# 🧩 Componente principale

## LoginForm

Gestionează autentificarea utilizatorilor.

Funcții:

* verificarea credențialelor
* redirecționarea către aplicația principală
* acces către formularul de înregistrare

---

## RegisterForm

Permite crearea unui cont nou.

Validări:

* toate câmpurile completate
* parolele coincid
* username unic

---

## MainForm

Formularul principal al aplicației.

Caracteristici:

* **MDI Container**
* meniu principal
* toolbar
* status bar
* panou lateral pentru tipuri de asigurări

---

## PolicyForm

Formular pentru:

* creare poliță
* editare poliță

Date introduse:

* tip poliță
* nume titular
* sumă asigurată
* perioadă
* descriere

---

## PolicyListForm

Afișează toate polițele într-un **DataGridView**.

Permite:

* vizualizare
* editare
* ștergere
* reîmprospătare date

---

# 📦 Modele de date

## User

```csharp
class User
{
    public string Username;
    public string PasswordHash;
    public string FullName;
}
```

---

## Policy

```csharp
class Policy
{
    public Guid Id;
    public PolicyType Type;
    public string HolderName;
    public decimal SumInsured;
    public DateTime StartDate;
    public DateTime EndDate;
}
```

---

## PolicyType

Enumerare pentru tipuri de asigurări:

```
Auto
Viata
Medical
Calatorie
Locuinta
Business
AlteAsigurari
```

---

# 🔐 Securitate

Parolele utilizatorilor nu sunt salvate în format text.

Se utilizează **hash SHA256**.

Exemplu:

```csharp
using (var sha = SHA256.Create())
{
    var bytes = Encoding.UTF8.GetBytes(password);
    var hash = sha.ComputeHash(bytes);
}
```

---

# 🗄 Stocarea datelor

Aplicația utilizează o clasă statică:

```
DataStore
```

Aceasta conține:

```
Users
Policies
CurrentUser
```

Datele sunt păstrate **în memorie**.

---

# ⚙ Tehnologii utilizate

| Tehnologie    | Descriere               |
| ------------- | ----------------------- |
| C#            | limbaj de programare    |
| .NET          | platformă de dezvoltare |
| Windows Forms | interfață grafică       |
| SHA256        | hashing parole          |
| DataGridView  | afișare date            |

---

# ▶ Instalare și rulare

## 1️⃣ Clonarea repository-ului

```bash
git clone https://github.com/username/AsigurariApp.git
```

---

## 2️⃣ Deschiderea proiectului

Deschide proiectul în:

```
Visual Studio
```

---

## 3️⃣ Rularea aplicației

Apasă:

```
F5
```

sau

```
Start Debugging
```

---

# 👤 Utilizator demo

La pornirea aplicației există un cont implicit:

```
username: admin
password: admin123
```

---

# 📚 Ce demonstrează proiectul

Acest proiect demonstrează:

* dezvoltarea aplicațiilor desktop
* programarea orientată pe obiecte
* utilizarea controalelor WinForms
* gestionarea evenimentelor
* lucrul cu DataGridView
* organizarea aplicațiilor MDI

---

# 🔮 Îmbunătățiri viitoare

Posibile extinderi ale aplicației:

* integrare **SQL Server / SQLite**
* export polițe în **PDF**
* sistem de **rapoarte**
* autentificare securizată
* design modern (Material UI)

---

# 📄 Licență

Acest proiect este realizat în scop **educațional**.

Licență recomandată:

```
MIT License
```

---

# 👨‍💻 Autor

**Andrei**

Student – dezvoltare software


---


