using System;
using System.IO;
using System.Windows.Forms;

namespace MyPLC
{
    class cla_TXT
    {
        public string Read_txt(string path)
        {
            try
            {
                var content = File.ReadAllText(path);
                var values = content.Replace("\r\n", "\r");
                return values;
            }
            catch(Exception ex)
            {
                MessageBox.Show("读取TXT异常" + ex.ToString());
            }
            return "-1";
        }

        public bool Clear_txt(string path)
        {
            try
            {
                var sw = new StreamWriter(path);
                sw.Dispose();
                var w = "";
                sw.Write(w);
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("清空TXT异常" + ex.ToString());
            }
            return false;
        }

        public bool Write_txt(string path,string info)
        {
            try
            {
                var sw = new StreamWriter(path);
                var w = info;
                sw.Write(w);
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入TXT异常" + ex.ToString());
            }
            return false;
        }

        public bool Modify_txt(string path,int num,string newbyte)
        {
            try 
            {
                var str = Read_txt(path);
      //          MessageBox.Show(str);
                if (str != "-1")
                {
                    var c = str.ToCharArray();
                    c[num] = newbyte.ToCharArray()[0];
                    var value = new string(c);
                    Write_txt(path, value);
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改TXT异常" + ex.ToString());
            }
            return false;
        }

    }
}
