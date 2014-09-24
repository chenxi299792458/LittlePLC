using System;
using System.Windows.Forms;

namespace MyPLC
{
    public partial class Form1 : Form
    {
        private ClaXmlXY _xmlXY = new ClaXmlXY();

        private ClaXmlD _xmlD = new ClaXmlD();

        private ClaXmlM _xmlM = new ClaXmlM();

        private ClaXmlUpper _xmlUpper=new ClaXmlUpper();

        public Form1()
        {
            InitializeComponent();

            _xmlXY.NewXml();

            _xmlD.NewXml();

            _xmlM.NewXml();

            _xmlUpper.NewXml();

            stateIndicatorComponent1.StateIndex = 1;

            var t = new Timer();

            t.Enabled = true;

            t.Interval = 10;

            t.Tick += t_Tick;

            #region 关标志

            stateIndicatorComponent17.StateIndex = 1;

            stateIndicatorComponent2.StateIndex = 1;

            stateIndicatorComponent3.StateIndex = 1;

            stateIndicatorComponent4.StateIndex = 1;

            stateIndicatorComponent5.StateIndex = 1;

            stateIndicatorComponent6.StateIndex = 1;

            stateIndicatorComponent7.StateIndex = 1;

            stateIndicatorComponent8.StateIndex = 1;

            stateIndicatorComponent9.StateIndex = 1;

            stateIndicatorComponent10.StateIndex = 1;

            stateIndicatorComponent11.StateIndex = 1;

            stateIndicatorComponent12.StateIndex = 1;

            stateIndicatorComponent13.StateIndex = 1;

            stateIndicatorComponent14.StateIndex = 1;

            stateIndicatorComponent15.StateIndex = 1;

            stateIndicatorComponent16.StateIndex = 1;

            #endregion
        }

        void t_Tick(object sender, EventArgs e)
        {
            var c = lab_state1.Text.ToCharArray()[0];

            switch (c)
            {
                case '-': lab_state1.Text = "\\"; break;

                case '\\': lab_state1.Text = "|"; break;

                case '|': lab_state1.Text = "/"; break;

                case '/': lab_state1.Text = "-"; break;

                default: lab_state1.Text = "-"; break;
            }

            var str_output = _xmlXY.ReadCell("output");

            for (var i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent9.StateIndex = 3;

                        else
                            stateIndicatorComponent9.StateIndex = 1;

                        break;

                    case 1:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent10.StateIndex = 3;

                        else
                            stateIndicatorComponent10.StateIndex = 1;

                        break;

                    case 2:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent11.StateIndex = 3;

                        else
                            stateIndicatorComponent11.StateIndex = 1;

                        break;

                    case 3:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent12.StateIndex = 3;

                        else
                            stateIndicatorComponent12.StateIndex = 1;

                        break;

                    case 4:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent13.StateIndex = 3;

                        else
                            stateIndicatorComponent13.StateIndex = 1;

                        break;

                    case 5:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent14.StateIndex = 3;

                        else
                            stateIndicatorComponent14.StateIndex = 1;

                        break;

                    case 6:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent15.StateIndex = 3;

                        else
                            stateIndicatorComponent15.StateIndex = 1;

                        break;

                    case 7:

                        if (str_output[i] == 0x31)

                            stateIndicatorComponent16.StateIndex = 3;

                        else
                            stateIndicatorComponent16.StateIndex = 1;

                        break;
                }
            }

        }

        #region INPUT按键

