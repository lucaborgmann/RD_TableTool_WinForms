using RD_TableTool_WinForms.Properties;
using System;
using System.Xml;
using System.Xml.Linq;

namespace RD_TableTool_WinForms
{
    public class FileHelper
    {
        public FileHelper()
        {
        }

        public static void SaveFile(
            string filePath,
            string nameText,
            string labelText,
            string propertyText,
            string formPatternText,
            DataGridView dataGridView)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            if (dataGridView != null)
            {
                dataGridView.EndEdit();
                dataGridView.CurrentCell = null; // Fokus wegnehmen -> Änderungen erzwingen
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            FormHelper.ReplaceTagContent(doc, "//name", nameText);
            FormHelper.ReplaceTagContent(doc, "//label", labelText);
            FormHelper.ReplaceTagContent(doc, "//property", propertyText);
            FormHelper.ReplaceTagContent(doc, "//formpattern", formPatternText);

            List<Dictionary<string, string>> dataListValues = ExtractDataGridValues(dataGridView);

            FormHelper.UpdateDataGridContent(doc, dataListValues);

            doc.Save(filePath);
        }

        private static List<Dictionary<string, string>> ExtractDataGridValues(DataGridView dataGridView)
        {
            var dataListValues = new List<Dictionary<string, string>>();

            if (dataGridView != null)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var rowData = new Dictionary<string, string>
                    {
                            { "Name", row.Cells["Column1"].Value?.ToString() },
                            { "Label", row.Cells["Column2"].Value?.ToString() },
                            { "BaseEDT", row.Cells["Column3"].Value?.ToString() },
                            { "CreateEDT", row.Cells["Column4"].Value?.ToString() },
                            { "AlternateKey", row.Cells["Column5"].Value?.ToString() }
                    };

                        dataListValues.Add(rowData);
                    }
                }
            }

            return dataListValues;
        }

        public static void LoadFile(
            string filePath,
            TextBox nameTextBox, // Muss Textbox sein wegen unten
            TextBox labelTextBox,
            TextBox propertyTextBox,
            ComboBox formPatternComboBox,
            DataGridView dataGridView)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            Settings.Default.CurrentPath = filePath; // Aktuellen Pfad merken

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            Dictionary<string, Action<string>> tagHandlers = new Dictionary<string, Action<string>>
            {
                { "name", value => nameTextBox.Text = value },
                { "label", value => labelTextBox.Text = value },
                { "property", value => propertyTextBox.Text = value },
                { "formpattern", value => formPatternComboBox.Text = value }
            };

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (tagHandlers.TryGetValue(node.Name, out var handler))
                {
                    handler(node.InnerText);
                }
            }

            XmlNodeList nodes = doc.SelectNodes("//datagrid/field");

            dataGridView.Rows.Clear();

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

        public static void SaveFileAs(
            string filePath,
            TextBox nameTextBox,
            TextBox labelTextBox,
            TextBox propertyTextBox,
            ComboBox formPatternComboBox,
            DataGridView dataGridView)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            dataGridView.EndEdit(); // sorgt dafür, dass alle Daten aus dem DataGrid übernommen werden 

            try
            {
                var data = new XElement("root",
                    new XElement("name", nameTextBox.Text),
                    new XElement("label", labelTextBox.Text),
                    new XElement("property", propertyTextBox.Text),
                    new XElement("formpattern", formPatternComboBox.Text),
                    new XElement("datagrid", // fügt ein neues Kind-Element hinzu, welches um weitere Elemente erweitert wird
                        dataGridView.Rows.Cast<DataGridViewRow>() // Konvertiert die Zeilen des DataGrid in eine Sammlung
                            .Where(row => !row.IsNewRow) // filtert die Zeilen heraus, die neue Zeilen sind 
                            .Select(row =>
                            { // wendet die Lambda-Funktion auf alle Zeilen an
                                var fieldname = row.Cells["Column1"].Value?.ToString() ?? string.Empty; // extrahiert den Wert in der Zelle Column1 und konvertiert ihn in einen String; ist der Wert null, wird ein leerer String verwendet
                                var fieldlabel = row.Cells["Column2"].Value?.ToString() ?? string.Empty;
                                var baseEDT = row.Cells["Column3"].Value?.ToString() ?? string.Empty;
                                var createEDT = row.Cells["Column4"].Value?.ToString() ?? string.Empty;
                                var alternateKey = row.Cells["Column5"].Value?.ToString() ?? string.Empty;

                                // Debugging-Ausgabe
                                Console.WriteLine($"fieldname: {fieldname}, fieldlabel: {fieldlabel}, baseEDT: {baseEDT}, createEDT: {createEDT}, alternateKey: {alternateKey}");

                                // erstellt ein neues XML-Element "field" mit den extrahierten Werten als Kind-Elemente
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

                Settings.Default.CurrentPath = filePath; // ermöglicht das fehlerfreie Verwenden der Save-Methode, da der aktuelle Pfad gesetzt wird

                MessageBox.Show("XML-Datei erfolgreich gespeichert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

