using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using Microsoft.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Collections;
using System.IO;

namespace ias.client
{
    class GClass1
    {
        public static class MyGlobals
        {
            public const string strcon = "Data Source=10.179.45.40\\sqlinstance;Initial Catalog=concare;User ID=concaredata;Password=concareconzerv;Connection Timeout=300";
            
            public static string gAccID;
            public static string gSesaID;
            public static string gUsername;
            public static string gEmpID;
            public static string gDept;
            public static string gTitle;
            public static string gCompname;
            public static string gCompAddress;
            public static string gCompAddress1;
            public static string gCompCity;
            public static string gCompState;
            public static string gCompCountry;
            public static string gCompPhno;
            public static string gCompFax;
            public static string gCompCareEmail;
            public static string gCompSuppEmail;
            public static string gCompWeb;
            public static string gCompToll;
            public static string gCompCST;
            public static string gCompTin;
            public static string gCompPAN;
            public static string gCompSerTax;
            public static string gResponsibilityEmail;
            public static string gManagerEmail;
            public static string gHREmail;
            public static string gProject;
            public static string LoginSysName;
            //public static Dictionary<string, string> _childWindows = new Dictionary<string, string>();
            // <summary>
            // Static value protected by access routine.
            // </summary>
            static int _globalValue;
            public static class DataGridHelper
            {
                public static DataGridCell GetCell(DataGridCellInfo dataGridCellInfo)
                {
                    if (!dataGridCellInfo.IsValid)
                    {
                        return null;
                    }

                    var cellContent = dataGridCellInfo.Column.GetCellContent(dataGridCellInfo.Item);
                    if (cellContent != null)
                    {
                        return (DataGridCell)cellContent.Parent;

                    }
                    else
                    {
                        return null;
                    }
                }
                public static int GetRowIndex(DataGridCell dataGridCell)
                {
                    // Use reflection to get DataGridCell.RowDataItem property value.
                    System.Reflection.PropertyInfo rowDataItemProperty = dataGridCell.GetType().GetProperty("RowDataItem", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                    DataGrid dataGrid = GetDataGridFromChild(dataGridCell);

                    return dataGrid.Items.IndexOf(rowDataItemProperty.GetValue(dataGridCell, null));
                }
                public static DataGrid GetDataGridFromChild(DependencyObject dataGridPart)
                {
                    if (VisualTreeHelper.GetParent(dataGridPart) == null)
                    {
                        throw new NullReferenceException("Control is null.");
                    }
                    if (VisualTreeHelper.GetParent(dataGridPart) is DataGrid)
                    {
                        return (DataGrid)VisualTreeHelper.GetParent(dataGridPart);
                    }
                    else
                    {
                        return GetDataGridFromChild(VisualTreeHelper.GetParent(dataGridPart));
                    }
                }
            }
            // <summary>
            // Access routine for global variable.
            // </summary>
            public static int GlobalValue
            {
                get
                {
                    return _globalValue;
                }
                set
                {
                    _globalValue = value;
                }
            }
            public static void ExecuteSQL(string SQLQuery)
            {
                SqlConnection conn = new SqlConnection(GClass1.MyGlobals.strcon);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand("Command", conn);


                cmd.CommandText = SQLQuery;//@"DELETE * FROM Customers";

                cmd.ExecuteNonQuery();

                conn.Close();//function logic ...
            }

            public static double GetMaxNo(string MaxSql)
            {
                double functionReturnValue = 0;
                SqlCommand cmSQL = default(SqlCommand);
                SqlDataReader drSQL = default(SqlDataReader);
                SqlConnection conn = new SqlConnection(GClass1.MyGlobals.strcon);
                try
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Open();
                    cmSQL = new SqlCommand(MaxSql, conn);
                    drSQL = cmSQL.ExecuteReader();
                    if (drSQL.HasRows)
                    {
                        drSQL.Read();
                        if (drSQL.FieldCount > 0)
                        {
                            string firstItem = Convert.ToString(drSQL[0]);
                            if (string.IsNullOrEmpty(firstItem))
                            {
                                functionReturnValue = 1;
                            }
                            else
                            {
                                functionReturnValue = Convert.ToDouble(firstItem);
                                functionReturnValue = functionReturnValue + 1;
                            }
                        }
                        else
                        {
                            functionReturnValue = 1;
                        }
                    }
                    else
                    {
                        functionReturnValue = 1;
                    }
                    drSQL.Close();
                    conn.Close();
                    cmSQL.Dispose();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The following error occured :rn" + ex.ToString(), "Inprocess Quality"); 

                }
                finally
                {
                    conn.Close();
                }
                return functionReturnValue;
            }
    
