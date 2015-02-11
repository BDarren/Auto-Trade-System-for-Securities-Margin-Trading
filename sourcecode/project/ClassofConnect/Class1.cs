using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ClassofConnect
{
    public enum GoodsState
    {
        buy=0,
        sell=1
    }
    public class GoodsInfo
    {
        public int Amount
        { get; set; }
        public double Price
        { get; set; }
        public GoodsState State 
        { get; set; }
    }

    public class StockInfo
    {
		public string Name  //股票名字
        { get; set; }
        public double TodayOpen    //今日开盘价
        { get; set; }
        public double YesterdayClose   //昨日收盘价
        { get; set; }
        public double Current  //当前价
        { get; set; }
        public double High //今日最高价
        { get; set; }
        public double Low  //今日最低价
        { get; set; }
        public double Buy  //买一价
        { get; set; }
        public double Sell //卖一价
        { get; set; }
        public int VolAmount    //成交股票数，单位“股”。100股为1手。 
        { get; set; }
        public double VolMoney //成交额，单位“元”。一般需要转换成“万元”。 
        { get; set; }
        public List<GoodsInfo> BuyList  //买单情况
        { get; set; }
        public List<GoodsInfo> SellList //卖单情况 
        { get; set; }
        public DateTime Time    //日期时间
        { get; set; }
        public override string ToString()
        {
            return Name + ": " + VolAmount + ":" + Current;
        }  
    }
    public interface IDataService
    {
        List<StockInfo> GetCurrent(string stockCode);
        List<StockInfo> GetCurrent(List<string> stockCodes);
    }
    public class Sina : IDataService
    {

        private const string dataurl = "http://hq.sinajs.cn/list=";
        #region IStockInfo Members
        private List<StockInfo> PrevInfo=new List<StockInfo>();
        public List<StockInfo> GetCurrent(string stockCode)
        {
            try
            {
                //if (stockCode.Substring(0, 2) == "60")//上海是600打头
                //{
                //    stockCode = "sh" + stockCode;
                //}
                //else if (stockCode.Substring(0, 2) == "00")//深圳
                //{
                //    stockCode = "sz" + stockCode;
                //}
                //else if (stockCode.Substring(0, 3) == "300")//创业板块
                //{
                //    stockCode = "sz" + stockCode;
                //}
                //else if (stockCode.Substring(0, 3) == "510")//
                //{
                //    stockCode = "sh" + stockCode;
                //}
                //else if (stockCode.Substring(0, 3) == "159")//
                //{
                //    stockCode = "sz" + stockCode;
                //}
                //else
                //{
                //    stockCode = "0";
                //}
                if (stockCode.Substring(0, 1) == "6" || stockCode.Substring(0, 1) == "5" || stockCode.Substring(0, 1) == "9")
                    stockCode = "sh" + stockCode;
                else
                    stockCode = "sz" + stockCode;
                string url =dataurl+stockCode;
                WebClient MyWebClient = new WebClient();
                Byte[] pageData = MyWebClient.DownloadData(url);
                string pageHtml = Encoding.Default.GetString(pageData);
                PrevInfo.Add(Parse(pageHtml));
                return PrevInfo;
            }
            catch
            {
                if (PrevInfo.Count == 0)
                {
                    StockInfo info = new StockInfo();
                    info.Name = null;
                    PrevInfo.Add(info);
                }
                return PrevInfo;
            }

        }
        public List<StockInfo> GetCurrent(List<string> stockCodes)
        {
            string url = dataurl;
            int num = stockCodes.Count;
            for (int i = 0; i < num; i++)
            {
                //if (stockCodes[i].Substring(0, 2) == "60")//上海是600打头
                //{
                //    stockCodes[i] = "sh" + stockCodes[i];
                //}
                //else if (stockCodes[i].Substring(0, 2) == "00")//深圳
                //{
                //    stockCodes[i] = "sz" + stockCodes[i];
                //}
                //else if (stockCodes[i].Substring(0, 3) == "300")//创业板块
                //{
                //    stockCodes[i] = "sz" + stockCodes[i];
                //}
                //else if (stockCodes[i].Substring(0, 3) == "510")//
                //{
                //    stockCodes[i] = "sh" + stockCodes[i];
                //}
                //else if (stockCodes[i].Substring(0, 3) == "159")//
                //{
                //    stockCodes[i] = "sz" + stockCodes[i];
                //}
                //else
                //{
                //    stockCodes[i] = "0";
                //}
                if (stockCodes[i].Substring(0, 1) == "6" || stockCodes[i].Substring(0, 1) == "5" || stockCodes[i].Substring(0, 1) == "9")
                    stockCodes[i] = "sh" + stockCodes[i];
                else
                    stockCodes[i] = "sz" + stockCodes[i];
                if (i==num-1)
                    url += stockCodes[i];
                else
                    url += stockCodes[i] + ",";
            }
            try
            {
                WebClient MyWebClient = new WebClient();
                Byte[] pageData = MyWebClient.DownloadData(url);
                string pageHtml = Encoding.Default.GetString(pageData);
                string[] temp = pageHtml.Split('\n');
                for (int i = 0; i < num; i++)
                {
                    PrevInfo.Add(Parse(temp[i]));
                }
                return PrevInfo;
            }
            catch
            {
                if (PrevInfo.Count == 0)
                {
                    StockInfo info = new StockInfo();
                    info.Name = null;
                    PrevInfo.Add(info);
                }
                return PrevInfo;
            }
        }
        public static StockInfo Parse(string content)
        {
            // var hq_str_sh600066 = "宇通客车,9.27,9.35,9.76,9.80,9.27,9.77,9.78,4567858,44306952,3100,9.77,1200,9.76,20500,9.75,1400,9.74,15300,9.73,10030,9.78,28093,9.79,156827,9.80,2800,9.81,6400,9.82,2009-01-09,15:03:32";
            int start = content.IndexOf('"') + 1;
            int end = content.IndexOf('"', start);
            string input = content.Substring(start, end - start);
            string[] temp = input.Split(',');
            StockInfo info = new StockInfo();
            if (temp.Length != 32&&temp.Length!= 33)
            {
                info.Name = null;
                return info ;
            }
            info.Name = temp[0];
            info.TodayOpen = double.Parse(temp[1]);
            info.YesterdayClose = double.Parse(temp[2]);
            info.Current = double.Parse(temp[3]);
            info.High = double.Parse(temp[4]);
            info.Low = double.Parse(temp[5]);
            info.Buy = double.Parse(temp[6]);
            info.Sell = double.Parse(temp[7]);
            info.VolAmount = int.Parse(temp[8]);
            info.VolMoney = double.Parse(temp[9]);
            info.BuyList = new List<GoodsInfo>(5);
            int index = 10;
            for (int i = 0; i < 5; i++)
            {
                GoodsInfo goods = new GoodsInfo();
                goods.State = GoodsState.buy;
                goods.Amount = int.Parse(temp[index]);
                index++;
                goods.Price = double.Parse(temp[index]);
                index++;
                info.BuyList.Add(goods);
            }
            info.SellList = new List<GoodsInfo>(5);
            for (int i = 0; i < 5; i++)
            {
                GoodsInfo goods = new GoodsInfo();
                goods.State = GoodsState.sell;
                goods.Amount = int.Parse(temp[index]);
                index++;
                goods.Price = double.Parse(temp[index]);
                index++;
                info.SellList.Add(goods);
            }
            info.Time = DateTime.Parse(temp[30] + " " + temp[31]);
            return info;
        }
        #endregion
    }
}
