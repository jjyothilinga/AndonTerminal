using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Timers;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;
using ias.shared;


namespace ias.client
{
    /// <summary>
    /// Interaction logic for WaterFallChart.xaml
    /// </summary>
    public partial class WaterFallChart : UserControl, IScreen
    {
        public enum DeviceCommand
        {
            SET_PLANNED_QUANTITY = 1, SET_SCHEDULE = 2, SET_SHIFT_TIMINGS = 3,
            SET_BREAK_TIMINGS = 4, SET_RTC = 5, SET_QA_PERIOD = 6, SET_LINE_NAME = 7,
            SET_LINE_MODEL = 8, UPDATE_LINE_DATA = 9
        };


        public enum TransactionStatus
        {
            NONE = 0, OPEN = 1, INPROCESS = 2,
            COMPLETE = 3, TIMEOUT = 4
        };

        //LineStatusCollection lineStatus = null;
        String customerLogoPath = String.Empty;

        System.Timers.Timer appTimer = null;
        double timertick = 1000.0;

        public WaterFallChart()
        {
            //timertick = Convert.ToDouble(ConfigurationSettings.AppSettings["TIMERTICK"]);
            

            //appTimer = new System.Timers.Timer(10);
            //appTimer.Elapsed += new System.Timers.ElapsedEventHandler(butt_Click);
            //appTimer.AutoReset = false;

            InitializeComponent();
            
   //button1_Click(
            //appTimer.Start();
            
        }

       

        public void update()
        {
            graphgen("01-01-2014", DateTime.Now.Date.ToString("MM-dd-yyyy"));
        }

