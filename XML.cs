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
        public static string scriptDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); //Speichert den Pfad zum verzeichnis
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
                templateDoc.Load($"{scriptDir}\\MenuItemTemplate.xml");
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
                //templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\RD_TableTool_WinForms\\EdtTemplate.xml");
                templateDoc.Load($"{scriptDir}\\EdtTemplate.xml");
                System.Diagnostics.Debug.WriteLine("CrerateMenuItem: Lädt die Template-Datei: ");
                System.Diagnostics.Debug.WriteLine($"{scriptDir}\\EdtTemplate.xml");
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
                    System.Diagnostics.Debug.WriteLine("CrerateMenuItem: Ersetzt das Label");
                }

                System.Diagnostics.Debug.WriteLine("Bis hier wird es AusgeführtS");
                System.Diagnostics.Debug.WriteLine($"BaseEDT Wert: {baseEDT}");

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
                    // typeAttr.Value = "AxEdtString";
                    typeAttr.Value = $"AxEdt{baseEDT}";
                    Console.WriteLine("i:type erfolgreich gesetzt.");
                }
                




                // Neue XML-Datei speichern
                newDoc.Save($"{outputPath}\\{name}.xml");
                System.Diagnostics.Debug.WriteLine("CrerateMenuItem: XML-Datei aktualisiert");
                //updateBasedEDT(baseEDT,outputPath);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Laden des XML-Dokuments: {ex.Message}");
            }

        }

        /*
        public static void updateBasedEDT(string pBaseEDT,string pPath)
        {
            string baseEDT = pBaseEDT;
            string Path = pPath;

            XmlDocument doc = new XmlDocument();
            //templateDoc.Load("C:\\Users\\LucaBorgmann\\source\\repos\\RD_TableTool_WinForms\\EdtTemplate.xml");
            doc.Load(Path);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("i", "http://www.w3.org/2001/XMLSchema-instance");

            // AxEdt-Node holen
            XmlNode axEdtNode = doc.SelectSingleNode("//AxEdt", nsmgr);
            if (axEdtNode != null)
            {
                // Attribut 'i:type' setzen oder aktualisieren
                XmlAttribute typeAttr = axEdtNode.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
                if (typeAttr == null)
                {
                    typeAttr = doc.CreateAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance");
                    axEdtNode.Attributes.Append(typeAttr);
                }

                // Wert zuweisen
                // typeAttr.Value = "AxEdtString";
                typeAttr.Value = $"AxEdt{baseEDT}";
                Console.WriteLine("i:type erfolgreich gesetzt.");
            }


            doc.Save(Path);


        }
        */
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
                System.Diagnostics.Debug.WriteLine("CreateTable: Lädt die Template-Datei");

                //erstellt das neue Dokuement
                XmlDocument newDoc = new XmlDocument();
                newDoc.LoadXml(templateDoc.OuterXml); //Speichert den Inhalt des Templates in dem neuen Dokument
                
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
                        System.Diagnostics.Debug.WriteLine("CreateTable: Ersetzt das Label");
                    }
                }

                //falls die Variable firstNameNode umbenannt wurde wird der Inhalt des XML Elements Überschrieben
                if (firstNameNode != null)
                {
                    firstNameNode.InnerText = name;
                    System.Diagnostics.Debug.WriteLine("CreateTable: Ersetzt den ersten Namen");
                }

                XmlNodeList fieldNodes = newDoc.SelectNodes("//Fields"); //sucht den XML Knoten Fiels 
                XmlNode fieldNode = fieldNodes?.Count > 0 ? fieldNodes[fieldNodes.Count - 1] : null; // Wenn mindestens ein Element enthalten ist wird das letzte Berücksichtigt sonst auf Null gesetzt 

                if (fieldNode != null) // Wenn Felder angegeben wurden 
                {
                    System.Diagnostics.Debug.WriteLine("CreateTable: fieldNode ist nicht leer");

                    foreach (Dictionary<string, string> fieldDict in fieldList) 
                    {
                        if (fieldDict != null)
                        {
                            //Extrahieren von Feldwerten
                            string fieldName = fieldDict["Name"];
                            string fieldLabel = fieldDict["Label"];
                            string fieldBaseEDT = fieldDict["BaseEDT"];
                            string fieldCreatEDT = fieldDict["CreateEDT"];


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
                                edt.InnerText = fieldName; // Oder fieldBaseEDT
                                axTableField.AppendChild(edt);

                                XmlElement ignore = newDoc.CreateElement("IgnoreEDTRelation");
                                ignore.InnerText = "Yes";
                                axTableField.AppendChild(ignore);

                                // ⬇️ Wichtig: Füge in das <Fields>-Tag ein, nicht an ein neues Root-Element
                                fieldNode.AppendChild(axTableField);
                            }
                            else
                            {
                                //neuer AxtableField Knoten 
                                XmlElement newAxTableField = newDoc.CreateElement("AxTableField");
                                newAxTableField.SetAttribute("xmlns", ""); //weißt dem Knoten die Nötigen Attribute zu 

                                XmlAttribute typeAttribute = newDoc.CreateAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance"); //fügt dem Knoten ein neues Typattribut hinzu
                                typeAttribute.Value = "AxTableField" + fieldBaseEDT;
                                //Fügt des neuen Attributs
                                newAxTableField.Attributes.Append(typeAttribute);

                                //Erstellen des Name-Elements
                                XmlElement nameElement = newDoc.CreateElement("Name");
                                //neuen Wert zuweisen
                                nameElement.InnerText = fieldName;
                                //hinzufügen zum neuen ELements
                                newAxTableField.AppendChild(nameElement);

                                // Hier wird das neue Feld direkt in fieldNode eingefügt, ohne zusätzliches <Fields>
                                fieldNode.AppendChild(newAxTableField);
                            }
                        }
                    }


                    //es muss noch in der Zeile  public class TestForm extends FormRun "TestForm" durch den Namen ergänzt werde

                    //Definieren des Suchenden Strings 
                    string searchString = "public class TestTable extends common";
                    // Knoten finden, die den Suchstring enthalten
                    XmlNodeList searchStringNodes = newDoc.SelectNodes("//*[contains(text(), '" + searchString + "')]");

                    // Teil des Strings ersetzen
                    foreach (XmlNode node in searchStringNodes)
                    {
                        //Umm sicher zu gehen das <![CDATA[ nicht verloren geht
                        if (node.NodeType == XmlNodeType.CDATA)
                        {
                            node.InnerText = node.InnerText.Replace("TestTable",$"{name}");
                        }
                        else
                        {
                            string updatedText = node.InnerText.Replace("TestTable", $"{name}"); // Ersetzt den TestTable durch den Wert der Variable
                            XmlCDataSection cdata = newDoc.CreateCDataSection(updatedText); //CDATA Abschnitt wird erstelllt 
                            XmlElement declarationElement = newDoc.CreateElement("Declaration"); // neues XML-Element Declaratiom wird 
                            declarationElement.AppendChild(cdata); // fügt CDATA als Kindknoten hinzu 
                            node.ParentNode.ReplaceChild(declarationElement, node); //Ersetzt den Ursprünglichen Knoten 

                        }
                    }  

                    /*
                    XmlNode controlsNode = newDoc.SelectSingleNode("//Controls");
                    if (controlsNode != null) 
                    {
                        XmlElement newElement = newDoc.CreateElement("NewElement");
                        newElement.InnerText = "Test";
                        controlsNode.AppendChild(newElement);   
                    }
                    */



                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Das Field-Tag konnte nicht gefunden werden");
                }

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

        public static void CreateForm(string pName, string pOutputPath, string pFormpatterm, List<Dictionary<string, string>> fieldList)
        {
            string name = pName;
            string outputPath = pOutputPath;

            try
            {
                System.Diagnostics.Debug.WriteLine("diese Create Form wird ausgeführt");
                //Lädt die Datei 
                XmlDocument templateDoc = new XmlDocument();
                templateDoc.Load($"{scriptDir}\\FormTemplate2.xml");

                System.Diagnostics.Debug.WriteLine($"der Pfad zum Template ist: {scriptDir}\\TableTemplate2.xml");
                System.Diagnostics.Debug.WriteLine("CreateForm: Lädt die Template-Datei");

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
                    System.Diagnostics.Debug.WriteLine("CreateForm: Ersetzt den ersten Namen");


                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("CreateForm: <Name> -Tag nicht gefunden!");
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
                            System.Diagnostics.Debug.WriteLine($"Dictonary ausgeben Create Form {kvp.Key},Value: {kvp.Value}");
                            if (kvp.Key == "Name")
                            {
                                // Neues AxFormDataSourceField-Element erstellen
                                XmlElement newFieldElement = newDoc.CreateElement("AxFormDataSourceField");

                                // DataField-Element erstellen und hinzufügen
                                XmlElement dataFieldElement = newDoc.CreateElement("DataField");
                                //dataFieldElement.InnerText = "NeuesDataField"; // Hier name des Feldes angeben 

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

                        if (counter == 4) // Beim 4. <Controls>-Element einfügen
                        {
                            string tmpFieldName = "";
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

                                System.Diagnostics.Debug.WriteLine($"Processing Field: {tmpFieldName}, Type: {tmpBaseEDT}");

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
                    Console.WriteLine("Fields-Element nicht gefunden.");
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

                templateDoc.Load($"{scriptDir}\\EntityTemplate.xml");
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

        public static void CreatePrivileges()
        {

        }

    }
}