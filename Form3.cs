using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using MyPLC.MDXY;

namespace MyPLC
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private const string FilePath = @"C:\Users\Administrator\Desktop\MyPLC -5-22\MyPLC\bin\Debug";

        private int chooseNum = 0;

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chooseNum = 1;

            gridView1.Columns.Clear();

            const string fileName = "dataM.xml";

            string filePathM = FilePath + "\\" + fileName;

            ClaXML.ClaXmlm xml = new ClaXML.ClaXmlm();

            List<string[]> GetMList = xml.GetMList(filePathM);

            DataTable dt = new DataTable();

            dt.Columns.Add("name");

            dt.Columns.Add("info");

            foreach (var stringInfo in GetMList)
            {
                DataRow dr = dt.NewRow();

                dr[0] = stringInfo[0];

                dr[1] = stringInfo[1];

                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chooseNum = 2;

            gridView1.Columns.Clear();

            const string fileName = "dataD.xml";

            string filePathM = FilePath + "\\" + fileName;

            ClaXML.ClaXmld xml = new ClaXML.ClaXmld();

            List<string[]> getDList = xml.GetDList(filePathM);

            DataTable dt = new DataTable();

            dt.Columns.Add("name");

            dt.Columns.Add("info");

            foreach (var stringInfo in getDList)
            {
                DataRow dr = dt.NewRow();

                dr[0] = stringInfo[0];

                dr[1] = stringInfo[1];

                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chooseNum = 3;

            gridView1.Columns.Clear();

            const string fileName = "dataXY.xml";

            const string filePathM = FilePath + "\\" + fileName;

            var xml = new ClaXML.ClaXmlXY();

            List<string[]> getMList = xml.GetXList(filePathM);

            var dt = new DataTable();

            dt.Columns.Add("name");

            dt.Columns.Add("info");

            foreach (var stringInfo in getMList)
            {
                DataRow dr = dt.NewRow();

                dr[0] = stringInfo[0];

                dr[1] = stringInfo[1];

                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            chooseNum = 4;

            gridView1.Columns.Clear();

            const string fileName = "dataXY.xml";

            string filePathM = FilePath + "\\" + fileName;

            var xml = new ClaXML.ClaXmlXY();

            List<string[]> GetMList = xml.GetYList(filePathM);

            DataTable dt = new DataTable();

            dt.Columns.Add("name");

            dt.Columns.Add("info");

            foreach (var stringInfo in GetMList)
            {
                DataRow dr = dt.NewRow();

                dr[0] = stringInfo[0];

                dr[1] = stringInfo[1];

                dt.Rows.Add(dr);
            }
            gridControl1.DataSource = dt;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var changedRowNumber = e.RowHandle.ToString(CultureInfo.InvariantCulture);

            var changedCloumnName = e.Column.GetCaption();

            if (changedCloumnName != "info" && Convert.ToInt32(changedRowNumber) >= 0)
            {
                MessageBox.Show("we have a problem!");
            }
            else
            {
                switch (chooseNum)
                {
                    case 1://M
                        ClaXML.ClaXmlm xmlm = new ClaXML.ClaXmlm();

                        DataRow dr1 = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                        xmlm.SaveMinfo(dr1[0].ToString(), dr1[1].ToString());

                        break;
                    case 2://D
                        ClaXML.ClaXmld xmld = new ClaXML.ClaXmld();

                        DataRow dr2 = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                        xmld.SaveDinfo(dr2[0].ToString(), dr2[1].ToString());

                        break;
                    case 3://X
                        ClaXML.ClaXmlXY xmlx = new ClaXML.ClaXmlXY();

                        string infoNew1="";

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            DataRow dr = gridView1.GetDataRow(i);

                            infoNew1 += dr[1];
                        }
                        xmlx.SaveXinfo("", infoNew1);
                        break;
                    case 4://Y
                        ClaXML.ClaXmlXY xmlxy = new ClaXML.ClaXmlXY();

                        string infoNew2 = "";

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle + i);

                            infoNew2 += dr[1];
                        }
                        xmlxy.SaveXinfo("", infoNew2);

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
