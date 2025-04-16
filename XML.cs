using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;

namespace RD_Table_Tool
{
    class XML
    {
        public static string scriptDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); //Speichert den Pfad zum verzeichnis
        //Konstruktor
        public XML() { }
        public static void CreateMenuItem(string pName, string pLabel, string pOutputPath)
        { 

            try
            {
                // Template laden
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load($"{scriptDir}\\MenuItemTemplate.xml");
                Debug.WriteLine("Lädt die Template-Datei");

                // Neue XML-Datei erstellen und den Inhalt der Template-Datei übernehmen
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);
                
                string name = pName;
                string label = pLabel;
                string outputPath = pOutputPath;

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
                Debug.WriteLine("XML-Datei aktualisiert");

            }
            catch (XmlException ex)
            {
                Debug.WriteLine($"Fehler beim Laden des XML-Dokuments: {ex.Message}");
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void CreateEDT(string pName, string pLabel, string pBaseEDT, string pOutputPath)
        {
            string name = pName;
            string label = pLabel;
            string baseEDT = pBaseEDT;
            string outputPath = pOutputPath;

            
            Debug.WriteLine("CrerateMenuItem: Methode wird aufgerufen");


            try
            {
                XmlDocument templateDoc = new XmlDocument();
                //templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\RD_TableTool_WinForms\\EdtTemplate.xml");
                templateDoc.Load($"{scriptDir}\\EdtTemplate.xml");
                Debug.WriteLine("CrerateMenuItem: Lädt die Template-Datei: ");
                Debug.WriteLine($"{scriptDir}\\EdtTemplate.xml");

                // Neue XML-Datei erstellen und den Inhalt der Template-Datei übernehmen
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);

                // Tags im Dokument updaten
                XmlNodeList nodes = newDoc.GetElementsByTagName("Name");
                foreach (XmlNode node in nodes)
                {
                    node.InnerText = name;
                    Debug.WriteLine("CrerateMenuItem: Ersetzt den Namen");
                }

                nodes = newDoc.GetElementsByTagName("Label");
                foreach (XmlNode node in nodes)
                {
                    node.InnerText = label;
                    Debug.WriteLine("CrerateMenuItem: Ersetzt das Label");
                }

                Debug.WriteLine("Bis hier wird es AusgeführtS");
                Debug.WriteLine($"BaseEDT Wert: {baseEDT}");

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(newDoc.NameTable);
                nsmgr.AddNamespace("i", "http://www.w3.org/2001/XMLSchema-instance");

                // AxEdt-Node holen
                XmlNode axEdtNode = newDoc.SelectSingleNode("//AxEdt", nsmgr);
                if (axEdtNode != null)
                {
                    // Attribut 'i:type' setzen oder aktualisieren
                    XmlAttribute typeAttr = axEdtNode.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
                    if (typeAttr == null)
                    {
                        typeAttr = newDoc.CreateAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance");
                        axEdtNode.Attributes.Append(typeAttr);
                    }

                    // Wert zuweisen
                    typeAttr.Value = $"AxEdt{baseEDT}";
                    
                }
                
                // Neue XML-Datei speichern
                newDoc.Save($"{outputPath}\\{name}.xml");
                Debug.WriteLine("CrerateMenuItem: XML-Datei aktualisiert");
               

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Laden des XML-Dokuments: {ex.Message}");
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //Lädt das Template
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load($"{scriptDir}\\TableTemplate.xml");
                Debug.WriteLine("CreateTable: Lädt die Template-Datei");

                //erstellt das neue Dokument
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml); // Template in neues Dokument kopieren

                XmlNodeList nodes = newDoc.SelectNodes("//Name | //Label"); //Sucht alle XML-Knoten dessen taganme Name oder Label ist
                XmlNode firstNameNode = null;

                foreach (XmlNode node in nodes)
                {
                    if (node.Name == "Name")
                    {
                        firstNameNode = node;
                        break;
                    }
                }

                foreach (XmlNode node in nodes)
                {
                    if (node.Name == "Label")
                    {
                        node.InnerText = label;
                        Debug.WriteLine("CreateTable: Ersetzt das Label");
                    }
                }
                //falls die Variable firstNameNode umbenannt wurde wird der Inhalt des XML Elements Überschrieben
                if (firstNameNode != null)
                {
                    firstNameNode.InnerText = name;
                    Debug.WriteLine("CreateTable: Ersetzt den ersten Namen");
                }

                XmlNodeList fieldNodes = newDoc.SelectNodes("//Fields");  //sucht den XML Knoten Fiels 
                bool hasAlternateKey = fieldList.Any(f => f.ContainsKey("AlternateKey") && f["AlternateKey"].Equals("Yes", StringComparison.OrdinalIgnoreCase));
                XmlNode fieldNode = fieldNodes?.Count > 0 ? fieldNodes[fieldNodes.Count - 1] : null;// Wenn mindestens ein Element enthalten ist wird das letzte Berücksichtigt sonst auf Null gesetzt 

                if (fieldNode != null) // Wenn Felder angegeben wurden 
                {
                   Debug.WriteLine("CreateTable: fieldNode ist nicht leer");

                    foreach (Dictionary<string, string> fieldDict in fieldList)
                    {
                        if (fieldDict != null)
                        {
                            //Extrahieren von Feldwerten
                            string fieldName = fieldDict["Name"];
                            string fieldLabel = fieldDict["Label"];
                            string fieldBaseEDT = fieldDict["BaseEDT"];
                            string fieldCreatEDT = fieldDict["CreateEDT"];
                            string fiedlAlternateKey = fieldDict["AlternateKey"];

                            if (fieldCreatEDT.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                            {
                                XmlElement axTableField = newDoc.CreateElement("AxTableField");
                                axTableField.SetAttribute("xmlns", "");

                                XmlAttribute typeAttr = newDoc.CreateAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance");
                                typeAttr.Value = $"AxTableField{fieldBaseEDT}";
                                axTableField.Attributes.Append(typeAttr);

                                XmlElement fieldNameElement = newDoc.CreateElement("Name");
                                fieldNameElement.InnerText = fieldName;
                                axTableField.AppendChild(fieldNameElement);

                                XmlElement edt = newDoc.CreateElement("ExtendedDataType");
                                edt.InnerText = fieldName;
                                axTableField.AppendChild(edt);

                                XmlElement ignore = newDoc.CreateElement("IgnoreEDTRelation");
                                ignore.InnerText = "Yes";
                                axTableField.AppendChild(ignore);
                                //Wichtig: Füge in das <Fields>-Tag ein, nicht an ein neues Root-Element
                                fieldNode.AppendChild(axTableField);
                            }
                            else
                            {
                                XmlElement newAxTableField = newDoc.CreateElement("AxTableField");
                                newAxTableField.SetAttribute("xmlns", "");//weißt dem Knoten die Nötigen Attribute zu 

                                XmlAttribute typeAttribute = newDoc.CreateAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance");
                                typeAttribute.Value = "AxTableField" + fieldBaseEDT;
                                newAxTableField.Attributes.Append(typeAttribute);

                                XmlElement nameElement = newDoc.CreateElement("Name");
                                nameElement.InnerText = fieldName;
                                newAxTableField.AppendChild(nameElement);

                                fieldNode.AppendChild(newAxTableField);
                            }

                            if (fiedlAlternateKey.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                            {
                                Debug.WriteLine("AlternateKey ist als ja ausgewählt");
                            }
                        }
                    }

                    // Wenn ein Alternate Key angegeben ist wird ein Index angelegt 
                    if (hasAlternateKey)
                    {
                        Debug.WriteLine("Erstelle AxTableIndex für AlternateKey");

                        XmlNodeList indexesNodes = newDoc.SelectNodes("//Indexes"); //sucht nach dem Tag Indexes
                        if (indexesNodes != null && indexesNodes.Count > 0) //wenn index vorhanden und nciht gleich null ist
                        {
                            XmlNode indexesNode = indexesNodes[0]; // den ersten Treffer manipulieren

                            XmlElement axTableIndex = newDoc.CreateElement("AxTableIndex"); //neuen XML Tag AxtableIndex erstellen 

                            XmlElement indexName = newDoc.CreateElement("Name"); // neues XML Tag Name erstellen 
                            indexName.InnerText = "AlternateKey"; // dem neuen Tag den Inhalt zuweisen 
                            axTableIndex.AppendChild(indexName); // den neuen Tag Name als Kindelement unter AxTable index einfügen 

                            XmlElement alternateKey = newDoc.CreateElement("AlternateKey"); 
                            alternateKey.InnerText = "Yes";
                            axTableIndex.AppendChild(alternateKey);

                            XmlElement fieldsElement = newDoc.CreateElement("Fields");

                            foreach (var fieldDict in fieldList) // Werte des DataGrid durch gehen und Felder hinzufügen 
                            {
                                if (fieldDict.ContainsKey("AlternateKey") && fieldDict["AlternateKey"].Equals("Yes", StringComparison.OrdinalIgnoreCase)) // wenn im der Spalte Alternate Key ja íst (unabhängig von der Groß und klein schreibung)
                                {
                                    string altKeyFieldName = fieldDict["Name"];
                                    XmlElement axTableIndexTag = newDoc.CreateElement("AxTableIndexField");
                                    fieldsElement.AppendChild(axTableIndexTag);
                                    XmlElement dataFieldTag = newDoc.CreateElement("DataField");
                                    dataFieldTag.InnerText = altKeyFieldName;
                                    axTableIndexTag.AppendChild(dataFieldTag);
                                }
                            }

                            axTableIndex.AppendChild(fieldsElement);
                            indexesNode.AppendChild(axTableIndex);
                        }
                        else
                        {
                            Debug.WriteLine("Indexes-Knoten wurde nicht gefunden!");
                        }
                    }

                    // Ersetze Klassennamen in CDATA
                    string searchString = "public class TestTable extends common";
                    XmlNodeList searchStringNodes = newDoc.SelectNodes("//*[contains(text(), '" + searchString + "')]");

                    foreach (XmlNode node in searchStringNodes)
                    {
                        if (node.NodeType == XmlNodeType.CDATA)
                        {
                            node.InnerText = node.InnerText.Replace("TestTable", $"{name}");
                        }
                        else
                        {
                            string updatedText = node.InnerText.Replace("TestTable", $"{name}");
                            XmlCDataSection cdata = newDoc.CreateCDataSection(updatedText);
                            XmlElement declarationElement = newDoc.CreateElement("Declaration");
                            declarationElement.AppendChild(cdata);
                            node.ParentNode.ReplaceChild(declarationElement, node);
                        }
                    }
                }
                else
                {
                   Debug.WriteLine("Das Field-Tag konnte nicht gefunden werden");
                }

                //nur zum Debuggen ? 
                string[] paths = { @$"{outputPath}", $"{name}", ".xml" };
                string fullPath = Path.Combine(paths); 
                Debug.WriteLine($"Ausgabe fullpath: {fullPath}");

                newDoc.Save($"{outputPath}\\{name}.xml");
                Debug.WriteLine("CreateTable: XML-Datei aktualisiert");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Laden des XML-Dokuments: {ex.Message}");
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateForm(string pName, string pOutputPath, string pFormpatterm, List<Dictionary<string, string>> fieldList)
        {
            string name = pName;
            string outputPath = pOutputPath;

            try
            {
                Debug.WriteLine("diese Create Form wird ausgeführt");
                //Lädt die Datei 
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load($"{scriptDir}\\FormTemplate2.xml");

                Debug.WriteLine($"der Pfad zum Template ist: {scriptDir}\\TableTemplate2.xml");
                Debug.WriteLine("CreateForm: Lädt die Template-Datei");

                // Schreibt die Werte der Template Datei in die neue XML Datei 
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml);
                //bis hier evtl als Methode in eine eigene Helperklasse Outsourcen


                //Hier muss das template überarbeitet werden
                XmlNamespaceManager nsManager = new XmlNamespaceManager(newDoc.NameTable);
                nsManager.AddNamespace("ax", "Microsoft.Dynamics.AX.Metadata.V6"); //fügt den NameSpace hinzu

                //ersten Knoten unter <Name> unter <AxForm> finden 
                XmlNode nameNode = newDoc.SelectSingleNode("/ax:AxForm/ax:Name", nsManager);

                if (nameNode != null)
                {
                    nameNode.InnerText = name;
                    Debug.WriteLine("CreateForm: Ersetzt den ersten Namen");


                }
                else
                {
                    Debug.WriteLine("CreateForm: <Name> -Tag nicht gefunden!");
                }

                //es muss noch in der Zeile  public class TestForm extends FormRun "TestForm" durch den Namen ergänzt werde
                string searchString = "ToolTest2603"; //Wird Überall im Dokuemnt ersezt 
                XmlNodeList nodes = newDoc.SelectNodes("//*[contains(text(), '" + searchString + "')]");

                foreach (XmlNode node in nodes)
                {
                    node.InnerText = node.InnerText.Replace(searchString, name);
                }

                // CDATA wieder einfügen da es scheinbar verschluckt wird:
                XmlNode methodNode = newDoc.SelectSingleNode("//*[local-name()='Method']"); //Namespace unabhängige Suche nach dem Element Method
                if (methodNode != null)
                {
                    XmlNode sourceNode = methodNode.SelectSingleNode("*[local-name()='Source']"); //Namespace unabhängige Suche nach dem Kindknoten Source gesucht
                    if (sourceNode != null)
                    {
                        string originalText = sourceNode.InnerText; //aktueller Inhalt des Tags 

                        sourceNode.RemoveAll();//Löscht den aktuellen Inhalt 
                        XmlCDataSection cdata = newDoc.CreateCDataSection(originalText.Trim()); // neuer Knoten Character Data 
                        sourceNode.AppendChild(cdata); //fügt den Knoten hinzu
                    }
                }

                XmlNode fieldsNode = newDoc.SelectSingleNode("//Fields");

                //Falls Felder vorhanden sind ein neues Element für jedes Feld einfügen 
                if (fieldsNode != null)
                {
                    //Durch die Dictonarys durchgehen für alle Namen die 
                    foreach (var dict in fieldList)
                    {
                        foreach (var kvp in dict)
                        {
                            Debug.WriteLine($"Dictonary ausgeben Create Form {kvp.Key},Value: {kvp.Value}");
                            if (kvp.Key == "Name")
                            {
                                // Neues AxFormDataSourceField-Element erstellen
                                XmlElement newFieldElement = newDoc.CreateElement("AxFormDataSourceField");

                                // DataField-Element erstellen und hinzufügen
                                XmlElement dataFieldElement = newDoc.CreateElement("DataField");
                                dataFieldElement.InnerText = kvp.Value;

                                // DataField-Element zum AxFormDataSourceField-Element hinzufügen
                                newFieldElement.AppendChild(dataFieldElement);

                                // Neues AxFormDataSourceField-Element zum Fields-Element hinzufügen
                                fieldsNode.AppendChild(newFieldElement);
                            }
                        }
                    }

                    XmlNodeList controlsNodes = newDoc.SelectNodes("//Controls"); // Alle <Controls>-Elemente finden
                    int counter = 0;

                    foreach (XmlNode controlsNode in controlsNodes)
                    {
                        counter++;

                        if (counter == 4) // Beim 4. <Controls>-Element einfügen //ToDo: Prüfen ob es mit einem namen geht, wenn nicht genauer kommentieren 
                        {
                            string tmpFieldName = ""; //ToDo: Prüfen ob notwendig
                            string tmpBaseEDT = "";

                            string xsiNameSpace = "http://www.w3.org/2001/XMLSchema-instance";

                            // Stelle sicher, dass das Root-Element den Namespace kennt
                            XmlElement root = newDoc.DocumentElement;
                            if (root != null && root.GetAttribute("xmlns:i") == "")
                            {
                                root.SetAttribute("xmlns:i", xsiNameSpace);
                            }

                            foreach (var dict in fieldList)
                            {
                                tmpFieldName = dict.ContainsKey("Name") ? dict["Name"] : "";
                                tmpBaseEDT = dict.ContainsKey("BaseEDT") ? dict["BaseEDT"] : "";

                                Debug.WriteLine($"Processing Field: {tmpFieldName}, Type: {tmpBaseEDT}");

                                // Überprüfen, ob ein <AxFormControl> mit dem gleichen DataField-Wert existiert
                                bool exists = controlsNode.SelectNodes("AxFormControl").Cast<XmlNode>()
                                                  .Any(control => control.SelectSingleNode("DataField")?.InnerText == tmpFieldName);

                                if (!exists)
                                {
                                    XmlElement newElement = newDoc.CreateElement("AxFormControl");

                                    // Hier den xmlns-Attribut hinzufügen
                                    newElement.SetAttribute("xmlns", "");

                                    // Setze i:type mit Namespace
                                    if (tmpBaseEDT == "Int")
                                    {
                                        newElement.SetAttribute("type", xsiNameSpace, "AxFormIntegerControl");
                                    }
                                    else
                                    {
                                        newElement.SetAttribute("type", xsiNameSpace, $"AxForm{tmpBaseEDT}Control");
                                    }

                                    XmlElement nameElement = newDoc.CreateElement("Name");
                                    nameElement.InnerText = $"{name}_{tmpFieldName}";
                                    newElement.AppendChild(nameElement);

                                    XmlElement typeElement = newDoc.CreateElement("Type");
                                    if (tmpBaseEDT.Equals("Int", StringComparison.OrdinalIgnoreCase))
                                    {
                                        typeElement.InnerText = "Integer";
                                    }
                                    else
                                    {
                                        typeElement.InnerText = tmpBaseEDT;
                                    }
                                    newElement.AppendChild(typeElement);

                                    XmlElement formControlExtensionElement = newDoc.CreateElement("FormControlExtension");
                                    formControlExtensionElement.SetAttribute("nil", xsiNameSpace, "true");
                                    newElement.AppendChild(formControlExtensionElement);

                                    XmlElement dataFieldElement = newDoc.CreateElement("DataField");
                                    dataFieldElement.InnerText = tmpFieldName;
                                    newElement.AppendChild(dataFieldElement);

                                    XmlElement dataSourceElement = newDoc.CreateElement("DataSource");
                                    dataSourceElement.InnerText = name;
                                    newElement.AppendChild(dataSourceElement);

                                    controlsNode.AppendChild(newElement);
                                }
                            }
                            break;
                        }
                    }

                }
                else
                {
                    Debug.WriteLine("Fields-Element nicht gefunden.");
                }

                //Speichert das Dokument muss am Ende von allem Passieren !!
                newDoc.Save($"{outputPath}\\{name}.xml");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Das Field-Tag konnte nicht gefunden werden");
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateDataEntity(string pTabellenName,string pOutputpath, string pLabel, List<Dictionary<string, string>> fieldList)
        {
            string name = pTabellenName;
            string outputPath = pOutputpath;
            string label = pLabel;

            try
            {
                XmlDocument newDoc = XMLHelper.LoadTemplate($"{scriptDir}\\DataEntityTemplate.xml");
                newDoc.LoadXml(newDoc.OuterXml);


                // Alle <Name> Tags auswählen
                XmlNodeList nameNodes = newDoc.GetElementsByTagName("Name");
                Debug.WriteLine($"Name node größe: {nameNodes.Count}"); 

                if (nameNodes.Count > 0)
                {
                    // Erstes <Name> Tag bearbeiten
                    XmlNode firstNameNode = nameNodes[0];
                    firstNameNode.InnerText = $"{name}Entity";
                    Debug.WriteLine("Erstes Element angepasst");

                    // Letztes <Name> Tag bearbeiten
                    XmlNode lastNameNode = nameNodes[nameNodes.Count - 1];
                    lastNameNode.InnerText = name;
                    Debug.WriteLine("Letztes Element angepasst");
                }
                else
                {
                    Debug.WriteLine("Kein Element name gefunden");
                }

                //Namen der Entität ersetzen 
                XmlNode declarationNode = newDoc.SelectSingleNode("//SourceCode/Declaration"); //Pfad zu der Declaration 

                //Wenn Deklaration gefunden werden konnte und das erste Kindelement eine XmlCDataSection ist
                if (declarationNode != null && declarationNode.FirstChild is XmlCDataSection cdataSection)
                {
                    string originalCode = cdataSection.Value; //speichert den aktuellen Inhalt der Section 

                    // Ersetze den Klassennamen
                    string modifiedCode = originalCode.Replace("IndexTest4Entity", $"{name}Entity");

                    // Aktualisiert CDATA Inhalt
                    cdataSection.Value = modifiedCode;
                 
                    Debug.WriteLine("Konnte erstezt werden"); 
                }
                else
                {
                    Debug.WriteLine("Konnte nicht  erstezt werden");
                }


                //Überarbeiten von Table, PublicCollectionName,PublicEntityName und Label
                var nodeUdates = new Dictionary<string, string>
                {
                    { "PublicCollectionName", name },
                    { "PublicEntityName", name },
                    { "Table", name },
                    { "Label",label},
                    { "DataManagementStagingTable",name}
                };

                foreach (var update in nodeUdates)
                {
                    XmlNodeList nodes = newDoc.GetElementsByTagName(update.Key);
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Name == "DataManagementStagingTable")
                        {
                            node.InnerText = $"{update.Value}Staging";
                        }
                        else
                        {
                            node.InnerText = update.Value;
                        }
                    }
                }
                //hinzufügen der Felder

                XmlNodeList fieldNodes = newDoc.SelectNodes("//Fields");

                if (fieldNodes.Count >= 6)
                {
                    // Auf das sechste <Fields>-Tag zugreifen
                    XmlNode sixthFieldNode = fieldNodes[5];
                    //sixthFieldNode.InnerText = "Ist das die Richtige stelle ? ";

                    foreach (Dictionary<string,string> fieldDict in fieldList)
                    {
                        if (fieldDict != null)
                        {
                            //Extrahieren von Feldwerten
                            string fieldName = fieldDict["Name"];
                           
                            XmlElement axDataEntityViewField = newDoc.CreateElement("AxDataEntityViewField");
                            axDataEntityViewField.SetAttribute("xmlns", "");

                            XmlAttribute typeAttr = newDoc.CreateAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance");
                            typeAttr.Value = "AxDataEntityViewMappedField";
                            axDataEntityViewField.Attributes.Append(typeAttr);

                            XmlElement nameElement = newDoc.CreateElement("Name");
                            nameElement.InnerText = $"{fieldName}";
                            axDataEntityViewField.AppendChild(nameElement);

                            XmlElement dataFieldElement = newDoc.CreateElement("DataField");
                            dataFieldElement.InnerText = $"{fieldName}";
                            axDataEntityViewField.AppendChild(dataFieldElement);

                            XmlElement dataSourceElement = newDoc.CreateElement("DataSource");
                            dataSourceElement.InnerText = $"{name}";
                            axDataEntityViewField.AppendChild(dataSourceElement);

                            // Datenstruktur in das sechste <Fields>-Tag einfügen
                            sixthFieldNode.AppendChild(axDataEntityViewField);

                        }
                    }

                    //hinzufügen der Felder unter dem Pfad Keys/AxDataEntityViewKey/AxDataEntityViewKeyField
                    XmlNode sevenFieldNode = fieldNodes[6];
                    foreach (Dictionary<string, string> fieldDict in fieldList)
                    {
                        if (fieldDict != null)
                        {
                            //Extrahieren von Feldwerten
                            string fieldName = fieldDict["Name"];

                            XmlElement axDataEntityViewKeyField = newDoc.CreateElement("AxDataEntityViewKeyField");

                            XmlElement dataField = newDoc.CreateElement("DataField");
                            dataField.InnerText = fieldName;
                            axDataEntityViewKeyField.AppendChild (dataField);

                            sevenFieldNode.AppendChild(axDataEntityViewKeyField);
                        }
                    }

                    Debug.WriteLine("Sechstes <Fields>-Tag gefunden und bearbeitet");
                }


                //Speichert das Dokument muss am Ende stehen !!!!
                newDoc.Save($"{outputPath}\\{name}Entity.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreatePrivileges(string pOutputPath,string pTableName,string pTableLabel)
        {
            try
            {
                XMLHelper.CreatePrivilegesFile(pTableName, $"{scriptDir}\\PrivilegesMaintainTemplate.xml", pTableLabel, $"{pOutputPath}\\{pTableName}Maintain.xml", "Maintain");
                XMLHelper.CreatePrivilegesFile(pTableName, $"{scriptDir}\\PrivilegesViewTemplate.xml", pTableLabel, $"{pOutputPath}\\{pTableName}View.xml", "View");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public static void CreateDataEntityPrivileges(string pOutputPath, string pTableName)
        {
            XMLHelper.CreateDataEntityPrivileges(pTableName, $"{scriptDir}\\DataEntityPrivilegesMaintain.xml", $"{pOutputPath}\\{pTableName}DataEntityMaintain.xml", "Maintain");
            XMLHelper.CreateDataEntityPrivileges(pTableName, $"{scriptDir}\\DataEntityPrivilegesView.xml", $"{pOutputPath}\\{pTableName}DataEntityView.xml", "View");
        }

    }
}