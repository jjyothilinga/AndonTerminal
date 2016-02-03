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
namespace SupportGroupUtility
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
            qry = @"SELECT * FROM shift where id > 0 ORDER BY id";

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

            DateTime from = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,
                6,0,0);
            DateTime to = from.AddDays(1);

            qry = String.Format(qry, lines, from.ToString("MM-dd-yyyy"),
               to.ToString("MM-dd-yyyy"));
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();


            if(dt.Rows[0][0] == DBNull.Value )
                return string.Empty;
            else  return((int) dt.Rows[0][0]).ToString();
            
        }



        #region SUPPORT GROUP MANAGEMENT

        public ContactCollection getContacts()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            ContactCollection contacts = new ContactCollection();

            String qry = String.Empty;
            qry = @"SELECT * FROM contacts order by Name";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable contactTable = new DataTable();
            contactTable.Load(dr);
            dr.Close();

            qry = String.Empty;
            qry = @"SELECT * FROM lines";

            cmd = new SqlCommand(qry, con);
            dr = cmd.ExecuteReader();
            DataTable lineTable = new DataTable();
            lineTable.Load(dr);
            dr.Close();

            cmd.Dispose();

            for (int i = 0; i < contactTable.Rows.Count; i++)
            {
                bool isprocurement = false;
                if (contactTable.Rows[i]["isProcurement"] == DBNull.Value) ;

                else if ((int)contactTable.Rows[i]["isProcurement"] == 1)
                    isprocurement = true;

                Contact c = new Contact((string)contactTable.Rows[i]["ID"],
                    (string)contactTable.Rows[i]["password"],
                    (string)contactTable.Rows[i]["name"],
                    (string)contactTable.Rows[i]["number"],
                    isprocurement);

                createAssociation(c,
                    contactTable.Rows[i]["lineAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[i]["lineAssociation"],
                    contactTable.Rows[i]["shiftAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[i]["shiftAssociation"],
                    contactTable.Rows[i]["departmentAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[i]["departmentAssociation"],
                    contactTable.Rows[i]["escalationAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[i]["escalationAssociation"]);




                contacts.Add(c);
            }

            con.Close();
            con.Dispose();
            return contacts;
        }


        public Contact getContact(String id, String password)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            

            String qry = String.Empty;
            qry = @"SELECT * FROM contacts where ID = '{0}' and password = '{1}'";

            qry = String.Format(qry, id, password);

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable contactTable = new DataTable();
            contactTable.Load(dr);
            dr.Close();

            cmd.Dispose();

            if (contactTable.Rows.Count == 0)
                return null;

            bool isprocurement = false;
            if (contactTable.Rows[0]["isProcurement"] == DBNull.Value) ;

            else if ((int)contactTable.Rows[0]["isProcurement"] == 1)
                isprocurement = true;

            Contact c = new Contact((string)contactTable.Rows[0]["ID"],
                (string)contactTable.Rows[0]["password"],
                (string)contactTable.Rows[0]["name"],
                (string)contactTable.Rows[0]["number"],
                isprocurement);


                createAssociation(c,
                    contactTable.Rows[0]["lineAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[0]["lineAssociation"],
                    contactTable.Rows[0]["shiftAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[0]["shiftAssociation"],
                    contactTable.Rows[0]["departmentAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[0]["departmentAssociation"],
                    contactTable.Rows[0]["escalationAssociation"] == DBNull.Value ? String.Empty : (String)contactTable.Rows[0]["escalationAssociation"]);




                
            

            con.Close();
            con.Dispose();
            return c;
        }




        private ShiftAssociationCollection createShiftAssociation(DataTable lineTable, String association)
        {
            ShiftAssociationCollection Association = new ShiftAssociationCollection();

            for (int i = 0; i < lineTable.Rows.Count; i++)
            {
                Association.Add(new ShiftAssociationInfo((int)lineTable.Rows[i]["id"],
                     (string)lineTable.Rows[i]["description"],
                     (association.ToCharArray())[i] == '0' ? false : true));
            }
            return Association;
        }



        public  void createAssociation(Contact c)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry;
            qry = String.Empty;
            qry = @"SELECT * FROM lines";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable lineTable = new DataTable();
            lineTable.Load(dr);
            dr.Close();


            qry = @"SELECT * FROM shift";
            cmd = new SqlCommand(qry, con);
            dr = cmd.ExecuteReader();
            DataTable shiftTable = new DataTable();
            shiftTable.Load(dr);
            dr.Close();


            qry = @"SELECT * FROM departments";
            cmd = new SqlCommand(qry, con);
            dr = cmd.ExecuteReader();
            DataTable departmentTable = new DataTable();
            departmentTable.Load(dr);
            dr.Close();


            qry = @"SELECT * FROM escalation";
            cmd = new SqlCommand(qry, con);
            dr = cmd.ExecuteReader();
            DataTable escalationTable = new DataTable();
            escalationTable.Load(dr);
            dr.Close();

            cmd.Dispose();

            LineAssociationCollection lineAssociation = new LineAssociationCollection();

            for (int i = 0; i < lineTable.Rows.Count; i++)
            {
                lineAssociation.Add(new LineAssociationInfo((int)lineTable.Rows[i]["id"],
                     (string)lineTable.Rows[i]["description"],
                     true));
            }
            c.LineAssociation = lineAssociation;

            ShiftAssociationCollection shiftAssociation = new ShiftAssociationCollection();

            for (int i = 0; i < shiftTable.Rows.Count; i++)
            {
                shiftAssociation.Add(new ShiftAssociationInfo((int)shiftTable.Rows[i]["id"],
                     (string)shiftTable.Rows[i]["description"],
                     false));
            }
            c.ShiftAssociation = shiftAssociation;

            DepartmentAssociationCollection departmentAssociation = new DepartmentAssociationCollection();

            for (int i = 0; i < departmentTable.Rows.Count; i++)
            {
                departmentAssociation.Add(new DepartmentAssociationInfo((int)departmentTable.Rows[i]["id"],
                     (string)departmentTable.Rows[i]["description"],
                     false));
            }
            c.DepartmentAssociation = departmentAssociation;


            EscalationAssociationCollection escalationAssociation = new EscalationAssociationCollection();

            for (int i = 0; i < escalationTable.Rows.Count; i++)
            {
                escalationAssociation.Add(new EscalationAssociationInfo((int)escalationTable.Rows[i]["id"],
                     (string)escalationTable.Rows[i]["description"],
                     false));
            }
            c.EscalationAssociation = escalationAssociation;



            con.Close();
            con.Dispose();

        }

        public void createAssociation(Contact c, String lineTag, string shiftTag, string departmentTag, string escalationTag)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry;
            qry = String.Empty;
            qry = @"SELECT * FROM lines";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable lineTable = new DataTable();
            lineTable.Load(dr);
            dr.Close();


            qry = @"SELECT * FROM shift";
            cmd = new SqlCommand(qry, con);
            dr = cmd.ExecuteReader();
            DataTable shiftTable = new DataTable();
            shiftTable.Load(dr);
            dr.Close();


            qry = @"SELECT * FROM departments";
            cmd = new SqlCommand(qry, con);
            dr = cmd.ExecuteReader();
            DataTable departmentTable = new DataTable();
            departmentTable.Load(dr);
            dr.Close();


            qry = @"SELECT * FROM escalation";
            cmd = new SqlCommand(qry, con);
            dr = cmd.ExecuteReader();
            DataTable escalationTable = new DataTable();
            escalationTable.Load(dr);
            dr.Close();

            cmd.Dispose();

            LineAssociationCollection lineAssociation = new LineAssociationCollection();

            for (int i = 0; i < lineTable.Rows.Count; i++)
            {
                lineAssociation.Add(new LineAssociationInfo((int)lineTable.Rows[i]["id"],
                     (string)lineTable.Rows[i]["description"],
                     (lineTag.ToCharArray())[i] == '0' ? false : true));
            }
            c.LineAssociation = lineAssociation;

            ShiftAssociationCollection shiftAssociation = new ShiftAssociationCollection();

            for (int i = 0; i < shiftTable.Rows.Count; i++)
            {
                shiftAssociation.Add(new ShiftAssociationInfo((int)shiftTable.Rows[i]["id"],
                     (string)shiftTable.Rows[i]["description"],
                     (shiftTag.ToCharArray())[i] == '0' ? false : true));
            }
            c.ShiftAssociation = shiftAssociation;

            DepartmentAssociationCollection departmentAssociation = new DepartmentAssociationCollection();

            for (int i = 0; i < departmentTable.Rows.Count; i++)
            {
                departmentAssociation.Add(new DepartmentAssociationInfo((int)departmentTable.Rows[i]["id"],
                     (string)departmentTable.Rows[i]["description"],
                     (departmentTag.ToCharArray())[i] == '0' ? false : true));
            }
            c.DepartmentAssociation = departmentAssociation;


            EscalationAssociationCollection escalationAssociation = new EscalationAssociationCollection();

            for (int i = 0; i < escalationTable.Rows.Count; i++)
            {
                escalationAssociation.Add(new EscalationAssociationInfo((int)escalationTable.Rows[i]["id"],
                     (string)escalationTable.Rows[i]["description"],
                     (escalationTag.ToCharArray())[i] == '0' ? false : true));
            }
            c.EscalationAssociation = escalationAssociation;



            con.Close();
            con.Dispose();

        }


        public void updateContact(Contact c)
        {

            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry = String.Empty;
            SqlCommand cmd;
            if (c.ID == String.Empty)
            {
                qry = @"insert into contacts(name,number) values('{0}','{1}') ";
                qry = String.Format(qry, c.Name, c.Number);
                cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();

                qry = @"select * from contacts where name='{0}'and number='{1}' ";
                qry = String.Format(qry, c.Name, c.Number);
                cmd = new SqlCommand(qry, con);

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                dr.Close();

                c.ID = (String)dt.Rows[0]["ID"];
            }
            else
            {
                qry = @"update contacts set name='{0}', number='{1}' where ID={2}";
                qry = String.Format(qry, c.Name, c.Number, c.ID);
                cmd = new SqlCommand(qry, con);

                cmd.ExecuteNonQuery();
            }
            qry = @"update contacts set lineAssociation ='{0}',shiftAssociation = '{1}',
                       departmentAssociation='{2}',escalationAssociation='{3}' where ID= {4}";
            qry = String.Format(qry, getLineAssociationTag(c.LineAssociation),
                                    getShiftAssociationTag(c.ShiftAssociation),
                                    getDepartmentAssociationTag(c.DepartmentAssociation),
                                    getEscalationAssociationTag(c.EscalationAssociation),
                                    c.ID);
            cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        public string getLineAssociationTag(LineAssociationCollection line)
        {
            Char[] a = new Char[line.Count];
            int i = 0;
            foreach (LineAssociationInfo l in line)
            {
                a[i++] = (l.IsAssociated == false) ? '0' : '1';
            }

            return new String(a);

        }


        public string getShiftAssociationTag(ShiftAssociationCollection shift)
        {
            Char[] a = new Char[shift.Count];
            int i = 0;
            foreach (ShiftAssociationInfo l in shift)
            {
                a[i++] = (l.IsAssociated == false) ? '0' : '1';
            }

            return new String(a);

        }

        public string getDepartmentAssociationTag(DepartmentAssociationCollection dep)
        {
            Char[] a = new Char[dep.Count];
            int i = 0;
            foreach (DepartmentAssociationInfo l in dep)
            {
                a[i++] = (l.IsAssociated == false) ? '0' : '1';
            }

            return new String(a);

        }


        public string getEscalationAssociationTag(EscalationAssociationCollection esc)
        {
            Char[] a = new Char[esc.Count];
            int i = 0;
            foreach (EscalationAssociationInfo l in esc)
            {
                a[i++] = (l.IsAssociated == false) ? '0' : '1';
            }

            return new String(a);

        }


        public void deleteContact(Contact c)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            String qry = String.Empty;
            qry = @"delete from contacts where slNo= {0} ";
            qry = String.Format(qry, c.ID);
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();
            cmd.Dispose();



            con.Close();
            con.Dispose();
        }


        #endregion

        
            


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

        public DataTable GetReportData(DateTime from, DateTime to,String Lines, String Departments)
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

        #region ProcurementTab

        public OpenIssueCollection GetOpenProcurementIssues()
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

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                openIssues.Add(new OpenIssue((int)dt.Rows[i]["SlNo"], (String)dt.Rows[i]["PartNo"],
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
            qry = String.Format(qry, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), openIssue.RecordID);
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
