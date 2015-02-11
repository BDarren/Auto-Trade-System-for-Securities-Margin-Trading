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

namespace Project02
{
    /// <summary>
    /// BasicPage2.xaml 的交互逻辑
    /// </summary>
    public partial class BasicPage2 : UserControl
    {
        List<IntPtr> tv = new List<IntPtr>();
        static Thread Autoupdate1;
        static Thread Autoupdate2;
        static Thread autokaicang;
        static bool autopingcang;
        static double zhiying;
        static double zhishun;
        static Thread autowatch;
        static int lv3num;
        static string searchcode1;
        static string searchcode2;
        IntPtr thishwnd = new IntPtr();
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
        public static ObservableCollection<listitem> listitems1 = new ObservableCollection<listitem>();
        public static ObservableCollection<listitem> listitems2 = new ObservableCollection<listitem>();

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
            private double total;
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
            public double Total
            {
                get { return total; }
                set
                {
                    if (value == total)
                        return;
                    else
                    {
                        total = value;
                        this.Changed("Total");
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
            x.Number = lv3num + 1;
            lv3num++;
            x.Code = t.Code;
            x.Price = t.Price;
            x.Current = t.Price;
            x.Amount = t.Amount;
            x.Total = t.Amount * t.Price;
            x.State = (SelectState)t.State;
            x.Time = DateTime.Now.ToString();
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
        public static ObservableCollection<Selectitem> selectitems3 = new ObservableCollection<Selectitem>() 
        {  (new Selectitem { ID = 2, Name = SelectState.担保物卖出.ToString() }),
           (new Selectitem { ID = 4, Name = SelectState.融券卖出.ToString() }),
           (new Selectitem { ID = 5, Name = SelectState.卖券还款.ToString() })};
        public static ObservableCollection<Selectitem> selectitems4 = new ObservableCollection<Selectitem>() 
        {  (new Selectitem { ID = 1, Name = SelectState.担保物买入.ToString() }),    
           (new Selectitem { ID = 3, Name = SelectState.融资买入.ToString() }),
           (new Selectitem { ID = 6, Name = SelectState.买券还券.ToString() })};

        private delegate void showupdate(List<StockInfo> now);
        private void showlv1(List<StockInfo> now)
        {
            if (listitems1.Count == 0)
            {
                listitems1.Add(new listitem { Name = "卖五", Price = now[0].SellList[4].Price.ToString(), Amount = now[0].SellList[4].Amount.ToString() });
                listitems1.Add(new listitem { Name = "卖四", Price = now[0].SellList[3].Price.ToString(), Amount = now[0].SellList[3].Amount.ToString() });
                listitems1.Add(new listitem { Name = "卖三", Price = now[0].SellList[2].Price.ToString(), Amount = now[0].SellList[2].Amount.ToString() });
                listitems1.Add(new listitem { Name = "卖二", Price = now[0].SellList[1].Price.ToString(), Amount = now[0].SellList[1].Amount.ToString() });
                listitems1.Add(new listitem { Name = "卖一", Price = now[0].SellList[0].Price.ToString(), Amount = now[0].SellList[0].Amount.ToString() });
                listitems1.Add(new listitem { Name = "最新", Price = now[0].Current.ToString(), Amount = null });
                listitems1.Add(new listitem { Name = "买一", Price = now[0].BuyList[0].Price.ToString(), Amount = now[0].BuyList[0].Amount.ToString() });
                listitems1.Add(new listitem { Name = "买二", Price = now[0].BuyList[1].Price.ToString(), Amount = now[0].BuyList[1].Amount.ToString() });
                listitems1.Add(new listitem { Name = "买三", Price = now[0].BuyList[2].Price.ToString(), Amount = now[0].BuyList[2].Amount.ToString() });
                listitems1.Add(new listitem { Name = "买四", Price = now[0].BuyList[3].Price.ToString(), Amount = now[0].BuyList[3].Amount.ToString() });
                listitems1.Add(new listitem { Name = "买五", Price = now[0].BuyList[4].Price.ToString(), Amount = now[0].BuyList[4].Amount.ToString() });
            }
        }
        private void showlv2(List<StockInfo> now)
        {
            if (listitems2.Count == 0)
            {
                listitems2.Add(new listitem { Name = "卖五", Price = now[0].SellList[4].Price.ToString(), Amount = now[0].SellList[4].Amount.ToString() });
                listitems2.Add(new listitem { Name = "卖四", Price = now[0].SellList[3].Price.ToString(), Amount = now[0].SellList[3].Amount.ToString() });
                listitems2.Add(new listitem { Name = "卖三", Price = now[0].SellList[2].Price.ToString(), Amount = now[0].SellList[2].Amount.ToString() });
                listitems2.Add(new listitem { Name = "卖二", Price = now[0].SellList[1].Price.ToString(), Amount = now[0].SellList[1].Amount.ToString() });
                listitems2.Add(new listitem { Name = "卖一", Price = now[0].SellList[0].Price.ToString(), Amount = now[0].SellList[0].Amount.ToString() });
                listitems2.Add(new listitem { Name = "最新", Price = now[0].Current.ToString(), Amount = null });
                listitems2.Add(new listitem { Name = "买一", Price = now[0].BuyList[0].Price.ToString(), Amount = now[0].BuyList[0].Amount.ToString() });
                listitems2.Add(new listitem { Name = "买二", Price = now[0].BuyList[1].Price.ToString(), Amount = now[0].BuyList[1].Amount.ToString() });
                listitems2.Add(new listitem { Name = "买三", Price = now[0].BuyList[2].Price.ToString(), Amount = now[0].BuyList[2].Amount.ToString() });
                listitems2.Add(new listitem { Name = "买四", Price = now[0].BuyList[3].Price.ToString(), Amount = now[0].BuyList[3].Amount.ToString() });
                listitems2.Add(new listitem { Name = "买五", Price = now[0].BuyList[4].Price.ToString(), Amount = now[0].BuyList[4].Amount.ToString() });
            }
        }


        private delegate void showLabelOfTime(List<StockInfo> now);
        public void showLabelOfTime1(List<StockInfo> now)
        {
            this.LabelOfTime1.Content = now[0].Time.TimeOfDay.ToString();
        }
        public void showLabelOfTime2(List<StockInfo> now)
        {
            this.LabelOfTime2.Content = now[0].Time.TimeOfDay.ToString();
        }
        private delegate void showStokeName(List<StockInfo> now);
        public void showStokeName1(List<StockInfo> now)
        {
            this.StokeName1.Content = now[0].Name;
        }
        public void showStokeName2(List<StockInfo> now)
        {
            this.StokeName2.Content = now[0].Name;
        }
        private delegate void showmsg(string s);
        public void showmsgbx(string s)
        {
            ModernDialog.ShowMessage(s, "", MessageBoxButton.OK);
        }
        private delegate void showsum(double t);
        public void showyk(double t)
        {
            this.Sum.Content = "总盈亏：" + t.ToString();
        }
        private delegate void addwatch(watchitem t);
        private void add(watchitem t)
        {
            watchitems.Add(t);
        }
        private delegate void showkc(string s);
        public void showkaicang(string s)
        {
            this.Autokaicang.Content = s;
        }
        private delegate void showpc(string s);
        public void showpingcang(string s)
        {
            this.Autopingcang.Content = s;
        }

        public BasicPage2()
        {
            InitializeComponent();
            Auto con = new Auto();
            thishwnd = con.GetWindowHwnd("融资融券");
            this.SelectDealState1.ItemsSource = selectitems1;
            this.SelectDealState2.ItemsSource = selectitems2;
            this.SelectDealState3.ItemsSource = selectitems3;
            this.SelectDealState4.ItemsSource = selectitems4;
            this.listview1.ItemsSource = listitems1;
            this.listview2.ItemsSource = listitems2;
            this.listview3.ItemsSource = watchitems;
            autopingcang = false;
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

        private void UpdateStock1()
        {
            bool flag = true;
            while (true)
            {
                List<StockInfo> now = getstoke(searchcode1);
                if (now[0].Name == null)
                {
                    if (flag)
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入代码有误");
                    break;
                }
                else
                {
                    flag = false;
                    if (listitems1.Count != 0)
                    {
                        listitems1[0].Price = now[0].SellList[4].Price.ToString();
                        listitems1[0].Amount = now[0].SellList[4].Amount.ToString();
                        listitems1[1].Price = now[0].SellList[3].Price.ToString();
                        listitems1[1].Amount = now[0].SellList[3].Amount.ToString();
                        listitems1[2].Price = now[0].SellList[2].Price.ToString();
                        listitems1[2].Amount = now[0].SellList[2].Amount.ToString();
                        listitems1[3].Price = now[0].SellList[1].Price.ToString();
                        listitems1[3].Amount = now[0].SellList[1].Amount.ToString();
                        listitems1[4].Price = now[0].SellList[0].Price.ToString();
                        listitems1[4].Amount = now[0].SellList[0].Amount.ToString();
                        listitems1[5].Price = now[0].Current.ToString();
                        listitems1[6].Price = now[0].BuyList[0].Price.ToString();
                        listitems1[6].Amount = now[0].BuyList[0].Amount.ToString();
                        listitems1[7].Price = now[0].BuyList[1].Price.ToString();
                        listitems1[7].Amount = now[0].BuyList[1].Amount.ToString();
                        listitems1[8].Price = now[0].BuyList[2].Price.ToString();
                        listitems1[8].Amount = now[0].BuyList[2].Amount.ToString();
                        listitems1[9].Price = now[0].BuyList[3].Price.ToString();
                        listitems1[9].Amount = now[0].BuyList[3].Amount.ToString();
                        listitems1[10].Price = now[0].BuyList[4].Price.ToString();
                        listitems1[10].Amount = now[0].BuyList[4].Amount.ToString();
                    }
                    else
                        this.Dispatcher.Invoke(new showupdate(showlv1), now);
                    this.Dispatcher.Invoke(new showLabelOfTime(showLabelOfTime1), now);
                    this.Dispatcher.Invoke(new showStokeName(showStokeName1), now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
        private void UpdateStock2()
        {
            bool flag = true;
            while (true)
            {
                List<StockInfo> now = getstoke(searchcode2);
                if (now[0].Name == null)
                {
                    if (flag)
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入代码有误");
                    break;
                }
                else
                {
                    flag = false;
                    if (listitems2.Count != 0)
                    {
                        listitems2[0].Price = now[0].SellList[4].Price.ToString();
                        listitems2[0].Amount = now[0].SellList[4].Amount.ToString();
                        listitems2[1].Price = now[0].SellList[3].Price.ToString();
                        listitems2[1].Amount = now[0].SellList[3].Amount.ToString();
                        listitems2[2].Price = now[0].SellList[2].Price.ToString();
                        listitems2[2].Amount = now[0].SellList[2].Amount.ToString();
                        listitems2[3].Price = now[0].SellList[1].Price.ToString();
                        listitems2[3].Amount = now[0].SellList[1].Amount.ToString();
                        listitems2[4].Price = now[0].SellList[0].Price.ToString();
                        listitems2[4].Amount = now[0].SellList[0].Amount.ToString();
                        listitems2[5].Price = now[0].Current.ToString();
                        listitems2[6].Price = now[0].BuyList[0].Price.ToString();
                        listitems2[6].Amount = now[0].BuyList[0].Amount.ToString();
                        listitems2[7].Price = now[0].BuyList[1].Price.ToString();
                        listitems2[7].Amount = now[0].BuyList[1].Amount.ToString();
                        listitems2[8].Price = now[0].BuyList[2].Price.ToString();
                        listitems2[8].Amount = now[0].BuyList[2].Amount.ToString();
                        listitems2[9].Price = now[0].BuyList[3].Price.ToString();
                        listitems2[9].Amount = now[0].BuyList[3].Amount.ToString();
                        listitems2[10].Price = now[0].BuyList[4].Price.ToString();
                        listitems2[10].Amount = now[0].BuyList[4].Amount.ToString();
                    }
                    else
                        this.Dispatcher.Invoke(new showupdate(showlv2), now);
                    this.Dispatcher.Invoke(new showLabelOfTime(showLabelOfTime2), now);
                    this.Dispatcher.Invoke(new showStokeName(showStokeName2), now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void buycode1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buycode1.Text.Length == 6 && searchcode1 != buycode1.Text)
            {
                if (Autoupdate1 != null)
                    Autoupdate1.Abort();
                searchcode1=buycode1.Text;
                Thread t = new Thread(new ThreadStart(UpdateStock1));
                t.IsBackground = true;
                t.Name = "刷新线程1" + DateTime.Now.ToString();
                t.Start();
                Autoupdate1 = t;
            }
        }

        private void buycode2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buycode2.Text.Length == 6 && searchcode2 != buycode2.Text)
            {
                if (Autoupdate2 != null)
                    Autoupdate2.Abort();
                searchcode2 = buycode2.Text;
                Thread t = new Thread(new ThreadStart(UpdateStock2));
                t.IsBackground = true;
                t.Name = "刷新线程2" + DateTime.Now.ToString();
                t.Start();
                Autoupdate2 = t;
            }
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

        private void listview1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listitem t = listview1.SelectedItem as listitem;
            if (t != null)
                buyprice1.Text = t.Price;
        }

        private void listview1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (listview1.SelectedItem != null)
                listview1.SelectedItem = null;
        }

        private void listview2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listitem t = listview2.SelectedItem as listitem;
            if (t != null)
                buyprice2.Text = t.Price;
        }

        private void listview2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (listview2.SelectedItem != null)
                listview2.SelectedItem = null;
        }

        private void AutoDeal(InputStock temp)
        {
            Auto con = new Auto();
            if (tv.Count != 7)
            {
                ModernDialog.ShowMessage("未匹配到下单软件", "", MessageBoxButton.OK);
                PPbutton.IsEnabled = true;
            }
            else
            {
                bool t = con.AutoSelect(tv, ((SelectState)temp.State).ToString());
                if (t)
                {
                System.Threading.Thread.Sleep(300);
                int i = con.AutoBuy(temp);
                con.SetWindow(thishwnd);
                if (i == 0)
                {
                    ModernDialog.ShowMessage("成功下单", "", MessageBoxButton.OK);
                    watchitem show = new watchitem();
                    show = makwatchitem(temp);
                    watchitems.Add(show);
                    StartWatch();
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

        private void buy1_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectDealState1.SelectedValue != null)
            {
                InputStock temp = new InputStock();
                if (string.IsNullOrWhiteSpace(buycode1.Text) || string.IsNullOrWhiteSpace(buyprice1.Text) || string.IsNullOrWhiteSpace(buyamount1.Text))
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else
                {
                    temp.Code = buycode1.Text;
                    temp.Price = double.Parse(buyprice1.Text);
                    temp.Amount = int.Parse(buyamount1.Text);
                    temp.State = (DealState)(int.Parse(this.SelectDealState1.SelectedValue.ToString()));
                    int length = buyprice1.Text.Length - buyprice1.Text.IndexOf(".") - 1;
                    if (temp.Amount % 100 != 0 || length > 3)
                        ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    else
                        AutoDeal(temp);
                }
            }
            else
                ModernDialog.ShowMessage("没有选择交易方式", "", MessageBoxButton.OK);
        }

        private void buy2_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectDealState2.SelectedValue != null)
            {
                InputStock temp = new InputStock();
                if (string.IsNullOrWhiteSpace(buycode1.Text) || string.IsNullOrWhiteSpace(buyprice1.Text) || string.IsNullOrWhiteSpace(buyamount1.Text))
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else
                {
                    temp.Code = buycode1.Text;
                    temp.Price = double.Parse(buyprice1.Text);
                    temp.Amount = int.Parse(buyamount1.Text);
                    temp.State = (DealState)(int.Parse(this.SelectDealState2.SelectedValue.ToString()));
                    int length = buyprice1.Text.Length - buyprice1.Text.IndexOf(".") - 1;
                    if (temp.Amount % 100 != 0 || length > 3)
                        ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    else
                        AutoDeal(temp);
                }
            }
            else
                ModernDialog.ShowMessage("没有选择交易方式", "", MessageBoxButton.OK);
        }

        private void buy3_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectDealState3.SelectedValue != null)
            {
                InputStock temp = new InputStock();
                if (string.IsNullOrWhiteSpace(buycode2.Text) || string.IsNullOrWhiteSpace(buyprice2.Text) || string.IsNullOrWhiteSpace(buyamount2.Text))
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else
                {
                    temp.Code = buycode2.Text;
                    temp.Price = double.Parse(buyprice2.Text);
                    temp.Amount = int.Parse(buyamount2.Text);
                    temp.State = (DealState)(int.Parse(this.SelectDealState3.SelectedValue.ToString()));
                    int length = buyprice2.Text.Length - buyprice2.Text.IndexOf(".") - 1;
                    if (temp.Amount % 100 != 0 || length > 3)
                        ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    else
                        AutoDeal(temp);
                }
            }
            else
                ModernDialog.ShowMessage("没有选择交易方式", "", MessageBoxButton.OK);
        }

        private void buy4_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectDealState3.SelectedValue != null)
            {
                InputStock temp = new InputStock();
                if (string.IsNullOrWhiteSpace(buycode2.Text) || string.IsNullOrWhiteSpace(buyprice2.Text) || string.IsNullOrWhiteSpace(buyamount2.Text))
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else
                {
                    temp.Code = buycode2.Text;
                    temp.Price = double.Parse(buyprice2.Text);
                    temp.Amount = int.Parse(buyamount2.Text);
                    temp.State = (DealState)(int.Parse(this.SelectDealState4.SelectedValue.ToString()));
                    int length = buyprice2.Text.Length - buyprice2.Text.IndexOf(".") - 1;
                    if (temp.Amount % 100 != 0 || length > 3)
                        ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    else
                        AutoDeal(temp);
                }
            }
            else
                ModernDialog.ShowMessage("没有选择交易方式", "", MessageBoxButton.OK);
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

        private void buycode2_KeyDown(object sender, KeyEventArgs e)
        {
            CheckInt(sender, e);
        }

        private void buyprice2_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void buycode1_KeyDown(object sender, KeyEventArgs e)
        {
            CheckInt(sender, e);
        }

        private void buyprice1_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void buyjine1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(buyprice1.Text) && !string.IsNullOrWhiteSpace(buyjine1.Text))
            {
                double p = Double.Parse(buyprice1.Text);
                int amount;
                amount = (int)(double.Parse(buyjine1.Text) / p / 100.0 + 0.5) * 100;
                buyamount1.Text = amount.ToString();
            }
        }

