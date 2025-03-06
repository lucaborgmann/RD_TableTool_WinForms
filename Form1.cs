using RD_Table_Tool;
using RD_TableTool_WinForms.Properties;
using System.Collections;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RD_TableTool_WinForms
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView; //um es überall in der Klasse zu verwenden

        //Pfade zu den AusgabeOrdnern
        String MenuItemOutputDirectoryPath = Settings.Default.OutputMenuItemPath;
        String EdtOutputPath = Settings.Default.OutputEDTPath;
        String TableOutputPath = Settings.Default.OutputTablePath;
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

            // Erstelle die Menüpunkte
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem optionsMenu = new ToolStripMenuItem("Options");

            // Füge die Menüpunkte zum MenuStrip hinzu
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(optionsMenu);

            // Füge das MenuStrip zum Formular hinzu
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);


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
                            XML.CreateEDT(dataGridNameValue, dataGridNameValue, "Test", $"C:\\Users\\LucaBorgmann\\OneDrive - Roedl Dynamics GmbH\\Desktop\\Abschlussprojekt\\EDT");
                        }
                    }
                }

                // Übergabe der Liste an die CreateTable-Methode
                XML.CreateTable(name, label, "C:\\Users\\LucaBorgmann\\OneDrive - Roedl Dynamics GmbH\\Desktop\\Abschlussprojekt\\Tables", dataListValues);
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
    }

    //Klasse um die Daten besser speichern zu können 
    public class DataGridViewRowData
    {
        public string Name { get; set; }
        public string Label { get; set; }   
        public string BaseEDT { get; set; }

        public string CreateEDT {  get; set; }
        public string alternateKey { get; set; }

    }

}
