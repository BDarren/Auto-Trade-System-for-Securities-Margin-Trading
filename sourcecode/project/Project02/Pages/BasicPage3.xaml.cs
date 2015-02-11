using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassofConnect;
using ClassofControl;
using System.Data;
using System.Windows.Threading;
using System.Windows.Interop;
using System.Threading;
using FirstFloor.ModernUI.Windows.Controls;
using Project02.Content;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Project02.Pages
{
    /// <summary>
    /// Interaction logic for BasicPage3.xaml
    /// </summary>
    public partial class BasicPage3 : UserControl
    {
        #region Properties
        public static List<string> StockList { get; set; }
        public string SelectedStoke { get; set; }
        #endregion

        static string ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        List<IntPtr> tv = new List<IntPtr>();
        IntPtr thishwnd = new IntPtr();
        static Thread Autokaicang;
        static Thread Autopingcang;
        static Thread Autoupdate;
        static Thread autowatch;
        static int lv2num;
        string searchcode;
        static setdata settings = new setdata();

        public class setdata
        {
            public double yjfl { get; set; }
            public double yhsl { get; set; }
            public double rzlx { get; set; }
            public double rqlx { get; set; }
            public int rzts { get; set; }
            public int rqts { get; set; }
        }

        public class listitem : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private string name;
            private string price;
            private string amount;
            public string Name
            {
                get { return name; }
                set
                {
                    if (value == name)
                        return;
                    else
                    {
                        name = value;
                        this.Changed("Name");
                    }
                }
            }
            public string Price
            {
                get { return price; }
                set
                {
                    if (value == price)
                        return;
                    else
                    {
                        price = value;
                        this.Changed("Price");
                    }
                }
            }
            public string Amount
            {
                get { return amount; }
                set
                {
                    if (value == amount)
                        return;
                    else
                    {
                        amount = value;
                        this.Changed("Amount");
                    }
                }
            }
            private void Changed(string PropertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        public static ObservableCollection<listitem> listitems = new ObservableCollection<listitem>();

        public class historyitem
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public double Price { get; set; }
            public int Amount { get; set; }
            public string Time { get; set; }
            public SelectState State { get; set; }
            public historyitem(InputStock t)
            {
                Code = t.Code;
                Price = t.Price;
                Amount = t.Amount;
                State = (SelectState)t.State;
                Time = DateTime.Now.ToString();
                Name = StockList.Find(
                delegate(string s)
                {
                    if (s.IndexOf(Code) != -1)
                        return true;
                    else
                        return false;
                }).Substring(7);
            }
            public historyitem()
            {
                Code = null;
            }
        }

        /*****************************************************
          * 函数功能：计算盈利
          * 输入： price 购买价格
          *        current 当前价格
          *        amount 数量
          *        state  0 买方向
          *               1 卖方向
          * 输出： 盈利
         ******************************************************/
        private static double getprofit(double price, double current, int amount, SelectState State)
        {
            if (State == SelectState.担保物买入 || State == SelectState.融资买入)
                return Math.Round((amount * current * (1 - settings.yjfl - settings.yhsl) - amount * price * (1 + settings.rzlx * settings.rzts / 365 + settings.yjfl)), 2);
            else if (State == SelectState.担保物卖出 || State == SelectState.融券卖出)
                return Math.Round((amount * price * (1 - settings.yjfl - settings.yhsl - settings.rqlx * settings.rqts / 365) - amount * current * (1 + settings.yjfl)), 2);
            else
                return 0;
        }

        public class watchitem : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private int number;
            private string name;
            private string code;
            private double price;
            private double current;
            private int amount;
            private string time;
            private SelectState state;
            private double profit;
            private void Changed(string PropertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
            public int Number
            {
                get { return number; }
                set
                {
                    if (value == number)
                        return;
                    else
                    {
                        number = value;
                        this.Changed("Number");
                    }
                }
            }
            public string Name
            {
                get { return name; }
                set
                {
                    if (value == name)
                        return;
                    else
                    {
                        name = value;
                        this.Changed("Name");
                    }
                }
            }
            public string Code
            {
                get { return code; }
                set
                {
                    if (value == code)
                        return;
                    else
                    {
                        code = value;
                        this.Changed("Code");
                    }
                }
            }
            public double Price
            {
                get { return price; }
                set
                {
                    if (value == price)
                        return;
                    else
                    {
                        price = value;
                        this.Changed("Price");
                    }
                }
            }
            public double Current
            {
                get { return current; }
                set
                {
                    if (value == current)
                        return;
                    else
                    {
                        current = value;
                        this.Changed("Current");
                    }
                }
            }
            public int Amount
            {
                get { return amount; }
                set
                {
                    if (value == amount)
                        return;
                    else
                    {
                        amount = value;
                        this.Changed("Amount");
                    }
                }
            }
            public string Time
            {
                get { return time; }
                set
                {
                    if (value == time)
                        return;
                    else
                    {
                        time = value;
                        this.Changed("Time");
                    }
                }
            }
            public SelectState State
            {
                get { return state; }
                set
                {
                    if (value == state)
                        return;
                    else
                    {
                        state = value;
                        this.Changed("State");
                    }
                }
            }
            public double Profit
            {
                get { return profit; }
                set
                {
                    if (value == profit)
                        return;
                    else
                    {
                        profit = value;
                        this.Changed("Profit");
                    }
                }
            }
        }
        public static ObservableCollection<watchitem> watchitems = new ObservableCollection<watchitem>();

        public watchitem makwatchitem(InputStock t)
        {
            watchitem x = new watchitem();
            x.Number = lv2num + 1;
            lv2num++;
            x.Code = t.Code;
            x.Price = t.Price;
            x.Current = t.Price;
            x.Amount = t.Amount;
            x.State = (SelectState)t.State;
            x.Time = DateTime.Now.ToString();
            x.Name = StockList.Find(
            delegate(string s)
            {
                if (s.IndexOf(x.Code) != -1)
                    return true;
                else
                    return false;
            }).Substring(7);
            x.Profit = getprofit(x.Price, x.Current, x.Amount, x.State);
            return x;
        }

        public class Selectitem
        {
            public string Name { set; get; }
            public int ID { set; get; }
        }
        public static ObservableCollection<Selectitem> selectitems1 = new ObservableCollection<Selectitem>() 
        {  (new Selectitem { ID = 1, Name = SelectState.担保物买入.ToString() }),    
           (new Selectitem { ID = 3, Name = SelectState.融资买入.ToString() }),
           (new Selectitem { ID = 6, Name = SelectState.买券还券.ToString() })};
        public static ObservableCollection<Selectitem> selectitems2 = new ObservableCollection<Selectitem>() 
        {  (new Selectitem { ID = 2, Name = SelectState.担保物卖出.ToString() }),
           (new Selectitem { ID = 4, Name = SelectState.融券卖出.ToString() }),
           (new Selectitem { ID = 5, Name = SelectState.卖券还款.ToString() })};

        private delegate void showlv1(List<StockInfo> now);
        private void show1(List<StockInfo> now)
        {
            if (listitems.Count == 0)
            {
                listitems.Add(new listitem { Name = "卖五", Price = now[0].SellList[4].Price.ToString(), Amount = now[0].SellList[4].Amount.ToString() });
                listitems.Add(new listitem { Name = "卖四", Price = now[0].SellList[3].Price.ToString(), Amount = now[0].SellList[3].Amount.ToString() });
                listitems.Add(new listitem { Name = "卖三", Price = now[0].SellList[2].Price.ToString(), Amount = now[0].SellList[2].Amount.ToString() });
                listitems.Add(new listitem { Name = "卖二", Price = now[0].SellList[1].Price.ToString(), Amount = now[0].SellList[1].Amount.ToString() });
                listitems.Add(new listitem { Name = "卖一", Price = now[0].SellList[0].Price.ToString(), Amount = now[0].SellList[0].Amount.ToString() });
                listitems.Add(new listitem { Name = "最新", Price = now[0].Current.ToString(), Amount = null });
                listitems.Add(new listitem { Name = "买一", Price = now[0].BuyList[0].Price.ToString(), Amount = now[0].BuyList[0].Amount.ToString() });
                listitems.Add(new listitem { Name = "买二", Price = now[0].BuyList[1].Price.ToString(), Amount = now[0].BuyList[1].Amount.ToString() });
                listitems.Add(new listitem { Name = "买三", Price = now[0].BuyList[2].Price.ToString(), Amount = now[0].BuyList[2].Amount.ToString() });
                listitems.Add(new listitem { Name = "买四", Price = now[0].BuyList[3].Price.ToString(), Amount = now[0].BuyList[3].Amount.ToString() });
                listitems.Add(new listitem { Name = "买五", Price = now[0].BuyList[4].Price.ToString(), Amount = now[0].BuyList[4].Amount.ToString() });
            }
        }
        private delegate void showlab(List<StockInfo> now);
        public void show2(List<StockInfo> now)
        {
            this.LabelOfTime.Content = now[0].Time.TimeOfDay.ToString();
        }
        private delegate void showname(List<StockInfo> now);
        public void show3(List<StockInfo> now)
        {
            this.StokeName.Content = now[0].Name;
        }
        private delegate void showkc(string s);
        public void show4(string s)
        {
            this.Autokc.Content = s;
        }
        private delegate void showsum(double t);
        public void show5(double t)
        {
            this.Sum.Content = "总盈亏：" + t.ToString();
        }
        private delegate void showpc(string s);
        public void show6(string s)
        {
            this.Autopc.Content = s;
        }
        private delegate void showmsg(string s);
        public void showmsgbx(string s)
        {
            ModernDialog.ShowMessage(s, "", MessageBoxButton.OK);
        }
        private delegate void addlv2(watchitem t);
        private void add(watchitem t)
        {
            watchitems.Add(t);
        }

        private void readsql()//获取历史记录
        {
            int n = 0;
            listview3.Items.Clear();
            SqlConnection conn = new SqlConnection(ConStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "tradeinfo_select"; //存储过程的名称
            cmd.CommandType = CommandType.StoredProcedure;  //设置类型为存储过程
            try
            {
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read() && n < 10)
                {
                    historyitem show = new historyitem();
                    show.Code = sdr["stock_ID"].ToString();
                    show.Name = sdr["tradename"].ToString();
                    show.State = (SelectState)Enum.Parse(typeof(SelectState), sdr["tradetype"].ToString(), false);
                    show.Time = sdr["tradetime"].ToString();
                    show.Price = double.Parse(sdr["tradeprice"].ToString());
                    show.Amount = int.Parse(sdr["tradeamount"].ToString());
                    listview3.Items.Add(show);
                    n++;
                }
                sdr.Close();
            }
            catch (System.Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        
        private void getstockname()//获取股票代码和对应的名称
        {
            SqlConnection conn = new SqlConnection(ConStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "stockinfo_select"; //存储过程的名称
            cmd.CommandType = CommandType.StoredProcedure;  //设置类型为存储过程
            try
            {
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    StockList.Add(sdr["stock_ID"].ToString() + " " + sdr["stock_name"].ToString());
                }
                sdr.Close();
            }
            catch (System.Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public BasicPage3()
        {
            InitializeComponent();
            Auto con = new Auto();
            thishwnd = con.GetWindowHwnd("融资融券");
            lv2num = 0;
            this.SelectDealState1.ItemsSource = selectitems1;
            this.SelectDealState2.ItemsSource = selectitems2;
            this.listview1.ItemsSource = listitems;
            this.listview2.ItemsSource = watchitems;
            StockList = new List<string>();
            //getstockname();//获取股票代码和对应的名称

            StreamReader sr = new StreamReader(@"StockList.txt", System.Text.Encoding.Default);
            string s = sr.ReadLine();
            while (s != null)
            {
                StockList.Add(s);
                s = sr.ReadLine();
            }
            sr.Close();

            StokeCode.ItemsSource = StockList;
            
            //readsql();//获取历史记录

            settings.yjfl = double.Parse(yjfl.Text);
            settings.yhsl = double.Parse(yhsl.Text);
            settings.rzlx = double.Parse(rzlx.Text);
            settings.rqlx = double.Parse(rqlx.Text);
            settings.rzts = int.Parse(rzts.Text);
            settings.rqts = int.Parse(rqts.Text);
        }

        private List<StockInfo> getstoke(string code)
        {
            Sina con = new Sina();
            List<StockInfo> now = new List<StockInfo>();
            now = con.GetCurrent(code);
            return now;
        }

        private void UpdateStock()
        {
            bool flag = true;
            while (true)
            {
                List<StockInfo> now = getstoke(searchcode);
                if (now[0].Name == null)
                {
                    if (flag)
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入代码有误");
                    break;
                }
                else
                {
                    flag = false;
                    if (listitems.Count != 0)
                    {
                        listitems[0].Price = now[0].SellList[4].Price.ToString();
                        listitems[0].Amount = now[0].SellList[4].Amount.ToString();
                        listitems[1].Price = now[0].SellList[3].Price.ToString();
                        listitems[1].Amount = now[0].SellList[3].Amount.ToString();
                        listitems[2].Price = now[0].SellList[2].Price.ToString();
                        listitems[2].Amount = now[0].SellList[2].Amount.ToString();
                        listitems[3].Price = now[0].SellList[1].Price.ToString();
                        listitems[3].Amount = now[0].SellList[1].Amount.ToString();
                        listitems[4].Price = now[0].SellList[0].Price.ToString();
                        listitems[4].Amount = now[0].SellList[0].Amount.ToString();
                        listitems[5].Price = now[0].Current.ToString();
                        listitems[6].Price = now[0].BuyList[0].Price.ToString();
                        listitems[6].Amount = now[0].BuyList[0].Amount.ToString();
                        listitems[7].Price = now[0].BuyList[1].Price.ToString();
                        listitems[7].Amount = now[0].BuyList[1].Amount.ToString();
                        listitems[8].Price = now[0].BuyList[2].Price.ToString();
                        listitems[8].Amount = now[0].BuyList[2].Amount.ToString();
                        listitems[9].Price = now[0].BuyList[3].Price.ToString();
                        listitems[9].Amount = now[0].BuyList[3].Amount.ToString();
                        listitems[10].Price = now[0].BuyList[4].Price.ToString();
                        listitems[10].Amount = now[0].BuyList[4].Amount.ToString();
                    }
                    else
                        this.Dispatcher.Invoke(new showlv1(show1), now);
                    this.Dispatcher.Invoke(new showlab(show2), now);
                    this.Dispatcher.Invoke(new showname(show3), now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchcode = (string)this.StokeCode.Dispatcher.Invoke(new Func<string>(() =>
            {
                return StokeCode.getstring();
            }));
            if (Autoupdate != null)
                Autoupdate.Abort();
            buycode.Text = searchcode;
            Thread t = new Thread(new ThreadStart(UpdateStock));
            t.IsBackground = true;
            t.Name = "刷新线程" + DateTime.Now.ToString();
            t.Start();
            Autoupdate = t;
        }

        private void listview1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listitem t = listview1.SelectedItem as listitem;
            if (t != null)
                buyprice.Text = t.Price;
        }

        private void PPbutton_Click(object sender, RoutedEventArgs e)
        {
            Auto con = new Auto();
            tv = con.GetTreeViewInfo();
            if (tv.Count == 7)
            {
                ModernDialog.ShowMessage("成功匹配到下单软件", "", MessageBoxButton.OK);
                Header.Text = "匹配到下单软件";
                PPbutton.IsEnabled = false;
            }
            else
                ModernDialog.ShowMessage("匹配下单软件失败", "", MessageBoxButton.OK);
        }

        private static void addsql(historyitem show)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "tradeinfo_insert"; //存储过程的名称
            cmd.CommandType = CommandType.StoredProcedure;  //设置类型为存储过程
            cmd.Parameters.Add("@Stockid", SqlDbType.NVarChar, 15).Value = show.Code;
            cmd.Parameters.Add("@trade_name", SqlDbType.NVarChar, 15).Value = show.Name;
            cmd.Parameters.Add("@trade_type", SqlDbType.NVarChar, 15).Value = show.State.ToString();
            cmd.Parameters.Add("@trade_time", SqlDbType.DateTime).Value = show.Time;
            cmd.Parameters.Add("@trade_price", SqlDbType.Decimal).Value = show.Price;
            cmd.Parameters.Add("@trade_amount", SqlDbType.Int).Value = show.Amount;
            try
            {
                conn.Open();
                int answer = cmd.ExecuteNonQuery();
                if (answer != 1)
                    MessageBox.Show("连接数据库失败");
            }
            catch (System.Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void AutoDeal(DealState dealstate)
        {
            Auto con = new Auto();
            if (tv.Count != 7)
            {
                ModernDialog.ShowMessage("未匹配到下单软件", "", MessageBoxButton.OK);
                PPbutton.IsEnabled = true;
            }
            else
            {
                InputStock temp = new InputStock();
                if (string.IsNullOrWhiteSpace(buycode.Text) || string.IsNullOrWhiteSpace(buyprice.Text) || string.IsNullOrWhiteSpace(buyamount.Text))
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else
                {
                    temp.Code = buycode.Text;
                    temp.Price = double.Parse(buyprice.Text);
                    temp.Amount = int.Parse(buyamount.Text);
                    temp.State = dealstate;
                    int length = buyprice.Text.Length - buyprice.Text.IndexOf(".") - 1;
                    if (temp.Amount % 100 != 0 || length > 3)
                        ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    else
                    {
                        bool t = con.AutoSelect(tv, ((SelectState)dealstate).ToString());
                        if (t)
                        {
                            System.Threading.Thread.Sleep(300);
                            int i = con.AutoBuy(temp);
                            con.SetWindow(thishwnd);
                            /*
                                return 0 成功操作
                                return 1 没找到button
                                return 2 没有找到msg标题
                                return 3 没找到button与text
                                return 4 输入价格超出当天涨跌停限制
                                return 5 msg标题没有匹配的操作
                                return 6 tv选择与操作不匹配
                                return 7 输入stock错误
                                return 8 没有找到相应窗体
                                return 9 没有找到下单程序
                                return 10 没有获取弹出窗口
                            */
                            if (i == 0)
                            {
                                ModernDialog.ShowMessage("成功下单", "", MessageBoxButton.OK);
                                watchitem show = new watchitem();
                                show = makwatchitem(temp);
                                watchitems.Add(show);
                                StartWatch();
                                historyitem his = new historyitem(temp);
                                addsql(his);
                            }
                            else if (i == 1) ModernDialog.ShowMessage("没找到button", "", MessageBoxButton.OK);
                            else if (i == 2) ModernDialog.ShowMessage("没有找到msg标题", "", MessageBoxButton.OK);
                            else if (i == 3) ModernDialog.ShowMessage("没找到button与text", "", MessageBoxButton.OK);
                            else if (i == 4) ModernDialog.ShowMessage("输入价格超出当天涨跌停限制", "", MessageBoxButton.OK);
                            else if (i == 5) ModernDialog.ShowMessage("弹出消息框未知", "", MessageBoxButton.OK);
                            else if (i == 6) ModernDialog.ShowMessage("选择不匹配", "", MessageBoxButton.OK);
                            else if (i == 7) ModernDialog.ShowMessage("输入数据不正确", "", MessageBoxButton.OK);
                            else if (i == 8) ModernDialog.ShowMessage("获取窗体出错", "", MessageBoxButton.OK);
                            else if (i == 9) ModernDialog.ShowMessage("未启动下单软件", "", MessageBoxButton.OK);
                            else if (i == 10) ModernDialog.ShowMessage("没有获取弹出窗口", "", MessageBoxButton.OK);
                            else if (i > 10) ModernDialog.ShowMessage("发生未知错误", "", MessageBoxButton.OK);
                        }
                        else
                        {
                            ModernDialog.ShowMessage("选择出错", "", MessageBoxButton.OK);
                            PPbutton.IsEnabled = true;
                        }
                    }
                }
            }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectDealState1.SelectedValue != null)
            {
                AutoDeal((DealState)(int.Parse(this.SelectDealState1.SelectedValue.ToString())));
            }
            else
                ModernDialog.ShowMessage("没有选择交易方式", "", MessageBoxButton.OK);
        }

        private void sell_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectDealState2.SelectedValue != null)
            {
                AutoDeal((DealState)(int.Parse(this.SelectDealState2.SelectedValue.ToString())));
            }
            else
                ModernDialog.ShowMessage("没有选择交易方式", "", MessageBoxButton.OK);
        }

        private void cancelbuy_Click(object sender, RoutedEventArgs e)
        {
            Auto con = new Auto();
            int t = con.AutoCancel(0);
            con.SetWindow(thishwnd);
            if (t == 0)
                ModernDialog.ShowMessage("撤买成功", "", MessageBoxButton.OK);
            else
                ModernDialog.ShowMessage("撤单失败，请手动撤单！", "", MessageBoxButton.OK);
        }

        private void cancelsell_Click(object sender, RoutedEventArgs e)
        {
            Auto con = new Auto();
            int t = con.AutoCancel(1);
            con.SetWindow(thishwnd);
            if (t == 0)
                ModernDialog.ShowMessage("撤卖成功", "", MessageBoxButton.OK);
            else
                ModernDialog.ShowMessage("撤单失败，请手动撤单！", "", MessageBoxButton.OK);
        }

        private class myThread
        {
            public InputStock temp1;
            public InputStock temp2;
            public int state;
        }

        public void watchbuy(object x)//thread function
        {
            myThread T = (myThread)x;
            int flag = 0;
            List<StockInfo> now = new List<StockInfo>();
            while (true)
            {
                Sina con = new Sina();
                if (T.temp1.Code != null)
                    now = con.GetCurrent(T.temp1.Code);
                else
                    now = con.GetCurrent(T.temp2.Code);
                if (now[0].Name == null)
                    break;
                if (now[0].Current >= T.temp1.Price && T.temp1.Code != null)
                {
                    flag = 1;
                    if (T.state == 2)
                        T.temp1.Price = now[0].Current;
                    break;
                }
                else if (now[0].Current <= T.temp2.Price && T.temp2.Code != null)
                {
                    flag = 2;
                    if (T.state == 2)
                        T.temp2.Price = now[0].Current;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
            if (flag == 1)
            {
                if (tv.Count != 7) this.Dispatcher.Invoke(new showmsg(showmsgbx), "未匹配到下单软件");
                else
                {
                    Auto auto = new Auto();
                    bool t = auto.AutoSelect(tv, ((SelectState)T.temp1.State).ToString());
                    if (t)
                    {
                        System.Threading.Thread.Sleep(300);
                        int i = auto.AutoBuy(T.temp1);
                        auto.SetWindow(thishwnd);
                        if (i == 0)
                        {
                            this.Dispatcher.Invoke(new showmsg(showmsgbx), "成功下单");
                            watchitem show = new watchitem();
                            show = makwatchitem(T.temp1);
                            this.Dispatcher.Invoke(new addlv2(add), show);
                            StartWatch();
                            historyitem his = new historyitem(T.temp1);
                            addsql(his);
                        }
                        else if (i == 1) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没找到button");
                        else if (i == 2) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没有找到msg标题");
                        else if (i == 3) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没找到button与text");
                        else if (i == 4) this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入价格超出当天涨跌停限制");
                        else if (i == 5) this.Dispatcher.Invoke(new showmsg(showmsgbx), "弹出消息框未知");
                        else if (i == 6) this.Dispatcher.Invoke(new showmsg(showmsgbx), "选择不匹配");
                        else if (i == 7) this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入数据不正确");
                        else if (i == 8) this.Dispatcher.Invoke(new showmsg(showmsgbx), "获取窗体出错");
                        else if (i == 9) this.Dispatcher.Invoke(new showmsg(showmsgbx), "未启动下单软件");
                        else if (i == 10) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没有获取弹出窗口");
                        else if (i > 10) this.Dispatcher.Invoke(new showmsg(showmsgbx), "发生未知错误");
                    }
                    else
                    {
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "选择出错");
                    }
                }
            }
            else if (flag == 2)
            {
                if (tv.Count != 7) this.Dispatcher.Invoke(new showmsg(showmsgbx), "未匹配到下单软件");
                else
                {
                    Auto auto = new Auto();
                    bool t = auto.AutoSelect(tv, ((SelectState)T.temp2.State).ToString());
                    if (t)
                    {
                        System.Threading.Thread.Sleep(300);
                        int i = auto.AutoBuy(T.temp2);
                        auto.SetWindow(thishwnd);
                        if (i == 0)
                        {
                            this.Dispatcher.Invoke(new showmsg(showmsgbx), "成功下单");
                            watchitem show = new watchitem();
                            show = makwatchitem(T.temp2);
                            this.Dispatcher.Invoke(new addlv2(add), show);
                            StartWatch();
                            historyitem his = new historyitem(T.temp2);
                            addsql(his);
                        }
                        else if (i == 1) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没找到button");
                        else if (i == 2) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没有找到msg标题");
                        else if (i == 3) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没找到button与text");
                        else if (i == 4) this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入价格超出当天涨跌停限制");
                        else if (i == 5) this.Dispatcher.Invoke(new showmsg(showmsgbx), "弹出消息框未知");
                        else if (i == 6) this.Dispatcher.Invoke(new showmsg(showmsgbx), "选择不匹配");
                        else if (i == 7) this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入数据不正确");
                        else if (i == 8) this.Dispatcher.Invoke(new showmsg(showmsgbx), "获取窗体出错");
                        else if (i == 9) this.Dispatcher.Invoke(new showmsg(showmsgbx), "未启动下单软件");
                        else if (i == 10) this.Dispatcher.Invoke(new showmsg(showmsgbx), "没有获取弹出窗口");
                        else if (i > 10) this.Dispatcher.Invoke(new showmsg(showmsgbx), "发生未知错误");
                    }
                    else
                    {
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "选择出错");
                    }
                }
            }
            else
            {
                this.Dispatcher.Invoke(new showmsg(showmsgbx), "获取当前股票信息错误");
            }
            if (T.state == 1)
                this.Autokc.Dispatcher.Invoke(new showkc(show4), "自动开仓");
            else if (T.state == 2)
                this.Autokc.Dispatcher.Invoke(new showpc(show6), "自动平仓");
        }


        private void Autokc_Click(object sender, RoutedEventArgs e)
        {
            if (Autokc.Content.ToString() == "自动开仓")
            {
                Auto con = new Auto();
                bool flag = false;
                InputStock temp1 = new InputStock();
                InputStock temp2 = new InputStock();
                if (tv.Count != 7)
                {
                    ModernDialog.ShowMessage("未匹配到下单软件", "", MessageBoxButton.OK);
                    PPbutton.IsEnabled = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(buycode.Text) || string.IsNullOrWhiteSpace(buyamount.Text))
                {
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    return;
                }
                if (int.Parse(buyamount.Text) % 100 != 0)
                {
                    ModernDialog.ShowMessage("输入股票数量错误", "", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(watchpricebig.Text) && string.IsNullOrWhiteSpace(watchpricesmall.Text))
                {
                    ModernDialog.ShowMessage("输入价格错误", "", MessageBoxButton.OK);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(watchpricebig.Text))
                {
                    temp1.Code = buycode.Text;
                    temp1.Price = double.Parse(watchpricebig.Text);
                    temp1.Amount = int.Parse(buyamount.Text);
                    if (radio1.IsChecked == true)
                        temp1.State = DealState.买入股票;
                    else if (radio2.IsChecked == true)
                        temp1.State = DealState.融券卖出;
                    else
                    {
                        ModernDialog.ShowMessage("输入交易方式错误", "", MessageBoxButton.OK);
                        return;
                    }
                    flag = true;
                }
                if (!string.IsNullOrWhiteSpace(watchpricesmall.Text))
                {
                    temp2.Code = buycode.Text;
                    temp2.Price = double.Parse(watchpricesmall.Text);
                    temp2.Amount = int.Parse(buyamount.Text);
                    if (radio3.IsChecked == true)
                        temp2.State = DealState.买入股票;
                    else if (radio4.IsChecked == true)
                        temp2.State = DealState.融券卖出;
                    else
                    {
                        if (flag == false)
                        {
                            ModernDialog.ShowMessage("输入交易方式错误", "", MessageBoxButton.OK);
                            return;
                        }
                    }
                }
                myThread temp = new myThread();
                temp.temp1 = temp1;
                temp.temp2 = temp2;
                temp.state = 1;
                Thread t = new Thread(new ParameterizedThreadStart(watchbuy));
                t.IsBackground = true;
                t.Name = "自动开仓线程";
                t.Start(temp);
                Autokaicang = t;
                Autokc.Content = "开仓停止";
            }
            else if (Autokc.Content.ToString() == "开仓停止")
            {
                Autokaicang.Abort();
                Autokc.Content = "自动开仓";
            }
        }

        private void Autopc_Click(object sender, RoutedEventArgs e)
        {
            if (Autopc.Content.ToString() == "自动平仓")
            {
                Auto con = new Auto();
                int num;
                bool flag = false;
                watchitem t = new watchitem();
                InputStock temp1 = new InputStock();
                InputStock temp2 = new InputStock();
                if (tv.Count != 7)
                {
                    ModernDialog.ShowMessage("未匹配到下单软件", "", MessageBoxButton.OK);
                    PPbutton.IsEnabled = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(pcnumber.Text))
                {
                    ModernDialog.ShowMessage("输入错误序号", "", MessageBoxButton.OK);
                    return;
                }
                num = int.Parse(pcnumber.Text);
                for (int i = 0; i < watchitems.Count; i++)
                    if (watchitems[i].Number == num)
                        t = watchitems[i];
                if (t == null)
                {
                    ModernDialog.ShowMessage("输入错误序号", "", MessageBoxButton.OK);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(zycj.Text))
                {
                    flag = true;
                    temp1.Amount = t.Amount;
                    temp1.Code = t.Code;
                    if (t.State == SelectState.担保物买入)
                    {
                        temp1.State = DealState.融券卖出;
                        temp1.Price = t.Price + double.Parse(zycj.Text);
                    }
                    else if (t.State == SelectState.融券卖出)
                    {
                        temp1.State = DealState.买入股票;
                        temp1.Price = t.Price - double.Parse(zycj.Text);
                    }
                }
                if (!string.IsNullOrWhiteSpace(zscj.Text))
                {
                    flag = true;
                    temp2.Amount = t.Amount;
                    temp2.Code = t.Code;
                    if (t.State == SelectState.担保物买入)
                    {
                        temp2.State = DealState.融券卖出;
                        temp2.Price = t.Price - double.Parse(zscj.Text);
                    }
                    else if (t.State == SelectState.融券卖出)
                    {
                        temp2.State = DealState.买入股票;
                        temp2.Price = t.Price + double.Parse(zscj.Text);
                    }
                }
                if (!flag)
                {
                    ModernDialog.ShowMessage("未输入平仓差价", "", MessageBoxButton.OK);
                    return;
                }
                myThread temp = new myThread();
                temp.temp1 = temp1;
                temp.temp2 = temp2;
                temp.state = 2;
                Thread tt = new Thread(new ParameterizedThreadStart(watchbuy));
                tt.IsBackground = true;
                tt.Name = "自动平仓线程";
                tt.Start(temp);
                Autopingcang = tt;
                Autopc.Content = "平仓停止";
            }
            else if (Autopc.Content.ToString() == "平仓停止")
            {
                Autopingcang.Abort();
                Autopc.Content = "自动平仓";
            }
        }

        private void listview3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            historyitem t = listview3.SelectedItem as historyitem;
            if (t != null)
            {
                if (t.State == SelectState.担保物买入)
                    SelectDealState1.SelectedItem = selectitems1[0];
                else if (t.State == SelectState.融资买入)
                    SelectDealState1.SelectedItem = selectitems1[1];
                else if (t.State == SelectState.买券还券)
                    SelectDealState1.SelectedItem = selectitems1[2];
                else if (t.State == SelectState.担保物卖出)
                    SelectDealState2.SelectedItem = selectitems2[0];
                else if (t.State == SelectState.融券卖出)
                    SelectDealState2.SelectedItem = selectitems2[1];
                else if (t.State == SelectState.卖券还款)
                    SelectDealState2.SelectedItem = selectitems2[2];
                buycode.Text = t.Code;
                buyprice.Text = t.Price.ToString();
                buyamount.Text = t.Amount.ToString();
            }
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            readsql();
        }

        private void yjfl_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings.yjfl = double.Parse(yjfl.Text);
        }
        private void yhsl_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings.yhsl = double.Parse(yhsl.Text);
        }
        private void rzlx_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings.rzlx = double.Parse(rzlx.Text);
        }
        private void rqlx_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings.rqlx = double.Parse(rqlx.Text);
        }
        private void rzts_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings.rzts = int.Parse(rzts.Text);
        }
        private void rqts_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings.rqts = int.Parse(rqts.Text);
        }

        private void Autowatch()
        {
            List<string> s = new List<string>();
            double sum;
            while (true)
            {
                s.Clear();
                sum = 0;
                if (watchitems.Count == 0) break;
                foreach (watchitem items in watchitems)
                    s.Add(items.Code);
                Sina con = new Sina();
                List<StockInfo> now = new List<StockInfo>();
                now = con.GetCurrent(s);
                for (int i = 0; i < watchitems.Count; i++)
                {
                    watchitems[i].Current = now[i].Current;
                    watchitems[i].Profit = getprofit(watchitems[i].Price, watchitems[i].Current, watchitems[i].Amount, watchitems[i].State);
                    sum += watchitems[i].Profit;
                }
                this.Dispatcher.Invoke(new showsum(show5), sum);
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void StartWatch()
        {
            if (autowatch == null)
            {
                Thread t = new Thread(Autowatch);
                t.IsBackground = true;
                t.Name = "监控线程";
                t.Start();
                autowatch = t;
            }
        }

        private void listview2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            watchitem t = listview2.SelectedItem as watchitem;
            if (t != null)
            {
                pcnumber.Text = t.Number.ToString();
            }
        }
        private void listview1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (listview1.SelectedItem != null)
                listview1.SelectedItem = null;
        }
        private void listview2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (listview2.SelectedItem != null)
                listview2.SelectedItem = null;
        }
        private void listview3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (listview3.SelectedItem != null)
                listview3.SelectedItem = null;
        }

        private void CheckDouble(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal || e.Key.ToString() == "Tab")
            {
                if (txt.Text.Contains(".") && e.Key == Key.Decimal)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                if (txt.Text.Contains(".") && e.Key == Key.OemPeriod)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void CheckInt(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key.ToString() == "Tab")
            {
                e.Handled = false;
                return;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9)) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                e.Handled = false;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void buyprice_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void buycode_KeyDown(object sender, KeyEventArgs e)
        {
            CheckInt(sender, e);
        }

        private void buyamount_KeyDown(object sender, KeyEventArgs e)
        {
            CheckInt(sender, e);
        }

        private void pcnumber_KeyDown(object sender, KeyEventArgs e)
        {
            CheckInt(sender, e);
        }

        private void watchpricebig_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void watchpricesmall_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void zycj_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void zscj_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }



    }
}
