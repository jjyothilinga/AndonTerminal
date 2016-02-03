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
using ias.andonmanager;
using System.Threading;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace IAS
{
    /// <summary>
    /// Interaction logic for startPage.xaml
    /// </summary>
    /// 

    public enum DeviceCommand
    {
        SET_PLANNED_QUANTITY = 1, SET_SCHEDULE = 2, SET_SHIFT_TIMINGS = 3,
        SET_BREAK_TIMINGS = 4, SET_RTC = 5, SET_QA_PERIOD = 6, SET_LINE_NAME = 7,
        SET_LINE_MODEL = 8, UPDATE_LINE_DATA = 9,
        ISSUE = 10,RESOLVE = 11
    };
    public partial class StartPage : Window
    {
        AndonManager andonManager = null;
        String _dbConnectionString = String.Empty;
        DataAccess dataAccess = null;
        Queue<int> lineQ = null;
        Queue<int> departmentQ = null;
        DataTable departmentTable = null;
        lineCollection lines = null;
        ShiftCollection shifts = null;
        ContactCollection contacts = null;

        Dictionary<int, Issue> Issues = null;
        List<double> timeout = null;

        System.Timers.Timer commandTimer = null;
        String dataSeperator = ";";
        XmlSerializer xmlSerializer;

        int whID = -1;


        String serverType = String.Empty;
        public StartPage()
        {
            try
            {
                InitializeComponent();
                //_dbConnectionString = "Data Source=SAMBRAHMA\\SQLEXPRESS;database=IAS_Schneider;User id=sa; Password=mei123$%^;";
                _dbConnectionString  = ConfigurationSettings.AppSettings["DBConStr"];

                string lineList = ConfigurationSettings.AppSettings["LINES"];

                whID = Convert.ToInt32(ConfigurationSettings.AppSettings["WH_ID"]);

                DataAccess.conStr = _dbConnectionString;

                MainMenu _mainMenu = new MainMenu(_dbConnectionString);
                _mainFrame.Navigate(_mainMenu);
                dataAccess = new DataAccess();
                lines = dataAccess.getLines();
                lineQ = new Queue<int>();
                String[] linesLst = separateCommandData(lineList);

                foreach (line l in lines)
                {
                    foreach (String s in linesLst)
                    {

                        if (l.ID == Convert.ToInt32(s))
                        {
                            lineQ.Enqueue(l.ID);
                        }
                    }
                }

                departmentTable = dataAccess.getDepartmentInfo("");

                departmentQ = new Queue<int>();
                for (int i = 0; i < departmentTable.Rows.Count; i++)
                    departmentQ.Enqueue((int)departmentTable.Rows[i]["id"]);
                andonManager = new AndonManager(lineQ,departmentQ,AndonManager.MODE.MASTER);

                shifts = dataAccess.getShifts();

                contacts = dataAccess.getContacts();


                xmlSerializer = new XmlSerializer(typeof(AndonAlertEventArgs));

                serverType = ConfigurationSettings.AppSettings["ServerType"];
                dataAccess.updateIssueMarquee();


                commandTimer = new System.Timers.Timer(3 * 1000);
                commandTimer.Elapsed += new System.Timers.ElapsedEventHandler(commandTimer_Elapsed);
                commandTimer.AutoReset = false;

                commandTimer.Start();

                Issues = new Dictionary<int, Issue>();
                timeout = dataAccess.getEscalationTimeout();
                               
                
                andonManager.andonAlertEvent += new EventHandler<AndonAlertEventArgs>(andonManager_andonAlertEvent);

                andonManager.start();
               
            }
            catch( Exception e)
            {
                tbMsg.Text+= e.Message;
            }
        }

        void commandTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            commandTimer.Stop();
            DataTable dt;

            DeviceCommand cmd;
            String commandData = String.Empty;
            dt = dataAccess.getCommands();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                updateMsg("Processing Command");
                int lineId = (int)dt.Rows[i]["line_id"];
                cmd = (DeviceCommand)dt.Rows[i]["command"];
                commandData = (String)dt.Rows[i]["command_data"];
                processCommand(lineId, cmd, commandData);

                dataAccess.updateCommandStatus((int)dt.Rows[i]["id"],
                 DataAccess.TransactionStatus.COMPLETE);
            }

            commandTimer.Start();
            
        }


        void andonManager_andonAlertEvent(object sender, AndonAlertEventArgs e)
        {

            if (serverType == "SUB")
            {

                StringWriter writer = new StringWriter();
                xmlSerializer.Serialize(writer, e);
                String eventData = writer.ToString();
                dataAccess.addCommand(e.DeviceID,DeviceCommand.ISSUE, eventData);
                return;
            }


            int recordId = -1;

            try
            {
                

                foreach (LogEntry lg in e.DeviceLog)
                {
                    String logMsg = String.Empty;
                    if (lg.Station == 99)
                    {
                        string status = dataAccess.getDeviceStatus(e.DeviceID);
                        if (status == "ON")
                        {
                            dataAccess.updateDeviceStatus(e.DeviceID, "OFF",lg.Data);

                            tbMsg.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                               new Action(() =>
                               {
                                   tbMsg.Text += "Logoff : Line - " + lines.getLineName(e.DeviceID) + ";"
                                    + "By - " + lg.Data + "----at" + DateTime.Now.ToString()+ Environment.NewLine;
                               }));
                        }
                        else if( status == "OFF")
                        {
                            dataAccess.updateDeviceStatus(e.DeviceID, "ON",lg.Data);

                            tbMsg.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               tbMsg.Text += "Logon : Line - " + lines.getLineName(e.DeviceID) + ";"
                                + "By - " + lg.Data + "----at" + DateTime.Now.ToString()+ Environment.NewLine;
                           }));
                        }
                        continue;
                    }
                    if (lg.Department == 0)
                    {
                         dataAccess.updateActualQuantity(e.DeviceID, Convert.ToInt32( lg.Data));

                        tbMsg.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() =>
                        {
                            tbMsg.Text +=lines.getLineName(e.DeviceID) + ":"+
                                "Production Quantity - " + lg.Data.ToString()+ "--AT "
                                + DateTime.Now.ToString()+Environment.NewLine;
                        }));

                        continue;
                    }
                    

                    logMsg += lines.getLineName(e.DeviceID) + ":";


                    if (e.DeviceID == whID)
                    {
                        logMsg += departmentTable.Rows[lg.Department - 1]["description"] + "-";
                        logMsg += "PART #" + lg.Data;
                        recordId = dataAccess.findIssueRecord(e.DeviceID, lg.Station, lg.Department, lg.Data);
                    }

                    else
                    {
                        String stationName = lines.getStationName(e.DeviceID, lg.Station);
                        if (stationName == string.Empty)
                            stationName = "Station #" + lg.Station.ToString();

                        logMsg += stationName + ":";
                        logMsg += departmentTable.Rows[lg.Department - 1]["description"] + "-";

                        if (lg.Department == 3)
                        {
                            logMsg += "PART #" + lg.Data;
                            recordId = dataAccess.findIssueRecord(e.DeviceID, lg.Station, lg.Department, lg.Data);
                        }
                        else
                        {
                            String className = lines.getClassName(e.DeviceID, lg.Station, lg.Department, Convert.ToInt32(lg.Data));

                            if (className == String.Empty)
                                lg.Data = "Class Code #" + Convert.ToInt32(lg.Data);
                            else lg.Data = className;
                            recordId = dataAccess.findIssueRecord(e.DeviceID, lg.Station, lg.Department, lg.Data);

                            logMsg += "--" + lg.Data;

                        }


                    }
                  
                    
                    


                    if (recordId == -1)     //new issue
                    {

                        recordId =
                            dataAccess.insertIssueRecord(e.DeviceID, lg.Station, lg.Department, lg.Data, logMsg);

                        Issue issue = new Issue(e.DeviceID,lg.Station,lg.Department,logMsg +"-at "+DateTime.Now.ToShortTimeString(),timeout);
                        issue.issueEscalationEvent+=new EventHandler<issueEscalateEventArgs>(issue_issueEscalationEvent);

                        Issues.Add(recordId,issue);

                        issue.raise();

                        logMsg += "--Issue Raised" + "----at: " + DateTime.Now.ToString() + Environment.NewLine;

                        

                        
                    }
                    else
                    {
                        if (dataAccess.getIssueStatus(recordId) == "raised")
                        {
                            dataAccess.updateIssueStatus(recordId, "acknowledged");
                            logMsg += "--Issue Acknowledged" + "----at: " + DateTime.Now.ToString(); 
                        }
                        else if (dataAccess.getIssueStatus(recordId) == "acknowledged")
                        {

                            dataAccess.updateIssueStatus(recordId);

                            if (Issues.ContainsKey(recordId))
                            {
                                Issues[recordId].resolve();
                                Issues.Remove(recordId);
                            }

                            logMsg += "--Issue Resolved" + "----at: " + DateTime.Now.ToString() ;
                        }
                        else return;
                    }
                    dataAccess.updateIssueMarquee();
                    tbMsg.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                            new Action(() => { tbMsg.Text += logMsg + Environment.NewLine; }));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Error", MessageBoxButton.OK);
            }

        }

        void issue_issueEscalationEvent(object sender, issueEscalateEventArgs e)
        {
            Issue issue = (Issue)sender;

            
            List<Shift> sl = shifts.getShifts(DateTime.Now.TimeOfDay);

            List<int> slID = new List<int>();

            foreach( Shift s in sl)
                slID.Add(s.ID);




            List<Contact> receivers =  contacts.getContactList(issue.Line,slID ,
                                    issue.Department, e.escalationLevel);
            foreach (Contact c in receivers)
                dataAccess.insertSmsTrigger(c.Number, issue.Message, 1, DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));

            if( e.escalationLevel > 0 )
                tbMsg.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                        new Action(() => { tbMsg.Text += issue.Message + "-- Escalation Level" +e.escalationLevel.ToString()
                            + Environment.NewLine; }));


        }


        private void processCommand(int lineId, DeviceCommand cmd, String cmdData)
        {
            

            String msg = String.Empty;
            String[] cmdParams = new String[5];

            try
            {
                switch (cmd)
                {
                    case DeviceCommand.SET_PLANNED_QUANTITY:
                        updateMsg("Processing Command - "+ DeviceCommand.SET_PLANNED_QUANTITY);

                        cmdParams = separateCommandData(cmdData);
                        int shift = Convert.ToInt32(cmdParams[0]);
                        int session = Convert.ToInt32(cmdParams[1]);
                        int plannedQty = int.Parse(cmdParams[2]);

                        if( lines.getLineName(lineId) == String.Empty)
                        {
                            updateMsg("Line not supported");
                            return;
                        }
                        Shift s = shifts.getShift(shift);
                        if ( s == null)
                        {
                            updateMsg( "Shift not supported");
                            return;
                        }
                
                        Session se = s.Sessions.getSession(session);
                        if( se == null)
                        {
                            updateMsg("Session not supported");
                            return;
                        }
                        

                        msg = "Setting Target Qty for Line : {0},Shift : {1} Session:{2} Qty:{3}";



                        msg = String.Format(msg, lines.getLineName(lineId), s.Name,se.Name, plannedQty);

                        updateMsg(msg);

                        dataAccess.updateTargetQuantity(lineId, shift,session ,plannedQty);

                        break;

                    case DeviceCommand.ISSUE:
                        StringReader reader = new StringReader(cmdData);
                        AndonAlertEventArgs eventArgs = (AndonAlertEventArgs)xmlSerializer.Deserialize(reader);
                        andonManager_andonAlertEvent(this, eventArgs);

                        break;


                    default:

                        break;
                }
            }
            catch (SqlException se)
            {

            }
        }

        private String[] separateCommandData(String cmdData)
        {

            return
                cmdData.Split(dataSeperator.ToCharArray());
        }



        void updateMsg(String msg)
        {
            tbMsg.Dispatcher.BeginInvoke(DispatcherPriority.Background,
         new Action(() =>
         {
             tbMsg.Text += msg + Environment.NewLine;
         }));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            commandTimer.Stop();
            commandTimer.Close();
            commandTimer.Dispose();

            if (andonManager != null)
                andonManager.stop();

        }
    }
}
