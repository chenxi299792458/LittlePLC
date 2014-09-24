using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyPLC
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            fillPortList();
        }

        private ClaXmlXY _xmlXY = new ClaXmlXY();

        private ClaXmlD _xmlD = new ClaXmlD();

        private ClaXmlM _xmlM = new ClaXmlM();

        private byte[] _messageReceived { get; set; }

        private byte _slaveAddress;

        private int _slaveDelay;

        private void fillPortList()
        {
            foreach (var port in SerialPort.GetPortNames())
            {
                comboBoxSerialPorts.Items.Add(port);
            }comboBoxSerialPorts.SelectedIndex = 0;
        }

        private void textBoxSlaveDelay_Validating(object sender, CancelEventArgs e)
        {
            var tempInt = 0;

            try
            {
                tempInt = Convert.ToInt32(textBoxSlaveDelay.Text);

                if (tempInt < 0)
                {
                    tempInt = 0;

                    textBoxSlaveDelay.Text = "0";
                }

                _slaveDelay = tempInt;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());

                textBoxSlaveDelay.Text = "0";
            }
        }

        private void textBoxSlaveAddress_Validating(object sender, CancelEventArgs e)
        {
            var tempInt = 0;

            byte tempByte = 0;

            try
            {
                tempInt = Convert.ToInt32(textBoxSlaveAddress.Text);

                if (tempInt > 255)
                {
                    tempInt = 255;

                    textBoxSlaveAddress.Text = "255";
                }
                tempByte = Convert.ToByte(tempInt);

                _slaveAddress = tempByte;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());

                textBoxSlaveAddress.Text = "1";
            }
        }

        private void radioButtonOn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOn.Checked)
            {
                groupBoxConnParameters.Enabled = false;

                _serialPort.PortName = Convert.ToString(comboBoxSerialPorts.SelectedItem);

                if (radioButtonParityEven.Checked)

                    _serialPort.Parity = Parity.Even;

                else if (radioButtonParityNone.Checked)

                    _serialPort.Parity = Parity.None;

                else if (radioButtonParityOdd.Checked)

                    _serialPort.Parity = Parity.Odd;

                if (radioButtonDataBits7.Checked)

                    _serialPort.DataBits = 7;

                else if (radioButtonDataBits8.Checked)

                    _serialPort.DataBits = 8;

                if (radioButtonStopBit1.Checked)

                    _serialPort.StopBits = StopBits.One;

                if (radioButtonStopBit2.Checked)

                    _serialPort.StopBits = StopBits.Two;

                _serialPort.BaudRate = Convert.ToInt32(textBoxBaud.Text);

                try
                {
                    _serialPort.Open();

                    backgroundWorker1.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    radioButtonOff.Checked = true;

                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                backgroundWorker1.CancelAsync();

                groupBoxConnParameters.Enabled = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            while (worker != null && !worker.CancellationPending)
            {
                byte[] reDatas = new byte[_serialPort.BytesToRead];

                _serialPort.Read(reDatas, 0, reDatas.Length);//读取数据

                _messageReceived = reDatas;

                /////////////////////////////////////////////////////////////////////
                Thread.Sleep(100);

                string hexString = string.Empty;

                if (reDatas != null)
                {
                    StringBuilder strB = new StringBuilder();

                    for (int i = 0; i < reDatas.Length; i++)
                    {
                        strB.Append(reDatas[i].ToString("X2"));
                    }
                    hexString = strB.ToString();
                }
                Debug.WriteLine(DateTime.Now + hexString);


                ///////////////////////////////////////////////////////////////////////

                if (_messageReceived.Length < 4)
                {
                }
                else
                {
                    #region Function-1 读1or多路开关量输出状态
                    if (_messageReceived[0] == _slaveAddress && _messageReceived[1] == 1)
                    {
                        if (Modbus.CRCStuff.checkCRC(_messageReceived, _messageReceived.Length))
                        {
                            var messageToSend = createRespondMessage_1();
                            Thread.Sleep(_slaveDelay);
                            _serialPort.Write(messageToSend, 0, messageToSend.Length);
                            addLog(createLogStr(messageToSend), LogType.TX, worker);
                        }
                        else
                        {
                            addLog("", LogType.CRC_ERR, worker);
                        }
                    }
                    #endregion

                    #region Function-3 读多路寄存器输入
                    if (_messageReceived[0] == _slaveAddress && _messageReceived[1] == 3)
                    {
                        if (Modbus.CRCStuff.checkCRC(_messageReceived, _messageReceived.Length))
                        {
                            var messageToSend = createRespondMessage_3();
                            Thread.Sleep(_slaveDelay);
                            _serialPort.Write(messageToSend, 0, messageToSend.Length);
                            addLog(createLogStr(messageToSend), LogType.TX, worker);
                        }
                        else
                        {
                            addLog("", LogType.CRC_ERR, worker);
                        }
                    }
                    #endregion

                    #region Function-5 写1路开关量输出
                    if (_messageReceived[0] == _slaveAddress && _messageReceived[1] == 5)
                    {
                        if (Modbus.CRCStuff.checkCRC(_messageReceived, _messageReceived.Length))
                        {
                            var messageToSend = createRespondMessage_5();
                            Thread.Sleep(_slaveDelay);
                            _serialPort.Write(messageToSend, 0, messageToSend.Length);
                            addLog(createLogStr(messageToSend), LogType.TX, worker);
                        }
                        else
                        {
                            addLog("", LogType.CRC_ERR, worker);
                        }
                    }
                    #endregion

                    #region Function_10 写多路寄存器

                    if (_messageReceived[0] == _slaveAddress && _messageReceived[1] == 16)
                    {
                        if (Modbus.CRCStuff.checkCRC(_messageReceived, _messageReceived.Length))
                        {
                            var messageToSend = createRespondMessage_10();

                            Thread.Sleep(_slaveDelay);

                            _serialPort.Write(messageToSend, 0, messageToSend.Length);

                            addLog(createLogStr(messageToSend), LogType.TX, worker);
                        }
                        else
                        {
                            addLog("", LogType.CRC_ERR, worker);
                        }
                    }

                    #endregion

                    _serialPort.DiscardInBuffer();
                }
            }
            _serialPort.Close();

            e.Cancel = true;

            Thread.Sleep(1000);
        }

        //Function-1 读1or多路开关量输出状态
        private byte[] createRespondMessage_1()
        {
            const int bytesToSend = 6;

            var respondMessage = new byte[bytesToSend];

            var startAddress = (_messageReceived[2] << 8) | _messageReceived[3];

            startAddress = startAddress - 2048;

            var numberOfPoints = (_messageReceived[4] << 8) | _messageReceived[5];
            
            respondMessage[0] = _slaveAddress;

            respondMessage[1] = 0x01;

            respondMessage[2] = 0x01;

            #region 得到M数据返回byte【3】中

            var j = 0;

            string getMinfo = "";

            for (var i = 0; i < numberOfPoints; i++, j++)
            {
                string CellNAme = "M" + (startAddress + j);

                getMinfo += _xmlM.ReadCell(CellNAme);
            }
            char[] charTemp = getMinfo.ToCharArray();

            Array.Reverse(charTemp);

            getMinfo = new string(charTemp);

            string HexMinfo = string.Format("{0:x}", Convert.ToInt32(getMinfo, 2));

            byte[] ByteMinfo = BitConverter.GetBytes((Convert.ToInt32(HexMinfo, 16)));

            #endregion

            respondMessage[3] = ByteMinfo[0];

            var crcCalculation = Modbus.CRCStuff.calculateCRC(respondMessage, bytesToSend - 2);

            respondMessage[bytesToSend - 2] = crcCalculation[0];

            respondMessage[bytesToSend - 1] = crcCalculation[1];

            return respondMessage;
        }

        //Function-3 读多路寄存器输入
        private byte[] createRespondMessage_3()
        {
            var numberOfPoints = (_messageReceived[4] << 8) | _messageReceived[5];

            var bytesToSend = 2 * numberOfPoints + 5;

            var respondMessage = new byte[bytesToSend];

            respondMessage[0] = _slaveAddress;

            respondMessage[1] = 3;

            respondMessage[2] = Convert.ToByte(2 * numberOfPoints);
            
            var startAddress = (_messageReceived[2] << 8) | _messageReceived[3];

            startAddress = startAddress - 4096;

            string CellNAme;

            for (var i = 0; i < numberOfPoints/2; i++)
            {
                CellNAme = "D" + (startAddress + i*2);

                byte[] ieeeBytes = FloatToIeee(CellNAme);

                respondMessage[4 * i + 3] = ieeeBytes[2];

                respondMessage[4 * i + 4] = ieeeBytes[3];

                respondMessage[4 * i + 5] = ieeeBytes[0];

                respondMessage[4 * i + 6] = ieeeBytes[1];
            }
            var crcCalculation = Modbus.CRCStuff.calculateCRC(respondMessage, bytesToSend - 2);

            respondMessage[bytesToSend - 2] = crcCalculation[0];

            respondMessage[bytesToSend - 1] = crcCalculation[1];

            return respondMessage;
        }

        //Function-5 写1路开关量输出
        private byte[] createRespondMessage_5()
        {
            var respondMessage = _messageReceived;

            var startAddress = (_messageReceived[2] << 8) | _messageReceived[3];

            startAddress = startAddress - 2048;

            var keyStatus = (_messageReceived[4] << 8) | _messageReceived[5];

            if (keyStatus==0)
            {
                string CellNAme = "M" + (startAddress );

                _xmlM.Modify(CellNAme,"0");
            }
            else
            {
                string CellNAme = "M" + (startAddress );

                _xmlM.Modify(CellNAme, "1");
            }

            return respondMessage;
        }

        //Function_10 写多路寄存器
        private byte[] createRespondMessage_10()
        {
            var bytesToSend = 0;

            bytesToSend = 8;

            var respondMessage = new byte[bytesToSend];

            respondMessage[0] = _slaveAddress;

            respondMessage[1] = 16;

            respondMessage[2] = _messageReceived[2];

            respondMessage[3] = _messageReceived[3];

            respondMessage[4] = _messageReceived[4];

            respondMessage[5] = _messageReceived[5];

            var startAddress = (_messageReceived[2] << 8) | _messageReceived[3];

            startAddress = startAddress - 4096;

            string CellNAme = "D" + (startAddress );

           // var numberOfPoints = (_messageReceived[4] << 8) | _messageReceived[5];

            float setValue = IeeeToFloat();

            _xmlD.Modify(CellNAme,setValue.ToString(CultureInfo.InvariantCulture));

            var crcCalculation = Modbus.CRCStuff.calculateCRC(respondMessage, bytesToSend - 2);

            respondMessage[bytesToSend - 2] = crcCalculation[0];

            respondMessage[bytesToSend - 1] = crcCalculation[1];

            return respondMessage;
        }

        private float IeeeToFloat()
        {
            //_messageReceived在这里是全局变量 哪里都可以用到 改了的话需要加一个byte[]
            
            byte[] ieeeBytes=new byte[4];

            ieeeBytes[0] = _messageReceived[8];

            ieeeBytes[1] = _messageReceived[7];

            ieeeBytes[2] = _messageReceived[10];

            ieeeBytes[3] = _messageReceived[9];

            int ieeeINT = ieeeBytes[0] << 24 | ieeeBytes[1] << 16 | ieeeBytes[2] << 8 | ieeeBytes[3];

            float resultF = BitConverter.ToSingle(BitConverter.GetBytes(ieeeINT), 0);

            return resultF;
        }

        private byte[] FloatToIeee(string name)
        {
            byte[] returnBytes=new byte[]{0x00,0x00};

            if (name[0].ToString().ToUpper() == "D")
            {
                string strget = _xmlD.ReadCell(name);

                float numberF = float.Parse(strget);

                returnBytes = BitConverter.GetBytes(numberF);

                return returnBytes;
            }
            return new byte[] {0x00, 0x00};
        }

        private void addLog(String log, LogType logType, BackgroundWorker worker)
        {
            var now = DateTime.Now;

            var tmpStr = "";

            switch (logType)
            {
                case LogType.CRC_ERR:
                    tmpStr = ">" + now.ToLongTimeString() + ">CRC Check Failed";
                    break;
                case LogType.RX:
                    tmpStr = ">" + now.ToLongTimeString() + ">RX:" + log;
                    break;
                case LogType.TX:
                    tmpStr = ">" + now.ToLongTimeString() + ">TX:" + log;
                    break;
            }
            worker.ReportProgress(0, tmpStr);
        }

        private String createLogStr(byte[] message)
        {
            var tmpStr = "";

            foreach (var oneByte in message)
            {
                var byteString = oneByte.ToString("X");

                if (byteString.Length == 1)
                {
                    byteString = "0" + byteString;
                }
                tmpStr = tmpStr + byteString + " ";
            }
            return tmpStr;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            listBoxCommLog.Items.Add(e.UserState);
        }

        enum LogType
        {
            RX,
            TX,
            CRC_ERR
        }
    }
}