        void graphgen(string dtfrm,string dtto)
        {
            int xPos;
            int yPos;
            var r = new Random();
            xPos = r.Next(300);
            yPos = r.Next(200);
            SqlConnection conn = new SqlConnection(GClass1.MyGlobals.strcon);
            //string query = "select 'Total',CONVERT(VARCHAR(11),[Date of Production],106)[Date of Production],Cast([Total UT Hrs]as numeric(10,1))[UT Hours],Cast([Total DT Hrs]as numeric(10,1))[DT Hours],[Quantity Produced],Cast([OptTime]as numeric(10,1))[Operator Time],Cast([SupTime]as numeric(10,1))[Support Function Time],Cast([OptTime]+[SupTime]as numeric(10,1))[Total TS],Cast(([Total UT Hrs]/([OptTime]+[SupTime]))*100 as numeric(10,1))[IE] from--CONVERT(VARCHAR(11),KS_Date,106)[Support Function Date],(Select sum([Total OT Hrs])[OptTime],[Date of Production],sum([Total DT Hrs])[Total DT Hrs],sum([Total UT Hrs]) [Total UT Hrs],Sum([Quantity Produced])[Quantity Produced] from(	select MeterProduct,[Date of Production],sum([OT Hours])[Total OT Hrs],Sum([Quantity Produced])[Quantity Produced],Sum([DT Hours])[Total DT Hrs],Sum([UT Hours])[Total UT Hrs],sum([DT Hours])/sum([OT Hours])*100 [KE] from( select [Model],[OT Hours],([Total DT in TMU]/100000)*[Quantity Produced][DT Hours],([Total UT in TMU]/100000)*[Quantity Produced][UT Hours],[Date of Production],[Quantity Produced] from	(select t1.[Product] ,t1.[Total in TMU] [Total DT in TMU],t2.[Total in TMU] [Total UT in TMU] From (select IE_Product [Product],SUM(IE_Time)[Total in TMU],IE_Level [Level] from Tbl_Mast_MeterModel,Tbl_IE_Trans_Main where meterID=IE_Product  and  IE_Level='DT' group by IE_Product,IE_Level)t1	left Join(select IE_Product [Product],SUM(IE_Time)[Total in TMU],IE_Level [Level] from Tbl_Mast_MeterModel,Tbl_IE_Trans_Main where meterID=IE_Product  and IE_Level='UT' group by IE_Product,IE_Level	)t2	on t1.[Product]=t2.[Product])t3	Right Join	(SELECT KE_Product [Model],Sum(Shift_Hours*KE_People)[OT Hours],Sum(	Case when Tbl_Mast_MeterModel.MeterIEYN='Y' then Tbl_IE_KE.KE_Qty else 0 END)[Quantity Produced],KE_Date [Date of Production] FROM Tbl_IE_KE ,Tbl_Mast_MeterModel,Tbl_IE_Master_Shift where KE_Product=meterID and KE_ShiftID=Shift_ID  group by KE_Product,KE_Date)t4	on t4.[Model]=t3.[Product])tbl1	left Join(Select * from Tbl_Mast_MeterModel)tbl2 on tbl1.[Model]=tbl2.meterID   group by [Date of Production],MeterProduct)tblsumOT Group by [Date of Production])TableOT	left join			(Select sum(Shift_Hours*KS_People)[SupTime],KS_Date from Tbl_IE_Trans_KS,Tbl_IE_Master_Shift where KS_Shift=Shift_ID group by KS_Date )TableKS			on TableKS.KS_Date=TableOT.[Date of Production] where [Date of Production]='23-mar-14'";
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            // Instantiate a Sql command object
            SqlCommand command = new SqlCommand();

            //assign the command to be executed
            //command.CommandText = "select 'Total',CONVERT(VARCHAR(11),[Date of Production],106)[Date of Production],Cast([Total UT Hrs]as numeric(10,1))[UT Hours],Cast([Total DT Hrs]as numeric(10,1))[DT Hours],[Quantity Produced],Cast([OptTime]as numeric(10,1))[Operator Time],Cast([SupTime]as numeric(10,1))[Support Function Time],Cast([OptTime]+[SupTime]as numeric(10,1))[Total TS],Cast(([Total UT Hrs]/([OptTime]+[SupTime]))*100 as numeric(10,1))[IE] from(Select sum([Total OT Hrs])[OptTime],[Date of Production],sum([Total DT Hrs])[Total DT Hrs],sum([Total UT Hrs]) [Total UT Hrs],Sum([Quantity Produced])[Quantity Produced] from(	select MeterProduct,[Date of Production],sum([OT Hours])[Total OT Hrs],Sum([Quantity Produced])[Quantity Produced],Sum([DT Hours])[Total DT Hrs],Sum([UT Hours])[Total UT Hrs],sum([DT Hours])/sum([OT Hours])*100 [KE] from( select [Model],[OT Hours],([Total DT in TMU]/100000)*[Quantity Produced][DT Hours],([Total UT in TMU]/100000)*[Quantity Produced][UT Hours],[Date of Production],[Quantity Produced] from	(select t1.[Product] ,t1.[Total in TMU] [Total DT in TMU],t2.[Total in TMU] [Total UT in TMU] From (select IE_Product [Product],SUM(IE_Time)[Total in TMU],IE_Level [Level] from Tbl_Mast_MeterModel,Tbl_IE_Trans_Main where meterID=IE_Product  and  IE_Level='DT' group by IE_Product,IE_Level)t1	left Join(select IE_Product [Product],SUM(IE_Time)[Total in TMU],IE_Level [Level] from Tbl_Mast_MeterModel,Tbl_IE_Trans_Main where meterID=IE_Product  and IE_Level='UT' group by IE_Product,IE_Level	)t2	on t1.[Product]=t2.[Product])t3	Right Join	(SELECT KE_Product [Model],Sum(Shift_Hours*KE_People)[OT Hours],Sum(	Case when Tbl_Mast_MeterModel.MeterIEYN='Y' then Tbl_IE_KE.KE_Qty else 0 END)[Quantity Produced],KE_Date [Date of Production] FROM Tbl_IE_KE ,Tbl_Mast_MeterModel,Tbl_IE_Master_Shift where KE_Product=meterID and KE_ShiftID=Shift_ID  group by KE_Product,KE_Date)t4	on t4.[Model]=t3.[Product])tbl1	left Join(Select * from Tbl_Mast_MeterModel)tbl2 on tbl1.[Model]=tbl2.meterID   group by [Date of Production],MeterProduct)tblsumOT Group by [Date of Production])TableOT	left join			(Select sum(Shift_Hours*KS_People)[SupTime],KS_Date from Tbl_IE_Trans_KS,Tbl_IE_Master_Shift where KS_Shift=Shift_ID group by KS_Date )TableKS			on TableKS.KS_Date=TableOT.[Date of Production] where [Date of Production]='"+ dtfrm +"'";
            string sqldtl="select 'Total',Cast([Total UT Hrs]as numeric(10,1))[UT Hours],Cast([Total DT Hrs]as numeric(10,1))[DT Hours],[Quantity Produced],Cast([OptTime]as numeric(10,1))[Operator Time],Cast([SupTime]as numeric(10,1))[Support Function Time],Cast([OptTime]+[SupTime]as numeric(10,1))[Total TS],Cast(([Total UT Hrs]/([OptTime]+[SupTime]))*100 as numeric(10,1))[IE] from(Select sum([Total OT Hrs])[OptTime],sum([Total DT Hrs])[Total DT Hrs],sum([Total UT Hrs]) [Total UT Hrs],Sum([Quantity Produced])[Quantity Produced] from(select sum([OT Hours])[Total OT Hrs],Sum([Quantity Produced])[Quantity Produced],Sum([DT Hours])[Total DT Hrs],Sum([UT Hours])[Total UT Hrs],sum([DT Hours])/sum([OT Hours])*100 [KE] from(select [Model],[OT Hours],([Total DT in TMU]/100000)*[Quantity Produced][DT Hours],([Total UT in TMU]/100000)*[Quantity Produced][UT Hours],[Quantity Produced] from	(select t1.[Product] ,t1.[Total in TMU] [Total DT in TMU],t2.[Total in TMU] [Total UT in TMU] From 	(select IE_Product [Product],SUM(IE_Time)[Total in TMU],IE_Level [Level] from Tbl_Mast_MeterModel,Tbl_IE_Trans_Main where meterID=IE_Product  and  IE_Level='DT' group by IE_Product,IE_Level )t1 left Join	(select IE_Product [Product],SUM(IE_Time)[Total in TMU],IE_Level [Level] from Tbl_Mast_MeterModel,Tbl_IE_Trans_Main where meterID=IE_Product  and IE_Level='UT' group by IE_Product,IE_Level)t2	on t1.[Product]=t2.[Product])t3	Right Join	(SELECT KE_Product [Model],Sum(Shift_Hours*KE_People)[OT Hours],Sum(Case when Tbl_Mast_MeterModel.MeterIEYN='Y' then Tbl_IE_KE.KE_Qty else 0 END)[Quantity Produced],KE_Date [Date of Production] FROM Tbl_IE_KE ,Tbl_Mast_MeterModel,Tbl_IE_Master_Shift where KE_Product=meterID and KE_ShiftID=Shift_ID and KE_Date>'"+ dtfrm +"' and KE_Date<'"+ dtto +"' group by KE_Product,KE_Date)t4 on t4.[Model]=t3.[Product])tbl1 left Join	(Select * from Tbl_Mast_MeterModel)tbl2 on tbl1.[Model]=tbl2.meterID )tblsumOT )TableOT cross join (Select sum(Shift_Hours*KS_People)[SupTime] from Tbl_IE_Trans_KS,Tbl_IE_Master_Shift where KS_Shift=Shift_ID and KS_Date>'"+ dtfrm +"' and KS_Date<'"+ dtto +"')TableKS ";

            command.CommandText = sqldtl;
            //bind the command object to the connection
            command.Connection = conn;
            SqlDataReader dr = command.ExecuteReader();
            string total = "";
            //string dateprod = "";
            Decimal uthr = 0;
            Decimal dthr = 0;
            //int qty=10;
            Decimal ot = 0;
            Decimal supt = 0;
            Decimal TOTts = 0;
            Decimal IE = 0;
            if (dr.HasRows)
            {
                dr.Read();
                if (!dr.IsDBNull(6))
                {
                    TOTts = dr.GetDecimal(6);
                }
                else
                {
                    TOTts = 0;
                }
                //adding item to the combobox. you can also use dr[1] to get the country name
                
                if (!dr.IsDBNull(0))
                {
                    total = (dr.GetString(0));
                }
                else
                {
                    total = "0";
                }
                //if (!dr.IsDBNull(1))
                //{
                //    dateprod = dr.GetString(1);
                //}
                //else
                //{
                //    dateprod = "0";
                //}
                if (!dr.IsDBNull(1))
                {
                    uthr = dr.GetDecimal(1);
                }
                else
                {
                     uthr = 0;
                }
                if (!dr.IsDBNull(2))
                {
                    dthr = dr.GetDecimal(2);
                }
                else
                {
                     dthr = 0;
                }
                if (!dr.IsDBNull(4))
                {
                    ot = dr.GetDecimal(4);
                }
                else
                {
                    ot = 0;
                }
                //qty = dr.GetInt32(3);
                if (!dr.IsDBNull(5))
                {
                    supt = dr.GetDecimal(5);
                }
                else
                {
                    supt = 0;
                }
                
                if (!dr.IsDBNull(7))
                {
                     IE = dr.GetDecimal(7);
                }
                else
                {
                     IE = 0;
                }
                

            }
            dr.Close();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            // Instantiate a Sql command object
            SqlCommand command1 = new SqlCommand();

            //assign the command to be executed
            command1.CommandText = "select dwntn_Category,(Sum([DT_DTHr]*[KE_People]) +((Cast(Sum([DT_DTMin]*[KE_People])as Decimal(10,2))/60))) [DT Hrs] from Tbl_IE_Trans_DownTime,Tbl_IE_Mast_DownTime,Tbl_IE_KE where DT_DTID=DWNTM_ID and DT_KEID=KE_ID  and KE_Date > '" + dtfrm + "' and KE_Date < '"+ dtto +"' group by dwntn_Category ";

            //bind the command object to the connection
            command1.Connection = conn;
            SqlDataReader dr1 = command1.ExecuteReader();
            Decimal BrkDwn = 0;
            Decimal MtStg = 0;
            Decimal Othrs = 0;
            Decimal Qty = 0;
            Decimal OELoss = 0;
            if (dr1.HasRows)
            {
//                while(reader.Read())
//{   
//    int from = int.Parse(reader['AmountFrom'].toString());
//    int to = int.Parse(reader['AmountTo'].toString());
//    int discount = int.Parse(reader['DiscPer'].toString());

//    if(totalPurchase >= from && totalPurchase < to) {
//        MessageBox.Show("You got a discount of " + discount + "%");
//    }
//}
                while(dr1.Read())
                {
                    if (!dr1.IsDBNull(1))
                    {
                        if (dr1.GetString(0) == "Break Down")
                        {
                            BrkDwn  = dr1.GetDecimal(1);
                        }
                        else if (dr1.GetString(0) == "Material Shortage")
                        {
                            MtStg= dr1.GetDecimal(1);
                        }
                        else if (dr1.GetString(0) == "Others")
                        {
                            Othrs = dr1.GetDecimal(1);
                        }
                        else if (dr1.GetString(0) == "Quality")
                        {
                            Qty = dr1.GetDecimal(1);
                        }
                    }
                    else
                    {
                        BrkDwn = 0;
                        MtStg = 0;
                        Othrs = 0;
                        Qty = 0;
                    }
                }
            }
            OELoss = ot - BrkDwn - MtStg - Othrs - Qty-dthr;
            dr1.Close();
            canvas1.Children.Clear();
            double canwid = canvas1.Width;
            double canwidth = canvas1.ActualWidth;
            double canheight = canvas1.ActualHeight;
            //DateTime parsedDate;
            //if (Othrs == dthr + BrkDwn + MtStg + Othrs + Qty)
            //{

            //}
            //else
            //{
            //    MessageBox.Show("Downtime entered is not matching to OT hours and DT Hours");
            //}
            if (TOTts != 0)
            {
                canvas1.Children.Add(new Button { Name = "Btn1",FontSize=13, Background = new LinearGradientBrush( Colors.SandyBrown,Colors.White, new Point(0.5, 0.1), new Point(0.5, 0)), Width = canwidth/15, Height = (double)(((canheight) / (double)TOTts) * (double)uthr), Content = "UT Hr " + (double)uthr, Margin = new Thickness(0, canheight - (double)((canheight /(double) TOTts) * (double) uthr), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn2", FontSize = 13, Background = new LinearGradientBrush(Colors.LimeGreen, Colors.GreenYellow, new Point(0.5, 0.1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * dthr), Content = "DT Hr " + (double)dthr, Margin = new Thickness((canwidth / 15) * 1, (canheight) - (double)(((decimal)(canheight) / TOTts) * dthr), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn9", FontSize = 13, Background = new LinearGradientBrush(Colors.Red, Colors.White, new Point(0.5, 1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * BrkDwn), Content = "Breakdown " + (double)BrkDwn, Margin = new Thickness((canwidth / 15) * 2, (canheight) - (double)(((decimal)(canheight) / TOTts) * ((dthr) + (BrkDwn))), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn10", FontSize = 13, Background = new LinearGradientBrush(Colors.Red, Colors.White, new Point(0.5, 1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * MtStg), Content = "Material Shortage " + (double)MtStg, Margin = new Thickness((canwidth / 15) * 3, (canheight) - (double)(((decimal)(canheight) / TOTts) * ((dthr) + (BrkDwn) + (MtStg))), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn11", FontSize = 13, Background = new LinearGradientBrush(Colors.Red, Colors.White, new Point(0.5, 1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * Othrs), Content = "Others " + (double)Othrs, Margin = new Thickness((canwidth / 15) * 4, (canheight) - (double)(((decimal)(canheight) / TOTts) * ((dthr) + (BrkDwn) + (MtStg) + (Othrs))), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn12", FontSize = 13, Background = new LinearGradientBrush(Colors.Red, Colors.White, new Point(0.5, 1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * Qty), Content = "Quality " + (double)Qty, Margin = new Thickness((canwidth / 15) * 5, (canheight) - (double)(((decimal)(canheight) / TOTts) * ((dthr) + (BrkDwn) + (MtStg) + (Othrs) + (Qty))), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn17", FontSize = 13, Background = new LinearGradientBrush(Colors.Red, Colors.White, new Point(0.5, 1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * OELoss), Content = "OE Loss " + (double)(OELoss), Margin = new Thickness((canwidth / 15) * 6, (canheight) - (double)(((decimal)(canheight) / TOTts) * ((dthr) + (BrkDwn) + (MtStg) + (Othrs) + (Qty) + (OELoss))), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn3", FontSize = 13, Background = new LinearGradientBrush(Colors.Gold, Colors.White, new Point(0.5, 0.1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * ot), Content = "OT " + (double)ot, Margin = new Thickness((canwidth / 15) * 7, (canheight) - (double)(((decimal)(canheight) / TOTts) * ot), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn4", FontSize = 13, Background = new LinearGradientBrush(Colors.HotPink, Colors.White, new Point(0.5, 0.1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * supt), Content = "Supt " + (double)supt, Margin = new Thickness((canwidth / 15) * 8, (canheight) - (double)((((decimal)(canheight) / TOTts) * ((ot) + (supt)))), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn5", FontSize = 13, Background = new LinearGradientBrush(Colors.DodgerBlue, Colors.White, new Point(0.5, 0.1), new Point(0.5, 0)), Width = canwidth / 15, Height = (double)(((decimal)(canheight) / TOTts) * TOTts), Content = "TS " + (double)TOTts, Margin = new Thickness((canwidth / 15) * 9, (canheight) - (double)(((decimal)(canheight) / TOTts) * TOTts), 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn6", Background = new LinearGradientBrush(Colors.YellowGreen, Colors.Yellow, new Point(0.5, 1), new Point(0.5, 0)), Width = 55, Height = 30, Content = "IE " + (double)(Math.Round(IE, 1)), Margin = new Thickness((canwidth / 15) * 11, 70, 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn7", Background = new LinearGradientBrush(Colors.YellowGreen, Colors.Yellow, new Point(0.5, 1), new Point(0.5, 0)), Width = 55, Height = 30, Content = "KD " + (double)(Math.Round((uthr / dthr) * 100, 1)), Margin = new Thickness((canwidth / 15) * 11.6, 70, 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn8", Background = new LinearGradientBrush(Colors.YellowGreen, Colors.Yellow, new Point(0.5, 1), new Point(0.5, 0)), Width = 55, Height = 30, Content = "KE " + (double)(Math.Round(((uthr / ot) * 100), 1)), Margin = new Thickness((canwidth / 15) * 12.2, 70, 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn13", Background = new LinearGradientBrush(Colors.DarkOrange, Colors.WhiteSmoke, new Point(0.5, 0.5), new Point(0.5, 0)), Width = 200, Height = 30, Content = "Breakdown " + (double)(Math.Round(BrkDwn, 1)), Margin = new Thickness((canwidth / 15) * 11, 105, 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn14", Background = new LinearGradientBrush(Colors.DarkOrange, Colors.WhiteSmoke, new Point(0.5, 0.5), new Point(0.5, 0)), Width = 200, Height = 30, Content = "Material Shortage " + (double)(Math.Round(MtStg, 1)), Margin = new Thickness((canwidth / 15) * 11, 140, 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn15", Background = new LinearGradientBrush(Colors.DarkOrange, Colors.WhiteSmoke, new Point(0.5, 0.5), new Point(0.5, 0)), Width = 200, Height = 30, Content = "Others " + (double)(Math.Round(Othrs, 1)), Margin = new Thickness((canwidth / 15) * 11, 175, 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn16", Background = new LinearGradientBrush(Colors.DarkOrange, Colors.WhiteSmoke, new Point(0.5, 0.5), new Point(0.5, 0)), Width = 200, Height = 30, Content = "Quality " + (double)(Math.Round(Qty, 1)), Margin = new Thickness((canwidth / 15) * 11, 210, 0, 0) });
                canvas1.Children.Add(new Button { Name = "Btn18", Background = new LinearGradientBrush(Colors.DarkOrange, Colors.WhiteSmoke, new Point(0.5, 0.5), new Point(0.5, 0)), Width = 200, Height = 30, Content = "Operator Efficiency Loss " + (double)(Math.Round(OELoss, 1)), Margin = new Thickness((canwidth / 15) * 11, 245, 0, 0) });
                canvas1.Background = new LinearGradientBrush(Colors.Transparent, Colors.Transparent, new Point(0.5, 1), new Point(0.5, 0));
                //DateTime dtfm = Convert.ToDateTime(dtfrm);
                //DateTime dtt = Convert.ToDateTime(dtto);
                //dtfrm=dtfm.ToString("dd-MMM-yyyy");
                //dtto = dtt.ToString("dd-MMM-yyyy");
                //canvas1.Children.Add(new Label { Name = "Lbl1", Background = new LinearGradientBrush(Colors.Transparent, Colors.Transparent, new Point(0.5, 1), new Point(0.5, 0)),  Width = 200, Height = 30, Content = "From: " + dtfrm, Margin = new Thickness((canwidth / 15) * 11, 280, 0, 0) });
                //canvas1.Children.Add(new Label { Name = "Lbl2", Background = new LinearGradientBrush(Colors.Transparent, Colors.Transparent, new Point(0.5, 1), new Point(0.5, 0)),  Width = 200, Height = 30, Content = "To:    " + dtto, Margin = new Thickness((canwidth / 15) * 11, 315, 0, 0) });
                //canvas1.Children.Add(new Label { Name = "Lbl2", Background = new LinearGradientBrush(Colors.Transparent, Colors.Transparent, new Point(0.5, 1), new Point(0.5, 0)),  Width = 200, Height = 30, Content = "Product:   " + CboProduct.Text, Margin = new Thickness((canwidth / 15) * 11, 350, 0, 0) });
            }
            else
            {
                //string[] dateValues = { "30-12-2011", "12-30-2011", 
                //              "30-12-11", "12-30-11" };
                //string pattern = "DD-MMM-yyyy";
                ////DateTime parsedDate;

                //foreach (var dateValue in dateValues)
                //{
                //    if (DateTime.TryParseExact(dt, pattern, null,
                //                              DateTimeStyles.None, out parsedDate))
                //        MessageBox.Show("Converted '{0}' to {1:d}.",dateValue+ " "+ parsedDate);
                //    else
                //        MessageBox.Show("Unable to convert '{0}' to a date and time.", dt);
                //}
                canvas1.Children.Add(new Label { Name = "Lbl1", Background = new LinearGradientBrush(Colors.YellowGreen, Colors.Green, new Point(0.5, 1), new Point(0.5, 0)), FontSize = 20, Width = 400, Height = 30, Content = "Details for " + dtfrm +" to " + dtto +" not Present" });
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(canvas1); i++)
            {
                var control = VisualTreeHelper.GetChild(canvas1, i);

                if (control is TextBox)
                {
                    (control as TextBox).Text = String.Empty;
                }
                else if (control is ComboBox)
                {
                    (control as ComboBox).Text = String.Empty;
                }
                else if (control is Button)
                {
                    //MessageBox.Show("Hi"+ control.ToString());
                    //ClearControls(control);
                }
            }
            //button1_Click(
        }

        private void butt_Click(object sender, System.Timers.ElapsedEventArgs e)
        {
            //graphgen(DTPIEGraphFrom.Text,DTPIEGraphTo.Text);
            graphgen("01-01-2014", DateTime.Now.Date.ToShortDateString());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            graphgen("01-01-2014", DateTime.Now.Date.ToString("MM-dd-yyyy"));
            //string xml = "<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'";
            //xml += " xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'";
            //xml += " Height='26' Margin='45,0,0,30'";
            //xml += " Name='button3'";
            //xml += " VerticalAlignment='Bottom'";
            //xml += " HorizontalAlignment='Left' Width='65'>Test</Button>";


            //Stream strm = new MemoryStream(ASCIIEncoding.Default.GetBytes(xml));
            //Button myButton = (Button)System.Windows.Markup.XamlReader.Load(strm);
            //MyGrid.Children.Add(myButton);

            //Type myType = typeof(WpfApplication10.MainWindow);
            //MethodInfo myMethod = myType.GetMethod("button3_Click", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            //myButton.AddHandler(Button.ClickEvent, Delegate.CreateDelegate(Button.ClickEvent.HandlerType, myMethod, false));
        }
        
    }
}
