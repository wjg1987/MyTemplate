using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace My.Template.Common
{
    public class EmailXML
    {
        public string GetEmailValue(string xmlname)
        {
            string strPath = HttpContext.Current.Server.MapPath("~/Areas/Config/Web.config");
            return this.GetXmlNode(strPath, "configuration/configemail/" + xmlname);
        }

        public string ChangeEmail(string xmlname, string value)
        {
            string strPath = HttpContext.Current.Server.MapPath("~/Areas/Config/Web.config");
            string strNodeName = "configuration/configemail/" + xmlname;

            return this.UpdateXmlNode(strPath, strNodeName, value);
        }

        public string GetXmlNode(string strXmlPath, string strNode)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(strXmlPath);
            return xmldoc.SelectSingleNode(strNode).InnerText;
        }

        public string UpdateXmlNode(string path, string nodeName, string nodeValue)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(path);
            xmldoc.SelectSingleNode(nodeName).InnerText = nodeValue;
            xmldoc.Save(path);
            return nodeValue;
        }

    }
}
