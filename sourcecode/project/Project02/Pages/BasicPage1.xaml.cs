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

namespace Project02.Pages
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class BasicPage1 : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets or sets the country list.
        /// </summary>
        /// <value>The country list.</value>
        public List<string> StockList { get; set; }
        /// <summary>
        /// Gets or sets the selected country.
        /// </summary>
        /// <value>The selected country.</value>
        public string SelectedStoke { get; set; }
        #endregion

        List<IntPtr> tv = new List<IntPtr>();
        IntPtr thishwnd = new IntPtr();
        Dictionary<string, Thread> Systhread = new Dictionary<string, Thread>();
        //int sysint = 0;
        public class listitem
        {
            public string Name { get; set; }

            public string Price { get; set; }

            public string amount { get; set; }
        }

        public class watchitem
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public double WatchPrice { get; set; }
            public int Amount { get; set; }
            public string Time { get; set; }
            public SelectState State { get; set; }
        }
        public static ObservableCollection<watchitem> watchitems = new ObservableCollection<watchitem>();


        public class Selectitem
        {
            public string Name { set; get; }
            public int ID { set; get; }
        }
        public class Selectitems : ObservableCollection<Selectitem>
        {
            public Selectitems()
            {
                this.Add(new Selectitem { ID = 1, Name = SelectState.担保物买入.ToString() });
                this.Add(new Selectitem { ID = 2, Name = SelectState.担保物卖出.ToString() });
                this.Add(new Selectitem { ID = 3, Name = SelectState.融资买入.ToString() });
                this.Add(new Selectitem { ID = 4, Name = SelectState.融券卖出.ToString() });
                this.Add(new Selectitem { ID = 5, Name = SelectState.买券还券.ToString() });
                this.Add(new Selectitem { ID = 5, Name = SelectState.卖券还款.ToString() });
            }
        }
        public static ObservableCollection<Selectitem> selectitems = new ObservableCollection<Selectitem>() 
        {  (new Selectitem { ID = 1, Name = SelectState.担保物买入.ToString() }),
           (new Selectitem { ID = 2, Name = SelectState.担保物卖出.ToString() }),
           (new Selectitem { ID = 3, Name = SelectState.融资买入.ToString() }),
           (new Selectitem { ID = 4, Name = SelectState.融券卖出.ToString() }),
           (new Selectitem { ID = 5, Name = SelectState.卖券还款.ToString() }),
           (new Selectitem { ID = 6, Name = SelectState.买券还券.ToString() })};

        private List<StockInfo> getstoke(string code)
        {
            Sina con = new Sina();
            List<StockInfo> now = new List<StockInfo>();
            now = con.GetCurrent(code);
            return now;
        }

        private delegate void showlist1(List<StockInfo> now);
        private void show1(List<StockInfo> now)
        {
            this.listview1.ItemsSource = new listitem[]{
            new listitem{Name = "卖五",Price = now[0].SellList[4].Price.ToString(),amount = now[0].SellList[4].Amount.ToString()},
            new listitem{Name = "卖四",Price = now[0].SellList[3].Price.ToString(),amount = now[0].SellList[3].Amount.ToString()},
            new listitem{Name = "卖三",Price = now[0].SellList[2].Price.ToString(),amount = now[0].SellList[2].Amount.ToString()},
            new listitem{Name = "卖二",Price = now[0].SellList[1].Price.ToString(),amount = now[0].SellList[1].Amount.ToString()},
            new listitem{Name = "卖一",Price = now[0].SellList[0].Price.ToString(),amount = now[0].SellList[0].Amount.ToString()},
            new listitem{Name = "最新",Price = now[0].Current.ToString(),amount = null},
            new listitem{Name = "买一",Price = now[0].BuyList[0].Price.ToString(),amount = now[0].BuyList[0].Amount.ToString()},
            new listitem{Name = "买二",Price = now[0].BuyList[1].Price.ToString(),amount = now[0].BuyList[1].Amount.ToString()},
            new listitem{Name = "买三",Price = now[0].BuyList[2].Price.ToString(),amount = now[0].BuyList[2].Amount.ToString()},
            new listitem{Name = "买四",Price = now[0].BuyList[3].Price.ToString(),amount = now[0].BuyList[3].Amount.ToString()},
            new listitem{Name = "买五",Price = now[0].BuyList[4].Price.ToString(),amount = now[0].BuyList[4].Amount.ToString()}
            };
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
        private delegate void showmsg(string s);
        public static void showmsgbx(string s)
        {
            ModernDialog.ShowMessage(s, "", MessageBoxButton.OK);
        }
        public BasicPage1()
        {
            InitializeComponent();
            watchitems.Clear();
            this.listview2.ItemsSource = watchitems;
            this.SelectDealState.ItemsSource = selectitems;

            StockList = new List<string>();

            StreamReader sr = new StreamReader(@"StockList.txt", System.Text.Encoding.Default);
            string s = sr.ReadLine();
            while (s != null)
            {
                StockList.Add(s);
                s = sr.ReadLine();
            }
            StokeCode.ItemsSource = StockList;
            sr.Close();
        }

        private void UpdateStock()
        {
            bool flag = true;
            while (true)
            {
                string s = (string)this.StokeCode.Dispatcher.Invoke(new Func<string>(() =>
                {
                    return StokeCode.getstring();
                }));
                List<StockInfo> now = getstoke(s);
                if (now[0].Name == null)
                {
                    if (flag)
                        this.Dispatcher.Invoke(new showmsg(showmsgbx), "输入代码有误");
                    break;
                }
                else
                {
                    flag = false;
                    this.listview1.Dispatcher.Invoke(new showlist1(show1),now);
                    this.LabelOfTime.Dispatcher.Invoke(new showlab(show2), now);
                    this.StokeName.Dispatcher.Invoke(new showname(show3), now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(UpdateStock));
            t.IsBackground = true;
            t.Start();
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

        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            Auto con = new Auto();
            thishwnd = con.GetWindowHwnd("融资融券");
            if (tv.Count != 7) ModernDialog.ShowMessage("未匹配到下单软件", "", MessageBoxButton.OK);
            else
            {
                InputStock temp = new InputStock();
                if (string.IsNullOrWhiteSpace(buycode.Text) || string.IsNullOrWhiteSpace(buyprice.Text) || string.IsNullOrWhiteSpace(buyamount.Text)||this.SelectDealState.SelectedIndex == -1)
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else
                {
                    temp.Code = buycode.Text;
                    temp.Price = double.Parse(buyprice.Text);
                    temp.Amount = int.Parse(buyamount.Text);
                    temp.State = (DealState)(this.SelectDealState.SelectedIndex+1);
                    int length = buyprice.Text.Length - buyprice.Text.IndexOf(".") - 1;
                    if (temp.Amount % 100 != 0 || length > 3)
                        ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                    else
                    {
                        bool t = con.AutoSelect(tv, ((SelectState)(this.SelectDealState.SelectedIndex+1)).ToString());
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
                            */
                            if (i == 0) ModernDialog.ShowMessage("成功下单", "", MessageBoxButton.OK);
                            else if (i == 4) ModernDialog.ShowMessage("输入价格超出当天涨跌停限制", "", MessageBoxButton.OK);
                            else if (i == 5) ModernDialog.ShowMessage("弹出消息框未知", "", MessageBoxButton.OK);
                            else if (i == 6) ModernDialog.ShowMessage("选择不匹配", "", MessageBoxButton.OK);
                            else if (i == 7) ModernDialog.ShowMessage("输入数据不正确", "", MessageBoxButton.OK);
                            else if (i == 8) ModernDialog.ShowMessage("获取窗体出错", "", MessageBoxButton.OK);
                            else if (i == 9) ModernDialog.ShowMessage("未启动下单软件", "", MessageBoxButton.OK);
                            else if (i>10) ModernDialog.ShowMessage("发生未知错误", "", MessageBoxButton.OK);
                            else ModernDialog.ShowMessage("控件获取发生错误", "", MessageBoxButton.OK);
                        }
                        else
                        {
                            ModernDialog.ShowMessage("选择出错", "", MessageBoxButton.OK);
                        }
                    }
                }
            }
        }


        private delegate void removeitem(watchitem s);
        public static void remove(watchitem s)
        {
            watchitems.Remove(s);
        }



        class myThread 
        {
            public string Code;
            public double Price;
            public int Amount;
            public watchitem Select;
            public SelectState State;
            public IntPtr mainhwnd;
            public List<IntPtr> treeview;
            public BasicPage1 window;
            public void autobuy()
            {
                bool flag = false;
                InputStock temp = new InputStock();
                temp.Code = this.Code;
                temp.Price = this.Price;
                temp.Amount = this.Amount;
                temp.State = (DealState)((int)this.State + 1);
                List<StockInfo> now = new List<StockInfo>();
                while (true)
                {
                    Sina con = new Sina();
                    now = con.GetCurrent(temp.Code);
                    if (now[0].Name == null)
                        break;
                    if (temp.Price == now[0].Current)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                if (flag)
                {
                    if (treeview.Count != 7) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "未匹配到下单软件");
                    else
                    {
                        Auto auto = new Auto();
                        bool t = auto.AutoSelect(treeview, this.State.ToString());
                        if (t)
                        {
                            System.Threading.Thread.Sleep(300);
                            int i = auto.AutoBuy(temp);
                            auto.SetWindow(mainhwnd);
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
                            */
                            if (i == 0)
                            {
                                window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "成功下单");
                                window.Dispatcher.BeginInvoke(new removeitem(remove), Select);
                            }
                            else if (i == 4) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "输入价格超出当天涨跌停限制");
                            else if (i == 5) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "弹出消息框未知");
                            else if (i == 6) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "选择不匹配");
                            else if (i == 7) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "输入数据不正确");
                            else if (i == 8) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "获取窗体出错");
                            else if (i == 9) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "未启动下单软件");
                            else if (i > 10) window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "发生未知错误");
                            else window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "控件获取发生错误");
                        }
                        else
                        {
                            window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "选择出错");
                        }
                    }
                }
                else
                {
                   window.Dispatcher.BeginInvoke(new showmsg(showmsgbx), "无法获取当前股票价格");
                }
            }
        }

        private void Watching_Click(object sender, RoutedEventArgs e)
        {
            myThread temp = new myThread();
            if (string.IsNullOrWhiteSpace(buycode.Text) || string.IsNullOrWhiteSpace(buyprice.Text) || string.IsNullOrWhiteSpace(buyamount.Text))
                ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
            else
            {
                Auto con = new Auto();
                temp.Code = buycode.Text;
                temp.Price = double.Parse(buyprice.Text);
                temp.Amount = int.Parse(buyamount.Text);

                temp.State = (SelectState)(this.SelectDealState.SelectedIndex);

                thishwnd = con.GetWindowHwnd("融资融券");
                temp.mainhwnd = thishwnd;
                temp.treeview = tv;
                temp.window = this;
                int inputlength = buyprice.Text.Length - buyprice.Text.IndexOf(".") - 1;

                double pz=0,pd=0;
                Sina link=new Sina();
                List<StockInfo> now = new List<StockInfo>();
                now = link.GetCurrent(temp.Code);
                int length = now[0].YesterdayClose.ToString().Length - now[0].YesterdayClose.ToString().IndexOf(".") - 1;
                if (length == 2)
                {
                    pz = (double)((int)((now[0].YesterdayClose * 1.1 + 0.005) * 100)) / 100;
                    pd = (double)((int)((now[0].YesterdayClose * 0.9 + 0.005) * 100)) / 100;
                }
                else if (length == 3)
                {
                    pz = (double)((int)((now[0].YesterdayClose * 1.1 + 0.0005) * 1000)) / 1000;
                    pd = (double)((int)((now[0].YesterdayClose * 0.9 + 0.0005) * 1000)) / 1000;
                }

                if (temp.Amount % 100 != 0 || inputlength > 3)
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else if(temp.Price>pz||temp.Price<pd)
                    ModernDialog.ShowMessage("输入价格超出当天涨跌停限制", "", MessageBoxButton.OK);
                else
                {
                    DateTime time = DateTime.Now;
                    string st = time.ToLongTimeString();
                    watchitem tempw = new watchitem { Name = now[0].Name, Code = temp.Code, WatchPrice = temp.Price, Amount = temp.Amount, Time = st, State = temp.State };
                    temp.Select = tempw;
                    Thread t = new Thread(new ThreadStart(temp.autobuy));
                    t.IsBackground = true;
                    //t.Name = "监控" + ('0'+sysint);
                    //sysint++;
                    t.Start();
                    Systhread.Add(st, t);
                    watchitems.Add(tempw);
                }
            }
        }

        private void StopWatch_Click(object sender, RoutedEventArgs e)
        {
            watchitem t = listview2.SelectedItem as watchitem;
            if (t != null)
            {
                Thread temp = Systhread[t.Time];
                temp.Abort();
                watchitems.Remove(t);
            }
            else
                ModernDialog.ShowMessage("没有选中任何项", "", MessageBoxButton.OK);
        }
    }
}
