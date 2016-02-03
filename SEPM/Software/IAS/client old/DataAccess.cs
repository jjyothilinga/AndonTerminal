using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Sql;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ias.shared;
namespace ias.client
{
    public class DataAccess
    {


        private String conStr;
        private SqlConnection con;

        public DataAccess()
        {
            conStr = ConfigurationSettings.AppSettings["DBConStr"];
            con = new SqlConnection(conStr);
            try
            {
                con.Open();
            }
            catch (SqlException s)
            {
                throw s;
            }
        }


        public int getDays()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry = String.Empty;
            qry = @"SELECT value FROM config where [key]='ltaDate'";


            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            cmd.Dispose();
            DateTime lta = DateTime.Parse((String)dt.Rows[0][0]);
            TimeSpan ts = DateTime.Now.Subtract(lta);
            return ts.Days;
        }



        public DataTable getProductionLineInfo(String lines)
        {
             SqlConnection con = new SqlConnection(conStr);
             con.Open();
            String qry = String.Empty;
            qry = @"SELECT lines.id , lines.description 
                    FROM lines where lines.id in ({0}) ";

            qry = String.Format(qry, lines);

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            cmd.Dispose();
            return dt;
        }


        public ObservableCollection<LineAvailability> getAvailability()
        {
            ObservableCollection<LineAvailability> Lines = new ObservableCollection<LineAvailability>();
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry = @"select lines.id as ID,lines.description as Name,Logon,Logoff
                    from (select device,status,timestamp as Logon from device_status_tracker where status='ON' AND TIMESTAMP >'{0}' and TIMESTAMP <'{1}' )as LOGON_TBL 
                    INNER join lines ON lines.id = LOGON_TBL.device
                left outer join (select device,status,timestamp as Logoff from device_status_tracker where status='OFF'AND TIMESTAMP >'{0}' and TIMESTAMP <'{1}')as LOGOFF_TBL on LOGON_TBL.device= LOGOFF_TBL.device
                  where lines.id <> 10  order by lines.id ";

                DateTime ts = DateTime.Parse(DateTime.Now.ToShortDateString() + " 06:00:00" );

                qry = String.Format(qry, ts.ToString("yyyy-MM-dd HH:mm:ss"),ts.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));

                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();
            int curID = -1;
            LineAvailability la= null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (curID != (int)dt.Rows[i]["ID"])
                {
                    if (la != null)
                        Lines.Add(la);

                    la = new LineAvailability((int)dt.Rows[i]["ID"], (string)dt.Rows[i]["NAME"], (DateTime)dt.Rows[i]["Logon"],
                        dt.Rows[i]["Logoff"] == DBNull.Value ? DateTime.Now : (DateTime)dt.Rows[i]["Logoff"]);
                    curID = (int)dt.Rows[i]["ID"];
                }
                else
                {
                    la.updateRunHours((DateTime)dt.Rows[i]["Logon"],
                        dt.Rows[i]["Logoff"] == DBNull.Value ? DateTime.Now : (DateTime)dt.Rows[i]["Logoff"]);
                }
            }
            if (la != null)
                Lines.Add(la);

            foreach (LineAvailability l in Lines)
            {
                qry = @"select distinct
                        raised.timestamp as RAISED , 
                        resolved.timestamp as RESOLVED,
                        departments.id as DEPARTMENT
                        from issues
                        inner join lines on lines.id = issues.line
                        inner join departments on issues.department = departments.id
                        inner join ( select issue, timestamp from issue_tracker where status = 'raised') as raised 
                        on raised.issue = issues.slNo

                        left outer join (select issue , timestamp from issue_tracker where status = 'resolved')
                        as resolved on resolved.issue = issues.slNo 
                        where raised.timestamp >= '{0}' and raised.timestamp <= '{1}' and lines.id = {2}";

                
                qry = String.Format(qry, ts.ToString("yyyy-MM-dd HH:mm:ss"), ts.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                    ,l.ID);

                cmd = new SqlCommand(qry, con);
                dr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                dr.Close();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    l.updateDowntime(((DateTime)dt.Rows[i]["RAISED"]).ToString(),
                        dt.Rows[i]["RESOLVED"] == DBNull.Value ? String.Empty : ((DateTime)dt.Rows[i]["RESOLVED"]).ToString(),
                        (int)dt.Rows[i]["DEPARTMENT"]);
                }

            }

            return Lines;
        }



        public DataTable getProductionLineInfo()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry = String.Empty;
            qry = @"SELECT lines.id , lines.description 
                    FROM lines ";
            String qry1 = String.Empty;

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            cmd.Dispose();
            return dt;
        }


       public ShiftCollection getShifts()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            ShiftCollection shifts= new ShiftCollection();

            String qry = String.Empty;
            qry = @"SELECT * FROM shift where id > 1 ORDER BY id";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            cmd.Dispose();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                shifts.Add(new Shift((int)dt.Rows[i]["id"],(string)dt.Rows[i]["description"],
                   ((TimeSpan) dt.Rows[i]["from"]),((TimeSpan) dt.Rows[i]["to"])));
            }

            con.Close();
            con.Dispose();
            return shifts;
        }

       public SessionCollection getSessions(int shift)
       {

           SqlConnection con = new SqlConnection(conStr);
           con.Open();

           SessionCollection sessions = new SessionCollection();

           String qry = String.Empty;
           qry = @"SELECT * FROM sessions where shift={0} ORDER BY id";

           qry = String.Format(qry, shift);
           SqlCommand cmd = new SqlCommand(qry, con);
           SqlDataReader dr = cmd.ExecuteReader();
           DataTable dt = new DataTable();
           dt.Load(dr);
           dr.Close();
           cmd.Dispose();

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               sessions.Add(new Session(shift, (int)dt.Rows[i]["id"], (string)dt.Rows[i]["description"],
                   ((TimeSpan)dt.Rows[i]["from"]).ToString(), ((TimeSpan)dt.Rows[i]["to"]).ToString()));
           }

           con.Close();
           con.Dispose();
           return sessions;
       }


        public DataTable addCommand(int lineID, int command, int status, String cmdData, String date)
        {
            SqlCommand cmd = new SqlCommand("addCommand", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@line_id", lineID);
            cmd.Parameters.AddWithValue("@command", command);
            cmd.Parameters.AddWithValue("@commandData", cmdData);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@req_date", date);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        public String getMarquee()
        {
            String qry = String.Empty;
            qry = @"select * from config where [key]='marquee'";
            SqlConnection prvCon = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(qry, prvCon);
            prvCon.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            prvCon.Close();
            prvCon.Dispose();

            return (String)dt.Rows[0]["value"];
        }


        public String getIssueMarquee()
        {
            String qry = String.Empty;
            qry = @"select * from config where [key]='issueMarquee'";
            SqlConnection prvCon = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(qry, prvCon);
            prvCon.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            prvCon.Close();
            prvCon.Dispose();

            return (String)dt.Rows[0]["value"];
        }



        public DataTable getProductionQuantity(int shift,String lines)
        {
            String qry = String.Empty;
            qry = @"select lines.description as [ProductionLine], 
                    target.quantity as [TargetQuantity],
                    actual.quantity as [ActualQuantity]
                    from actual inner join lines on actual.line = lines.id 
                    inner join [target] on target.line = lines.id
                    where shift = {0} and session = {1} ";

            String qry1 = String.Empty;
            if (lines != String.Empty)
            {
                qry1 = " and lines.id in ({0})";
                qry1 = String.Format(qry1, lines);
            }

            qry += qry1;

            qry = String.Format(qry,shift);

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();

            return dt ;

        }

        public List<int> getBreakDownStatus(String lines)
        {
            String qry = String.Empty;
            List<int> stationList = new List<int>();
            qry = @"select distinct line from issues where department = 1 and status = 'raised' ";

            String qry1 = String.Empty;
            if (lines != String.Empty)
            {
                qry1 = " and line in ({0})";
                qry1 = String.Format(qry1, lines);
            }

            qry += qry1;

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();

            for(int i = 0; i< dt.Rows.Count; i++)
            {
                stationList.Add((int)dt.Rows[i]["line"]);
            }

            return stationList;

        }

        public List<int> getQualityStatus(String lines)
        {
            String qry = String.Empty;
            List<int> stationList = new List<int>();


            qry = @"select distinct line from issues where department = 2 and status = 'raised' ";


            String qry1 = String.Empty;
            if (lines != String.Empty)
            {
                qry1 = " and line in ({0})";
                qry1 = String.Format(qry1, lines);
            }

            qry += qry1;




            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stationList.Add((int)dt.Rows[i]["line"]);
            }


            return stationList;

        }


        public List<int> getMaterialShortageStatus(String lines)
        {
            String qry = String.Empty;
            List<int> stationList = new List<int>();

            qry = @"select distinct line from issues where department = 3 and status = 'raised' ";

            String qry1 = String.Empty;
            if (lines != String.Empty)
            {
                qry1 = " and line in ({0})";
                qry1 = String.Format(qry1, lines);
            }

            qry += qry1;

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stationList.Add((int)dt.Rows[i]["line"]);
            }


            return stationList;

        }



        public String getTargetQuantity(int shift , int session, int id)
        {
            String qry = String.Empty;
            List<int> stationList = new List<int>();

            qry = @"select quantity from target where shift in ({0})
                        and session in ({1}) and line = {2} order by timestamp desc";

            qry = String.Format(qry, shift, session, id);
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();

            

            int quantity = (dt.Rows.Count == 0) ? -1 : (int) dt.Rows[0][0];
            return (quantity == -1) ?  string.Empty : quantity.ToString();

        }


        public String getActualQuantity(int lines )
        {
            String qry = String.Empty;
            List<int> stationList = new List<int>();

            qry = @"select Top(1)  quantity from actual where line = {0} and
                    DATEDIFF(DD,timestamp,GETDATE()) < 1 order by timestamp desc";

            qry = String.Format(qry, lines);
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();


            int quantity = (dt.Rows.Count == 0) ? -1 : (int)dt.Rows[0][0];
            return (quantity == -1) ? string.Empty : quantity.ToString();
        }


        public String getProducedQuantity(int lines)
        {
            String qry = String.Empty;
            List<int> stationList = new List<int>();

            qry = @"select Sum(quantity) from actual where line={0} and 
                    timestamp > '{1}' and  timestamp < '{2}'";

            qry = String.Format(qry, lines, DateTime.Now.ToString("MM-dd-yyyy") +" 06:00:00",
               new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1).ToString("MM-dd-yyyy") +" 06:00:00");
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();


            if(dt.Rows[0][0] == DBNull.Value )
                return string.Empty;
            else  return((int) dt.Rows[0][0]).ToString();
            
        }

            


        #region ReportTab

        public DataTable GetReportData(DateTime from, DateTime to)
        {

            SqlConnection localCon = new SqlConnection(conStr);
            to = to.AddDays(1);
            String qry = @"select distinct Substring(Convert(nvarchar,raised.timestamp,0),0,12) as DATE, 
                        
                        lines.description as LINE , 
                        stations.description as STATION,
                        departments.description as ISSUE , 
                        issues.data as DETAILS,
                        CONVERT(TIME(0), raised.timestamp,0) as RAISED , 
                        CONVERT(TIME(0), acknowledged.timestamp,0) as ACKNOWLEDGED , 
                        CONVERT(TIME(0), resolved.timestamp,0) as RESOLVED ,
                        CONVERT(Time(0) , resolved.timestamp - raised.timestamp , 0) as DOWNTIME 
                        from issues
                        left outer join stations on (stations.id = issues.station and stations.line = issues.line)
                        inner join lines on lines.id = issues.line
                        inner join departments on issues.department = departments.id
                        inner join ( select issue, timestamp from issue_tracker where status = 'raised') as raised 
                        on raised.issue = issues.slNo
                        left outer join (select issue , timestamp from issue_tracker where status = 'acknowledged')
                        as acknowledged on acknowledged.issue = issues.slNo 
                        left outer join (select issue , timestamp from issue_tracker where status = 'resolved')
                        as resolved on resolved.issue = issues.slNo 
                        where raised.timestamp >= '{0}' and raised.timestamp <= '{1}' ";

            qry = String.Format(qry, from.ToShortDateString(), to.ToShortDateString());

            localCon.Open();

            SqlCommand cmd = new SqlCommand(qry, localCon);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dr.Close();

            localCon.Close();
            localCon.Dispose();

            return dt;


        }



        #endregion


        #region ISSUES_TAB

        public DataTable GetOpenIssues()
        {
            SqlConnection localCon = new SqlConnection(conStr);
            String qry = @"select distinct Substring(Convert(nvarchar,issues.timestamp,0),0,12) as DATE, 
                        issues.slNo as SlNo,
                        issues.station as STATION_NO,
                        lines.description as LINE , 
                        stations.description as STATION_NAME,
                        departments.description as ISSUE , 
                        issues.data as DETAILS,
                        CONVERT(TIME(0), issues.timestamp,0) as RAISED 
                        from issues
                        LEFT OUTER JOIN stations on (stations.id = issues.station and stations.line = issues.line)
                        inner join lines on lines.id = issues.line
                        inner join departments on issues.department = departments.id
                        where status='raised'";


            localCon.Open();

            SqlCommand cmd = new SqlCommand(qry, localCon);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dr.Close();

            localCon.Close();
            localCon.Dispose();

            return dt;
        }

        public void updateIssueMarquee()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();


            string issueMarquee = String.Empty;
            String qry = @"select message from issues where [status] = 'raised' and DATEDIFF(hh,timestamp,GETDATE())<=24";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                issueMarquee += (String)dt.Rows[i]["message"] + "; ";
            }
            qry = @"update config set value ='{0}' where [key]='issueMarquee'";
            qry = String.Format(qry, issueMarquee);

            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
        }

        public void updateIssueStatus(int slNo)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            String qry = String.Empty;
            qry = @"update issues set status = 'resolved' , timestamp = '{0}' where slNo={1}";
            qry = String.Format(qry, DateTime.Now, slNo);
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();
            cmd.Dispose();

            con.Close();
            con.Dispose();
        }


        public void updateIssueStatus(DataTable dt)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String qry = String.Empty;
                qry = @"update issues set status = 'resolved' , timestamp = '{0}' where slNo={1}";
                qry = String.Format(qry, DateTime.Now, (int)dt.Rows[i]["SlNo"]);
                SqlCommand cmd = new SqlCommand(qry, con);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            con.Close();
            con.Dispose();
        }


        #endregion



        public void close()
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

    }
}
