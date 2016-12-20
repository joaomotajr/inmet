// ============================================
// 
// FlexImage.Core
// FlexXML.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System.Xml;

#endregion

namespace FlexImage.Core
{
    public static class FlexXML
    {
        public static XmlDocument ConvertStringToXml(string xmlStr)
        {
            var xmlDoc = new XmlDocument();
            if (xmlStr.ToUpper().Contains("?xml version") == false)
                xmlDoc.LoadXml(@"<?xml version=""1.0""?>" + xmlStr);
            else
                xmlDoc.LoadXml(xmlStr);

            return xmlDoc;
        }

        public static string ReadNode(XmlDocument xmlDoc, string nodeName)
        {
            string nodeValue = "";

            XmlNode xmlNode;
            xmlNode = xmlDoc.SelectSingleNode(nodeName);
            if (xmlNode != null)
                nodeValue = xmlNode.InnerText;

            return nodeValue;
        }

        public static XmlDocument UpdateNode(XmlDocument xmlDoc, string nodeName, string nodeValue)
        {
            XmlNode xmlUpdateNode;
            xmlUpdateNode = xmlDoc.SelectSingleNode(nodeName);
            if (xmlUpdateNode != null)
            {
                //Nó já existe, deve apenas atualizar valor
                xmlUpdateNode.InnerText = nodeValue;
            }
            else
            {
                XmlElement elmRoot = xmlDoc.DocumentElement;
                XmlElement elmNew = xmlDoc.CreateElement(nodeName);
                XmlText txt = xmlDoc.CreateTextNode(nodeValue);
                elmRoot.AppendChild(elmNew);
                elmRoot.LastChild.AppendChild(txt);
            }

            return xmlDoc;
        }
    }
}