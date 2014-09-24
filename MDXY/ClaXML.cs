using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace MyPLC.MDXY
{
    class ClaXML
    {
        internal class ClaXmlm
        {
            private static string xmlfilePath { get; set; }

            public List<string[]> GetMList(string filePath)
            {
                xmlfilePath = filePath;

                List<string[]> returnList = new List<string[]>();

                var xmldoc = new XmlDocument();

                xmldoc.Load(xmlfilePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        string[] returnString = new string[2];

                        XmlElement xe = (XmlElement)nodeList[i];

                        returnString[0] = xe.LocalName;

                        returnString[1] = xe.GetAttribute("status");

                        returnList.Add(returnString);
                    }
                    return returnList;
                }
                return null;
            }

            public bool SaveMinfo(string cellname, string cellinfonew)
            {
                var xmldoc = new XmlDocument();

                xmldoc.Load(xmlfilePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    foreach (var nl in nodeList)
                    {
                        XmlElement xe = (XmlElement)nl;

                        if (xe.LocalName == cellname)
                        {
                            xe.SetAttribute("status", cellinfonew);

                            xmldoc.Save(xmlfilePath);

                            return true;
                        }
                    }
                }
                return false;
            }
        }

        internal class ClaXmld
        {
            private static string xmlfilePath { get; set; }

            public List<string[]> GetDList(string filePath)
            {
                xmlfilePath = filePath;

                List<string[]> returnList = new List<string[]>();

                var xmldoc = new XmlDocument();

                xmldoc.Load(xmlfilePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        string[] returnString = new string[2];

                        XmlElement xe = (XmlElement)nodeList[i];

                        returnString[0] = xe.LocalName;

                        returnString[1] = xe.GetAttribute("value");

                        returnList.Add(returnString);
                    }
                    return returnList;
                }
                return null;
            }

            public bool SaveDinfo(string cellname, string cellinfonew)
            {
                var xmldoc = new XmlDocument();

                xmldoc.Load(xmlfilePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    foreach (var nl in nodeList)
                    {
                        XmlElement xe = (XmlElement)nl;

                        if (xe.LocalName == cellname)
                        {
                            xe.SetAttribute("value", cellinfonew);

                            xmldoc.Save(xmlfilePath);

                            return true;
                        }
                    }
                }
                return false;
            }
        }

        internal class ClaXmlXY
        {
            private static string xmlfilePath { get; set; }

            public List<string[]> GetXList(string filePath)
            {
                xmlfilePath = filePath;

                List<string[]> returnList = new List<string[]>();

                var xmldoc = new XmlDocument();

                xmldoc.Load(filePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    foreach (var nl in nodeList)
                    {
                        XmlElement xe = (XmlElement)nl;

                        if (xe.LocalName == "input")
                        {
                            string strInput = xe.GetAttribute("num");

                            for (int i = 0; i < strInput.Length; i++)
                            {
                                string[] returnString = new string[2];

                                returnString[0] = "X" + i;

                                returnString[1] = strInput[i].ToString(CultureInfo.InvariantCulture);

                                returnList.Add(returnString);
                            }
                            return returnList;
                        }
                    }
                }
                return null;
            }

            public List<string[]> GetYList(string filePath)
            {
                xmlfilePath = filePath;

                List<string[]> returnList = new List<string[]>();

                var xmldoc = new XmlDocument();

                xmldoc.Load(filePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    foreach (var nl in nodeList)
                    {
                        XmlElement xe = (XmlElement)nl;

                        if (xe.LocalName == "output")
                        {
                            string strInput = xe.GetAttribute("num");

                            for (int i = 0; i < strInput.Length; i++)
                            {
                                string[] returnString = new string[2];

                                returnString[0] = "Y" + i;

                                returnString[1] = strInput[0].ToString(CultureInfo.InvariantCulture);

                                returnList.Add(returnString);
                            }
                            return returnList;
                        }
                    }
                }
                return null;
            }

            public bool SaveXinfo(string cellname, string cellinfonew)
            {
                var xmldoc = new XmlDocument();

                xmldoc.Load(xmlfilePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    foreach (var nl in nodeList)
                    {
                        XmlElement xe = (XmlElement)nl;

                        if (xe.LocalName == "input")
                        {
                            xe.SetAttribute("num", cellinfonew);

                            xmldoc.Save(xmlfilePath);

                            return true;
                        }
                    }
                }
                return false;
            }

            public bool SaveYinfo(string cellname, string cellinfonew)
            {
                var xmldoc = new XmlDocument();

                xmldoc.Load(xmlfilePath);

                var selectSingleNode = xmldoc.SelectSingleNode("Cell");

                if (selectSingleNode != null)
                {
                    var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                    foreach (var nl in nodeList)
                    {
                        XmlElement xe = (XmlElement)nl;

                        if (xe.LocalName == "output")
                        {
                            xe.SetAttribute("num", cellinfonew);

                            xmldoc.Save(xmlfilePath);

                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
