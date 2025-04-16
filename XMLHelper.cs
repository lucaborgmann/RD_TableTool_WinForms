using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace RD_Table_Tool
{
    internal class XMLHelper
    {

        public static XmlDocument LoadTemplate(string templatePath)
        {
            XmlDocument templateDoc = new XmlDocument();
            templateDoc.Load(templatePath);

            XmlDocument newDoc = new XmlDocument();
            newDoc.LoadXml(templateDoc.OuterXml);

            return newDoc;
        }
        public static void CreatePrivilegesFile(string pName, string pTemplatePath, string pTableLabel,string pOutputPath,string type)
        {
            string name = pName; 
            string label = pTableLabel;
            string outputPath = pOutputPath;
            XmlDocument newDoc = XMLHelper.LoadTemplate(pTemplatePath);

            newDoc.LoadXml(newDoc.OuterXml); //ergibt wenig sinn ist Doppelt 


            var nodeUpdates = new Dictionary<string, string>
            {
                { "Name", name },
                { "Label", label },
                { "ObjectName", name }
            };


            bool isFirstNameTag = true;

            foreach (var update in nodeUpdates)
            {
                XmlNodeList nodesTest = newDoc.GetElementsByTagName(update.Key);
                foreach (XmlNode node in nodesTest)
                {
                    //Für den Ausnahme beim ersten
                    if (update.Key == "Name" && isFirstNameTag)
                    {
                        // Ausnahme für das erste Tag "Name"
                        node.InnerText = $"{update.Value}{type}";
                        isFirstNameTag = false;
                        //continue; // Überspringt das erste "Name" Tag
                    }
                    else
                    {
                        node.InnerText = update.Value;
                    }
                }
            }
            newDoc.Save(pOutputPath);
        }

        public static void CreateDataEntityPrivileges(string pName, string pTemplatePath, string pOutputPath, string type)
        {
            XmlDocument newDoc = XMLHelper.LoadTemplate(pTemplatePath);

           XmlNodeList nameNodes = newDoc.GetElementsByTagName("Name");

            if (nameNodes.Count>0)
            {
                XmlNode firstNameNode = nameNodes[0];
                firstNameNode.InnerText = $"{pName}Entity{type}";
                System.Diagnostics.Debug.WriteLine("Erstes Element angepasst");

                // Letztes <Name> Tag bearbeiten
                XmlNode lastNameNode = nameNodes[nameNodes.Count - 1];
                //lastNameNode.InnerText = pName;
                lastNameNode.InnerText = $"{pName}Entity";
                System.Diagnostics.Debug.WriteLine("Letztes Element angepasst");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Kein Element name gefunden");
            }

            newDoc.Save(pOutputPath);

        }
    }
}
