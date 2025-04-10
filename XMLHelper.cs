using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
    }
}
