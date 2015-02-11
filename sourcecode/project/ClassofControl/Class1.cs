using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ClassofControl
{
    public enum DealState
    {
        空      = 0,
        买入股票 = 1,
        卖出股票 = 2,
        融资买入 = 3,
        融券卖出 = 4,
        卖券还款 = 5,
        买券还券 = 6
    }

    public enum SelectState
    {
        担保物买入 = 1,
        担保物卖出 = 2,
        融资买入 = 3,
        融券卖出 = 4,
        卖券还款 = 5,
        买券还券 = 6,
    }

    public class InputStock
    {
        public string Code
        { get; set; }
        public double Price
        { get; set; }
        public int Amount
        { get; set; }
        public DealState State
        { get; set; }
    }
    public class OutputStock
    {
        public string Code
        { get; set; }
        public double Price
        { get; set; }
        public int Amount
        { get; set; }
        public DealState State
        { get; set; }
    }

    public interface IControl
    {
        int AutoBuy(InputStock stockCode);//自动交易
        OutputStock AutoGetInfo();
        int AutoCancel(int state);
        bool AutoSelect(List<IntPtr> tv,string s);//自动选择treeview
        List<IntPtr> GetTreeViewInfo();//获取treeview节点句柄
        void SetWindow(IntPtr hwnd);
        IntPtr GetWindowHwnd(string s);
    }
    public class Auto : IControl
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessager(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageGetText(IntPtr hWnd, uint msg, UIntPtr wParam, StringBuilder lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private extern static int SendMessageGetTextLength(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        private static extern void SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll ", EntryPoint = "GetDlgItem", SetLastError = true)]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        const int VK_F3 = 0x72;
        const uint BM_CLICK = 0xF5; //鼠标点击的消息，对于各种消息的数值，大家还是得去API手册
        const uint WM_SETTEXT = 0x000C;
        const uint WM_KEYDOWN = 0x0100;
        const uint WM_KEYUP = 0x0101;
        const uint WM_CHAR = 0x0102;
        const uint WM_LBUTTONDOWN = 0x201;
        const uint WM_LBUTTONUP = 0x202;
        const int WM_GETTEXT = 0x0D;
        const int WM_GETTEXTLENGTH = 0x0E;
        const int BM_GETCHECK = 0x00F0;
        const int BM_SETCHECK = 0x00F1;
        const int BM_GETSTATE = 0x00F2;

        const int TVGN_ROOT = 0;
        const int TVGN_NEXT = 1;
        const int TVGN_CHILD = 4;
        const int TVGN_CARET = 9;
        const int TVGN_NEXTVISIBLE = 6;
        const int TV_FIRST = 0x1100;
        const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        const int TVM_EXPAND = (TV_FIRST + 2);
        const int TVM_SELECTITEM = (TV_FIRST + 11);
        const int TVE_EXPAND = 2;

        static IntPtr FindWindow(string processName)
        {
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            if (arrayProcess.Length > 0)
                return arrayProcess[0].MainWindowHandle;
            else
                return IntPtr.Zero;
        }

        private bool check(InputStock stockCode)
        {
            if (stockCode.Code != null && stockCode.Price != 0 && stockCode.Amount != 0 && stockCode.State != 0) 
                return true;
            else 
                return false;
        }

        private string getstring(IntPtr hwnd)
        {
            int len = SendMessageGetTextLength(hwnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
            StringBuilder s = new StringBuilder(len);
            SendMessageGetText(hwnd, WM_GETTEXT, new UIntPtr((uint)len), s);
            return s.ToString();
        }

        /*******************************************************************************************/
        private int ControlqrMsg(IntPtr hwndmsg,IntPtr mainhwnd)
        {
            int n = 0;
            IntPtr msgyes = GetDlgItem(hwndmsg, 0x00000006);
            if (msgyes != IntPtr.Zero)
                PostMessage(msgyes, BM_CLICK, 0, 0);
            else
                return 1;
            System.Threading.Thread.Sleep(500);
            IntPtr msg = GetLastActivePopup(mainhwnd);
            while (msg == IntPtr.Zero)
            {
                n++;
                if (n > 5) break;
                System.Threading.Thread.Sleep(1000);
                msg = GetLastActivePopup(mainhwnd);
            }
            if(msg==IntPtr.Zero)
                return 10;
            IntPtr msgstring = GetDlgItem(msg, 0x000003EC);
            msgyes = GetDlgItem(msg, 0x00000002);
            if (msgstring != IntPtr.Zero && msgyes != IntPtr.Zero)
            {
                string s = getstring(msgstring);
                if (s.IndexOf("您的") != -1)
                {
                    PostMessage(msgyes, BM_CLICK, 0, 0);
                    return 0;
                }
                else
                {
                    if (msgyes != IntPtr.Zero)
                        PostMessage(msgyes, BM_CLICK, 0, 0);
                    return 5;
                }
            }
            else
            {
                if (msgyes != IntPtr.Zero)
                    PostMessage(msgyes, BM_CLICK, 0, 0);
                return 3;
            }
        }

        private int ControltsxxMsg(IntPtr hwndmsg, IntPtr mainhwnd)
        {
            IntPtr msgstring = GetDlgItem(hwndmsg, 0x00000410);
            if (msgstring != IntPtr.Zero)
            {
                string s = getstring(msgstring);
                if (s.IndexOf("委托价格") != -1)
                {
                    IntPtr msgyes = GetDlgItem(hwndmsg, 0x00000006);
                    if (msgyes != IntPtr.Zero)
                        PostMessage(msgyes, BM_CLICK, 0, 0);
                    else
                        return 1;
                    System.Threading.Thread.Sleep(500);
                    IntPtr msg = GetLastActivePopup(mainhwnd); //弹出窗口查找句柄
                    if (msg == IntPtr.Zero)
                        return 10;
                    IntPtr msgtitle = GetDlgItem(msg, 0x00000555);
                    if (msgtitle != IntPtr.Zero)
                    {
                        s = getstring(msgtitle);
                        if (s.IndexOf("委托确认") != -1)
                        {
                            return ControlqrMsg(msg, mainhwnd);
                        }
                        else return 5;
                    }
                    else
                        return 2;
                }
                else if (s.IndexOf("您输入的价格已超出涨跌停限制") != -1)
                {
                    IntPtr msgno = GetDlgItem(hwndmsg, 0x00000007);
                    if (msgno != IntPtr.Zero)
                    {
                        PostMessage(msgno, BM_CLICK, 0, 0);
                        return 4;
                    }
                    else
                        return 1;
                }
                else
                    return 5;//其他提示信息
            }
            else
                return 2;
        }

        private int ControltsMsg(IntPtr hwndmsg, IntPtr mainhwnd)
        {

            IntPtr msgyes = GetDlgItem(hwndmsg, 0x00000002);
            if (msgyes != IntPtr.Zero)
            {
                PostMessage(msgyes, BM_CLICK, 0, 0);
                return 0;
            }
            else
                return 1;
        }
        /*******************************************************************************************/

        /*****************************************************
         * 函数功能：自动下单
         * 输入： InputStock：
         *          股票代码，价格，数量，交易方式
         * 输出： return 0 成功操作
                  return 1 没找到button
                  return 2 没有找到msg标题
                  return 3 没找到button与text
                  return 4 价格输入出错
                  return 5 msg标题没有匹配的操作
                  return 6 tv选择与操作不匹配
                  return 7 输入stock错误
                  return 8 没有找到相应窗体
                  return 9 没有找到下单程序   
                  return 10 没有获取弹出窗口
        ******************************************************/
        public int AutoBuy(InputStock stockCode)
        {
            if (!check(stockCode)) return 7;
            IntPtr hwnd = FindWindow("xiadan");
            if (hwnd != IntPtr.Zero)
            {
                SetForegroundWindow(hwnd);
                IntPtr frame = FindWindowEx(hwnd, 0, "AfxMDIFrame42s", null);
                IntPtr dlg = GetDlgItem(frame, 0x0000E901);
                IntPtr dlgTitle = GetDlgItem(dlg, 0x000005C6);

                if (dlgTitle != IntPtr.Zero)
                {
                    string s = getstring(dlgTitle);
                    if (s.IndexOf(stockCode.State.ToString()) == -1)
                        return 6;
                    IntPtr stcokCodeBox = GetDlgItem(dlg, 0x00000408);
                    IntPtr priceBox = GetDlgItem(dlg, 0x00000409);
                    IntPtr numberBox = GetDlgItem(dlg, 0x0000040A);
                    SendMessage(stcokCodeBox, WM_SETTEXT, 0, stockCode.Code);
                    SendMessage(priceBox, WM_SETTEXT, 0, stockCode.Price.ToString());
                    SendMessage(numberBox, WM_SETTEXT, 0, stockCode.Amount.ToString());
                    IntPtr dlgok = GetDlgItem(dlg, 0x000003EE);
                    if (dlgok != IntPtr.Zero)
                        PostMessage(dlgok, BM_CLICK, 0, 0);
                    else
                        return 1;
                    System.Threading.Thread.Sleep(500);
                    IntPtr msg = GetLastActivePopup(hwnd); //弹出窗口查找句柄
                    if (msg == IntPtr.Zero)
                        return 10;
                    IntPtr msgtitle = GetDlgItem(msg, 0x00000555);
                    if (msgtitle != IntPtr.Zero)
                    {
                       string ss = getstring(msgtitle);
                       if (ss.IndexOf("委托确认") != -1)
                       {
                            return ControlqrMsg(msg, hwnd);
                       }
                       else if (ss.IndexOf("提示信息") != -1)
                       {
                           return ControltsxxMsg(msg, hwnd);
                       }
                       else if (ss.IndexOf("提示") != -1)
                       {
                           return ControltsMsg(msg, hwnd);
                       }
                       else
                       {
                            return 5;
                       }
                    }
                    else
                    {
                            return 2;
                    }
                }
                else
                {
                    return 8;
                }
            }
            else
            {
                return 9;
            }
        }

        private IntPtr getpopdlg(IntPtr hwnd)
        {
            return GetLastActivePopup(hwnd);
        }

        /*****************************************************
         * 函数功能：自动撤单
         * 输入： state 0 撤买
                  state 1 撤卖
         * 输出： 0 成功
         *        1 无法获取xiadan软件句柄
         *        2 无法获取弹出对话框的确定按钮
         *        3 无法获得弹出对话框，可能无单可撤
        ******************************************************/
        public int AutoCancel(int state)
        {
            IntPtr hwnd = FindWindow("xiadan");
            if (hwnd != IntPtr.Zero)
            {
                //PostMessage(hwnd, WM_KEYDOWN, VK_F3, 0);
                //PostMessage(hwnd, WM_KEYUP, VK_F3, 0);
                SetForegroundWindow(hwnd);
                IntPtr frame = FindWindowEx(hwnd, 0, "AfxMDIFrame42s", null);
                IntPtr dlg = GetDlgItem(frame, 0x0000E901);

                IntPtr checkbox = GetDlgItem(dlg, 0x00000DDF);
                IntPtr f5 = GetDlgItem(dlg, 0x00008016);
                IntPtr button=IntPtr.Zero;
                if (state == 0) button = GetDlgItem(dlg, 0x00007532);
                else if(state ==1) button = GetDlgItem(dlg, 0x00007533);

                IntPtr dlg1 = GetDlgItem(dlg, 0x00000417);
                IntPtr dlg2 = GetDlgItem(dlg1, 0x000000C8);
                IntPtr GridCtrl = GetDlgItem(dlg2, 0x00000417);
                if (GridCtrl != IntPtr.Zero && checkbox != IntPtr.Zero && f5 != IntPtr.Zero && button != IntPtr.Zero)
                {
                    PostMessage(GridCtrl, WM_KEYDOWN, 0x52, 1);
                    PostMessage(GridCtrl, WM_KEYUP, 0x52, 1);
                    System.Threading.Thread.Sleep(100);
                    if (SendMessager(checkbox, BM_GETCHECK, 0, 0) != 1)
                    {
                        SendMessager(checkbox, BM_CLICK, 0, 0);
                        System.Threading.Thread.Sleep(300);
                    }
                    //else
                    //{
                    //    PostMessage(f5, BM_CLICK, 0, 0);
                    //    System.Threading.Thread.Sleep(1000);
                    //}
                    PostMessage(button, BM_CLICK, 0, 0);
                    
                    System.Threading.Thread.Sleep(500);
                    IntPtr msg = getpopdlg(hwnd); //弹出窗口查找句柄
                    IntPtr msgtitle = GetDlgItem(msg, 0x00000410);
                    if (msgtitle != IntPtr.Zero)
                    {
                        string s = getstring(msgtitle);
                        if (s.IndexOf("您确认要撤销这") != -1)
                        {
                            IntPtr ok = GetDlgItem(msg, 0x00000006);
                            if (ok != IntPtr.Zero)
                            {
                                PostMessage(ok, BM_CLICK, 0, 0);
                                return 0;
                            }
                            else
                                return 2;
                        }
                    }
                    else
                        return 3;
                }
            }
            return 1;
        }
        /*****************************************************
         * 函数功能：获取treeview信息
         * 输入： 无
         * 输出： List<IntPtr>
         * list的个数为7，前6个为需要控制的6个节点的句柄，第7个为treeview的的句柄
         * 如果个数不为7，则判定为获取失败
        ******************************************************/
        public List<IntPtr> GetTreeViewInfo()
        {
            bool flag = true;
            List<IntPtr> p = new List<IntPtr>();
            IntPtr hwnd = FindWindow("xiadan");
            if (hwnd != IntPtr.Zero)
            {
                IntPtr frame = FindWindowEx(hwnd, 0, "AfxMDIFrame42s", null);
                IntPtr frame1 = FindWindowEx(frame, 0, "AfxWnd42s", null);
                IntPtr frame2 = GetDlgItem(frame1, 0x00000201);
                IntPtr frame3 = GetDlgItem(frame2, 0x000000C8);
                IntPtr hTreeWnd = GetDlgItem(frame3, 0x00000201);
                if (hTreeWnd != IntPtr.Zero)
                {
                    IntPtr hRootItem = SendMessage(hTreeWnd, TVM_GETNEXTITEM, TVGN_ROOT, 0);
                    SendMessage(hTreeWnd, TVM_EXPAND, TVE_EXPAND, hRootItem);
                    IntPtr hChildItem = hRootItem;

                    for (int i = 0; i < 6; i++)
                    {
                        hChildItem = SendMessage(hTreeWnd, TVM_GETNEXTITEM, TVGN_NEXTVISIBLE, hChildItem);
                        if (hChildItem == IntPtr.Zero)
                        {
                            flag = false;
                            break;
                        }
                        SendMessage(hTreeWnd, TVM_SELECTITEM, TVGN_CARET, hChildItem);
                        p.Add(hChildItem);
                    }
                    if (flag)
                    {
                        p.Add(hTreeWnd);
                        return p;
                    }
                    else
                        return p;
                }
                else
                    return p;
            }
            else
                return p;
        }
        /*****************************************************
         * 函数功能：自动选择treeview对应string的节点
         * 输入：tv为获取的treeviewinfo，格式与上面对应
         *       s 为需要选择节点的字符串内容
         * 输出：true 成功
         *       false 失败
        ******************************************************/
        public bool AutoSelect(List<IntPtr> tv, string s)
        {
            int t = (int)(SelectState)Enum.Parse(typeof(SelectState), s, false)-1;
            if (tv.Count == 7 && t >= 0 && t <= 5)
            {
                if (SendMessage(tv[6], TVM_SELECTITEM, TVGN_CARET, tv[t]) != IntPtr.Zero)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public OutputStock AutoGetInfo()
        {
            OutputStock t = new OutputStock();
            return t;
        }
        public void SetWindow(IntPtr hwnd)
        {
            SetForegroundWindow(hwnd);
        }
        public IntPtr GetWindowHwnd(string s)
        {
            return FindWindow(null, s);
        }
    }
}
