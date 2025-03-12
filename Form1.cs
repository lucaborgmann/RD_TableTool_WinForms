using Microsoft.VisualBasic;
using RD_Table_Tool;
using RD_TableTool_WinForms.Properties;
using System.Collections;
using System.Runtime.InteropServices.Swift;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RD_TableTool_WinForms
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView; //um es überall in der Klasse zu verwenden
        public static string scriptDirForm = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        //Pfade zu den AusgabeOrdnern
        String MenuItemOutputDirectoryPath = Settings.Default.OutputMenuItemPath;
        String OutputEDTPath = Settings.Default.OutputEDTPath;
        String OutputTablePath = Settings.Default.OutputTablePath;
        String FormsOutputpath = Settings.Default.OutputFormsPath;

        bool isCheckedMenuItems = false;
        bool isChecked_Entity = false;

        

        public Form1()
        {
            InitializeComponent();
            InitializeMenu();
            InitializeDataGridView();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //Verhindert das Vergrößern des Fensters 
            this.MaximizeBox = false; // Optional: Deaktiviert die Maximieren-Schaltfläche
        }

        private void InitializeMenu()
        {
            // Erstelle ein neues MenuStrip-Steuerelement
            MenuStrip menuStrip = new MenuStrip();

            // Erstelle die Menüpunkte
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem optionsMenu = new ToolStripMenuItem("Options");

            // Erstelle Untermenüpunkte für "File"
            ToolStripMenuItem SaveAsFileMenuItem = new ToolStripMenuItem("Save as");
            ToolStripMenuItem openFileMenuItem = new ToolStripMenuItem("Open");
            ToolStripMenuItem saveFileMenuItem = new ToolStripMenuItem("Save");

            // Füge die Untermenüpunkte dem "File"-Menü hinzu
            fileMenu.DropDownItems.Add(saveFileMenuItem);
            fileMenu.DropDownItems.Add(SaveAsFileMenuItem);
            fileMenu.DropDownItems.Add(openFileMenuItem);
           

            // Füge die Menüpunkte zum MenuStrip hinzu
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(optionsMenu);

            // Füge das MenuStrip zum Formular hinzu
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // Füge Ereignishandler für die Menüpunkte hinzu
            fileMenu.Click += new EventHandler(FileMenu_Click);
            //optionsMenu.Click += new EventHandler(OptionsMenu_Click);

            // Füge Ereignishandler für die Untermenüpunkte hinzu
           //newFileMenuItem.Click += new EventHandler(NewFileMenuItem_Click);
           openFileMenuItem.Click += new EventHandler(OpenFileMenuItem_Click);
           saveFileMenuItem.Click += new EventHandler(SaveFileMenuItem_Click);
           SaveAsFileMenuItem.Click += new EventHandler(SaveAsFileMenuItem_Click);

        }

        //Datagrid Initalisieren
        private void InitializeDataGridView()
        {
            // Erstelle ein neues DataGridView-Steuerelement
            dataGridView = new DataGridView
            {
                Location = new Point(10, 300), // Positioniere das DataGridView
                Size = new Size(750, 250), // Setze die Größe des DataGridView
                AllowUserToAddRows = true, // Erlaube das Hinzufügen von Zeilen
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill //füllt die Spaltenbreite automatisch auf 
            };

            // Füge fünf Spalten hinzu
            dataGridView.Columns.Add("Column1", "Name");
            dataGridView.Columns.Add("Column2", "Label");
            dataGridView.Columns.Add("Column3", "BaseEDT");
            dataGridView.Columns.Add("Column4", "CreateEDT");
            dataGridView.Columns.Add("Column5", "Alternate Key");

            FormPatternCombobox.Items.Add("Simple List");

            // Füge das DataGridView zum Formular hinzu
            this.Controls.Add(dataGridView);
        }


        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void PropertyLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            string name = this.NameTextBox.Text;
            string label = this.LabelTextBox.Text;
            string properties = this.PropertyTextBox.Text;

            // Liste zum Speichern der Werte aus dem DataGrid
            List<Dictionary<string, string>> dataListValues = new List<Dictionary<string, string>>();

            // Falls das DataGrid initialisiert ist
            if (dataGridView != null)
            {
                // Erstellen einer Liste mit allen Feldern des DataGrids
                foreach (DataGridViewRow row in dataGridView.Rows) // für jedes Element im DataGrid
                {
                    if (!row.IsNewRow) // Ignoriere die neue Zeile
                    {
                        string dataGridNameValue = row.Cells["Column1"].Value?.ToString();
                        string dataGridLabelValue = row.Cells["Column2"].Value?.ToString();
                        string baseEDT = row.Cells["Column3"].Value?.ToString();
                        string createEDT = row.Cells["Column4"].Value?.ToString();
                        string alternateKey = row.Cells["Column5"].Value?.ToString();

                        // Neues Dictionary für die Zeile erstellen
                        Dictionary<string, string> rowData = new Dictionary<string, string>
                        {
                            { "Name", dataGridNameValue },
                            { "Label", dataGridLabelValue },
                            { "BaseEDT", baseEDT },
                            { "CreateEDT", createEDT },
                            { "AlternateKey", alternateKey }
                        };

                        dataListValues.Add(rowData);

                        // Prüfen, ob ein neues EDT erstellt werden muss
                        if (createEDT.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                        {
                            System.Diagnostics.Debug.WriteLine("CreateEDT: ist ja");
                            System.Diagnostics.Debug.WriteLine($"Der ist {dataGridNameValue}");
                            XML.CreateEDT(dataGridNameValue, dataGridNameValue, "Test", OutputEDTPath);
                        }
                    }
                }

                // Übergabe der Liste an die CreateTable-Methode
                XML.CreateTable(name, label, OutputTablePath, dataListValues);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("DataGrid nicht initialisiert");
            }

            if (isCheckedMenuItems)
            {
                System.Diagnostics.Debug.WriteLine("Entity ist ausgewählt");

                XML.CreateMenuItem(name, label, MenuItemOutputDirectoryPath);
                System.Diagnostics.Debug.WriteLine("Erstellt", "MenuItem wurde erstellt");
            }

            if (isChecked_Entity)
            {
                System.Diagnostics.Debug.WriteLine("Entity ist ausgewählt");

            }

            //als letztes 
            XML.CreateForm(name, FormsOutputpath);
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateMenuItemsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isCheckedMenuItems = true;
        }

        private void CreateEntityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isChecked_Entity = true;
        }
        // Ereignishandler für den "File"-Menüpunkt
        private void FileMenu_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("File menu clicked!");
            System.Diagnostics.Debug.WriteLine("File Menu Clicked");
        }

        private void NewFileMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("New File Clicked");
        }


        private void SaveAsFileMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Save as wurde gedrückt");
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                Title = "Save an XML File"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFileAs(saveFileDialog.FileName);
            }
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = $"{scriptDirForm}",
                Filter = "XML Dateien (*.xml)|*.xml",
                Title = "Datei zum Öffnen auswählen"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show($"Öffnen: {ofd.FileName}");
                // Hier kannst du den Code hinzufügen, um die Datei zu laden und zu verarbeiten
                LoadFile(ofd.FileName); 
            }
            else
            {
                MessageBox.Show("Abbruch");
            }
        }

        private void SaveFileMenuItem_Click(object sender, EventArgs e) 
        {
            //Quelle: Einstieg in C# mit Visual Studio 2022 von Thomas Theis Thomas Theis Seite 352
            SaveFileDialog sfd = new SaveFileDialog()
            {
                InitialDirectory = scriptDirForm,
                Filter = "XML Dateien (*.xml)|*.xml",
                Title = "Datei zum Speichern auswählen"
            };
            MessageBox.Show(sfd.ShowDialog() == DialogResult.OK ? $"Speichern: {sfd.FileName}" : "Abbruch");
            SafeFile(sfd.FileName);
        }

        private void SafeFile(string pFilePath)
        {
            try
            {
                // der Pfad der aktuell verwendeten Datei 
            }
            catch(Exception ex)
            {

            }

        }

        private void LoadFile(string pFilePath)
        {
            //Lädt das angegebene XML Dokument
            XmlDocument doc = new XmlDocument();
            doc.Load(pFilePath);

            Dictionary<string, Action<string>> tagHandlers = new Dictionary<string, Action<string>> //name des XML Tags Action <string> Aktion die ausgeführt wird
{
                { "name", value => NameTextBox.Text = value }, //lamda Funktion weißt InnerText eines XML-Knotens das UI-Element zu
                { "label", value => LabelTextBox.Text = value },
                { "property", value => PropertyTextBox.Text = value },
                { "formpattern", value => FormPatternCombobox.Text = value }
            };

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (tagHandlers.TryGetValue(node.Name, out var handler)) //prüft aktuelles XmlNode als Schlüssel in in tagshandler Dictonary vorkommt
                {
                    handler(node.InnerText); //falls Handler existiert gespeicherte Aktion ausgeführt 
                }
            }
            
            XmlNodeList nodes = doc.SelectNodes("//datagrid/field");
            // Zeile für Zeile hinzufügen
            foreach (XmlNode node in nodes)
            {
                string fieldName = node.SelectSingleNode("fieldname")?.InnerText ?? "Kein Wert";
                string fieldLabel = node.SelectSingleNode("fieldlabel")?.InnerText ?? "Kein Wert";
                string baseEDT = node.SelectSingleNode("baseEDT")?.InnerText ?? "Kein Wert";
                string createEDT = node.SelectSingleNode("createEDT")?.InnerText ?? "Kein Wert";
                string alternateKey = node.SelectSingleNode("alternateKey")?.InnerText ?? "Kein Wert";

                dataGridView.Rows.Add(fieldName, fieldLabel, baseEDT, createEDT, alternateKey);
            }
        }

        private void SaveFileAs(string pFilePath)
        {

            dataGridView.EndEdit(); //sorgt dafür das alle Daten aus dem DataGrid übernommen werden 

            try
            {
                string filePath = pFilePath;

                var data = new XElement("root",
                    new XElement("name", this.NameTextBox.Text),
                    new XElement("label", this.LabelTextBox.Text),
                    new XElement("property", this.PropertyTextBox.Text),
                    new XElement("formpattern", this.FormPatternCombobox.Text),
                    new XElement("datagrid", //fügt ein neues Kind element hinzu welches um weitere erweitert wird 
                        this.dataGridView.Rows.Cast<DataGridViewRow>() //Konvertiert die Zeilen des DataGrid in eine Sammlung
                            .Where(row => !row.IsNewRow) //filtert die Zeilen heraus die neue Zeilen sind 
                            .Select(row => { //Wendet die Lamda funktion auf alle 
                                var fieldname = row.Cells["Column1"].Value?.ToString() ?? string.Empty; //Extrahiert den Wert in der Zelle Colum1 und konvertiert ihn in einen String ist der Wert null in einen Leeren string
                                var fieldlabel = row.Cells["Column2"].Value?.ToString() ?? string.Empty;
                                var baseEDT = row.Cells["Column3"].Value?.ToString() ?? string.Empty;
                                var createEDT = row.Cells["Column4"].Value?.ToString() ?? string.Empty;
                                var alternateKey = row.Cells["Column5"].Value?.ToString() ?? string.Empty;

                                // Debugging-Ausgabe
                                Console.WriteLine($"fieldname: {fieldname}, fieldlabel: {fieldlabel}, baseEDT: {baseEDT}, createEDT: {createEDT}, alternateKey: {alternateKey}");

                                //erstellt ein neues XML-Element "field" mit den extrahierten Werten als Kind Elemente
                                return new XElement("field",
                                    new XElement("fieldname", fieldname),
                                    new XElement("fieldlabel", fieldlabel),
                                    new XElement("baseEDT", baseEDT),
                                    new XElement("createEDT", createEDT),
                                    new XElement("alternateKey", alternateKey)
                                );
                            })
                    )
                );

                // XML-Dokument erstellen und speichern
                var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), data);
                xmlDocument.Save(filePath);

                MessageBox.Show("XML-Datei erfolgreich gespeichert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
    }


    //Klasse um die Daten besser speichern zu können (evtl rausnehmen wird aktuell nicht verwendet) 
    public class DataGridViewRowData
    {
        public string Name { get; set; }
        public string Label { get; set; }   
        public string BaseEDT { get; set; }

        public string CreateEDT {  get; set; }
        public string alternateKey { get; set; }

    }

}
