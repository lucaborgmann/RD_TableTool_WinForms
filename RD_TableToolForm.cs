using Microsoft.VisualBasic;
using RD_Table_Tool;
using RD_TableTool_WinForms.Properties;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices.Swift;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RD_TableTool_WinForms
{
    public partial class RD_TableToolForm : Form
    {
        public DataGridView dataGridView; //um es überall in der Klasse zu verwenden
        public static string scriptDirForm = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        //Pfade zu den AusgabeOrdnern
        String MenuItemOutputDirectoryPath = Settings.Default.OutputMenuItemPath;
        String OutputEDTPath = Settings.Default.OutputEDTPath;
        String OutputTablePath = Settings.Default.OutputTablePath;
        String FormsOutputpath = Settings.Default.OutputFormsPath;
        String PrivilegesOutputPath = Settings.Default.OutputPrivilegesPath;
        String DataEntityOutputPath = Settings.Default.OutputPathDataEntity;

        bool isCheckedMenuItems = false;
        bool isChecked_Entity = false;
        bool isChecked_Privileges = false;


        public RD_TableToolForm()
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
           

            // Füge Ereignishandler für die Untermenüpunkte hinzu
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

        /*
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        */

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //das hier ist Checkbox Privileges umbenennung läuft auf einen Fehler
        {
            if (isChecked_Privileges == false)
            {
                isChecked_Privileges = true;
            }
            else
            {
                isChecked_Privileges = false;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            string name = this.NameTextBox.Text;
            string label = this.LabelTextBox.Text;
            string properties = this.PropertyTextBox.Text;
            string formPattern = this.FormPatternCombobox.Text;

            Debug.WriteLine($" Das Trennzeichen ist {Path.DirectorySeparatorChar}");

            // Liste zum Speichern der Werte aus dem DataGrid
            List<Dictionary<string, string>> dataListValues = new List<Dictionary<string, string>>();

            try
            {
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
                                Debug.WriteLine("CreateEDT: ist ja");
                                Debug.WriteLine($"Der ist {dataGridNameValue}");
                                XML.CreateEDT(dataGridNameValue, dataGridNameValue, baseEDT, OutputEDTPath);
                            }
                        }
                    }

                    // Übergabe der Liste an die CreateTable-Methode
                    XML.CreateTable(name, label, OutputTablePath, dataListValues);


                }
                else
                {
                    Debug.WriteLine("DataGrid nicht initialisiert");
                }

                if (isCheckedMenuItems)
                {
                    Debug.WriteLine("Entity ist ausgewählt");

                    XML.CreateMenuItem(name, label, MenuItemOutputDirectoryPath);
                    Debug.WriteLine("Erstellt", "MenuItem wurde erstellt");
                }

                if (isChecked_Entity)
                {
                    Debug.WriteLine("Entity ist ausgewählt");
                    XML.CreateDataEntity(name, DataEntityOutputPath, label, dataListValues);
                    XML.CreateDataEntityPrivileges(PrivilegesOutputPath, name);
                }
                if (isChecked_Privileges)
                {
                    XML.CreatePrivileges(PrivilegesOutputPath, name, label);
                }

                //als letztes 
                XML.CreateForm(name, FormsOutputpath, formPattern, dataListValues);
                MessageBox.Show("Tabelle wurde erstellt", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
   
        private void SaveAsFileMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine($"Der Ordner der .exe ist {scriptDirForm}");
            Debug.WriteLine("Save as wurde gedrückt");
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                Title = "Save an XML File"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //SaveFileAs(saveFileDialog.FileName);
                FileHelper.SaveFileAs(saveFileDialog.FileName, this.NameTextBox, this.LabelTextBox, this.PropertyTextBox, this.FormPatternCombobox, dataGridView);
            }
        }
        // Ereignishandler für den "File"-Menüpunkt
        private void FileMenu_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("File Menu Clicked");
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine($"Vor dem Laden: {Settings.Default.CurrentPath}");
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = $"{scriptDirForm}",
                Filter = "XML Dateien (*.xml)|*.xml",
                Title = "Datei zum Öffnen auswählen"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileHelper.LoadFile(ofd.FileName, 
                    this.NameTextBox,
                    this.LabelTextBox, this.PropertyTextBox, 
                    this.FormPatternCombobox,dataGridView);
            }
            else
            {
                MessageBox.Show("Abbruch");
            }
        }

        private void SaveFileMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("das ist nur Save");
            FileHelper.SaveFile(Settings.Default.CurrentPath, 
                this.NameTextBox.Text, 
                this.LabelTextBox.Text, 
                this.PropertyTextBox.Text,
                this.FormPatternCombobox.Text,
                dataGridView);
        }
        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }   
    }
}