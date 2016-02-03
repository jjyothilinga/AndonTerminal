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
using Microsoft.Win32;
using System.IO;
using ias.shared;

namespace SupportGroupUtility
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        Contact c;
        DataAccess dataAccess;
        public Reports(Contact c)
        {
            InitializeComponent();
            this.c = c;
            dataAccess = new DataAccess();
        }

        #region ReportsTab

        System.Data.DataTable ReportTable = null;


        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            ReportTable = null;
            dgReportGrid.DataContext = null;

            ReportTable = dataAccess.GetReportData(dpFrom.SelectedDate.Value, dpTo.SelectedDate.Value,
                c.getLineAssociation(),c.getDepartmentAssociation());

            dgReportGrid.DataContext = ReportTable;

            dgReportGrid.Visibility = Visibility.Visible;



        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV (.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    String filename = dlg.FileName;
                    FileInfo report = new FileInfo(filename);
                    StreamWriter sw = report.CreateText();
                    sw.Write("DATE,LINE,STATION,ISSUE,RAISED,ACKNOWLEDGED,RESOLVED,DOWNTIME" + Environment.NewLine);

                    for (int i = 0; i < ReportTable.Rows.Count; i++)
                    {
                        String raisedTime = ReportTable.Rows[i]["RAISED"] == DBNull.Value ? ("")
                                            : ((TimeSpan)ReportTable.Rows[i]["RAISED"]).ToString();
                        String acknowledgedTime = ReportTable.Rows[i]["ACKNOWLEDGED"] == DBNull.Value ? ("")
                                            : ((TimeSpan)ReportTable.Rows[i]["ACKNOWLEDGED"]).ToString();
                        String resolvedTime = ReportTable.Rows[i]["RESOLVED"] == DBNull.Value ? ("")
                                            : ((TimeSpan)ReportTable.Rows[i]["RESOLVED"]).ToString();

                        String downTime = ReportTable.Rows[i]["DOWNTIME"] == DBNull.Value ? ("")
                                            : ((TimeSpan)ReportTable.Rows[i]["DOWNTIME"]).ToString();

                        String reportEntry = (String)ReportTable.Rows[i]["DATE"] + ","

                            + ((ReportTable.Rows[i]["LINE"]==DBNull.Value)?(""):(String)ReportTable.Rows[i]["LINE"]) + ","
                            + ((ReportTable.Rows[i]["STATION"] == DBNull.Value) ? ("") : (String)ReportTable.Rows[i]["STATION"]) + ","
                            + ((ReportTable.Rows[i]["ISSUE"] == DBNull.Value) ? ("") : (String)ReportTable.Rows[i]["ISSUE"]) + ","
                            + raisedTime + ","
                            + acknowledgedTime + ","
                            + resolvedTime + ","
                            + downTime;
                        sw.Write(reportEntry);
                        sw.Write(Environment.NewLine);
                    }
                    sw.Close();
                    MessageBox.Show("Report Generation Successful", "Report Generation Message",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Error Generating Report" + Environment.NewLine + exp.Message, "Report Generation Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        private void cmbViewTypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
