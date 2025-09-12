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
        //public DataGridView dataGridView; //um es �berall in der Klasse zu verwenden
        public static string scriptDirForm = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        //Pfade zu den AusgabeOrdnern
        // String MenuItemOutputDirectoryPath = Settings.Default.OutputMenuItemPath;
        String MenuItemOutputDirectoryPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputMenuItemPath}";
        String OutputEDTPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputEDTPath}";
        String OutputTablePath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputTablePath}";
        String FormsOutputpath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputFormsPath}";
        String PrivilegesOutputPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputPrivilegesPath}";
        String DataEntityOutputPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputPathDataEntity}";

        /*
        String OutputEDTPath = Settings.Default.OutputEDTPath;
        String OutputTablePath = Settings.Default.OutputTablePath;
        String FormsOutputpath = Settings.Default.OutputFormsPath;
        String PrivilegesOutputPath = Settings.Default.OutputPrivilegesPath;
        String DataEntityOutputPath = Settings.Default.OutputPathDataEntity;
        */

        bool isCheckedMenuItems = false;
        bool isChecked_Entity = false;
        bool isChecked_Privileges = false;


        public RD_TableToolForm()
        {
            InitializeComponent();
            InitializeMenu();
            //InitializeDataGridView();
            InitializeDataGrid();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //Verhindert das Vergr��ern des Fensters 
            this.MaximizeBox = false; // Optional: Deaktiviert die Maximieren-Schaltfl�che

            FormPatternCombobox.Items.Add("Simple List");
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

            //Erstelle Untermen�punkte f�r "Options"
            ToolStripMenuItem optionsFileMenuItem = new ToolStripMenuItem("Select Model");

            // F�ge die Untermen�punkte dem "File"-Men� hinzu
            fileMenu.DropDownItems.Add(saveFileMenuItem);
            fileMenu.DropDownItems.Add(SaveAsFileMenuItem);
            fileMenu.DropDownItems.Add(openFileMenuItem);

            //Unterpunkte dem "Options"-Men� hinzuf�gen
            optionsMenu.DropDownItems.Add(optionsFileMenuItem);

            // F�ge die Men�punkte zum MenuStrip hinzu
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(optionsMenu);

            // F�ge das MenuStrip zum Formular hinzu
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // F�ge Ereignishandler f�r die Men�punkte hinzu
            fileMenu.Click += new EventHandler(FileMenu_Click);
           

            // F�ge Ereignishandler f�r die Untermen�punkte hinzu
            openFileMenuItem.Click += new EventHandler(OpenFileMenuItem_Click);
            saveFileMenuItem.Click += new EventHandler(SaveFileMenuItem_Click);
            SaveAsFileMenuItem.Click += new EventHandler(SaveAsFileMenuItem_Click);

            //Hinzuf�gen eines Ereignishandlers f�r den Options Tab
            optionsFileMenuItem.Click += new EventHandler(OptionsFileMenuItem_Click);

        }

        private void InitializeDataGrid()
        { 
            DataGrid.Columns.Add("Column1", "Name");
            DataGrid.Columns.Add("Column2", "Label");
            DataGrid.Columns.Add("Column3", "BaseEDT");
            DataGrid.Columns.Add("Column4", "CreateEDT");
            DataGrid.Columns.Add("Column5", "Alternate Key");
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //das hier ist Checkbox Privileges umbenennung l�uft auf einen Fehler
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
                if (DataGrid != null)
                {
                    // Erstellen einer Liste mit allen Feldern des DataGrids
                    // f�r jedes Element im DataGrid
                    foreach (DataGridViewRow row in DataGrid.Rows) 
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
                                Debug.WriteLine("CreateEDT: ist ja");
                                Debug.WriteLine($"Der ist {dataGridNameValue}");
                                XML.CreateEDT(dataGridNameValue, 
                                    dataGridNameValue, 
                                    baseEDT,
                                    OutputEDTPath);
                            }
                        }
                    }

                    // �bergabe der Liste an die CreateTable-Methode
                    XML.CreateTable(name, label, OutputTablePath, dataListValues);


                }
                else
                {
                    Debug.WriteLine("DataGrid nicht initialisiert");
                }

                if (isCheckedMenuItems)
                {
                    Debug.WriteLine("Entity ist ausgew�hlt");

                    XML.CreateMenuItem(name, label, MenuItemOutputDirectoryPath);
                    Debug.WriteLine("Erstellt", "MenuItem wurde erstellt");
                }

                if (isChecked_Entity)
                {
                    Debug.WriteLine("Entity ist ausgew�hlt");
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
            Debug.WriteLine("Save as wurde gedr�ckt");
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                Title = "Save an XML File"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //SaveFileAs(saveFileDialog.FileName);
                DraftFileHandler.SaveFileAs(saveFileDialog.FileName, 
                                            this.NameTextBox, 
                                            this.LabelTextBox, 
                                            this.PropertyTextBox, 
                                            this.FormPatternCombobox,
                                            DataGrid);
            }
        }
        // Ereignishandler f�r den "File"-Men�punkt
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
                Title = "Datei zum �ffnen ausw�hlen"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DraftFileHandler.LoadFile(ofd.FileName, 
                    this.NameTextBox,
                    this.LabelTextBox, this.PropertyTextBox, 
                    this.FormPatternCombobox, DataGrid);
            }
            else
            {
                MessageBox.Show("Abbruch");
            }
        }

        private void SaveFileMenuItem_Click(object sender, EventArgs e)
        {
            DraftFileHandler.SaveFile(Settings.Default.CurrentPath, 
                this.NameTextBox.Text, 
                this.LabelTextBox.Text, 
                this.PropertyTextBox.Text,
                this.FormPatternCombobox.Text,
                DataGrid);
        }
        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        //Methode f�r den Eventhandler des Optionstab
        private void OptionsFileMenuItem_Click(object sender, EventArgs e)
        {
            /*
            using (ModelSelectionForm modelForm = new ModelSelectionForm())
            {

                modelForm.StartPosition = FormStartPosition.CenterParent; // Wichtig!
                var result = modelForm.ShowDialog(this); // "this" gibt das Hauptfenster als Paren

                
                if (modelForm.ShowDialog() == DialogResult.OK)
                {
                    //string selectedModel = modelForm.SelectedModel;
                    //MessageBox.Show($"Selected model: {selectedModel}", "Model Selection");
                    // Hier kannst du mit dem ausgew�hlten Modell weiterarbeiten
                }
               
            }
            */


            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Please select a model.";
                folderDialog.ShowNewFolderButton = true;


                // Standardordner setzen
                //folderDialog.SelectedPath = @"K:\AosService\PackagesLocalDirectory"; // hier noch den Standard pfad in die Settingsdatei �bernehmen

                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    string selectedPath = folderDialog.SelectedPath;
                    Settings.Default.SelectedModel = selectedPath; //wei�t den gew�hlten Pfad dem ausgew�hltem Model in der Settingsdatei zu 
                    MessageBox.Show($"Ausgew�hlter Ordner: {selectedPath}", "Ordnerauswahl");
                    Debug.WriteLine($"gesamter Pfad Ordner: {selectedPath}");

                    string lastFolder = new DirectoryInfo(selectedPath).Name; // speichert den letzten Ordnernamen um ihn nochmal anheften zu k�nnen
                    Debug.WriteLine($"letzter Ordner: {lastFolder}");
                    Settings.Default.Model = lastFolder; // Wei�t das Model in Model erneut zu 
                    Settings.Default.Save();


                    string fullpath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputMenuItemPath}";
                    Debug.WriteLine($"Zum testen des Pfades: {fullpath}");
                    refreshPaths(); 

                    //nur zum testen 
                    /*
                    MessageBox.Show($"Pfad zu den MenuItems: {MenuItemOutputDirectoryPath}", "Ordnerauswahl");
                    MessageBox.Show($"Pfad zu den EDTs: {OutputEDTPath}", "Ordnerauswahl");
                    MessageBox.Show($"Pfad zu den Tables: {OutputTablePath}", "Ordnerauswahl");
                    MessageBox.Show($"Pfad zu den Forms: {FormsOutputpath}", "Ordnerauswahl");
                    MessageBox.Show($"Pfad zu den Privileges: {PrivilegesOutputPath}", "Ordnerauswahl");
                    MessageBox.Show($"Pfad zu den DataEntity: {DataEntityOutputPath}", "Ordnerauswahl");
                    */
                }
            }
        }
        private void refreshPaths()
        {
            MenuItemOutputDirectoryPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputMenuItemPath}";
            OutputEDTPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputEDTPath}";
            OutputTablePath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputTablePath}";
            FormsOutputpath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputFormsPath}";
            PrivilegesOutputPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputPrivilegesPath}";
            DataEntityOutputPath = $"{Settings.Default.SelectedModel}\\{Settings.Default.Model}{Settings.Default.OutputPathDataEntity}";
        }

    }
}