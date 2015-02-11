using System.Windows.Forms;
namespace form
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tlje = new System.Windows.Forms.TextBox();
            this.yjfl = new System.Windows.Forms.TextBox();
            this.yhsl = new System.Windows.Forms.TextBox();
            this.rzlx = new System.Windows.Forms.TextBox();
            this.rzday = new System.Windows.Forms.TextBox();
            this.rqlx = new System.Windows.Forms.TextBox();
            this.rqday = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Buycode = new System.Windows.Forms.TextBox();
            this.Buyprice = new System.Windows.Forms.TextBox();
            this.Buyamount = new System.Windows.Forms.TextBox();
            this.buytime = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Buyname = new System.Windows.Forms.TextBox();
            this.systemtime = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Sellcode = new System.Windows.Forms.TextBox();
            this.Sellname = new System.Windows.Forms.TextBox();
            this.Sellprice = new System.Windows.Forms.TextBox();
            this.Sellamount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.selltime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.Total = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.jktime = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.Setbuy = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.Setsell = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.Buyprofit = new System.Windows.Forms.TextBox();
            this.Sellprofit = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.CalculateBuy = new System.Windows.Forms.Button();
            this.CalculateSell = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.CalculateTotal = new System.Windows.Forms.Label();
            this.listView3 = new System.Windows.Forms.ListViewNF();
            this.listView2 = new System.Windows.Forms.ListViewNF();
            this.listView1 = new System.Windows.Forms.ListViewNF();
            this.SuspendLayout();
            // 
            // tlje
            // 
            this.tlje.Location = new System.Drawing.Point(94, 29);
            this.tlje.Name = "tlje";
            this.tlje.Size = new System.Drawing.Size(100, 21);
            this.tlje.TabIndex = 0;
            // 
            // yjfl
            // 
            this.yjfl.Location = new System.Drawing.Point(94, 56);
            this.yjfl.Name = "yjfl";
            this.yjfl.Size = new System.Drawing.Size(100, 21);
            this.yjfl.TabIndex = 8;
            // 
            // yhsl
            // 
            this.yhsl.Location = new System.Drawing.Point(94, 83);
            this.yhsl.Name = "yhsl";
            this.yhsl.Size = new System.Drawing.Size(100, 21);
            this.yhsl.TabIndex = 9;
            // 
            // rzlx
            // 
            this.rzlx.Location = new System.Drawing.Point(94, 110);
            this.rzlx.Name = "rzlx";
            this.rzlx.Size = new System.Drawing.Size(100, 21);
            this.rzlx.TabIndex = 10;
            // 
            // rzday
            // 
            this.rzday.Location = new System.Drawing.Point(94, 137);
            this.rzday.Name = "rzday";
            this.rzday.Size = new System.Drawing.Size(100, 21);
            this.rzday.TabIndex = 11;
            // 
            // rqlx
            // 
            this.rqlx.Location = new System.Drawing.Point(94, 164);
            this.rqlx.Name = "rqlx";
            this.rqlx.Size = new System.Drawing.Size(100, 21);
            this.rqlx.TabIndex = 12;
            // 
            // rqday
            // 
            this.rqday.Location = new System.Drawing.Point(94, 191);
            this.rqday.Name = "rqday";
            this.rqday.Size = new System.Drawing.Size(100, 21);
            this.rqday.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "套利金额";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "佣金费率";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "印花税率";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "融资利息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "融资天数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "融券利息";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "融券天数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(230, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "买入";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "证券代码";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(231, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "买入价格";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(231, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "买入数量";
            // 
            // Buycode
            // 
            this.Buycode.Location = new System.Drawing.Point(290, 29);
            this.Buycode.Name = "Buycode";
            this.Buycode.Size = new System.Drawing.Size(100, 21);
            this.Buycode.TabIndex = 25;
            this.Buycode.TextChanged += new System.EventHandler(this.Buycode_TextChanged);
            // 
            // Buyprice
            // 
            this.Buyprice.Location = new System.Drawing.Point(290, 83);
            this.Buyprice.Name = "Buyprice";
            this.Buyprice.Size = new System.Drawing.Size(100, 21);
            this.Buyprice.TabIndex = 26;
            // 
            // Buyamount
            // 
            this.Buyamount.Location = new System.Drawing.Point(290, 110);
            this.Buyamount.Name = "Buyamount";
            this.Buyamount.ReadOnly = true;
            this.Buyamount.Size = new System.Drawing.Size(100, 21);
            this.Buyamount.TabIndex = 27;
            // 
            // buytime
            // 
            this.buytime.AutoSize = true;
            this.buytime.Location = new System.Drawing.Point(394, 213);
            this.buytime.Name = "buytime";
            this.buytime.Size = new System.Drawing.Size(53, 12);
            this.buytime.TabIndex = 29;
            this.buytime.Text = "数据时间";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(231, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 30;
            this.label12.Text = "股票名称";
            // 
            // Buyname
            // 
            this.Buyname.Location = new System.Drawing.Point(290, 56);
            this.Buyname.Name = "Buyname";
            this.Buyname.ReadOnly = true;
            this.Buyname.Size = new System.Drawing.Size(100, 21);
            this.Buyname.TabIndex = 31;
            // 
            // systemtime
            // 
            this.systemtime.AutoSize = true;
            this.systemtime.Location = new System.Drawing.Point(859, 465);
            this.systemtime.Name = "systemtime";
            this.systemtime.Size = new System.Drawing.Size(47, 12);
            this.systemtime.TabIndex = 33;
            this.systemtime.Text = "SyeTime";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(607, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 16);
            this.label13.TabIndex = 34;
            this.label13.Text = "卖出";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(608, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 35;
            this.label14.Text = "证券代码";
            // 
            // Sellcode
            // 
            this.Sellcode.Location = new System.Drawing.Point(667, 29);
            this.Sellcode.Name = "Sellcode";
            this.Sellcode.Size = new System.Drawing.Size(100, 21);
            this.Sellcode.TabIndex = 37;
            this.Sellcode.TextChanged += new System.EventHandler(this.Sellcode_TextChanged);
            // 
            // Sellname
            // 
            this.Sellname.Location = new System.Drawing.Point(667, 56);
            this.Sellname.Name = "Sellname";
            this.Sellname.ReadOnly = true;
            this.Sellname.Size = new System.Drawing.Size(100, 21);
            this.Sellname.TabIndex = 38;
            // 
            // Sellprice
            // 
            this.Sellprice.Location = new System.Drawing.Point(667, 83);
            this.Sellprice.Name = "Sellprice";
            this.Sellprice.Size = new System.Drawing.Size(100, 21);
            this.Sellprice.TabIndex = 39;
            // 
            // Sellamount
            // 
            this.Sellamount.Location = new System.Drawing.Point(667, 110);
            this.Sellamount.Name = "Sellamount";
            this.Sellamount.ReadOnly = true;
            this.Sellamount.Size = new System.Drawing.Size(100, 21);
            this.Sellamount.TabIndex = 40;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(608, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 41;
            this.label15.Text = "股票名称";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(608, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 42;
            this.label16.Text = "卖出价格";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(608, 113);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 43;
            this.label17.Text = "卖出数量";
            // 
            // selltime
            // 
            this.selltime.AutoSize = true;
            this.selltime.Location = new System.Drawing.Point(771, 213);
            this.selltime.Name = "selltime";
            this.selltime.Size = new System.Drawing.Size(53, 12);
            this.selltime.TabIndex = 45;
            this.selltime.Text = "数据时间";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "开始监控";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 269);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 47;
            this.button2.Text = "模拟监控";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(839, 360);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 48;
            this.label18.Text = "总盈亏";
            // 
            // Total
            // 
            this.Total.AutoSize = true;
            this.Total.Location = new System.Drawing.Point(886, 360);
            this.Total.Name = "Total";
            this.Total.Size = new System.Drawing.Size(11, 12);
            this.Total.TabIndex = 49;
            this.Total.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(34, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 16);
            this.label19.TabIndex = 50;
            this.label19.Text = "设置";
            // 
            // jktime
            // 
            this.jktime.AutoSize = true;
            this.jktime.Location = new System.Drawing.Point(35, 295);
            this.jktime.Name = "jktime";
            this.jktime.Size = new System.Drawing.Size(29, 12);
            this.jktime.TabIndex = 51;
            this.jktime.Text = "time";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(35, 425);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 12);
            this.label20.TabIndex = 52;
            this.label20.Text = "设定卖出价格";
            // 
            // Setbuy
            // 
            this.Setbuy.Location = new System.Drawing.Point(118, 422);
            this.Setbuy.Name = "Setbuy";
            this.Setbuy.Size = new System.Drawing.Size(100, 21);
            this.Setbuy.TabIndex = 53;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(35, 452);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 12);
            this.label21.TabIndex = 54;
            this.label21.Text = "设定买入价格";
            // 
            // Setsell
            // 
            this.Setsell.Location = new System.Drawing.Point(118, 449);
            this.Setsell.Name = "Setsell";
            this.Setsell.Size = new System.Drawing.Size(100, 21);
            this.Setsell.TabIndex = 55;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(328, 425);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 12);
            this.label22.TabIndex = 56;
            this.label22.Text = "买入盈亏";
            // 
            // Buyprofit
            // 
            this.Buyprofit.Location = new System.Drawing.Point(387, 420);
            this.Buyprofit.Name = "Buyprofit";
            this.Buyprofit.ReadOnly = true;
            this.Buyprofit.Size = new System.Drawing.Size(100, 21);
            this.Buyprofit.TabIndex = 58;
            // 
            // Sellprofit
            // 
            this.Sellprofit.Location = new System.Drawing.Point(387, 447);
            this.Sellprofit.Name = "Sellprofit";
            this.Sellprofit.ReadOnly = true;
            this.Sellprofit.Size = new System.Drawing.Size(100, 21);
            this.Sellprofit.TabIndex = 59;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(328, 452);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 60;
            this.label23.Text = "卖出盈亏";
            // 
            // CalculateBuy
            // 
            this.CalculateBuy.Location = new System.Drawing.Point(224, 420);
            this.CalculateBuy.Name = "CalculateBuy";
            this.CalculateBuy.Size = new System.Drawing.Size(100, 23);
            this.CalculateBuy.TabIndex = 61;
            this.CalculateBuy.Text = "计算买入盈亏";
            this.CalculateBuy.UseVisualStyleBackColor = true;
            this.CalculateBuy.Click += new System.EventHandler(this.CalculateBuy_Click);
            // 
            // CalculateSell
            // 
            this.CalculateSell.Location = new System.Drawing.Point(224, 449);
            this.CalculateSell.Name = "CalculateSell";
            this.CalculateSell.Size = new System.Drawing.Size(98, 23);
            this.CalculateSell.TabIndex = 62;
            this.CalculateSell.Text = "计算卖出盈亏";
            this.CalculateSell.UseVisualStyleBackColor = true;
            this.CalculateSell.Click += new System.EventHandler(this.CalculateSell_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(493, 425);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 63;
            this.label24.Text = "实际盈亏";
            // 
            // CalculateTotal
            // 
            this.CalculateTotal.AutoSize = true;
            this.CalculateTotal.Location = new System.Drawing.Point(552, 425);
            this.CalculateTotal.Name = "CalculateTotal";
            this.CalculateTotal.Size = new System.Drawing.Size(11, 12);
            this.CalculateTotal.TabIndex = 64;
            this.CalculateTotal.Text = "0";
            // 
            // listView3
            // 
            this.listView3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView3.Location = new System.Drawing.Point(118, 240);
            this.listView3.MultiSelect = false;
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(837, 117);
            this.listView3.TabIndex = 44;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // listView2
            // 
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView2.Location = new System.Drawing.Point(773, 29);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Scrollable = false;
            this.listView2.Size = new System.Drawing.Size(182, 183);
            this.listView2.TabIndex = 36;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // listView1
            // 
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(396, 29);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.Size = new System.Drawing.Size(182, 183);
            this.listView1.TabIndex = 32;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 486);
            this.Controls.Add(this.CalculateTotal);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.CalculateSell);
            this.Controls.Add(this.CalculateBuy);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.Sellprofit);
            this.Controls.Add(this.Buyprofit);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.Setsell);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.Setbuy);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.jktime);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.selltime);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Sellamount);
            this.Controls.Add(this.Sellprice);
            this.Controls.Add(this.Sellname);
            this.Controls.Add(this.Sellcode);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.systemtime);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Buyname);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.buytime);
            this.Controls.Add(this.Buyamount);
            this.Controls.Add(this.Buyprice);
            this.Controls.Add(this.Buycode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rqday);
            this.Controls.Add(this.rqlx);
            this.Controls.Add(this.rzday);
            this.Controls.Add(this.rzlx);
            this.Controls.Add(this.yhsl);
            this.Controls.Add(this.yjfl);
            this.Controls.Add(this.tlje);
            this.Name = "Form1";
            this.Text = "融资融券套利盈亏监控系统";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tlje;
        private System.Windows.Forms.TextBox yjfl;
        private System.Windows.Forms.TextBox yhsl;
        private System.Windows.Forms.TextBox rzlx;
        private System.Windows.Forms.TextBox rzday;
        private System.Windows.Forms.TextBox rqlx;
        private System.Windows.Forms.TextBox rqday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Buycode;
        private System.Windows.Forms.TextBox Buyprice;
        private System.Windows.Forms.TextBox Buyamount;
        private System.Windows.Forms.Label buytime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Buyname;
        private ListViewNF listView1;
        private System.Windows.Forms.Label systemtime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private ListViewNF listView2;
        private System.Windows.Forms.TextBox Sellcode;
        private System.Windows.Forms.TextBox Sellname;
        private System.Windows.Forms.TextBox Sellprice;
        private System.Windows.Forms.TextBox Sellamount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private ListViewNF listView3;
        private System.Windows.Forms.Label selltime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label Total;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label jktime;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox Setbuy;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Setsell;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox Buyprofit;
        private System.Windows.Forms.TextBox Sellprofit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button CalculateBuy;
        private System.Windows.Forms.Button CalculateSell;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label CalculateTotal;
    }
}