            public static void gridfillStoredProc(string StoredProc, Microsoft.Windows.Controls.DataGrid Grid)
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(StoredProc, GClass1.MyGlobals.strcon);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    System.Data.DataTable table = new System.Data.DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    dataAdapter.Fill(table);
                    //if (table.Rows.Count > 0)
                    //{
                    //    Grid.DataContext = table;
                    //}
                    //else
                    //{ 
                    Grid.DataContext = table;
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The following error occured :rn" + ex.ToString(), "Inprocess Quality");
                }
                finally
                { 
                } 
            }

            public static void gridfillSP(string grdsql, Microsoft.Windows.Controls.DataGrid Grid)
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = GClass1.MyGlobals.strcon;
                SqlCommand command = new SqlCommand("Get_GridDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@gAccessID", SqlDbType.Int).Value = GClass1.MyGlobals.gAccID;
                command.Parameters.Add("@gEmpID", SqlDbType.Int).Value = GClass1.MyGlobals.gEmpID;
                command.Parameters.Add("@gStage", SqlDbType.VarChar).Value = "KD";
                command.Parameters.Add("@gSoft", SqlDbType.VarChar).Value = "IE";
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //SqlDataAdapter dataAdapter = new SqlDataAdapter(grdsql, GClass1.MyGlobals.strcon);
                //SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                //System.Data.DataTable table = new System.Data.DataTable();
                //table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                //dataAdapter.Fill(table);
                //if (table.Rows.Count > 0)
                //{
                //    Grid.DataContext = table;
                //}
                //else
                //{ 
                //dgvIEKD.DataContext = dt;
            }

            public static void gridfill(string grdsql, Microsoft.Windows.Controls.DataGrid Grid)
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(grdsql, GClass1.MyGlobals.strcon);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    System.Data.DataTable table = new System.Data.DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    dataAdapter.Fill(table);
                    //if (table.Rows.Count > 0)
                    //{
                    //    Grid.DataContext = table;
                    //}
                    //else
                    //{ 
                    Grid.DataContext = table;
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The following error occured :rn" + ex.ToString(), "Inprocess Quality");
                }
                finally
                { 
                }
                
            }


            

            public static void combofill(string CboSQL, System.Windows.Controls.ComboBox Cbo)
            {
                // Instantiate a OleDb connection object
                SqlConnection connection = new SqlConnection();
                //assign the proper connection string(here we have an access database) to the connection
                connection.ConnectionString = GClass1.MyGlobals.strcon;

                // Instantiate a OleDb command object
                SqlCommand command = new SqlCommand();

                //assign the command to be executed
                command.CommandText = CboSQL;

                //bind the command object to the connection
                command.Connection = connection;

                try
                {
                    // opening the connection
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();


                    if (dr.HasRows)
                    {
                        // Iterate through the table
                        while (dr.Read())
                        {
                           
                            //adding item to the combobox. you can also use dr[1] to get the country name
                            Cbo.Items.Add(dr.GetString(0));//["statename"]);
                        }
                    }
                    dr.Close();
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show("The following error occured :rn" + ex.ToString(), "Inprocess Quality");
                }
                finally
                {
                    // close the connection and DataReader
                    connection.Close();
                    //dr.Close();
                }
            }
        }

        ///Role implementation class
        
        /// <summary>    
        /// This class provides a way to implement the role based security to the application.
        /// The screen fields are defined in the xml and this file reads the xml and loads it into
        /// cache memory as a strongly typed class to be accessed and used inthe screens.
        /// </summary> 
        public class Role
        {
            #region Private Fields
            private static Role RoleInstance = new Role();
            /// <summary>
            /// HybridDictionary cachedRoleFields, populated in Role's constructor, 
            /// is the core of this class, as it holds the collection of RoleField objects.
            /// cachedRoleFields can only be accessed through the Field property,
            /// which retrieves a particular RoleField object from the collection
            /// based on a string key. 
            /// </summary>
            private HybridDictionary cachedRoleFields = new HybridDictionary();
            private const string ROLE_SEPRATOR = "#";
            private const string ROLE_ASSIGNMENT = "=";
            #endregion

            #region Properties
            public static Role Instance
            {
                get { return RoleInstance; }
            }

            public static RoleField Field(string fieldName)
            {
                return ((RoleField)(RoleInstance.cachedRoleFields[fieldName]));
            }
            #endregion

            /// <summary>
            /// Contains the structure for the RoleField
            /// </summary> 
            public class RoleField
            {
                /// <summary>
                ///  Name is the key used in the calling code to retrieve the appropriate values. 
                /// </summary>
                public String Name = String.Empty;

                /// <summary>
                /// Control is the control Name in the respective screen
                /// </summary>
                public String Control = String.Empty;

                /// <summary>
                /// Collection to hold the property values either true/false
                /// </summary>
                public Hashtable Enabled = new Hashtable();

                /// <summary>
                /// Collection to hold the property values either true/false
                /// </summary>
                public Hashtable Visible = new Hashtable();
            }
            /// <summary>
            ///The constructor is only called once, the first time this class is instantiated. 
            ///Every time an instance of this object is called after the first time, 
            ///the "in memory" copy contained in RoleInstance is used. 
            ///The constructor performs the basics. 
            ///It reads RoleValiations.xml into memory, in a DataSet. 
            ///It reads through each row of the DataSet and populates a new RoleField object. 
            ///Finally, it adds the RoleField object to the cachedRoleFields collection. 
            ///Once the DataSet is populated it is not changed until it is reloaded from the 
            ///XML when the application is restarted. 
            /// </summary>
            private Role()
            {
                #region Local Variable Declaration
                RoleField Field = null;
                DataSet ds = new DataSet();
                string[] RoleEnabledList;
                string[] RoleEnabled;
                string[] RoleVisibleList;
                string[] RoleVisible;
                int Ctr = 0;
                #endregion

                try
                {
                    #region Reads ScreenRoles.xml into memory
                    string RoleFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"RoleValidations.xml");
                    //string RoleFilePath = WebConfigurationManager.AppSettings["RoleValidation"].ToString();
                    ds.ReadXml(RoleFilePath);
                    #endregion

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Reads through each row of the DataSet and populates a new Field object
                        Field = new RoleField();
                        Field.Name = dr["Name"].ToString();
                        //Field.Control = dr["Control"].ToString();
                        RoleEnabledList = dr["Enabled"].ToString().Split(ROLE_SEPRATOR.ToCharArray());

                        #region Spilting Enabled properties by role
                        for (Ctr = 0; Ctr < RoleEnabledList.Length; Ctr++)
                        {
                            RoleEnabled = RoleEnabledList[Ctr].Split(ROLE_ASSIGNMENT.ToCharArray());
                            Field.Enabled.Add(RoleEnabled[0], RoleEnabled[1]);
                        }
                        #endregion

                        RoleVisibleList = dr["Visible"].ToString().Split(ROLE_SEPRATOR.ToCharArray());

                        #region Spilting Enabled properties by role
                        for (Ctr = 0; Ctr < RoleVisibleList.Length; Ctr++)
                        {
                            RoleVisible = RoleVisibleList[Ctr].Split(ROLE_ASSIGNMENT.ToCharArray());
                            Field.Visible.Add(RoleVisible[0], RoleVisible[1]);
                        }
                        #endregion

                        #endregion

                        #region Adds the Field object to the cachedRoleFields collection.
                        cachedRoleFields.Add(Field.Name, Field);
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            /// <summary>
            /// The Reset method allows for dynamic reloading of ScreenRoles.xml during runtime. 
            /// Any screen/page/class could very easily call this method in a event handler to 
            /// allow administrators to reload ScreenRoles.xml on the fly without restarting 
            /// the application.
            /// </summary>
            public static void Reset()
            {
                RoleInstance = new Role();
            }
        }
    }
}