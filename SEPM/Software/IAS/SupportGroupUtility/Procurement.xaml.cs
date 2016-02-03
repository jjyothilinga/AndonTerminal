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
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using ias.andonmanager;

namespace SupportGroupUtility
{
    /// <summary>
    /// Interaction logic for Procurement.xaml
    /// </summary>
    /// 
    
    public enum DeviceCommand
    {
        SET_PLANNED_QUANTITY = 1, SET_SCHEDULE = 2, SET_SHIFT_TIMINGS = 3,
        SET_BREAK_TIMINGS = 4, SET_RTC = 5, SET_QA_PERIOD = 6, SET_LINE_NAME = 7,
        SET_LINE_MODEL = 8, UPDATE_LINE_DATA = 9,
        ISSUE = 10
    };
    public partial class Procurement : UserControl
    {
                    DataAccess dataAccess;
            OpenIssueCollection openIssues;
            System.Timers.Timer appTimer = null;
            XmlSerializer xmlSerializer;
        Contact c;
        public Procurement(Contact cc)
        {
            try
            {
                dataAccess = new DataAccess();

                string type = ConfigurationSettings.AppSettings["TYPE"];
                appTimer = new System.Timers.Timer(1 * 1000);
                appTimer.AutoReset = false;
                appTimer.Elapsed += new ElapsedEventHandler(appTimer_Elapsed);

                xmlSerializer = new XmlSerializer(typeof(AndonAlertEventArgs));

                InitializeComponent();

               
                this.c = cc;

                if (c.IsProcurement)
                    btnAcknowledge.Visibility = Visibility.Visible;
   

                appTimer.Start();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Window_Closing(this, new CancelEventArgs());
            }
        }

        void appTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            appTimer.Stop();
            appTimer.Interval = 5 * 1000;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                                    openIssues = dataAccess.GetOpenProcurementIssues();
                                   //lstPartRaised.DataContext = null;
                                   //lstPartRaised.DataContext = openIssues;
                                   dgOpenIssuesGrid.DataContext = null;
                                   dgOpenIssuesGrid.DataContext = openIssues;

                                   appTimer.Start();
                               
                           }));

        }

        

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (appTimer != null)
            {
                appTimer.Stop();
                appTimer.Elapsed -= appTimer_Elapsed;
            }

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (appTimer != null)
            {

                    openIssues = dataAccess.GetOpenProcurementIssues();
                    dgOpenIssuesGrid.DataContext = null;
                    dgOpenIssuesGrid.DataContext = openIssues;
                    //lstPartRaised.DataContext = null;
                    //lstPartRaised.DataContext = openIssues;
                    
                    appTimer.Start();

 


            }

        }

        private void dgOpenIssuesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



        }


        private void btnAcknowledge_Click(object sender, RoutedEventArgs e)
        {
            if (dgOpenIssuesGrid.SelectedIndex == -1)
                return;
            OpenIssue openIssue = (OpenIssue)dgOpenIssuesGrid.SelectedItem;
            LogEntry lg = new LogEntry(10, 3, openIssue.PartNo);
            List<LogEntry> l = new List<LogEntry>();
            l.Add(lg);
            AndonAlertEventArgs ae = new AndonAlertEventArgs(DateTime.Now,
                10, l);
            StringWriter writer = new StringWriter();
            xmlSerializer.Serialize(writer, ae);
            String eventData = writer.ToString();
            dataAccess.addCommand(ae.DeviceID, DeviceCommand.ISSUE, eventData);
            

            MessageBox.Show("Message Sent to Server", "Info",
               MessageBoxButton.OK, MessageBoxImage.Information);

            openIssues = dataAccess.GetOpenProcurementIssues();
            //lstPartRaised.DataContext = null;
            //lstPartRaised.DataContext = openIssues;
            dgOpenIssuesGrid.DataContext = null;
            dgOpenIssuesGrid.DataContext = openIssues;

            
        }
    }


    public class OpenIssue
    {
        #region INotifyPropetyChangedHandler
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        private int slNo;
        public String RecordID
        {
            get { return slNo.ToString(); }
            set
            {
                slNo = Convert.ToInt32(value);
                OnPropertyChanged("RecordID");
            }
        }

        string partNo = String.Empty;
        public string PartNo
        {
            get { return partNo; }
            set
            {
                partNo = value;
                OnPropertyChanged("PartNo");
            }
        }


        string status = String.Empty;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }


        public OpenIssue(int slNo, String partNo, String status)
        {
            this.slNo = slNo;
            this.PartNo = partNo;
            this.Status = status;
        }
    }

    public class OpenIssueCollection : ObservableCollection<OpenIssue>
    {
    }
}
