using System;
using System.Xml;

namespace RD_TableTool_WinForms
{
    public class DraftFileHelper
    {
        public DraftFileHelper()
        {

        }
        public static void ReplaceTagContent(XmlDocument doc, string xpath, string newValue)
        {
            XmlNodeList nodes = doc.SelectNodes(xpath);
            if (nodes != null)
            {
                foreach (XmlNode node in nodes) // Für jedes Element mit dem Namen xpath 
                {
                    node.InnerText = newValue; // ersetz den Tag Inhalt durch den neuen Wert 
                }
            }
        }

        public static void UpdateDataGridContent(XmlDocument doc, List<Dictionary<string, string>> pDictionaryList)
        {
            XmlNode datagridNode = doc.SelectSingleNode("//datagrid"); // Wählt das XML Element datagrird aus 
            if (datagridNode != null)
            {
                datagridNode.RemoveAll(); // Entferne alle vorhandenen "field"-Elemente

                foreach (var row in pDictionaryList) // Iteriere über jedes Dictionary in der Liste
                {
                    XmlElement fieldElement = doc.CreateElement("field");

                    XmlElement fieldnameElement = doc.CreateElement("fieldname"); //erstellt ein Element mit dem Namen Fieldname
                    //Prüft ob das Dictonary den Key Enthält Wert wird abhängig von der Bedingung gesetztt 
                    fieldnameElement.InnerText = row.ContainsKey("Name") ? row["Name"] : string.Empty;
                    //Füpgt ein Kind element hinzu 
                    fieldElement.AppendChild(fieldnameElement);

                    XmlElement fieldlabelElement = doc.CreateElement("fieldlabel");
                    fieldlabelElement.InnerText = row.ContainsKey("Label") ? row["Label"] : string.Empty;
                    fieldElement.AppendChild(fieldlabelElement);

                    XmlElement baseEDTElement = doc.CreateElement("baseEDT");
                    baseEDTElement.InnerText = row.ContainsKey("BaseEDT") ? row["BaseEDT"] : string.Empty;
                    fieldElement.AppendChild(baseEDTElement);

                    XmlElement createEDTElement = doc.CreateElement("createEDT");
                    createEDTElement.InnerText = row.ContainsKey("CreateEDT") ? row["CreateEDT"] : string.Empty;
                    fieldElement.AppendChild(createEDTElement);

                    XmlElement alternateKeyElement = doc.CreateElement("alternateKey");
                    alternateKeyElement.InnerText = row.ContainsKey("AlternateKey") ? row["AlternateKey"] : string.Empty;
                    fieldElement.AppendChild(alternateKeyElement);

                    datagridNode.AppendChild(fieldElement);// fügt ein neues 
                }
            }
        }
        public static List<Dictionary<string, string>> ExtractDataGridValues(DataGridView dataGridView)
        {
            var dataListValues = new List<Dictionary<string, string>>(); //Liste von Dictonarys

            if (dataGridView != null) // wenn nicht NUllS
            {
                foreach (DataGridViewRow row in dataGridView.Rows) // iteriert durch die dataGridView
                {
                    if (!row.IsNewRow) // Überprüft, ob die Zeile keine neue Zeile ist 
                    {
                        //Erstellt ein Dictionary für die aktuelle Zeile und extrahiert die Werte der Zellen
                        var rowData = new Dictionary<string, string>
                        {
                            { "Name", row.Cells["Column1"].Value?.ToString() },// Extrahiert den Wert der Zelle "Column1"
                            { "Label", row.Cells["Column2"].Value?.ToString() },
                            { "BaseEDT", row.Cells["Column3"].Value?.ToString() },
                            { "CreateEDT", row.Cells["Column4"].Value?.ToString() },
                            { "AlternateKey", row.Cells["Column5"].Value?.ToString() }
                        };

                        dataListValues.Add(rowData); // Fügt das Dictionary zur Liste hinzu

                    }
                }
            }

            return dataListValues; // Gibt die Liste von Dictionaries zurück
        }
    }


}
