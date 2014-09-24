using System;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MyPLC
{
    class ClaXmlXY
    {
        XmlElement _xmlelem;

        private static bool _busyflag = false;

        /// <summary>
        /// 新建data.xml，全部赋值为0；
        /// </summary>
        public void NewXml()
        {
            var xmldoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            var xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmldoc.AppendChild(xmldecl);

            //加入一个根元素
            _xmlelem = xmldoc.CreateElement("", "Cell", "");
            xmldoc.AppendChild(_xmlelem);

            //加入另外一个元素
            var root = xmldoc.SelectSingleNode("Cell");//查找<Employees> 

            var xe1 = xmldoc.CreateElement("input");//创建一个<Node>节点 
            xe1.SetAttribute("num", "00000000");//设置该节点genre属性 
            var xe2 = xmldoc.CreateElement("output");//创建一个<Node>节点 
            xe2.SetAttribute("num", "00000000");//设置该节点genre属性 

            if (root != null)
            {
                root.AppendChild(xe1);
                root.AppendChild(xe2);
            }


            //保存创建好的XML文档
            xmldoc.Save(Application.StartupPath + "\\dataXY.xml");
        }

        /// <summary>
        /// 修改data.xml，给其中放入新的数据；
        /// </summary>
        /// <param name="celltype">“input”or“output”</param>
        /// <param name="info">新数据</param>
        public bool Modify(string celltype, string info)
        {
            while (_busyflag)
            {
            }
            _busyflag = true;
            var re = new Regex(@"(^[0|1]{8}$)");
            if (!re.IsMatch(info))
            {
                return false;
            }
            var xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\dataXY.xml");
            var selectSingleNode = xmldoc.SelectSingleNode("Cell");
            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点
                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;
                    if (xe.LocalName == celltype)
                    {
                        xe.SetAttribute("num", info);
                    }
                }
            }
            xmldoc.Save(Application.StartupPath + "\\dataXY.xml");
            _busyflag = false;
            return true;
        }

        /// <summary>
        /// 读取data.xml;
        /// </summary>
        /// <param name="celltype"></param>
        /// <returns></returns>
        public string ReadCell(string celltype)
        {
            while (_busyflag)
            {
            }
            _busyflag = true;
            var strRe = "";
            var xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\dataXY.xml");
            var selectSingleNode = xmldoc.SelectSingleNode("Cell");
            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点
                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;
                    if (xe.LocalName == celltype)
                    {
                        strRe = xe.GetAttribute("num");
                    }
                }
            }
            xmldoc.Save(Application.StartupPath + "\\dataXY.xml");
            _busyflag = false;
            return strRe;
        }
        public void InitXml()
        {
            Modify("input", "00000000");
            Modify("output", "00000000");
        }
    }

    class ClaXmlD
    {
        private static bool _busyflag;

        /// <summary>
        /// 
        /// </summary>
        public void NewXml()
        {
            var xmldoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            var xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);

            xmldoc.AppendChild(xmldecl);

            //加入一个根元素
            XmlElement xmlelem = xmldoc.CreateElement("", "Cell", "");

            xmldoc.AppendChild(xmlelem);

            //加入另外一个元素
            var root = xmldoc.SelectSingleNode("Cell");//查找<Employees> 

            var xe1 = xmldoc.CreateElement("D");//创建一个<Node>节点 

            xe1.SetAttribute("value" , "0");

            if (root != null) root.AppendChild(xe1);//保存创建好的XML文档
            xmldoc.Save(Application.StartupPath + "\\dataD.xml");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Modify(string name, string info)
        {
            while (_busyflag)
            {
            }
            _busyflag = true;

            var xmldoc = new XmlDocument();

            xmldoc.Load(Application.StartupPath + "\\dataD.xml");

            var selectSingleNode = xmldoc.SelectSingleNode("Cell");

            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;

                    if (xe.LocalName == name)
                    {
                        xe.SetAttribute("value", info);
                        xmldoc.Save(Application.StartupPath + "\\dataD.xml");

                        _busyflag = false;

                        return true;
                    }
                }
                
                var root = xmldoc.SelectSingleNode("Cell");//查找<Employees> 

                var xe1 = xmldoc.CreateElement(name);//创建一个<Node>节点 

                xe1.SetAttribute("value", info);

                root.AppendChild(xe1);

                xmldoc.Save(Application.StartupPath + "\\dataD.xml");

                _busyflag = false;

                return true;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string ReadCell(string name)
        {
            string strRe = "";

            var xmldoc = new XmlDocument();

            xmldoc.Load(Application.StartupPath + "\\dataD.xml");

            var selectSingleNode = xmldoc.SelectSingleNode("Cell");

            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;

                    if (xe.LocalName == name)
                    {
                        try
                        {
                            strRe = xe.GetAttribute("value");

                            xmldoc.Save(Application.StartupPath + "\\dataD.xml");

                            return strRe;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
            }return "0";
        }
    }

    class ClaXmlM
    {
        // XmlElement _xmlelem;

        private static bool _busyflag;

        /// <summary>
        /// 
        /// </summary>
        public void NewXml()
        {
            var xmldoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>

            var xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);

            xmldoc.AppendChild(xmldecl);

            //加入一个根元素
            XmlElement xmlelem = xmldoc.CreateElement("", "Cell", "");

            xmldoc.AppendChild(xmlelem);

            //加入另外一个元素
            var root = xmldoc.SelectSingleNode("Cell");//查找<Employees> 
            for (int i = 0; i <= 50; i++)
            {
                var xe1 = xmldoc.CreateElement("M" + i);//创建一个<Node>节点 
                xe1.SetAttribute("status", "0");
                if (root != null) root.AppendChild(xe1);
            }

            //保存创建好的XML文档
            xmldoc.Save(Application.StartupPath + "\\dataM.xml");
        }


        public string[] XmlToDataTableByFile()
        {
            var result = new string[100];
            
            var xmldoc = new XmlDocument();

            xmldoc.Load(Application.StartupPath + "\\dataM.xml");

            var selectSingleNode = xmldoc.SelectSingleNode("Cell");

            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                int i = 0;

                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;
                    try
                    {
                        result[i] = xe.GetAttribute("status");

                        i++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }}
            }
           
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Modify(string name, string info)
        {
            while (_busyflag)
            {
            }
            _busyflag = true;

            var xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\dataM.xml");
            var selectSingleNode = xmldoc.SelectSingleNode("Cell");
            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点
                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;
                    if (xe.LocalName == name)
                    {
                        try
                        {
                            xe.SetAttribute("status", info);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            xmldoc.Save(Application.StartupPath + "\\dataM.xml");
            _busyflag = false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string ReadCell(string name)
        {

            string strRe = "";
            var xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\dataM.xml");
            var selectSingleNode = xmldoc.SelectSingleNode("Cell");
            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点
                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;
                    if (xe.LocalName == name)
                    {
                        try
                        {
                            strRe = xe.GetAttribute("status");
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
            }
            xmldoc.Save(Application.StartupPath + "\\dataM.xml");
            return strRe;
        }
    }

    class ClaXmlUpper
    {
        XmlElement _xmlelem;

        private static bool _busyflag = false;

        public void NewXml()
        {
            var xmldoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            var xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmldoc.AppendChild(xmldecl);

            //加入一个根元素
            _xmlelem = xmldoc.CreateElement("", "Cell", "");
            xmldoc.AppendChild(_xmlelem);

            //加入另外一个元素
            var root = xmldoc.SelectSingleNode("Cell");//查找<Employees> 

            var xe1 = xmldoc.CreateElement("Test");//创建一个<Node>节点 
            xe1.SetAttribute("status", "false");//设置该节点genre属性 

            if (root != null)
            {
                root.AppendChild(xe1);
            }

            //保存创建好的XML文档
            xmldoc.Save(Application.StartupPath + "\\dataUpper.xml");
        }

        public bool Modify(string cellName, string cellStatus)
        {
            while (_busyflag)
            {
            }
            _busyflag = true;

            var xmldoc = new XmlDocument();

            xmldoc.Load(Application.StartupPath + "\\dataUpper.xml");

            var selectSingleNode = xmldoc.SelectSingleNode("Cell");

            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes; //获取Employees节点的所有子节点

                foreach (XmlNode xn in nodeList) //遍历所有子节点 
                {
                    var xe = (XmlElement) xn;

                    if (xe.LocalName == cellName)
                    {
                        xe.SetAttribute("status", cellStatus.ToLower());

                        xmldoc.Save(Application.StartupPath + "\\dataUpper.xml");

                        _busyflag = false;
                        return true;
                    }
                }
            }
            return false;}

        public string ReadCell(string cellName)
        {
            while (_busyflag)
            {
            }
            _busyflag = true;

            var strRe = "";

            var xmldoc = new XmlDocument();

            xmldoc.Load(Application.StartupPath + "\\dataUpper.xml");

            var selectSingleNode = xmldoc.SelectSingleNode("Cell");

            if (selectSingleNode != null)
            {
                var nodeList = selectSingleNode.ChildNodes;//获取Employees节点的所有子节点

                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    var xe = (XmlElement)xn;

                    if (xe.LocalName == cellName)
                    {
                        strRe = xe.GetAttribute("status");

                        xmldoc.Save(Application.StartupPath + "\\dataUpper.xml");

                        _busyflag = false;

                        return strRe;
                    }
                }
                var root = xmldoc.SelectSingleNode("Cell");//查找<Employees> 

                var xe1 = xmldoc.CreateElement(cellName);//创建一个<Node>节点 

                xe1.SetAttribute("status", "false");//设置该节点genre属性 

                if (root != null)
                {
                    root.AppendChild(xe1);
                }
                xmldoc.Save(Application.StartupPath + "\\dataUpper.xml");

                _busyflag = false;

                return "false";
            }
            return "false";}
    }
}