        private char[] cha = { '0', '0', '0', '0', '0', '0', '0', '0' };

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (simpleButton1.Text == "on")
            {
                stateIndicatorComponent17.StateIndex = 3;

                simpleButton1.Text = "off";

                cha[0] = '1';
            }
            else
            {
                simpleButton1.Text = "on";

                stateIndicatorComponent17.StateIndex = 1;

                cha[0] = '0';
            }

            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (simpleButton2.Text == "on")
            {
                stateIndicatorComponent2.StateIndex = 3;

                simpleButton2.Text = "off";

                cha[1] = '1';
            }
            else
            {
                simpleButton2.Text = "on";

                stateIndicatorComponent2.StateIndex = 1;

                cha[1] = '0';
            }

            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (simpleButton3.Text == "on")
            {
                stateIndicatorComponent3.StateIndex = 3;

                simpleButton3.Text = "off";

                cha[2] = '1';
            }
            else
            {
                simpleButton3.Text = "on";

                stateIndicatorComponent3.StateIndex = 1;

                cha[2] = '0';
            }

            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (simpleButton4.Text == "on")
            {
                stateIndicatorComponent4.StateIndex = 3;

                simpleButton4.Text = "off";

                cha[3] = '1';
            }
            else
            {
                simpleButton4.Text = "on";

                stateIndicatorComponent4.StateIndex = 1;

                cha[3] = '0';
            }

            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (simpleButton5.Text == "on")
            {
                stateIndicatorComponent5.StateIndex = 3;

                simpleButton5.Text = "off";

                cha[4] = '1';
            }
            else
            {
                simpleButton5.Text = "on";

                stateIndicatorComponent5.StateIndex = 1;

                cha[4] = '0';
            }

            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (simpleButton6.Text == "on")
            {
                stateIndicatorComponent6.StateIndex = 3;

                simpleButton6.Text = "off";

                cha[5] = '1';
            }
            else
            {
                simpleButton6.Text = "on";

                stateIndicatorComponent6.StateIndex = 1;

                cha[5] = '0';
            }
            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (simpleButton7.Text == "on")
            {
                stateIndicatorComponent7.StateIndex = 3;

                simpleButton7.Text = "off";

                cha[6] = '1';
            }
            else
            {
                simpleButton7.Text = "on";

                stateIndicatorComponent7.StateIndex = 1;

                cha[6] = '0';
            }

            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (simpleButton8.Text == "on")
            {
                stateIndicatorComponent8.StateIndex = 3;

                simpleButton8.Text = "off";

                cha[7] = '1';
            }
            else
            {
                simpleButton8.Text = "on";

                stateIndicatorComponent8.StateIndex = 1;

                cha[7] = '0';
            }

            var str = new string(cha);

            _xmlXY.Modify("input", str);
        }

        #endregion

        private void but_load_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "LAD文件|*.lad";

                openFileDialog.RestoreDirectory = true;

                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fName = openFileDialog.FileName;

                    txtbox_adress.Text = fName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开csv异常" + ex.ToString());
            }
        }

        private int m_EnableTrace = 1;

        private string sLadderFilePath = "";

        private string sLogPath = "";

        private string m_DspLadderFilePath = "";

        private string m_DspLogPath = "";

        private Timer iTimerHandle;

        private cla_PLCCore pPLC = new cla_PLCCore();

        private void but_run_Click(object sender, EventArgs e)
        {
            sLadderFilePath = txtbox_adress.Text;

            if (sLadderFilePath.Length > 0)
            {
                m_DspLadderFilePath = sLadderFilePath;

                if (m_EnableTrace == 0 && sLogPath.Length == 0)
                {
                    MessageBox.Show("载入lad错误1");
                }
                else
                {
                    m_DspLogPath = sLogPath;

                    if (m_EnableTrace == 0)
                    {
                        pPLC.bEnableTrace = true;
                    }

                    pPLC.sLadderFilePath = sLadderFilePath;

                    pPLC.fRUN();

                    but_run.Enabled = false;

                    but_load.Enabled = false;

                    stateIndicatorComponent1.StateIndex = 3;

                    iTimerHandle = new Timer();

                    iTimerHandle.Interval = 10;

                    iTimerHandle.Tick += MyTimerProc;

                    iTimerHandle.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("载入lad错误2");
            }
        }

        void MyTimerProc(object sender, EventArgs e)
        {
            var str = lab_state.Text;

            switch (str)
            {
                case "-":
                    lab_state.Text = "\\";
                    break;
                case "\\":
                    lab_state.Text = "|";
                    break;
                case "|":
                    lab_state.Text = "/";
                    break;
                case "/":
                    lab_state.Text = "-";
                    break;
                default:
                    lab_state.Text = "-";
                    break;
            }
        }

        private void but_stop_Click(object sender, EventArgs e)
        {
            but_run.Enabled = true;

            but_load.Enabled = true;

            if (iTimerHandle!=null) iTimerHandle.Enabled = false;

            lab_state.Text = "-";

            if (pPLC != null)
            {
                pPLC.fSTOP();

                stateIndicatorComponent1.StateIndex = 1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _xmlXY.InitXml();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form2 frmForm2 = new Form2();

            frmForm2.Visible = true;

            frmForm2.Show();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form3 frmForm3 = new Form3();

            frmForm3.Visible = true;

            frmForm3.Show();
        }
    }
}
