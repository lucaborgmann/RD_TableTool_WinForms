using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RD_Table_Tool
{
    class XML
    {
        //Konstruktor
        public XML() { }

        public static void CreateMenuItem(string pName, string pLabel, string pOutputPath)
        {
            string name = pName;
            string label = pLabel;
            string outputPath = pOutputPath;

            System.Diagnostics.Debug.WriteLine($"OutPut Path: {outputPath}");

            try
            {
                // Template laden
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\RD_TableTool_WinForms\\EdtTemplate.xml");
                System.Diagnostics.Debug.WriteLine("Lädt die Template-Datei");

                // Neue XML-Datei erstellen und den Inhalt der Template-Datei übernehmen
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);

                var nodeUpdates = new Dictionary<string, string>
        {
                    { "Name", name },
                    { "Label", label },
                    { "Object", name }
                };

                foreach (var update in nodeUpdates)
                {
                    XmlNodeList nodes = newDoc.GetElementsByTagName(update.Key);
                    foreach (XmlNode node in nodes)
                    {
                        node.InnerText = update.Value;
                    }
                }
                // Neue XML-Datei speichern
                //newDoc.Save($"C:\\Users\\LucaBorgmann\\OneDrive - Roedl Dynamics GmbH\\Desktop\\Abschlussprojekt\\{name}.xml");
                newDoc.Save($"{outputPath}\\{name}.xml");
                System.Diagnostics.Debug.WriteLine("XML-Datei aktualisiert");

            }
            catch (XmlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Laden des XML-Dokuments: {ex.Message}");
            }
        }
        public static void CreateEDT(string pName, string pLabel, string pBaseEDT, string pOutputPath)
        {
            string name = pName;
            string label = pLabel;
            string baseEDT = pBaseEDT;
            string outputPath = pOutputPath;

            //System.Diagnostics.Debug.WriteLine($"OutPut Path: {outputPath}");
            System.Diagnostics.Debug.WriteLine("CrerateMenuItem: Methode wird aufgerufen");


            try
            {
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\RD_TableTool_WinForms\\EdtTemplate.xml");
                System.Diagnostics.Debug.WriteLine("CrerateMenuItem: Lädt die Template-Datei");
                // Neue XML-Datei erstellen und den Inhalt der Template-Datei übernehmen
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);

                // Tags im Dokument updaten
                XmlNodeList nodes = newDoc.GetElementsByTagName("Name");
                foreach (XmlNode node in nodes)
                {
                    node.InnerText = name;
                    System.Diagnostics.Debug.WriteLine("CrerateMenuItem: Ersetzt den Namen");
                }

                nodes = newDoc.GetElementsByTagName("Label");
                foreach (XmlNode node in nodes)
                {
                    node.InnerText = label;
                    System.Diagnostics.Debug.WriteLine("CrerateMenuItem: Ersetzt den das Label");
                }

                nodes = newDoc.GetElementsByTagName("Extends");
                foreach (XmlNode node in nodes)
                {
                    node.InnerText = baseEDT;
                    System.Diagnostics.Debug.WriteLine("CrerateMenuItem: Ersetzt den EDT");
                }

                // Neue XML-Datei speichern
                newDoc.Save($"{outputPath}\\{name}.xml");
                System.Diagnostics.Debug.WriteLine("CrerateMenuItem: XML-Datei aktualisiert");


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Laden des XML-Dokuments: {ex.Message}");
            }

        }

        public static void CreateTable(string pName, string pLabel, string pOutputPath, List<Dictionary<string, string>> fieldList)
        {
            string name = pName;
            string label = pLabel;
            string outputPath = pOutputPath;

            System.Diagnostics.Debug.WriteLine($"Größe der Liste: {fieldList.Count}");

            try
            {
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\RD_TableTool_WinForms\\TableTemplate.xml");
                System.Diagnostics.Debug.WriteLine("CreateTable: Lädt die Template-Datei");

                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);

                XmlNodeList nodes = newDoc.SelectNodes("//Name | //Label");

                foreach (XmlNode node in nodes)
                {
                    switch (node.Name)
                    {
                        case "Name":
                            node.InnerText = name;
                            System.Diagnostics.Debug.WriteLine("CreateTable: Ersetzt den Namen");
                            break;
                        case "Label":
                            node.InnerText = label;
                            System.Diagnostics.Debug.WriteLine("CreateTable: Ersetzt das Label");
                            break;
                    }
                }

                // Alle Nodes finden, nur das letzte weiterverwerten
                XmlNodeList fieldNodes = newDoc.SelectNodes("//Fields");
                XmlNode fieldNode = fieldNodes?.Count > 0 ? fieldNodes[fieldNodes.Count - 1] : null;

                if (fieldNode != null)
                {
                    System.Diagnostics.Debug.WriteLine("CreateTable: fieldNode ist nicht leer");

                    foreach (Dictionary<string, string> fieldDict in fieldList) // Durchläuft die inneren Dictionaries in der Liste
                    {
                        if (fieldDict != null)
                        {
                            // Zugriff auf die Werte im Dictionary
                            string fieldName = fieldDict["Name"];
                            string fieldLabel = fieldDict["Label"];
                            string fieldBaseEDT = fieldDict["BaseEDT"];
                            string fieldCreatEDT = fieldDict["CreateEDT"];

                            // Ausgabe der Werte
                            Console.WriteLine($"Name: {fieldName}, Label: {fieldLabel}, BaseEDT: {fieldBaseEDT}");

                            XmlElement neuesAxTableField = newDoc.CreateElement("AxTableField"); //erstellt ein neues XML Dokument

                            // Attribut für i:type setzen (Namespace beachten!)
                            XmlAttribute typeAttribute = newDoc.CreateAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance");

                            // Hier prüfen, ob CreateEDT Yes ist
                            if (fieldCreatEDT.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                            {
                                typeAttribute.Value = $"AxTableField{fieldName}";
                            }
                            else
                            {
                                typeAttribute.Value = $"AxTableField{fieldBaseEDT}";
                            }
                            neuesAxTableField.Attributes.Append(typeAttribute);

                            // Name-Element hinzufügen
                            XmlElement nameElement = newDoc.CreateElement("Name");
                            nameElement.InnerText = fieldName;
                            neuesAxTableField.AppendChild(nameElement);

                            fieldNode.AppendChild(neuesAxTableField);
                        }
                    }

                    System.Diagnostics.Debug.WriteLine("Neues Feld hinzugefügt");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Das Field-Tag konnte nicht gefunden werden");
                }

                // Speichern erst nach allen Änderungen
                string[] paths = { @$"{outputPath}", $"{name}", ".xml" };
                string fullPath = Path.Combine(paths);
                System.Diagnostics.Debug.WriteLine($"Ausgabe fullpath: {fullPath}");

                newDoc.Save($"{outputPath}\\{name}.xml");
                System.Diagnostics.Debug.WriteLine("CreateTable: XML-Datei aktualisiert");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Laden des XML-Dokuments: {ex.Message}");
            }
        }


        public static void CreateForm(string pName, string pOutputPath)
        {
            string name = pName;
            string outputPath = pOutputPath;

            try
            {
                //Lädt die Datei 
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\lucaborgmann\\RD_Table_Tool\\RD_Table_Tool\\FormTemplate.xml");
                System.Diagnostics.Debug.WriteLine("CreateForm: Lädt die Template-Datei");

                // Schreibt die Werte der Template Datei in die neue XML Datei 
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);

                //Hier muss das template überarbeitet werden
                XmlNamespaceManager nsManager = new XmlNamespaceManager(newDoc.NameTable);
                nsManager.AddNamespace("ax", "Microsoft.Dynamics.AX.Metadata.V6"); //fügt den NameSpace hinzu

                //ersten Knoten unter <Name> unter <AxForm> finden 
                XmlNode nameNode = newDoc.SelectSingleNode("/ax:AxForm/ax:Name", nsManager);

                if (nameNode != null)
                {
                    nameNode.InnerText = name;
                    System.Diagnostics.Debug.WriteLine("CreateForm: Ersetzt den ersten Namen");


                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("CreateForm: <Name> -Tag nicht gefunden!");
                }

                //es muss noch in der Zeile  public class TestForm extends FormRun "TestForm" durch den Namen ergänzt werde
                string searchString = "TestForm";
                XmlNodeList nodes = newDoc.SelectNodes("//*[contains(text(), '" + searchString + "')]");

                foreach (XmlNode node in nodes)
                {
                    node.InnerText = node.InnerText.Replace(searchString, name);
                }

                //Speichert das Dokument muss am Ende von allem Passieren !!
                newDoc.Save($"{outputPath}\\{name}.xml");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Das Field-Tag konnte nicht gefunden werden");
            }
        }

        public static void createEntity(string pTableName, string pIndexName, string pDataField, string pOutputPath, string pTablePath)
        {

            System.Diagnostics.Debug.WriteLine("CreateEntity wird ausgeführt");
            string tableName = pTableName;
            string IndexName = pIndexName;
            string outputPath = pOutputPath;
            string tablePath = pTablePath;
            string dataField = pDataField;

            try
            {
                //erstmal Entity XML anlegen
                //Lädt die Datei 
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\lucaborgmann\\RD_Table_Tool\\RD_Table_Tool\\EntityTemplate.xml");
                System.Diagnostics.Debug.WriteLine("CreateEntity: Lädt die Template-Datei");

                // Schreibt die Werte der Template Datei in die neue XML Datei 
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);

                //Name,DataSource und Table anpassen 

                XmlNodeList nodes = newDoc.SelectNodes("//DataSource | //Table");
                foreach (XmlNode node in nodes)
                {
                    node.InnerText = tableName;
                }

                newDoc.Save(outputPath);


                /*
                //Table XML bearbeiten
                XmlDocument doc = new XmlDocument();
                doc.Load(tablePath);  //angegebene Tabelle öffnen 

                //nur wenn da /index alleine steht muss dort <index> ergänzt werden 

                //neuen Eintrag unter Index einfügen 
                XmlElement newTableIndex = doc.CreateElement("AxTableField");
                XmlElement fieldName = doc.CreateElement("Name");
                fieldName.InnerText = pIndexName;

                newTableIndex.AppendChild(fieldName);

                doc.Save(tablePath);
                */

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}