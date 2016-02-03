using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Sql;
using ias.shared;

namespace PlanningUtility
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


        public DataTable getProductionLineInfo(String lines)
        {
            String qry = String.Empty;
            qry = @"SELECT Lines.id , stations.description 
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

        public void updateTarget(int line, String shift, int quantity,int reference,
            int plannedManpower, int maximumManpower,string timestamp)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            String qry = String.Empty;
            qry = @"insert into [target](line ,shift, quantity, product_reference,planned_manpower,maximum_manpower,timestamp)
                    values({0},'{1}',{2},{3},{4},{5},'{6}')";
            qry = String.Format(qry, line, shift, quantity, reference,plannedManpower,maximumManpower, timestamp);
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.ExecuteNonQuery();
            cmd.Dispose();

            con.Close();
            con.Dispose();
        }

        public ShiftCollection getShifts()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            ias.shared.ShiftCollection shifts = new ShiftCollection();

            String qry = String.Empty;
            qry = @"SELECT * FROM shift  where [description] <> 'General Shift' ORDER BY id  ";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            cmd.Dispose();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                shifts.Add(new Shift((int)dt.Rows[i]["id"], (string)dt.Rows[i]["description"],
                   ((TimeSpan)dt.Rows[i]["from"]), ((TimeSpan)dt.Rows[i]["to"])));
            }

            con.Close();
            con.Dispose();
            return shifts;
        }


     



        public DataTable getProductionQuantity(int shift, String lines)
        {
            String qry = String.Empty;
            qry = @"select stations.description as [ProductionLine], 
                    ProductionQuantity.targetQuantity as [TargetQuantity],
                    ProductionQuantity.actualQuantity as [ActualQuantity]
                    from ProductionQuantity inner join stations on ProductionQuantity.line = stations.id 
                    where shift = {0} ";

            String qry1 = String.Empty;
            if (lines != String.Empty)
            {
                qry1 = " and stations.id in ({0})";
                qry1 = String.Format(qry1, lines);
            }

            qry += qry1;

            qry = String.Format(qry, shift);

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();

            return dt;

        }



        #region References
        public List<Reference> getReferences(int line)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            List<Reference> references = new List<Reference>();


            String qry = String.Empty;
            qry = @"SELECT * FROM ProductReferences  where [line] ={0}  ";

            qry = String.Format(qry, line);

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            cmd.Dispose();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                references.Add(new Reference((int)dt.Rows[i]["id"], (String)dt.Rows[i]["Reference"],
                    (int)dt.Rows[i]["line"], (String)dt.Rows[i]["Description"]));
            }

            con.Close();
            con.Dispose();
            return references;
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
