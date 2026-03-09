# TERASIG — Lucrare practică: Aplicație vizuală "Nevopie" (proiect TERASIG)

Autor: [Nume Student]  
Proiect: TERASIG  
Dată: 2026-03-09

## Cuprins
1. Introducere  
2. Descrierea proiectului TERASIG / aplicația "Nevopie"  
3. Partea I — Aplicație vizuală cu controlere de bază  
4. Partea II — Aplicație complexă cu controlere frecvent utilizate  
5. Instrucțiuni de redactare și formatare (cerințele specifice)  
6. Instrucțiuni de instalare și rulare pentru TERASIG  
7. Structura proiectului TERASIG (fișiere și foldere)  
8. Exemple de cod (listings)  
9. Bibliografie  
10. Concluzie

---

## 1. Introducere
Această lucrare prezintă proiectul TERASIG — o implementare pedagogică numită "Nevopie" care ilustrează concepte fundamentale și intermediare de programare a interfețelor grafice în C# (Windows Forms). Scopul este să demonstreze plasarea componentelor, proprietățile, metodele și evenimentele lor, precum și funcționalități MDI și conversii de șiruri de caractere folosite frecvent în aplicațiile desktop.

---

## 2. Descrierea proiectului TERASIG / aplicația "Nevopie"
"Nevopie" este aplicația din cadrul proiectului TERASIG. Caracteristici principale:
- Interfață principală cu MenuStrip și ToolStrip;
- Exemple de ferestre copil (MDI) pentru documente/forme multiple;
- Panouri demonstrative cu controale: Button, TextBox, Label, RichTextBox, ComboBox, RadioButton, CheckBox, GroupBox, PictureBox, ProgressBar;
- Validare input și conversii sigure de tip (TryParse);
- Documentație și listări de cod incluse în folderele Docs și Listings.

Obiective specifice:
- Demonstrarea proprietăților și evenimentelor controalelor UI;
- Realizarea unei aplicații MDI simple;
- Arătarea rutinei de validare și conversie a datelor introduse de utilizator.

---

## 3. Partea I — Aplicație vizuală cu controlere de bază

3.1 Plasarea componentelor pe suprafața ferestrelor
- Folosiți Designer-ul Visual Studio sau inițializare în cod.
- Ex: setare locație și dimensiune:
```csharp
button1.Location = new Point(12, 12);
button1.Size = new Size(100, 30);
```

3.2 Descrierea controlerelor folosite în TERASIG
- Button — execută acțiuni la `Click`.
- TextBox — input simplu; `Text`, `MaxLength`, `ReadOnly`.
- Label — afișare text, stare.
- RichTextBox — text multiline și formatat.
- MenuStrip / ToolStrip — meniuri și comenzi rapide.
- GroupBox / Panel — pentru organizarea logică a controalelor.
- ComboBox / ListBox — selecție din listă.
- PictureBox — afișare imagini resursă.
- ProgressBar, DateTimePicker, NumericUpDown — controale auxiliare.

3.3 Elemente caracteristice: proprietăți, metode, evenimente
- Proprietăți uzuale: `Enabled`, `Visible`, `BackColor`, `ForeColor`, `Font`, `Text`, `Dock`, `Anchor`.
- Metode: `Show()`, `Hide()`, `Refresh()`, `Focus()`.
- Evenimente: `Click`, `TextChanged`, `CheckedChanged`, `MouseEnter`, `MouseLeave`, `KeyDown`, `FormClosing`.

3.4 Modificarea proprietăților la runtime
- Exemplu: activare/dezactivare buton în funcție de conținutul unui TextBox:
```csharp
private void textBoxName_TextChanged(object sender, EventArgs e)
{
    buttonSave.Enabled = !string.IsNullOrWhiteSpace(textBoxName.Text);
}
```

---

## 4. Partea II — Aplicație complexă cu controlere frecvent utilizate

4.1 Componente frecvent utilizate (descriere și utilizări practice)
- Butoane de comandă (Button) — legate la `Click`.
- Casete de editare (TextBox) — validare, `LostFocus` / `Validating`.
- Etichete (Label) — mesaje de stare/erori.
- Control multiline (RichTextBox) — afișare/edita text mare.
- Meniu principal (MenuStrip) și meniuri contextuale (ContextMenuStrip).
- Grupuri de butoane (RadioButton) în GroupBox pentru selecție exclusivă.

4.2 MDI — adăugare/excludere/modificare ferestre
- Setare formă părinte:
```csharp
this.IsMdiContainer = true;
```
- Creare fereastră copil:
```csharp
var child = new DocumentForm();
child.MdiParent = this;
child.Show();
```

4.3 Comportament și starea componentelor conform specificațiilor
- Exemplu: ascunderea unei secțiuni când opțiunea coresp. nu e selectată:
```csharp
panelAdvanced.Visible = checkBoxAdvanced.Checked;
```

