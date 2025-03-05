using System.Collections;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RD_TableTool_WinForms
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView; //um es überall in der Klasse zu verwenden
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

            //ArrayList zum speichern der Werte im aus dem DataGrid 
            ArrayList dataListValues = new ArrayList();
            //Falls das dataGrid initialiesiert ist
            if (dataGridView != null)
            {
                // erstellen einer ArrayList mit allen Feldern des DataGrids 
                foreach (DataGridViewRow row in dataGridView.Rows) //für jedes Element im DataGrid
                {
                    
                    if (!row.IsNewRow) // Ignoriere die neue Zeile
                    {
                        string dataGridNameValue = row.Cells["Column1"].Value?.ToString();
                        string dataGridLabelValue = row.Cells["Column2"].Value?.ToString();
                        string baseEDT = row.Cells["Column3"].Value?.ToString();
                        string createEDT = row.Cells["Column4"].Value?.ToString();
                        string alternateKey = row.Cells["Column5"].Value?.ToString();

                        // neues Objekt der Klasse DataGridViewRowData
                        DataGridViewRowData rowData = new DataGridViewRowData
                        {
                            Name = dataGridNameValue,
                            Label = dataGridLabelValue,
                            BaseEDT = baseEDT,
                            CreateEDT = createEDT,
                            alternateKey = alternateKey
                        };
                        dataListValues.Add(rowData);

                        //Prüfen ob ein neues EDT erstellt werden muss 
                        if (createEDT.Equals("Yes",StringComparison.OrdinalIgnoreCase))
                        {
                            System.Diagnostics.Debug.WriteLine("CreateEDT: ist ja ");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("CreateEDT: ist Nein");
                        }
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("DataGrid nicht initialiert");
            }




        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
