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
    /// Interaction logic for Monitor.xaml
    /// </summary>
    public partial class Monitor : UserControl,IScreen
    {
        DataAccess dataAccess = null;
        DataTable lineInfoTable = null;
        

        DataTable dt = null;

        ShiftCollection shifts = null;

        String lines = null;
        
        string[] seperator = { "," };
        String lineSelection = String.Empty;
        

        
        int timerElapsedCount = -1;
        

        System.Timers.Timer appTimer = null;
        double timertick = 1000.0;
        



        

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




        LineStatusCollection lineStatus = null;
        String customerLogoPath = String.Empty;




        
        public Monitor()
        {
            try
            {

                timertick = Convert.ToDouble(ConfigurationSettings.AppSettings["TIMERTICK"]);
                dataAccess = new DataAccess();
                dt = dataAccess.GetOpenIssues();

                shifts = dataAccess.getShifts();

                foreach (Shift s in shifts)
                {
                    s.Sessions = dataAccess.getSessions(s.ID);
                }
                lines = ConfigurationSettings.AppSettings["Lines_List_1"];

                lineInfoTable = dataAccess.getProductionLineInfo(lines);

                lineStatus = new LineStatusCollection();




                for (int i = 0; i < lineInfoTable.Rows.Count; i++)
                {
                    lineStatus.Add(new LineStatus((String)lineInfoTable.Rows[i]["description"],
                        (int)lineInfoTable.Rows[i]["id"]));

                }

                


                

                appTimer = new System.Timers.Timer(10);
                appTimer.Elapsed += new System.Timers.ElapsedEventHandler(appTimer_Elapsed);
                appTimer.AutoReset = false;

                InitializeComponent();
                LineStatsGrid.Columns[2].Visibility = Visibility.Visible;

                appTimer.Start();
            }

            

            catch (SqlException s)
            {
                MessageBox.Show("Unable to Connect to DataBase ", "ERROR",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

        }
        public void update()
        {

        }

      

        void appTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            appTimer.Stop();

            appTimer.Interval = 500;
            timerElapsedCount++;
            if (timerElapsedCount >= (50 ))
            {
                timerElapsedCount = 0;
            }

            if (timerElapsedCount == 0)
            {
 
                int targetShift = 0;
                int targetSession = 0;
                DeviceCommand cmd;
                String commandData = String.Empty;

                foreach (LineStatus l in lineStatus)
                {
                    l.ActualQuantity = string.Empty;
                    l.TargetQuantity = String.Empty;
                }
                TimeSpan ts = DateTime.Now.TimeOfDay;

                for (int i = 0; i < shifts.Count; i++)
                {
                    for (int j = 0; j < shifts[i].Sessions.Count; j++)
                    {
                        if (shifts[i].Sessions[j].IsWithin(ts))
                        {
                            if (j == 0)
                            {
                                targetShift = shifts[i].ID - 1;
                                targetSession = shifts[i].Sessions[shifts[i].Sessions.Count - 1].ID;
                            }
                            else
                            {
                                targetShift = shifts[i].ID;
                                targetSession = shifts[i].Sessions.getTargetSession(ts).ID - 1;
                            }
                            break;
                        }

                    }
                    if (targetShift != 0)
                        break;
                }

                foreach (LineStatus l in lineStatus)
                {
                    l.TargetQuantity = dataAccess.getTargetQuantity(targetShift, targetSession, l.id);
                    l.ActualQuantity = dataAccess.getActualQuantity(l.id);
                    l.ActualQuantity = dataAccess.getProducedQuantity(l.id);

                }




                List<int> stationList = dataAccess.getBreakDownStatus(lines);

                foreach (LineStatus ls in lineStatus)
                {
                    if (stationList.Contains(ls.id))
                    {
                        ls.Breakdown = 1;
                    }
                    else
                    {
                        ls.Breakdown = 0;
                    }
                }


                stationList = dataAccess.getQualityStatus(lines);

                foreach (LineStatus ls in lineStatus)
                {
                    if (stationList.Contains(ls.id))
                    {
                        ls.Quality = 1;
                    }
                    else
                    {
                        ls.Quality = 0;
                    }
                }


                stationList = dataAccess.getMaterialShortageStatus(lines);

                foreach (LineStatus ls in lineStatus)
                {
                    if (stationList.Contains(ls.id))
                    {
                        ls.MaterialShortage = 1;
                    }
                    else
                    {
                        ls.MaterialShortage = 0;
                    }
                }

            }

            foreach (LineStatus ls in lineStatus)
            {
                ls.updateStatus();
            }
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                 new Action(() =>
                 {
                     LineStatsGrid.DataContext = null;
                     LineStatsGrid.DataContext = lineStatus;

                 }));

            appTimer.Start();
        }

 
    }
}
