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
    /// BasicPage4.xaml 的交互逻辑
    /// </summary>
    public partial class BasicPage4 : UserControl
    {
        List<IntPtr> tv = new List<IntPtr>();
        IntPtr thishwnd = new IntPtr();

        public class Selectitem
        {
            public string Name { set; get; }
            public int ID { set; get; }
        }
        public static ObservableCollection<Selectitem> selectitems = new ObservableCollection<Selectitem>() 
        {  (new Selectitem { ID = 1, Name = SelectState.担保物买入.ToString() }),
           (new Selectitem { ID = 2, Name = SelectState.担保物卖出.ToString() }),
           (new Selectitem { ID = 3, Name = SelectState.融资买入.ToString() }),
           (new Selectitem { ID = 4, Name = SelectState.融券卖出.ToString() }),
           (new Selectitem { ID = 5, Name = SelectState.卖券还款.ToString() }),
           (new Selectitem { ID = 6, Name = SelectState.买券还券.ToString() })};

        public BasicPage4()
        {
            InitializeComponent();
            this.SelectDealState.ItemsSource = selectitems;
        }

        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            Auto con = new Auto();
            thishwnd = con.GetWindowHwnd("融资融券");
            if (tv.Count != 7)
            {
                ModernDialog.ShowMessage("未匹配到下单软件", "", MessageBoxButton.OK);
                PPbutton.IsEnabled = true;
            }
            else
            {
                InputStock temp = new InputStock();
                if (string.IsNullOrWhiteSpace(buycode.Text) || string.IsNullOrWhiteSpace(buyprice.Text) || string.IsNullOrWhiteSpace(buyamount.Text) || this.SelectDealState.SelectedIndex == -1)
                    ModernDialog.ShowMessage("输入错误", "", MessageBoxButton.OK);
                else
                {
                    temp.Code = buycode.Text;
                    temp.Price = double.Parse(buyprice.Text);
                    temp.Amount = int.Parse(buyamount.Text);
                    temp.State = (DealState)(this.SelectDealState.SelectedIndex + 1);
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
                            else if (i > 10) ModernDialog.ShowMessage("发生未知错误", "", MessageBoxButton.OK);
                            else ModernDialog.ShowMessage("控件获取发生错误", "", MessageBoxButton.OK);
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
    }
}
