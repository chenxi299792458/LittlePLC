using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MyPLC
{
    class cla_PLCCore
    {
        #region 一些常数值
        private string[] TableM = new string[100];

        private ClaXmlXY _xml = new ClaXmlXY();

        private readonly bool modeXml = true;

        public string sLadderFilePath { get; set; }         //lad path

        public bool bEnableTrace { get; set; }              // 

        public bool bRUN { get; set; }

        public int iProgSize { get; set; }

        private const int MAX_LADDER_DEPTH = 5;

        private const int MAX_NUM_INPUT = 8;

        private const int MAX_NUM_OUTPUT = 8;

        private const int MAX_NUM_AUX = 100;

        private const int MAX_NUM_STATUS = 100;

        private const int MAX_NUM_TIMER = 100;

        private const int MAX_NUM_COUNTER = 100;

        private const int MAX_NUM_REGISTER = 200;

        private const int MAX_NUM_UPPER = 10;

        private const int MAX_PROG_SIZE = 4096000;

        // private const int   TRACE_PATH "C:\\plccore.log";
        //private const int  LADDER_PATH "C:\\123.lad";

        private const int IS_INPUT = 1;

        private const int IS_OUTPUT = 2;

        private const int IS_AUX = 3;

        private const int IS_STATUS = 4;

        private const int IS_TIMER = 5;

        private const int IS_COUNTER = 6;

        private const int IS_REGISTER = 7;

        private const int IS_UPPER = 8;

        private cla_stPLCcs.stPLC_OBJ.stInPut[] pInPut;

        private cla_stPLCcs.stPLC_OBJ.stOutPut[] pOutPut;

        private cla_stPLCcs.stPLC_OBJ.stAUX[] pAUX;

        private cla_stPLCcs.stPLC_OBJ.stSTATUS[] pSTATUS;

        private cla_stPLCcs.stPLC_OBJ.stTimer[] pTimer;

        private cla_stPLCcs.stPLC_OBJ.stCounter[] pCounter;

        private string pMEMBUF;

        private System.Windows.Forms.Timer m_timerID;

        private Thread hFlowThread;

        private cla_PLCCore thePLCCore;
        #endregion

        public void Initialize()
        {
            pInPut = new cla_stPLCcs.stPLC_OBJ.stInPut[MAX_NUM_INPUT];

            for (var i = 0; i < MAX_NUM_INPUT; i++)
            {
                pInPut[i] = new cla_stPLCcs.stPLC_OBJ.stInPut();
            }

            pOutPut = new cla_stPLCcs.stPLC_OBJ.stOutPut[MAX_NUM_OUTPUT];
            for (var i = 0; i < MAX_NUM_OUTPUT; i++)
            {
                pOutPut[i] = new cla_stPLCcs.stPLC_OBJ.stOutPut();
            }

            pAUX = new cla_stPLCcs.stPLC_OBJ.stAUX[MAX_NUM_AUX];

            for (var i = 0; i < MAX_NUM_AUX; i++)
            {
                pAUX[i] = new cla_stPLCcs.stPLC_OBJ.stAUX();
            }

            pSTATUS = new cla_stPLCcs.stPLC_OBJ.stSTATUS[MAX_NUM_STATUS];

            for (var i = 0; i < MAX_NUM_STATUS; i++)
            {
                pSTATUS[i] = new cla_stPLCcs.stPLC_OBJ.stSTATUS();
            }

            pTimer = new cla_stPLCcs.stPLC_OBJ.stTimer[MAX_NUM_TIMER];

            for (var i = 0; i < MAX_NUM_TIMER; i++)
            {
                pTimer[i] = new cla_stPLCcs.stPLC_OBJ.stTimer();
            }

            pCounter = new cla_stPLCcs.stPLC_OBJ.stCounter[MAX_NUM_COUNTER];

            for (var i = 0; i < MAX_NUM_COUNTER; i++)
            {
                pCounter[i] = new cla_stPLCcs.stPLC_OBJ.stCounter();
            }
            bRUN = false;

            iProgSize = MAX_PROG_SIZE;

            hFlowThread = null;

            bEnableTrace = false;

            thePLCCore = this;

            //          iTimerCount = 0;

            fnRESETAll();

            fnWriteOutput();
        }

        private void fnRESETAll()
        {
            var i = 0;

            for (i = 0; i < MAX_NUM_INPUT; i++)
            {
                pInPut[i].status = false;
            }
            for (i = 0; i < MAX_NUM_OUTPUT; i++)
            {
                pOutPut[i].status = false;
            }
            for (i = 0; i < MAX_NUM_AUX; i++)
            {
                pAUX[i].status = false;
            }
            for (i = 0; i < MAX_NUM_STATUS; i++)
            {
                pSTATUS[i].status = false;
            }
            for (i = 0; i < MAX_NUM_TIMER; i++)
            {
                pTimer[i].status = false;

                pTimer[i].timeup = false;

                pTimer[i].clock = 0;

                pTimer[i].limit = 0;
            }
            for (i = 0; i < MAX_NUM_COUNTER; i++)
            {
                pCounter[i].count = 0;

                pCounter[i].fire = false;

                pCounter[i].limit = 0;

                pCounter[i].status = false;

                pCounter[i].filpflop = false;
            }}

        private void fnReadInput()
        {
            if (modeXml)
            {
                var str = _xml.ReadCell("input");

                for (var i = 0; i < MAX_NUM_INPUT; i++)
                {
                    pInPut[i].status = str[i] == '1';
                }
            }
            else
            {
                var txt = new cla_TXT();

                var pBuffer = txt.Read_txt(@"c:\input.txt");

                for (var i = 0; i < MAX_NUM_INPUT; i++)
                {
                    pInPut[i].status = pBuffer[i] == '1';
                }
            }
        }

        private void fnWriteOutput()
        {
            if (modeXml)
            {
                var str = "";

                for (var i = 0; i < MAX_NUM_OUTPUT; i++)
                {
                    var iStatus = pOutPut[i].status;

                    if (iStatus)
                    {
                        str += "1";
                    }
                    else
                    {
                        str += "0";
                    }
                }
                _xml.Modify("output", str);
            }
            else
            {
                var txt = new cla_TXT();

                var OutPutPath = @"c:\output.txt";

                for (var i = 0; i < MAX_NUM_OUTPUT; i++)
                {
                    var iStatus = pOutPut[i].status;

                    if (iStatus)
                    {
                        txt.Modify_txt(OutPutPath, i, "1");
                    }
                    else
                    {
                        txt.Modify_txt(OutPutPath, i, "0");
                    }
                }
            }
        }


        private void FnReadM()
        {
            var xmlm = new ClaXmlM();

            TableM = xmlm.XmlToDataTableByFile();

            for (int i = 0; i < MAX_NUM_AUX; i++)
            {
                if (TableM[i] == "1")
                {
                    pAUX[i].status = true;
                }
                else
                {
                    pAUX[i].status = false;
                }
            }
        }

        private void FnWriteM()
        {
            var xmlDm = new ClaXmlD();

            for (int i = 0; i < MAX_NUM_AUX; i++)
            {
                if (pAUX[i].status)
                {
                    TableM[i] = "1";
                }
                else
                {
                    TableM[i] = "0";
                }
            }
        }

        public int fRUN()
        {
            this.Initialize();
            if (bRUN == false)
            {
                bRUN = true;

                hFlowThread = new Thread(fnFlowCtrlThread);

                hFlowThread.Start();

                m_timerID = new System.Windows.Forms.Timer();

                m_timerID.Interval = 100;

                m_timerID.Tick += timerRoutine;

                m_timerID.Enabled = true;
            }
            else
            {
                return -2;
            }
            return 0;
        }

        public void fSTOP()
        {
            if (bRUN)
            {
                bRUN = false;

                try
                {
                    hFlowThread.Abort();

                    m_timerID.Enabled = false;
                }
                catch (ThreadAbortException ex1)
                {
                    MessageBox.Show("关闭core线程异常" + ex1.ToString());
                }
            }
        }

        private bool GetProgToken(ref int iParseProgOffset)  //用“回车+新行”共两字节，分割梯形图代码，分割完成一新行 return true；
        {
            for (; iParseProgOffset < pMEMBUF.Length; iParseProgOffset++)
            {
                if (pMEMBUF[iParseProgOffset] == '\r')
                {
                    return true;
                }
            }
            return false;
        }

        private int fnLDAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //    var str = pCOMMAND[1][1].ToString(CultureInfo.InvariantCulture) + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //     iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'X':
                    pPLC_OBJ[piStackDepth] = ~thePLCCore.pInPut[iObjIndex];
                    break;
                case 'Y':
                    pPLC_OBJ[piStackDepth] = ~thePLCCore.pOutPut[iObjIndex];
                    break;
                case 'M':
                    pPLC_OBJ[piStackDepth] = ~thePLCCore.pAUX[iObjIndex];
                    break;
                case 'S':
                    pPLC_OBJ[piStackDepth] = ~thePLCCore.pSTATUS[iObjIndex];
                    break;
                case 'T':
                    pPLC_OBJ[piStackDepth] = ~thePLCCore.pTimer[iObjIndex];
                    break;
                case 'C':
                    pPLC_OBJ[piStackDepth] = ~thePLCCore.pCounter[iObjIndex];
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnLDNotAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'X':
                    pPLC_OBJ[piStackDepth] = ~!thePLCCore.pInPut[iObjIndex];
                    break;
                case 'Y':
                    pPLC_OBJ[piStackDepth] = ~!thePLCCore.pOutPut[iObjIndex];
                    break;
                case 'M':
                    pPLC_OBJ[piStackDepth] = ~!thePLCCore.pAUX[iObjIndex];
                    break;
                case 'S':
                    pPLC_OBJ[piStackDepth] = ~!thePLCCore.pSTATUS[iObjIndex];
                    break;
                case 'T':
                    pPLC_OBJ[piStackDepth] = ~!thePLCCore.pTimer[iObjIndex];
                    break;
                case 'C':
                    pPLC_OBJ[piStackDepth] = ~!thePLCCore.pCounter[iObjIndex];
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnANDAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'X':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pInPut[iObjIndex]);
                    break;
                case 'Y':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pOutPut[iObjIndex]);
                    break;
                case 'M':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pAUX[iObjIndex]);
                    break;
                case 'S':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pSTATUS[iObjIndex]);
                    break;
                case 'T':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pTimer[iObjIndex]);
                    break;
                case 'C':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pCounter[iObjIndex]);
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnANDNotAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'X':
                    pPLC_OBJ[piStackDepth - 1] = ~((pPLC_OBJ[piStackDepth - 1] * !thePLCCore.pInPut[iObjIndex]));
                    break;
                case 'Y':
                    pPLC_OBJ[piStackDepth - 1] = ~((pPLC_OBJ[piStackDepth - 1] * !thePLCCore.pOutPut[iObjIndex]));
                    break;
                case 'M':
                    pPLC_OBJ[piStackDepth - 1] = ~((pPLC_OBJ[piStackDepth - 1] * !thePLCCore.pAUX[iObjIndex]));
                    break;
                case 'S':
                    pPLC_OBJ[piStackDepth - 1] = ~((pPLC_OBJ[piStackDepth - 1] * !thePLCCore.pSTATUS[iObjIndex]));
                    break;
                case 'T':
                    pPLC_OBJ[piStackDepth - 1] = ~((pPLC_OBJ[piStackDepth - 1] * !thePLCCore.pTimer[iObjIndex]));
                    break;
                case 'C':
                    pPLC_OBJ[piStackDepth - 1] = ~((pPLC_OBJ[piStackDepth - 1] * !thePLCCore.pCounter[iObjIndex]));
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnORAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'X':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + thePLCCore.pInPut[iObjIndex]);
                    break;
                case 'Y':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + thePLCCore.pOutPut[iObjIndex]);
                    break;
                case 'M':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + thePLCCore.pAUX[iObjIndex]);
                    break;
                case 'S':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + thePLCCore.pSTATUS[iObjIndex]);
                    break;
                case 'T':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + thePLCCore.pTimer[iObjIndex]);
                    break;
                case 'C':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + thePLCCore.pCounter[iObjIndex]);
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnORNotAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'X':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + !thePLCCore.pInPut[iObjIndex]);
                    break;
                case 'Y':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + !thePLCCore.pOutPut[iObjIndex]);
                    break;
                case 'M':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + !thePLCCore.pAUX[iObjIndex]);
                    break;
                case 'S':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + !thePLCCore.pSTATUS[iObjIndex]);
                    break;
                case 'T':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + !thePLCCore.pTimer[iObjIndex]);
                    break;
                case 'C':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] + !thePLCCore.pCounter[iObjIndex]);
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnOUTAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            var iK_Value = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'Y':
                    thePLCCore.pOutPut[iObjIndex].status = pPLC_OBJ[piStackDepth - 1].status;

                    break;
                case 'M':
                    thePLCCore.pAUX[iObjIndex].status = pPLC_OBJ[piStackDepth - 1].status;

                    break;
                case 'T':
                    thePLCCore.pTimer[iObjIndex].status = pPLC_OBJ[piStackDepth - 1].status;

                    var str_1 = pCOMMAND[2].Replace("K", "0");

                    iK_Value = Convert.ToInt32(str_1);

                    thePLCCore.pTimer[iObjIndex].limit = iK_Value;

                    break;
                case 'C':
                    if (pPLC_OBJ[piStackDepth - 1].status)
                    {
                        var str_2 = pCOMMAND[2][1].ToString();

                        iK_Value = Convert.ToInt32(str_2);

                        thePLCCore.pCounter[iObjIndex].limit = iK_Value;

                        if (thePLCCore.pCounter[iObjIndex].filpflop == false && thePLCCore.pCounter[iObjIndex].fire == false)
                        {
                            thePLCCore.pCounter[iObjIndex].count++;

                            thePLCCore.pCounter[iObjIndex].status = true;

                            thePLCCore.pCounter[iObjIndex].filpflop = true;
                        }
                    }
                    else
                    {
                        thePLCCore.pCounter[iObjIndex].filpflop = false;
                    }

                    if ((thePLCCore.pCounter[iObjIndex].count >= thePLCCore.pCounter[iObjIndex].limit) && thePLCCore.pCounter[iObjIndex].status)
                    {
                        thePLCCore.pCounter[iObjIndex].fire = true;
                    }
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnANBAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            pPLC_OBJ[piStackDepth - 2] = ~(pPLC_OBJ[piStackDepth - 2] * pPLC_OBJ[piStackDepth - 1]);

            return 0;
        }

        private int fnORBAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            pPLC_OBJ[piStackDepth - 2] = ~(pPLC_OBJ[piStackDepth - 2] + pPLC_OBJ[piStackDepth - 1]);

            return 0;
        }

        private int fnSETAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'Y':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pOutPut[iObjIndex].status = true;
                    break;
                case 'M':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pAUX[iObjIndex].status = true;
                    break;
                case 'S':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pSTATUS[iObjIndex].status = true;
                    break;
                case 'T':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pTimer[iObjIndex].status = true;
                    break;
                case 'C':
                    if (pPLC_OBJ[piStackDepth - 1].status)
                    {
                        thePLCCore.pCounter[iObjIndex].status = true;

                        thePLCCore.pCounter[iObjIndex].fire = true;
                    }
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnRSTAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iObjIndex = 0;

            //var str = pCOMMAND[1][1].ToString() + pCOMMAND[1][2].ToString() + pCOMMAND[1][3].ToString();

            //iObjIndex = Convert.ToInt32(str);

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'Y':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pOutPut[iObjIndex].status = false;
                    break;
                case 'M':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pAUX[iObjIndex].status = false;
                    break;
                case 'S':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pSTATUS[iObjIndex].status = false;
                    break;
                case 'T':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pTimer[iObjIndex].status = false;
                    break;
                case 'C':
                    if (pPLC_OBJ[piStackDepth - 1].status)
                    {
                        thePLCCore.pCounter[iObjIndex].status = false;

                        thePLCCore.pCounter[iObjIndex].count = 0;

                        thePLCCore.pCounter[iObjIndex].fire = false;
                        
                        thePLCCore.pCounter[iObjIndex].filpflop = false;
                    }
                    break;
                default:

                    return -1;
            }
            return 0;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private int fnCMPAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            var iCmpIndex1 = GetNumberKDTC(pCOMMAND[1]);

            var iCmpIndex2 = GetNumberKDTC(pCOMMAND[2]);

            var iObjIndex = GetNumber(pCOMMAND[3]);

            switch (pCOMMAND[3][0])
            {
                case 'Y':

                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pOutPut[iObjIndex].status = false;
                    break;
                case 'M':
                    ClaXmlM xmlM = new ClaXmlM();

                    xmlM.Modify(pCOMMAND[3], "0");

                    xmlM.Modify(pCOMMAND[3][0] + (iObjIndex + 1).ToString(CultureInfo.InvariantCulture), "0");

                    xmlM.Modify(pCOMMAND[3][0] + (iObjIndex + 2).ToString(CultureInfo.InvariantCulture), "0");

                    if (iCmpIndex1 == iCmpIndex2)
                    {
                        xmlM.Modify(pCOMMAND[3][0] + (iObjIndex + 1).ToString(CultureInfo.InvariantCulture), "1");
                    }
                    else if (iCmpIndex1 > iCmpIndex2)
                    {
                        xmlM.Modify(pCOMMAND[3], "1");
                    }
                    else if (iCmpIndex1 < iCmpIndex2)
                    {
                        xmlM.Modify(pCOMMAND[3][0] + (iObjIndex + 2).ToString(CultureInfo.InvariantCulture), "1");
                    }
                    break;
                case 'S':
                    if (pPLC_OBJ[piStackDepth - 1].status)

                        thePLCCore.pSTATUS[iObjIndex].status = false;

                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private void fnMOVAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            if (pPLC_OBJ[piStackDepth - 1].status)
            {
                var getNumber = GetNumberKDTC(pCOMMAND[1]);

                var xmlD = new ClaXmlD();

                xmlD.Modify(pCOMMAND[2], getNumber.ToString(CultureInfo.InvariantCulture));
            }
        }

        private int fnLDPAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            ClaXmlUpper xmlUpper = new ClaXmlUpper();

            string statusOld = xmlUpper.ReadCell(pCOMMAND[1]);

            var iObjIndex = 0;

            iObjIndex = GetNumber(pCOMMAND[1]);

            switch (pCOMMAND[1][0])
            {
                case 'X':
                    if ((statusOld == "false") && thePLCCore.pInPut[iObjIndex].status==true)
                    {
                        pPLC_OBJ[piStackDepth].status = true;

                        xmlUpper.Modify(pCOMMAND[1], thePLCCore.pInPut[iObjIndex].status.ToString());
                    }
                    else
                    {
                        pPLC_OBJ[piStackDepth].status = false;

                        xmlUpper.Modify(pCOMMAND[1], thePLCCore.pInPut[iObjIndex].status.ToString());
                    }
                    break;
                case 'Y':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pOutPut[iObjIndex]);
                    break;
                case 'M':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pAUX[iObjIndex]);
                    break;
                case 'S':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pSTATUS[iObjIndex]);
                    break;
                case 'T':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pTimer[iObjIndex]);
                    break;
                case 'C':
                    pPLC_OBJ[piStackDepth - 1] = ~(pPLC_OBJ[piStackDepth - 1] * thePLCCore.pCounter[iObjIndex]);
                    break;
                default:
                    return -1;
            }
            return 0;
        }

        private int fnDSUBAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            if (pPLC_OBJ[piStackDepth - 1].status)
            {
                var xmlD = new ClaXmlD();

                double getNumbera = 0;

                if (pCOMMAND[1][0] == 'D')
                {
                    string s = xmlD.ReadCell(pCOMMAND[1]);

                    getNumbera = Convert.ToDouble(s);
                }
                else
                {
                    getNumbera = GetNumberKDTC(pCOMMAND[1]);
                }

                double getNumberb = 0;

                if (pCOMMAND[2][0] == 'D')
                {
                    getNumberb = Convert.ToDouble(xmlD.ReadCell(pCOMMAND[2]));
                }
                else
                {
                    getNumberb = GetNumberKDTC(pCOMMAND[2]);
                }
                var numberC = getNumbera - getNumberb;

                xmlD.Modify(pCOMMAND[3], numberC.ToString(CultureInfo.InvariantCulture));

                return 0;
            }
            return 0;
        }

        private int fnDADDAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            if (pPLC_OBJ[piStackDepth - 1].status)
            {
                var xmlD = new ClaXmlD();

                double getNumbera = 0;

                if (pCOMMAND[1][0] == 'D')
                {
                    string s = xmlD.ReadCell(pCOMMAND[1]);

                    getNumbera = Convert.ToDouble(s);
                }
                else
                {
                    getNumbera = GetNumberKDTC(pCOMMAND[1]);
                }

                double getNumberb = 0;

                if (pCOMMAND[2][0] == 'D')
                {
                    getNumberb = Convert.ToDouble(xmlD.ReadCell(pCOMMAND[2]));
                }
                else
                {
                    getNumberb = GetNumberKDTC(pCOMMAND[2]);
                }
                var numberC = getNumbera + getNumberb;

                xmlD.Modify(pCOMMAND[3], numberC.ToString(CultureInfo.InvariantCulture));

                return 0;
            }
            return 0;
        }

        private int fnDMULAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            if (pPLC_OBJ[piStackDepth - 1].status)
            {
                var xmlD = new ClaXmlD();

                double getNumbera = 0;

                if (pCOMMAND[1][0] == 'D')
                {
                    string s = xmlD.ReadCell(pCOMMAND[1]);

                    getNumbera = Convert.ToDouble(s);
                }
                else
                {
                    getNumbera = GetNumberKDTC(pCOMMAND[1]);
                }

                double getNumberb = 0;

                if (pCOMMAND[2][0] == 'D')
                {
                    getNumberb = Convert.ToDouble(xmlD.ReadCell(pCOMMAND[2]));
                }
                else
                {
                    getNumberb = GetNumberKDTC(pCOMMAND[2]);
                }
                var numberC = getNumbera*getNumberb;

                xmlD.Modify(pCOMMAND[3], numberC.ToString(CultureInfo.InvariantCulture));

                return 0;
            }
            return 0;
        }

        private int fnDIVRAndCompute(string[] pCOMMAND, cla_stPLCcs.stPLC_OBJ[] pPLC_OBJ, ref int piStackDepth)
        {
            if (pPLC_OBJ[piStackDepth - 1].status)
            {
                var xmlD = new ClaXmlD();

                double getNumbera = 0;

                if (pCOMMAND[1][0] == 'D')
                {
                    string s = xmlD.ReadCell(pCOMMAND[1]);

                    getNumbera = Convert.ToDouble(s);
                }
                else
                {
                    getNumbera = GetNumberKDTC(pCOMMAND[1]);
                }

                double getNumberb = 0;

                if (pCOMMAND[2][0] == 'D')
                {
                    getNumberb = Convert.ToDouble(xmlD.ReadCell(pCOMMAND[2]));
                }
                else
                {
                    getNumberb = GetNumberKDTC(pCOMMAND[2]);
                }
                var numberC = getNumbera/getNumberb;

                xmlD.Modify(pCOMMAND[3], numberC.ToString(CultureInfo.InvariantCulture));

                return 0;
            }return 0;
        }

        private static int GetNumber(string par)
        {
            string strTempContent = par;

            strTempContent = Regex.Replace(strTempContent, @"[^\d]*", "");

            return Convert.ToInt32(strTempContent);
        }

        private double GetNumberKDTC(string par)
        {
            //ClaXmlDM xmlDm = new ClaXmlDM();
            if (Regex.Match(par, @"^\d.+$").Success)
            {
                return Convert.ToDouble(par);
            }

            string strTempContent = par;

            switch (strTempContent[0])
            {
                case 'K':
                    return GetNumber(strTempContent);
                case 'T':
                    return thePLCCore.pTimer[GetNumber(strTempContent)].limit;
                case 'C':
                    return thePLCCore.pCounter[GetNumber(strTempContent)].count;
                default:
                    return -1;
            }
        }

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        void timerRoutine(object sender, EventArgs e)
        {
            var i = 0;

            for (i = 0; i < MAX_NUM_TIMER; i++)
            {
                if (thePLCCore.pTimer[i].status)
                {
                    //          MessageBox.Show(i.ToString());
                    if (thePLCCore.pTimer[i].clock == thePLCCore.pTimer[i].limit)
                    {
                        thePLCCore.pTimer[i].timeup = true;
                    }
                    else
                    {
                        thePLCCore.pTimer[i].clock += 100;
                    }
                }
                else
                {
                    thePLCCore.pTimer[i].timeup = false;

                    thePLCCore.pTimer[i].clock = 0;
                }
            }
        }

        void fnFlowCtrlThread()
        {
            var cClearStack = ' ';

            var iTokenStartPos = 0;

            var iStackDepth = 0;

            var iParseProgOffset = 0;

            var iTempBufferLength = 0;

            var iRet = 0;

            var bProgEnd = false;

            var sCOMMAND = new string[5];

            var pTempBuffer = "";

            var pPLC_OBJ = new cla_stPLCcs.stPLC_OBJ[MAX_LADDER_DEPTH];

            for (var i = 0; i < MAX_LADDER_DEPTH; i++)
            {
                pPLC_OBJ[i] = new cla_stPLCcs.stPLC_OBJ();
            }

            var txt = new cla_TXT();

            thePLCCore.fnRESETAll();

            thePLCCore.fnWriteOutput();

            pMEMBUF = txt.Read_txt(sLadderFilePath);   //读取LD X000OUT Y000END 进入pMEMBUF

            while (thePLCCore.bRUN)
            {
                iStackDepth = 0;

                iParseProgOffset = 0;

                cClearStack = '0';
                /* 弄 Input 篈弄癘拘砰い */
                fnReadInput();
                do
                {
                    FnReadM();

                    iTokenStartPos = iParseProgOffset;
                    /* 眔 Ladder 祘Α琿 such as LD X000だ琿よΑ琌 0x0d,0x0a 醚才腹 */
                    bProgEnd = thePLCCore.GetProgToken(ref iParseProgOffset);

                    if (bProgEnd)
                    {
                        var strTemp1 = pMEMBUF.Substring(iTokenStartPos, (pMEMBUF.Length - iTokenStartPos));

                        var strTemp2 = strTemp1.Split('\r');

                        pTempBuffer = strTemp2[0];

                        iParseProgOffset += 1;

                        iTempBufferLength = pTempBuffer.Length;

                        /* 盢 LD X000 ち澄Θ aa[0] = LD,aa[1] = X000 */

                        sCOMMAND = pTempBuffer.Split(' ');
                        /* 亩 Ladder Prog Command */
                        // LD
                        if ((sCOMMAND[0][0] == 'L') && (sCOMMAND[0][1] == 'D') && (sCOMMAND[0].Length==2))
                        {
                            if (cClearStack == 0x01)
                            {
                                iStackDepth--;

                                cClearStack = Convert.ToChar(0x00);
                            }
                            iRet = thePLCCore.fnLDAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);
                            iStackDepth++;
                        }// LDI
                        else if ((sCOMMAND[0][0] == 'L') && (sCOMMAND[0][1] == 'D') && (sCOMMAND[0][2] == 'I'))
                        {
                            iRet = thePLCCore.fnLDNotAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            iStackDepth++;
                        }// CMP
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        else if ((sCOMMAND[0][0] == 'C') && (sCOMMAND[0][1] == 'M') && (sCOMMAND[0][2] == 'P'))
                        {iRet = thePLCCore.fnCMPAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);
                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        }// DMOV
                        else if ((sCOMMAND[0][0] == 'D') && (sCOMMAND[0][1] == 'M') && (sCOMMAND[0][2] == 'O') && (sCOMMAND[0][3] == 'V'))
                        {
                            thePLCCore.fnMOVAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);
                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        }// LDP
                        else if ((sCOMMAND[0][0] == 'L') && (sCOMMAND[0][1] == 'D') && (sCOMMAND[0][2] == 'P'))
                        {
                            if (cClearStack == 0x01)
                            {
                                iStackDepth--;

                                cClearStack = Convert.ToChar(0x00);
                            }
                            thePLCCore.fnLDPAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            iStackDepth++;
                            
                        }// DSUB
                        else if ((sCOMMAND[0][0] == 'D') && (sCOMMAND[0][1] == 'S') && (sCOMMAND[0][2] == 'U') && (sCOMMAND[0][3] == 'B'))
                        {
                            thePLCCore.fnDSUBAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);}// DIVR
                        else if ((sCOMMAND[0][0] == 'D') && (sCOMMAND[0][1] == 'I') && (sCOMMAND[0][2] == 'V') &&(sCOMMAND[0][3] == 'R'))
                        {
                            thePLCCore.fnDIVRAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        }// DADD
                        else if ((sCOMMAND[0][0] == 'D') && (sCOMMAND[0][1] == 'A') && (sCOMMAND[0][2] == 'D') && (sCOMMAND[0][3] == 'D'))
                        {
                            thePLCCore.fnDADDAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        }// DMUL
                        else if ((sCOMMAND[0][0] == 'D') && (sCOMMAND[0][1] == 'M') && (sCOMMAND[0][2] == 'U') && (sCOMMAND[0][3] == 'L'))
                        {
                            thePLCCore.fnDMULAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        }
 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                           
                            // AND
                        else if ((sCOMMAND[0][0] == 'A') && (sCOMMAND[0][1] == 'N') && (sCOMMAND[0][2] == 'D'))
                        {
                            iRet = thePLCCore.fnANDAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);
                        } // ANI
                        else if ((sCOMMAND[0][0] == 'A') && (sCOMMAND[0][1] == 'N') && (sCOMMAND[0][2] == 'I'))
                        {
                            iRet = thePLCCore.fnANDNotAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);
                        } // OR
                        else if ((sCOMMAND[0][0] == 'O') && (sCOMMAND[0][1] == 'R') && (sCOMMAND[0].Length == 2))
                        {
                            iRet = thePLCCore.fnORAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);
                        } // ORI
                        else if ((sCOMMAND[0][0] == 'O') && (sCOMMAND[0][1] == 'R') && (sCOMMAND[0][2] == 'I'))
                        {
                            iRet = thePLCCore.fnORNotAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);
                        } // ANB
                        else if ((sCOMMAND[0][0] == 'A') && (sCOMMAND[0][1] == 'N') && (sCOMMAND[0][2] == 'B'))
                        {
                            iRet = thePLCCore.fnANBAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            iStackDepth--;
                        } // ORB
                        else if ((sCOMMAND[0][0] == 'O') && (sCOMMAND[0][1] == 'R') && (sCOMMAND[0][2] == 'B'))
                        {
                            iRet = thePLCCore.fnORBAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            iStackDepth--;
                        } // SET
                        else if ((sCOMMAND[0][0] == 'S') && (sCOMMAND[0][1] == 'E') && (sCOMMAND[0][2] == 'T'))
                        {
                            iRet = thePLCCore.fnSETAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        } // RST
                        else if ((sCOMMAND[0][0] == 'R') && (sCOMMAND[0][1] == 'S') && (sCOMMAND[0][2] == 'T'))
                        {
                            thePLCCore.fnRSTAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        } // OUT
                        else if ((sCOMMAND[0][0] == 'O') && (sCOMMAND[0][1] == 'U') && (sCOMMAND[0][2] == 'T'))
                        {
                            thePLCCore.fnOUTAndCompute(sCOMMAND, pPLC_OBJ, ref iStackDepth);

                            if (iStackDepth > 0) cClearStack = Convert.ToChar(0x01);
                        }
                        else if ((sCOMMAND[0][0] == 'E') && (sCOMMAND[0][1] == 'N') && (sCOMMAND[0][2] == 'D'))
                        {
                            ;
                        }
                        else
                        {
                            ;
                        }

                        FnWriteM();
                    }
                } while (bProgEnd);

                thePLCCore.fnWriteOutput();

                Thread.Sleep(60);
            }

            thePLCCore.fnRESETAll();

            thePLCCore.fnWriteOutput();
        }
    }
}
