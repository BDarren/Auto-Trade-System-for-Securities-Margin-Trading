using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassofConnect;
using System.Timers;

namespace form
{
    public partial class Form1 : Form
    {
        static int update = 0;
        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
            listView1.Clear();
            listView1.Columns.Add("");
            listView1.Columns.Add("");
            listView1.Columns.Add("");
            listView2.Clear();
            listView2.Columns.Add("");
            listView2.Columns.Add("");
            listView2.Columns.Add("");
            listView3.Clear();
            listView3.Columns.Add("方向");
            listView3.Columns.Add("代码");
            listView3.Columns.Add("名称");
            listView3.Columns.Add("数量");
            listView3.Columns.Add("成交价");
            listView3.Columns.Add("成交金额");
            listView3.Columns.Add("现价");
            listView3.Columns.Add("盈亏");
            listView3.Columns[0].Width = 100;
            listView3.Columns[1].Width = 100;
            listView3.Columns[2].Width = 100;
            listView3.Columns[3].Width = 100;
            listView3.Columns[4].Width = 100;
            listView3.Columns[5].Width = 100;
            listView3.Columns[6].Width = 100;
            listView3.Columns[7].Width = 100;
            Buyprofit.Text = "0";
            Sellprofit.Text = "0";
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(TimedEvent);
            aTimer.Interval = 1 * 1000;    //配置文件中配置的秒数
            aTimer.Enabled = true;
            System.Timers.Timer sysTimer = new System.Timers.Timer();
            sysTimer.Elapsed += new ElapsedEventHandler(Event);
            sysTimer.Interval = 1 * 1000;    //配置文件中配置的秒数
            sysTimer.Enabled = true;
            //初始化参数
            tlje.Text = "100000";
            yjfl.Text = "0.001";
            yhsl.Text = "0.001";
            rzlx.Text = "0.08";
            rzday.Text = "0";
            rqlx.Text = "0.08";
            rqday.Text = "0";
        }
        private void TimedEvent(object source, ElapsedEventArgs e)//刷新定时器
        {
            if(listView1.Items.Count>0)
            {
                int t = show(this.listView1, Buycode.Text,1);  
            }
            if (listView2.Items.Count > 0)
            {
                int t = show(this.listView2, Sellcode.Text,2);
            }
            if (listView3.Items.Count > 0)
            {
                tt buy = new tt();
                buy = getbuylist(listView3.Items[0].SubItems[1].Text, double.Parse(listView3.Items[0].SubItems[4].Text), double.Parse(tlje.Text));
                tt sell = new tt();
                sell = getselllist(listView3.Items[1].SubItems[1].Text, double.Parse(listView3.Items[1].SubItems[4].Text), double.Parse(tlje.Text));
                if (buy.direction != null && sell.direction != null)
                {
                    listView3.Items.Clear();
                    Buyamount.Text = buy.amount.ToString();
                    Sellamount.Text = sell.amount.ToString();
                    play(buy);
                    play(sell);
                    double t = getsum();
                    Total.Text = t.ToString();
                    if (t >= 0) Total.ForeColor = Color.Red;
                    else Total.ForeColor = Color.Green;
                }
            }
        }
        delegate void SetValueEventHandler(Label label, string text);
        void SetValueEvent(Label label, string text)
        {
            label.Text = text;
        }
        private void Event(object source, ElapsedEventArgs e)
        {
            DateTime t = DateTime.Now;
            Invoke(new SetValueEventHandler(SetValueEvent), systemtime, t.ToLocalTime().ToString());
        }
        private void Buycode_TextChanged(object sender, EventArgs e)
        {
            int l = Buycode.Text.Length;
            if (l == 6)
            {
                int t = show(this.listView1, Buycode.Text,1);
                if (t == 0)
                {
                    MessageBox.Show("输入代码有误");
                    Buycode.Text = "";
                    listView1.EndUpdate();
                }
                else
                {
                    listView1.Refresh();
                }
            }
        }
        private void Sellcode_TextChanged(object sender, EventArgs e)
        {
            int l = Sellcode.Text.Length;
            if (l == 6)
            {
                int t = show(this.listView2, Sellcode.Text,2);
                if (t == 0)
                {
                    MessageBox.Show("输入代码有误");
                    Sellcode.Text = "";
                    listView2.EndUpdate();
                }
                else
                {
                    listView2.Refresh();
                }
            }
        }
        private int show(ListView listview, string code,int state)
        {
            if (update == 1) listview.EndUpdate();
            listview.BeginUpdate();
            update = 1;
            listview.Items.Clear();
            Sina con = new Sina();
            List<StockInfo> now = new List<StockInfo>();
            now = con.GetCurrent(code);
            if (now[0].Name == null)
                return 0;
            if (state == 1)
            {
                Buyname.Text = now[0].Name;
                buytime.Text = now[0].Time.TimeOfDay.ToString();
            }
            if (state == 2)
            {
                Sellname.Text = now[0].Name;
                selltime.Text = now[0].Time.TimeOfDay.ToString();
            }
            ListViewItem items;
            items = new ListViewItem("卖五");
            items.SubItems.Add(now[0].SellList[4].Price.ToString());
            items.SubItems.Add(now[0].SellList[4].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("卖四");
            items.SubItems.Add(now[0].SellList[3].Price.ToString());
            items.SubItems.Add(now[0].SellList[3].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("卖三");
            items.SubItems.Add(now[0].SellList[2].Price.ToString());
            items.SubItems.Add(now[0].SellList[2].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("卖二");
            items.SubItems.Add(now[0].SellList[1].Price.ToString());
            items.SubItems.Add(now[0].SellList[1].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("卖一");
            items.SubItems.Add(now[0].SellList[0].Price.ToString());
            items.SubItems.Add(now[0].SellList[0].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("最新");
            items.SubItems.Add(now[0].Current.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("买一");
            items.SubItems.Add(now[0].BuyList[0].Price.ToString());
            items.SubItems.Add(now[0].BuyList[0].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("买二");
            items.SubItems.Add(now[0].BuyList[1].Price.ToString());
            items.SubItems.Add(now[0].BuyList[1].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("买三");
            items.SubItems.Add(now[0].BuyList[2].Price.ToString());
            items.SubItems.Add(now[0].BuyList[2].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("买四");
            items.SubItems.Add(now[0].BuyList[3].Price.ToString());
            items.SubItems.Add(now[0].BuyList[3].Amount.ToString());
            listview.Items.Add(items);
            items = new ListViewItem("买五");
            items.SubItems.Add(now[0].BuyList[4].Price.ToString());
            items.SubItems.Add(now[0].BuyList[4].Amount.ToString());
            listview.Items.Add(items);
            update = 0;
            listview.EndUpdate();
            return 1;
        }

        public struct tt
        {
            public string direction;//方向
            public string code; //代码
            public string name; //名称
            public int amount;  //数量
            public double price;    //成交价
            public double current;  //现价
            public double profit;   //盈亏
        }

        public void play(tt temp)
        {
            ListViewItem items;
            items = new ListViewItem(temp.direction);
            items.SubItems.Add(temp.code);
            items.SubItems.Add(temp.name);
            items.SubItems.Add(temp.amount.ToString());
            items.SubItems.Add(temp.price.ToString());
            double t=temp.price*temp.amount;
            items.SubItems.Add(t.ToString());
            items.SubItems.Add(temp.current.ToString());
            items.SubItems.Add(temp.profit.ToString("f2")); ;
            if (temp.profit < 0)
                items.ForeColor = Color.Green;
            else
                items.ForeColor = Color.Red;
            this.listView3.Items.Add(items);
        }

        public tt getbuylist(string code,double price,double sum)
        {
            tt t = new tt();
            Sina con = new Sina();
            List<StockInfo> now = new List<StockInfo>();
            now = con.GetCurrent(code);
            if (now[0].Name == null)
            {
                t.direction = null;
                return t;
            }
            t.price = price;
            t.code = code;
            t.name = now[0].Name;
            t.direction = "买入";
            t.current = now[0].Current;
            t.amount = (int)Math.Ceiling(sum / 200 / price) * 100;
            t.profit = t.amount * t.current * (1 - double.Parse(yjfl.Text) - double.Parse(yhsl.Text)) - t.amount * price * (1 + double.Parse(rzlx.Text) * int.Parse(rzday.Text) / 365+double.Parse(yjfl.Text));
            return t;
        }

        public tt getselllist(string code, double price, double sum)
        {
            tt t = new tt();
            Sina con = new Sina();
            List<StockInfo> now = new List<StockInfo>();
            now = con.GetCurrent(code);
            if (now[0].Name == null)
            {
                t.direction = null;
                return t;
            }
            t.price = price;
            t.code = code;
            t.name = now[0].Name;
            t.direction = "卖出";
            t.current = now[0].Current;
            t.amount = (int)Math.Ceiling(sum / 200 / price) * 100;
            t.profit = t.amount * price * (1 - double.Parse(yjfl.Text) - double.Parse(yhsl.Text) - double.Parse(rqlx.Text) * int.Parse(rqday.Text) / 365) - t.amount * t.current * (1 + double.Parse(yjfl.Text));
            return t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tt buy = new tt();
            buy = getbuylist(Buycode.Text,double.Parse(Buyprice.Text),double.Parse(tlje.Text));
            tt sell = new tt();
            sell = getselllist(Sellcode.Text, double.Parse(Sellprice.Text),double.Parse(tlje.Text));
            if (buy.direction == null || sell.direction == null)
                MessageBox.Show("输入代码有误");
            else
            {
                jktime.Text = Convert.ToDateTime(systemtime.Text).ToLongTimeString();
                listView3.Items.Clear();
                Buyamount.Text = buy.amount.ToString();
                Sellamount.Text = sell.amount.ToString();
                play(buy);
                play(sell);
                double t=getsum();
                Total.Text = t.ToString();
                if (t >= 0) Total.ForeColor = Color.Red;
                else Total.ForeColor = Color.Green;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tt buy = new tt();
            buy = getbuylist(Buycode.Text, double.Parse(listView1.Items[5].SubItems[1].Text), double.Parse(tlje.Text));
            tt sell = new tt();
            sell = getselllist(Sellcode.Text, double.Parse(listView2.Items[5].SubItems[1].Text), double.Parse(tlje.Text));
            if (buy.direction == null || sell.direction == null)
                MessageBox.Show("输入代码有误");
            else
            {
                Buyprice.Text = listView1.Items[5].SubItems[1].Text;
                Sellprice.Text = listView2.Items[5].SubItems[1].Text;
                jktime.Text = Convert.ToDateTime(systemtime.Text).ToLongTimeString();
                listView3.Items.Clear();
                Buyamount.Text = buy.amount.ToString();
                Sellamount.Text = sell.amount.ToString();
                play(buy);
                play(sell);
                double t = getsum();
                Total.Text = t.ToString();
                if (t >= 0) Total.ForeColor = Color.Red;
                else Total.ForeColor = Color.Green;
            }
        }

        private double getsum()
        {
            double sum = 0;
            for (int i = 0; i < listView3.Items.Count; i++)
            {
                sum += double.Parse(listView3.Items[i].SubItems[7].Text);
            }
            return sum;
        }

        private void CalculateBuy_Click(object sender, EventArgs e)
        {
            double t = int.Parse(Buyamount.Text) * double.Parse(Setbuy.Text) * (1 - double.Parse(yjfl.Text) - double.Parse(yhsl.Text)) - int.Parse(Buyamount.Text) * double.Parse(Buyprice.Text) * (1 + double.Parse(rzlx.Text) * int.Parse(rzday.Text) / 365 + double.Parse(yjfl.Text));
            Buyprofit.Text = t.ToString("f2");
            t += double.Parse(Sellprofit.Text);
            if (t >= 0) CalculateTotal.ForeColor = Color.Red;
            else CalculateTotal.ForeColor = Color.Green;
            CalculateTotal.Text = t.ToString("f2");
        }

        private void CalculateSell_Click(object sender, EventArgs e)
        {
            double t = int.Parse(Sellamount.Text) * double.Parse(Sellprice.Text) * (1 - double.Parse(yjfl.Text) - double.Parse(yhsl.Text) - double.Parse(rqlx.Text) * int.Parse(rqday.Text) / 365) - int.Parse(Sellamount.Text) * double.Parse(Setsell.Text) * (1 + double.Parse(yjfl.Text));
            Sellprofit.Text = t.ToString("f2");
            t += double.Parse(Buyprofit.Text);
            if (t >= 0) CalculateTotal.ForeColor = Color.Red;
            else CalculateTotal.ForeColor = Color.Green;
            CalculateTotal.Text = t.ToString("f2");
        }
    }
}
