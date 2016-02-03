using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Sql;


namespace ReportingUtility
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


        public DataTable GetIssueReportData(DateTime from, DateTime to)
        {

            SqlConnection localCon = new SqlConnection(conStr);
            to = to.AddDays(1);
            String qry = @"select Substring(Convert(nvarchar,raised.timestamp,0),0,12) as DATE, 
                        
                        lines.description as LINE , 
                        stations.description as STATION,
                        departments.description as ISSUE , 
                        issues.data as DETAILS,
                        CONVERT(TIME(0), raised.timestamp,0) as RAISED , 
                        acknowledged.timestamp as ACKNOWLEDGED , 
                        resolved.timestamp as RESOLVED 
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
                        where raised.timestamp >= '{0}' and raised.timestamp <= '{1}' order by raised.timestamp ";

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



    }
}
