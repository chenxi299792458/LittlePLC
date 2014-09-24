namespace MyPLC
{
    class cla_stPLCcs
    {
        private const int MAX_LADDER_DEPTH = 5;
        private const int MAX_NUM_INPUT = 8;
        private const int MAX_NUM_OUTPUT = 8;
        private const int MAX_NUM_AUX = 500;
        private const int MAX_NUM_STATUS = 500;
        private const int MAX_NUM_TIMER = 200;
        private const int MAX_NUM_COUNTER = 100;
        private const int MAX_NUM_REGISTER = 200;
        private const int MAX_PROG_SIZE = 4096000;

        //private const string TRACE_PATH = "C:\\plccore.log";
        //private const string LADDER_PATH = "C:\\123.lad";

        private const int IS_INPUT = 1;
        private const int IS_OUTPUT = 2;
        private const int IS_AUX = 3;
        private const int IS_STATUS = 4;
        private const int IS_TIMER = 5;
        private const int IS_COUNTER = 6;
        private const int IS_REGISTER = 7;

        public class stPLC_OBJ
        {
            public stPLC_OBJ()
            {
                iClassType = IS_AUX;

                status = true;
            }
            public bool status;

            public int iClassType;

            public int TestIsKindOf(stPLC_OBJ a)
            {
                return a.iClassType;
            }

            public static stPLC_OBJ operator ~(stPLC_OBJ b)
            {
                var stT = (b.iClassType == 5) ? (stTimer)b : null;

                var stC = (b.iClassType == 6) ? (stCounter)b : null;

                if ((b.iClassType != IS_TIMER) && (b.iClassType != IS_COUNTER))

                    return new stPLC_OBJ { status = b.status };

                if (b.iClassType == IS_TIMER)

                    return new stPLC_OBJ { status = stT != null && (b.status & stT.timeup) };

                if (b.iClassType == IS_COUNTER)

                    return new stPLC_OBJ { status = stC != null && (b.status & stC.fire) };

                return new stPLC_OBJ { status = false };
            }

            public static stPLC_OBJ operator !(stPLC_OBJ b)
            {
                var _stPLC_OBJ = new stPLC_OBJ();

                var stT = (b.iClassType == 5) ? (stTimer)b : null;

                var stC = (b.iClassType == 6) ? (stCounter)b : null;

                switch (b.iClassType)
                {
                    case IS_INPUT:
                    case IS_OUTPUT:
                    case IS_AUX:
                    case IS_STATUS:
                        if (b.status)
                            _stPLC_OBJ.status = false;
                        else
                            _stPLC_OBJ.status = true;
                        break;
                    case IS_TIMER:
                        if (b.status && stT.timeup)

                            _stPLC_OBJ.status = false;

                        else
                            _stPLC_OBJ.status = true;

                        break;
                    case IS_COUNTER:
                        if (b.status && stC.fire)

                            _stPLC_OBJ.status = false;
                        else

                            _stPLC_OBJ.status = true;

                        break;
                }
                return _stPLC_OBJ;
            }

            public static stPLC_OBJ operator +(stPLC_OBJ a, stPLC_OBJ b)
            {
                var _stPLC_OBJ = new stPLC_OBJ();

                var stT = (b.iClassType == 5) ? (stTimer)b : null;

                var stC = (b.iClassType == 6) ? (stCounter)b : null;

                if ((a.iClassType != IS_TIMER) && (a.iClassType != IS_COUNTER))
                {
                    switch (a.iClassType)
                    {
                        case IS_INPUT:
                        case IS_OUTPUT:
                        case IS_AUX:
                        case IS_STATUS:
                            if (a.status || b.status)
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                            break;
                        case IS_TIMER:
                            if (a.status || (b.status && stT.timeup))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                            break;
                        case IS_COUNTER:
                            if (a.status || (b.status && stC.fire))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                            break;
                    }
                }
                else
                {
                    if (a.iClassType == IS_TIMER)
                    {
                        var d = (stTimer)a;
                        if (a.iClassType == IS_TIMER)
                        {
                            if ((d.status && d.timeup) || (b.status && stT.timeup))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                        else
                        {
                            if ((d.status && d.timeup) || (b.status && stC.fire))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                    }
                    if (a.iClassType == IS_COUNTER)
                    {
                        var d = (stCounter)a;
                        if (a.iClassType == IS_TIMER)
                        {
                            if ((d.status && d.fire) || (b.status && stT.timeup))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                        else
                        {
                            if ((d.status && d.fire) || (b.status && stC.fire))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                    }
                }
                return _stPLC_OBJ;
            }

            public static stPLC_OBJ operator *(stPLC_OBJ a, stPLC_OBJ b)
            {
                var _stPLC_OBJ = new stPLC_OBJ();
                var stT = (b.iClassType == 5) ? (stTimer)b : null;
                var stC = (b.iClassType == 6) ? (stCounter)b : null;
                if ((a.iClassType != IS_TIMER) && (a.iClassType != IS_COUNTER))
                {
                    switch (a.iClassType)
                    {
                        case IS_INPUT:
                        case IS_OUTPUT:
                        case IS_AUX:
                        case IS_STATUS:
                            if (a.status && b.status)
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                            break;
                        case IS_TIMER:
                            if (a.status && (b.status && stT.timeup))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                            break;
                        case IS_COUNTER:
                            if (a.status && (b.status && stC.fire))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                            break;
                    }
                }
                else
                {
                    if (a.iClassType == IS_TIMER)
                    {
                        var d = (stTimer)a;
                        if (a.iClassType == IS_TIMER)
                        {
                            if ((d.status && d.timeup) && (b.status && stT.timeup))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                        else
                        {
                            if ((d.status && d.timeup) && (b.status && stC.fire))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                    }
                    if (a.iClassType == IS_COUNTER)
                    {
                        var d = (stCounter)a;
                        if (a.iClassType == IS_TIMER)
                        {
                            if ((d.status && d.fire) && (b.status && stT.timeup))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                        else
                        {
                            if ((d.status && d.fire) && (b.status && stC.fire))
                                _stPLC_OBJ.status = true;
                            else
                                _stPLC_OBJ.status = false;
                        }
                    }
                }
                return _stPLC_OBJ;
            }


            /* 0:off,1:on */

            public class stInPut : stPLC_OBJ
            {
                public stInPut()
                {
                    this.status = false;
                    this.iClassType = IS_INPUT;
                }
            }

            public class stOutPut : stPLC_OBJ
            {
                public stOutPut()
                {
                    status = false;
                    iClassType = IS_OUTPUT;
                }
            }

            public class stAUX : stPLC_OBJ
            {
                public stAUX()
                {
                    this.status = false;
                    this.iClassType = IS_AUX;
                }
            }

            public class stSTATUS : stPLC_OBJ
            {
                public stSTATUS()
                {
                    status = false;
                    iClassType = IS_STATUS;
                }
            }

            public class stTimer : stPLC_OBJ
            {
                public stTimer()
                {
                    status = false;
                    limit = 0;
                    clock = 0;
                    timeup = false;
                    iClassType = IS_TIMER;
                }
                public int limit;
                public int clock;
                public bool timeup;
            }

            public class stCounter : stPLC_OBJ
            {
                public stCounter()
                {
                    status = false;
                    limit = 0;
                    count = 0;
                    fire = false;/*filpflop=0;*/
                    iClassType = IS_COUNTER;
                }
                public int limit;
                public int count;
                public bool fire;
                public bool filpflop;
            }

            public class stRegister : stPLC_OBJ
            {
                public stRegister()
                {
                    status = false;
                    iClassType = IS_REGISTER;
                }
                public int number = 0;
            }
        }
    }
}
