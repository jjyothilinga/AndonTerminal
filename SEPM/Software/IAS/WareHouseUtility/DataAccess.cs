using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Sql;


namespace WareHouseUtility
{
    class DataAccess
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


        public OpenIssueCollection GetOpenIssues()
        {
            SqlConnection localCon = new SqlConnection(conStr);
            String qry = @"select distinct Substring(Convert(nvarchar,issues.timestamp,0),0,12) as DATE, 
                        issues.slNo as SlNo,
                        issues.data as PartNo,
                        issues.status as Status
                        from issues
                       
                        where department=3 and line=10 and status <> 'resolved'";


            localCon.Open();

            OpenIssueCollection openIssues = new OpenIssueCollection();

            SqlCommand cmd = new SqlCommand(qry, localCon);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dr.Close();

            localCon.Close();
            localCon.Dispose();

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                openIssues.Add(new OpenIssue((int)dt.Rows[i]["SlNo"],(String)dt.Rows[i]["PartNo"],
                    (String)dt.Rows[i]["Status"]));
            }

            return openIssues;
        }


        public bool checkPart(String PartNo)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry = @"select * from issues where data='{0}' and status='raised'";

            qry = String.Format(qry, PartNo);

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            
            dr.Close();

            con.Close();
            con.Dispose();

            if (dt.Rows.Count > 0)
                return true;
            else return false;
        }

        public void resolveIssue(OpenIssue openIssue)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            String qry = String.Empty;
            qry = @"update issues set status = 'resolved', timestamp='{0}' where slNo={1} ";
            qry = String.Format(qry, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),openIssue.RecordID);
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            con.Close();
            con.Dispose();

        }

        public void addCommand(int device, DeviceCommand command, String data)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            String qry = String.Empty;
            qry = @"insert into command(line_id, command , command_data, status,request_timestamp)
                    values({0},{1},'{2}',{3},'{4}')";
            qry = String.Format(qry, device, (int)command, data, 1, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();

            cmd.Dispose();

            con.Close();
            con.Dispose();

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

        public DataTable GetReportData(DateTime from, DateTime to, String Lines, String Departments)
        {

            SqlConnection localCon = new SqlConnection(conStr);
            to = to.AddDays(1);
            String qry = @"select distinct Substring(Convert(nvarchar,raised.timestamp,0),0,12) as DATE, 
                        
                        lines.description as LINE , 
                        departments.description as DEPARTMENT , 
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
                        where raised.timestamp >= '{0}' and raised.timestamp <= '{1}'
                        and lines.id in ({2}) and departments.id in ({3})";

            qry = String.Format(qry, from.ToString("yyyy-MM-dd"), to.ToString("yyyy-MM-dd"), Lines, Departments);

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

    }
}
