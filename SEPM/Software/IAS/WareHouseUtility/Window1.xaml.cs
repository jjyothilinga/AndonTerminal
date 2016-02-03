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
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Sql;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ias.andonmanager;

namespace WareHouseUtility
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 


    public enum DeviceCommand
    {
        SET_PLANNED_QUANTITY = 1, SET_SCHEDULE = 2, SET_SHIFT_TIMINGS = 3,
        SET_BREAK_TIMINGS = 4, SET_RTC = 5, SET_QA_PERIOD = 6, SET_LINE_NAME = 7,
        SET_LINE_MODEL = 8, UPDATE_LINE_DATA = 9,
        ISSUE = 10
    };
    public partial class Window1 : Window
    {
        DataAccess dataAccess;
        OpenIssueCollection openIssues;
        System.Timers.Timer appTimer = null;
        XmlSerializer xmlSerializer;


        public Window1()
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
            //appTimer.Interval = 5 * 1000;
            //this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            //               new Action(() =>
            //               {
            //                   if (this.WindowState == WindowState.Maximized)
            //                   {
            //                       openIssues = dataAccess.GetOpenIssues();
            //                       //lstPartRaised.DataContext = null;
            //                       //lstPartRaised.DataContext = openIssues;
            //                       dgOpenIssuesGrid.DataContext = null;
            //                       dgOpenIssuesGrid.DataContext = openIssues;
                                   
            //                       appTimer.Start();
            //                   }
            //               }));
            
        }

        //private void btnRaise_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dataAccess.checkPart(tbPartNo.Text) == true)
        //    {
        //        MessageBox.Show("Issue with this Part No already raised", "Info", MessageBoxButton.OK,
        //            MessageBoxImage.Information);
        //        return;
        //    }
        //    LogEntry lg = new LogEntry(10, 3, tbPartNo.Text);
        //    List<LogEntry> l = new List<LogEntry>();
        //    l.Add(lg);
        //    AndonAlertEventArgs ae = new AndonAlertEventArgs(DateTime.Now,
        //        98, l);
        //    StringWriter writer = new StringWriter();
        //    xmlSerializer.Serialize(writer, ae);
        //    String eventData = writer.ToString();
        //    dataAccess.addCommand(ae.DeviceID, DeviceCommand.ISSUE, eventData);

        //    MessageBox.Show("Message Sent to Server", "Info",
        //        MessageBoxButton.OK, MessageBoxImage.Information);

        //    openIssues = dataAccess.GetOpenIssues();
        //    //lstPartRaised.DataContext = null;
        //    //lstPartRaised.DataContext = openIssues;
        //    dgOpenIssuesGrid.DataContext = null;
        //    dgOpenIssuesGrid.DataContext = openIssues;

        //    tbPartNo.Clear();
        //}

        //private void btnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dgOpenIssuesGrid.SelectedIndex == -1)
        //        return;
        //    OpenIssue openIssue = (OpenIssue)dgOpenIssuesGrid.SelectedItem;
        //    LogEntry lg = new LogEntry(10, 3, openIssue.PartNo);
        //    List<LogEntry> l = new List<LogEntry>();
        //    l.Add(lg);
        //    AndonAlertEventArgs ae = new AndonAlertEventArgs(DateTime.Now,
        //        10, l);
        //    StringWriter writer = new StringWriter();
        //    xmlSerializer.Serialize(writer, ae);
        //    String eventData = writer.ToString();
        //   // dataAccess.addCommand(ae.DeviceID, DeviceCommand.ISSUE, eventData);
        //    dataAccess.addCommand(ae.DeviceID, DeviceCommand.ISSUE, eventData);

        //    MessageBox.Show("Message Sent to Server", "Info",
        //       MessageBoxButton.OK, MessageBoxImage.Information);

        //    openIssues = dataAccess.GetOpenIssues();
        //    //lstPartRaised.DataContext = null;
        //    //lstPartRaised.DataContext = openIssues;
        //    dgOpenIssuesGrid.DataContext = null;
        //    dgOpenIssuesGrid.DataContext = openIssues;

        //    tbPartNo.Clear();

        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (appTimer != null)
            {
                appTimer.Stop();
                appTimer.Elapsed -= appTimer_Elapsed;
            }

        }

        //private void Window_StateChanged(object sender, EventArgs e)
        //{
        //    if (appTimer != null)
        //    {
        //        if (this.WindowState == WindowState.Maximized)
        //        {
        //            openIssues = dataAccess.GetOpenIssues();
        //            dgOpenIssuesGrid.DataContext = null;
        //            dgOpenIssuesGrid.DataContext = openIssues;
        //            //lstPartRaised.DataContext = null;
        //            //lstPartRaised.DataContext = openIssues;
        //            tbPartNo.Focus();
        //            appTimer.Start();

        //        }

        //        else if (this.WindowState == WindowState.Minimized)
        //        {
        //            if (appTimer != null)
        //                appTimer.Stop();
        //        }
        //    }

        //}

        private void dgOpenIssuesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
            
        }
    }
}