        private void buyprice1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(buyprice1.Text) && !string.IsNullOrWhiteSpace(buyjine1.Text))
            {
                double p = Double.Parse(buyprice1.Text);
                int amount;
                amount = (int)(double.Parse(buyjine1.Text) / p / 100.0 + 0.5) * 100;
                buyamount1.Text = amount.ToString();
            }
        }

        private void buyjine2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(buyprice2.Text) && !string.IsNullOrWhiteSpace(buyjine2.Text))
            {
                double p = Double.Parse(buyprice2.Text);
                int amount;
                amount = (int)(double.Parse(buyjine2.Text) / p / 100.0 + 0.5) * 100;
                buyamount2.Text = amount.ToString();
            }
        }

        private void buyprice2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(buyprice2.Text) && !string.IsNullOrWhiteSpace(buyjine2.Text))
            {
                double p = Double.Parse(buyprice2.Text);
                int amount;
                amount = (int)(double.Parse(buyjine2.Text) / p / 100.0 + 0.5) * 100;
                buyamount2.Text = amount.ToString();
            }
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

        private void totjine_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(totjine.Text))
            {
                int amount = int.Parse(totjine.Text) / 2;
                buyjine1.Text = amount.ToString();
                buyjine2.Text = amount.ToString();
            }
            else
            {
                buyjine1.Text = "";
                buyjine2.Text = "";
            }
        }

        private void totjine_KeyDown(object sender, KeyEventArgs e)
        {
            CheckInt(sender, e);
        }

        private void pricebig_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void pricesmall_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void pricezy_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void pricezs_KeyDown(object sender, KeyEventArgs e)
        {
            CheckDouble(sender, e);
        }

        private void dosell()
        {
            bool flag = false;
            if (watchitems.Count != 2)
                return;
            InputStock temp1=new InputStock();
            InputStock temp2=new InputStock();
            temp1.Code = watchitems[0].Code;
            temp1.Price = watchitems[0].Current;
            temp1.Amount = watchitems[0].Amount;
            if (watchitems[0].State == SelectState.担保物买入)
                temp1.State = DealState.融券卖出;
            else if(watchitems[0].State == SelectState.融券卖出)
                temp1.State = DealState.买券还券;
            temp2.Code = watchitems[1].Code;
            temp2.Price = watchitems[1].Current;
            temp2.Amount = watchitems[1].Amount;
            if (watchitems[1].State == SelectState.担保物买入)
                temp2.State = DealState.融券卖出;
            else if (watchitems[1].State == SelectState.融券卖出)
                temp2.State = DealState.买券还券;
            
            if (tv.Count != 7) this.Dispatcher.Invoke(new showmsg(showmsgbx), "未匹配到下单软件");
            else
            {
                Auto auto = new Auto();
                bool t = auto.AutoSelect(tv, ((SelectState)temp1.State).ToString());
                if (t)
                {
                    System.Threading.Thread.Sleep(300);
                    int i = auto.AutoBuy(temp1);
                    auto.SetWindow(thishwnd);
                    if (i == 0)
                        flag = true;
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
            if (flag)
            {
                Auto auto = new Auto();
                bool t = auto.AutoSelect(tv, ((SelectState)temp2.State).ToString());
                if (t)
                {
                    System.Threading.Thread.Sleep(300);
                    int i = auto.AutoBuy(temp2);
                    auto.SetWindow(thishwnd);
                    if (i == 0)
                    {
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "下单成功");
                    }
                    else
                    {
                        if (temp1.State == DealState.买入股票)
                        {
                            i = auto.AutoCancel(0);
                            auto.SetWindow(thishwnd);
                            if (i != 0)
                                this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                        }
                        else if (temp1.State == DealState.融券卖出)
                        {
                            i = auto.AutoCancel(1);
                            auto.SetWindow(thishwnd);
                            if (i != 0)
                                this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                        }
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "下单发生错误");
                    }
                }
                else
                {
                    int i;
                    if (temp1.State == DealState.买入股票)
                    {
                        i = auto.AutoCancel(0);
                        auto.SetWindow(thishwnd);
                        if (i != 0)
                            this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                    }
                    else if (temp1.State == DealState.融券卖出)
                    {
                        i = auto.AutoCancel(1);
                        auto.SetWindow(thishwnd);
                        if (i != 0)
                            this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                    }
                    this.Dispatcher.Invoke(new showmsg(showmsgbx), "下单发生错误");
                }
            }
            autopingcang = false;
            this.Dispatcher.Invoke(new showpc(showpingcang), "盈亏平仓");
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
                    watchitems[i].Name = now[i].Name;
                    watchitems[i].Current = now[i].Current;
                    watchitems[i].Profit = getprofit(watchitems[i].Price, watchitems[i].Current, watchitems[i].Amount, watchitems[i].State);
                    sum += watchitems[i].Profit;
                }
                this.Dispatcher.Invoke(new showsum(showyk), sum);
                if (autopingcang)
                {
                    if (sum >= zhiying)
                    {
                        dosell();
                    }
                    if (-1*sum >= zhishun)
                    {
                        dosell();
                    }
                }
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

        private class myThread
        {
            public InputStock temp1;
            public InputStock temp2;
            public double big;
            public double small;
            public double amount;
        }

        public void watchbuy(object x)//thread function
        {
            myThread T = (myThread)x;
            bool flag=false;
            bool ff = false;
            double p;
            List<StockInfo> now = new List<StockInfo>();
            watchitem show1 = new watchitem();
            watchitem show2 = new watchitem();
            while (true)
            {
                List<string> codes = new List<string>();
                codes.Add(T.temp1.Code);
                codes.Add(T.temp2.Code);
                Sina con = new Sina();
                now = con.GetCurrent(codes);
                if (now[0].Name == null || now[1].Name == null)
                {
                    ff = true;
                    break;
                }
                if (now[0].Current >= now[1].Current)
                {
                    p = now[0].Current - now[1].Current;
                    if (p >= T.big && T.big != -1)
                    {
                        T.temp1.Price = now[0].Current;
                        T.temp1.State = DealState.融券卖出;
                        T.temp1.Amount=(int)(T.amount/ now[0].Current / 100.0 + 0.5) * 100;
                        T.temp2.Price = now[1].Current;
                        T.temp2.State = DealState.买入股票;
                        T.temp2.Amount = (int)(T.amount / now[1].Current / 100.0 + 0.5) * 100;
                        break;
                    }
                    if (p <= T.small && T.small != -1)
                    {
                        T.temp1.Price = now[0].Current;
                        T.temp1.State = DealState.买入股票;
                        T.temp1.Amount = (int)(T.amount / now[0].Current / 100.0 + 0.5) * 100;
                        T.temp2.Price = now[1].Current;
                        T.temp2.State = DealState.融券卖出;
                        T.temp2.Amount = (int)(T.amount / now[1].Current / 100.0 + 0.5) * 100;
                        break;
                    }
                }
                else if (now[0].Current < now[1].Current)
                {
                    p = now[1].Current - now[0].Current;
                    if (p >= T.big && T.big != -1)
                    {
                        T.temp1.Price = now[0].Current;
                        T.temp1.State = DealState.买入股票;
                        T.temp1.Amount = (int)(T.amount / now[0].Current / 100.0 + 0.5) * 100;
                        T.temp2.Price = now[1].Current;
                        T.temp2.State = DealState.融券卖出;
                        T.temp2.Amount = (int)(T.amount / now[1].Current / 100.0 + 0.5) * 100;
                        break;
                    }
                    if (p <= T.small && T.small != -1)
                    {
                        T.temp1.Price = now[0].Current;
                        T.temp1.State = DealState.融券卖出;
                        T.temp1.Amount = (int)(T.amount / now[0].Current / 100.0 + 0.5) * 100;
                        T.temp2.Price = now[1].Current;
                        T.temp2.State = DealState.买入股票;
                        T.temp2.Amount = (int)(T.amount / now[1].Current / 100.0 + 0.5) * 100;
                        break;
                    }

                }
                System.Threading.Thread.Sleep(1000);
            }
            if (ff)
            {
                this.Dispatcher.Invoke(new showmsg(showmsgbx), "获取价格出错");
                this.Autokaicang.Dispatcher.Invoke(new showkc(showkaicang), "价差开仓");
                return;
            }
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
                        flag=true;
                        show1 = makwatchitem(T.temp1);
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
            if (flag)
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
                            this.Dispatcher.Invoke(new showmsg(showmsgbx), "下单成功");
                            flag = true;
                            show2 = makwatchitem(T.temp2);
                            this.Dispatcher.Invoke(new addwatch(add), show1);
                            this.Dispatcher.Invoke(new addwatch(add), show2);
                            StartWatch();
                        }
                        else
                        {
                            if (T.temp1.State == DealState.买入股票)
                            {
                                i = auto.AutoCancel(0);
                                auto.SetWindow(thishwnd);
                                if (i != 0)
                                    this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                            }
                            else if (T.temp1.State == DealState.融券卖出)
                            {
                                i = auto.AutoCancel(1);
                                auto.SetWindow(thishwnd);
                                if (i != 0)
                                    this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                            }
                            this.Dispatcher.Invoke(new showmsg(showmsgbx), "下单发生错误");
                        }
                    }
                    else
                    {
                            int i;
                            if (T.temp1.State == DealState.买入股票)
                            {
                                i = auto.AutoCancel(0);
                                auto.SetWindow(thishwnd);
                                if (i != 0)
                                    this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                            }
                            else if (T.temp1.State == DealState.融券卖出)
                            {
                                i = auto.AutoCancel(1);
                                auto.SetWindow(thishwnd);
                                if (i != 0)
                                    this.Dispatcher.Invoke(new showmsg(showmsgbx), "撤单失败，请手动撤单");
                            }
                            this.Dispatcher.Invoke(new showmsg(showmsgbx), "下单发生错误");
                    }
            }
            this.Autokaicang.Dispatcher.Invoke(new showkc(showkaicang), "价差开仓");
        }

        private void Autokaicang_Click(object sender, RoutedEventArgs e)
        {
            if (Autokaicang.Content.ToString() == "价差开仓")
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
                if (string.IsNullOrWhiteSpace(buycode1.Text) || string.IsNullOrWhiteSpace(buyjine1.Text) || string.IsNullOrWhiteSpace(buycode2.Text) || string.IsNullOrWhiteSpace(buyjine2.Text))
                {
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    return;
                }
                temp1.Code = buycode1.Text;
                temp2.Code = buycode2.Text;
                myThread temp = new myThread();
                temp.temp1 = temp1;
                temp.temp2 = temp2;
                temp.amount = double.Parse(buyjine1.Text);
                if (!string.IsNullOrWhiteSpace(pricebig.Text))
                {
                    temp.big = double.Parse(pricebig.Text);
                    flag = true;
                }
                else
                {
                    temp.big = -1;
                } 
                if (!string.IsNullOrWhiteSpace(pricesmall.Text))
                {
                    temp.small = double.Parse(pricesmall.Text);
                    flag = true;
                }
                else
                {
                    temp.small = -1;
                }
                if (!flag)
                {
                    ModernDialog.ShowMessage("未输入差价", "", MessageBoxButton.OK);
                    return;
                }
                Thread t = new Thread(new ParameterizedThreadStart(watchbuy));
                t.IsBackground = true;
                t.Name = "自动开仓线程";
                t.Start(temp);
                autokaicang = t;
                Autokaicang.Content = "开仓停止";
            }
            else if (Autokaicang.Content.ToString() == "开仓停止")
            {
                autokaicang.Abort();
                Autokaicang.Content = "价差开仓";
            }
        }

        private void Autopingcang_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            if (Autopingcang.Content.ToString() == "盈亏平仓")
            {
                if (tv.Count != 7)
                {
                    ModernDialog.ShowMessage("未匹配到下单软件", "", MessageBoxButton.OK);
                    PPbutton.IsEnabled = true;
                    return;
                }
                if (autowatch==null)
                {
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(pricezy.Text))
                {
                    zhiying = double.Parse(pricezy.Text);
                    flag = true;
                }
                else
                    zhiying = -1;
                if(!string.IsNullOrWhiteSpace(pricezs.Text))
                {
                    zhishun = double.Parse(pricezs.Text);
                    flag = true;
                }
                else
                    zhishun = -1;
                if (!flag)
                {
                    ModernDialog.ShowMessage("未输入价格", "", MessageBoxButton.OK);
                    return;
                }
                autopingcang = true;
            }
            else if (Autopingcang.Content.ToString() == "平仓停止")
            {
                if (autopingcang)
                    autopingcang = false;
                zhiying = -1;
                zhishun = -1;
                Autokaicang.Content = "盈亏平仓";
            }
        }
    }
}
