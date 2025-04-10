using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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

            newDoc.LoadXml(newDoc.OuterXml);


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
    }
}
