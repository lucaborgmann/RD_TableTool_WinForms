using Microsoft.VisualBasic;
using RD_Table_Tool;
using RD_TableTool_WinForms.Properties;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RD_TableTool_WinForms
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView; //um es �berall in der Klasse zu verwenden
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
        }

        private void InitializeMenu()
        {
            // Erstelle ein neues MenuStrip-Steuerelement
            MenuStrip menuStrip = new MenuStrip();

            // Erstelle die Men�punkte
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem optionsMenu = new ToolStripMenuItem("Options");

            // Erstelle Untermen�punkte f�r "File"
            ToolStripMenuItem SaveAsFileMenuItem = new ToolStripMenuItem("Save as");
            ToolStripMenuItem openFileMenuItem = new ToolStripMenuItem("Open");
            ToolStripMenuItem saveFileMenuItem = new ToolStripMenuItem("Save");

            // F�ge die Untermen�punkte dem "File"-Men� hinzu
            fileMenu.DropDownItems.Add(saveFileMenuItem);
            fileMenu.DropDownItems.Add(SaveAsFileMenuItem);
            fileMenu.DropDownItems.Add(openFileMenuItem);
           

            // F�ge die Men�punkte zum MenuStrip hinzu
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(optionsMenu);

            // F�ge das MenuStrip zum Formular hinzu
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // F�ge Ereignishandler f�r die Men�punkte hinzu
            fileMenu.Click += new EventHandler(FileMenu_Click);
            //optionsMenu.Click += new EventHandler(OptionsMenu_Click);

            // F�ge Ereignishandler f�r die Untermen�punkte hinzu
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
                Size = new Size(750, 250), // Setze die Gr��e des DataGridView
                AllowUserToAddRows = true, // Erlaube das Hinzuf�gen von Zeilen
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill //f�llt die Spaltenbreite automatisch auf 
            };

            // F�ge f�nf Spalten hinzu
            dataGridView.Columns.Add("Column1", "Name");
            dataGridView.Columns.Add("Column2", "Label");
            dataGridView.Columns.Add("Column3", "BaseEDT");
            dataGridView.Columns.Add("Column4", "CreateEDT");
            dataGridView.Columns.Add("Column5", "Alternate Key");

            FormPatternCombobox.Items.Add("Simple List");

            // F�ge das DataGridView zum Formular hinzu
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
                foreach (DataGridViewRow row in dataGridView.Rows) // f�r jedes Element im DataGrid
                {
                    if (!row.IsNewRow) // Ignoriere die neue Zeile
                    {
                        string dataGridNameValue = row.Cells["Column1"].Value?.ToString();
                        string dataGridLabelValue = row.Cells["Column2"].Value?.ToString();
                        string baseEDT = row.Cells["Column3"].Value?.ToString();
                        string createEDT = row.Cells["Column4"].Value?.ToString();
                        string alternateKey = row.Cells["Column5"].Value?.ToString();

                        // Neues Dictionary f�r die Zeile erstellen
                        Dictionary<string, string> rowData = new Dictionary<string, string>
                {
                    { "Name", dataGridNameValue },
                    { "Label", dataGridLabelValue },
                    { "BaseEDT", baseEDT },
                    { "CreateEDT", createEDT },
                    { "AlternateKey", alternateKey }
                };

                        dataListValues.Add(rowData);

                        // Pr�fen, ob ein neues EDT erstellt werden muss
                        if (createEDT.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                        {
                            System.Diagnostics.Debug.WriteLine("CreateEDT: ist ja");
                            System.Diagnostics.Debug.WriteLine($"Der ist {dataGridNameValue}");
                            XML.CreateEDT(dataGridNameValue, dataGridNameValue, "Test", OutputEDTPath);
                        }
                    }
                }

                // �bergabe der Liste an die CreateTable-Methode
                XML.CreateTable(name, label, OutputTablePath, dataListValues);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("DataGrid nicht initialisiert");
            }

            if (isCheckedMenuItems)
            {
                System.Diagnostics.Debug.WriteLine("Entity ist ausgew�hlt");

                XML.CreateMenuItem(name, label, MenuItemOutputDirectoryPath);
                System.Diagnostics.Debug.WriteLine("Erstellt", "MenuItem wurde erstellt");
            }

            if (isChecked_Entity)
            {
                System.Diagnostics.Debug.WriteLine("Entity ist ausgew�hlt");

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
        // Ereignishandler f�r den "File"-Men�punkt
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
            System.Diagnostics.Debug.WriteLine("Save as wurde gedr�ckt");
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = $"{scriptDirForm}",
                Filter = "XML Dateien (*.xml)|*.xml",
                Title = "Datei zum �ffnen ausw�hlen"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show($"�ffnen: {ofd.FileName}");
                // Hier kannst du den Code hinzuf�gen, um die Datei zu laden und zu verarbeiten
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
                Title = "Datei zum Speichern ausw�hlen"
            };
            MessageBox.Show(sfd.ShowDialog() == DialogResult.OK ? $"Speichern: {sfd.FileName}" : "Abbruch");
        }

        private void SafeFile(string pFilePath)
        {

        }

        private void LoadFile(string pFilePath)
        {
   
            XmlDocument doc = new XmlDocument();
            doc.Load(pFilePath);

            XmlNodeList elemList = doc.GetElementsByTagName("name");
            
            foreach (XmlNode node in elemList) 
            {
                string tagValue = node.InnerText;
                NameTextBox.Text = tagValue;
            }

            XmlNodeList elemList2 = doc.GetElementsByTagName("label");

            foreach (XmlNode node2 in elemList2)
            {
                string tagValue2 = node2.InnerText;
                LabelTextBox.Text = tagValue2;
            }

            XmlNodeList elemList3 = doc.GetElementsByTagName("property");
            foreach (XmlNode node3 in elemList3)
            {
                string tagValue3 = node3.InnerText;
                PropertyTextBox.Text = tagValue3;
            }


            XmlNodeList elemList4 = doc.GetElementsByTagName("formpattern");
            foreach (XmlNode node4 in elemList4)
            {
                string tagValue4 = node4.InnerText;
                FormPatternCombobox.Text = tagValue4;
            }
        }
            

    }


    //Klasse um die Daten besser speichern zu k�nnen 
    public class DataGridViewRowData
    {
        public string Name { get; set; }
        public string Label { get; set; }   
        public string BaseEDT { get; set; }

        public string CreateEDT {  get; set; }
        public string alternateKey { get; set; }

    }

}