4.4 Conversia șirurilor de caractere
- Conversii sigure:
```csharp
if (int.TryParse(textBoxQty.Text, out int qty)) { /* folosește qty */ }
else { MessageBox.Show("Introduceți un număr întreg valid."); }
```
- Formatare:
```csharp
labelPrice.Text = price.ToString("C2"); // Currencies, 2 zecimale
```

---

## 5. Instrucțiuni de redactare și formatare (cerințele specifice)
La redactarea lucrării finale (fișier Word / LibreOffice) respectați:
1. Pagina: A4; margini: stânga 30 mm, sus 20 mm, jos 20 mm, dreapta 10 mm.
2. Titlu: Arial, 14, Bold, centrat.
3. Corp text: Arial, 12, aliniere justificat, spațiu între rânduri 1.5.
4. Listinguri cod: Calibri, 10, aliniere left, spațiu 1. Includeți numere de linii dacă este posibil.
5. Imagini: numerotate și denumite sub imagine (centru), ex: "Figura 1 — Interfața principală".
6. Numerotarea paginilor: jos, centru.
7. Cuprins și bibliografie generate automat.
8. Foaie de titlu conform modelului instituției (include: titlu, autor, coordonator, facultate, data).

---

## 6. Instrucțiuni de instalare și rulare pentru TERASIG
Prerechizite:
- Visual Studio 2019/2022 (sau Rider) compatibil cu proiectul.
- .NET Target: verificați proprietățile proiectului (recomandat .NET 6 sau .NET Framework 4.7.2 — adaptabil).
- Pachete NuGet (dacă există) — rulați Restore.

Pași pentru rulare:
1. Clonați repo: git clone https://github.com/wernxxx/TERASIG.git
2. Deschideți soluția TERASIG.sln în Visual Studio.
3. Build -> Rebuild Solution.
4. Run (F5) — aplicația principală (Nevopie/MainForm) se va deschide.
5. Testați scenariile: creare fereastră copil, validare input, conversii.

Notă: Dacă doriți, pot adăuga instrucțiuni exacte pentru versiunea .NET folosită în repo; trimiteți-mi fișierul .csproj sau spuneți versiunea.

---

## 7. Structura proiectului TERASIG (sugestie / existent)
- /TERASIG
  - TERASIG.sln
  - /TERASIG (proiect C#)
    - Program.cs
    - MainForm.cs
    - MainForm.Designer.cs
    - ChildForm.cs
    - /Resources (imagini, iconuri)
    - /Docs (această lucrare, imagini, PDF)
    - /Listings (cod pentru anexă)
    - README.md (acest fișier)

Adaptați structura la repo-ul existent; dacă doriți, pot lista fișierele curente din folderul TERASIG.

---

## 8. Exemple de cod (listings pentru anexă)
Exemplu inițializare buton și handler:
```csharp
// MainForm.cs (parţial)
public MainForm()
{
    InitializeComponent();
    buttonNew.Click += ButtonNew_Click;
}

private void ButtonNew_Click(object sender, EventArgs e)
{
    var child = new ChildForm();
    child.MdiParent = this;
    child.Show();
}
```

Exemplu validare/conversie:
```csharp
private void buttonProcess_Click(object sender, EventArgs e)
{
    if (!int.TryParse(textBoxId.Text, out int id))
    {
        MessageBox.Show("ID invalid. Introduceți un număr întreg.");
        return;
    }
    // folosește id
}
```

---

## 9. Bibliografie (exemple)
- Microsoft Docs — Windows Forms: https://learn.microsoft.com/dotnet/desktop/winforms/
- C# Programming Guide — Microsoft Docs
- Material de curs TERASIG / note practice

(Adăugați referințele bibliografice folosite în realizarea proiectului.)

---

## 10. Concluzie
Concluzia lucrării TERASIG / Nevopie trebuie să:
- Rezume principalele realizări: demonstrări funcționale ale controlerelor, gestionare MDI, validare și conversii;
- Recunoască limitările și dificultățile întâmpinate (ex.: compatibilitate versiuni .NET, testare UI);
- Propună pași viitori: migrare/portare la WPF, îmbunătățirea UX, localizare, teste automate UI;
- Să conțină un comentariu personal asupra realizărilor în raport cu obiectivele inițiale.

---

Anexe recomandate (în repo, folder /Docs /Listings):
- Lista completă a fișierelor sursă comentate;
- Capturi de ecran pentru interfață (Figura 1, Figura 2);
- Listing complet al codului principal (font Calibri 10).

---

Ce am făcut și următorii pași
- Am adaptat textul general anterior pentru a fi specific proiectului TERASIG și aplicației "Nevopie" și am structurat documentul conform cerințelor tale (format pagină, fonturi, listări etc.).  
- Următorul pas: pot salva acest README.md în folderul TERASIG din repository (commit) — îmi dai confirmarea pentru a face commit și mesajul de commit dorit (ex: "Add project README for TERASIG / Nevopie")? Alternativ, pot ajusta conținutul (completa cu numele autorului, versiunea .NET, sau lista exactă de fișiere) înainte de a salva.
