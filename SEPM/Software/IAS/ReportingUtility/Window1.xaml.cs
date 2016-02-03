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
using Microsoft.Win32;

namespace ReportingUtility
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataAccess dataAccess;
        DailySummaryCollection dailySummaryCollection;

        public Window1()
        {
            try
            {
                dataAccess = new DataAccess();
                InitializeComponent();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }




        #region ReportsTab

        System.Data.DataTable ReportTable = null;


        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            ReportTable = null;
            dgReportGrid.DataContext = null;
            if (cmbViewTypeSelector.SelectedIndex == 0)
            {
                ReportTable = dataAccess.GetIssueReportData(dpFrom.SelectedDate.Value, dpTo.SelectedDate.Value);
            }
            else if (cmbViewTypeSelector.SelectedIndex == 1)
            {
                //dailySummaryCollection = dataAccess.GetDailySummary(dpFrom.SelectedDate.Value);
            }

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

                    if (cmbViewTypeSelector.SelectedIndex == 0)
                    {
                        sw.Write("DATE,LINE,STATION,ISSUE,DETAILS,RAISED,ACKNOWLEDGED,RESOLVED,DOWNTIME" + Environment.NewLine);

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

                            String line = ReportTable.Rows[i]["LINE"] == DBNull.Value ? ("")
                                                : ((string)ReportTable.Rows[i]["LINE"]);

                            String station = ReportTable.Rows[i]["STATION"] == DBNull.Value ? ("")
                                                : ((string)ReportTable.Rows[i]["STATION"]);

                            String issue = ReportTable.Rows[i]["ISSUE"] == DBNull.Value ? ("")
                                                : ((string)ReportTable.Rows[i]["ISSUE"]);

                            String details = ReportTable.Rows[i]["DETAILS"] == DBNull.Value ? ("")
                                                : ((string)ReportTable.Rows[i]["LINE"]);

                            String reportEntry = (String)ReportTable.Rows[i]["DATE"] + ","

                                                + line + ","
                                                + station + ","
                                                + issue + ","
                                                + details + ","
                                                + raisedTime + ","
                                                + acknowledgedTime + ","
                                                + resolvedTime + ","
                                                + downTime;
                            sw.Write(reportEntry);
                            sw.Write(Environment.NewLine);
                        }
                    }
                    else if (cmbViewTypeSelector.SelectedIndex == 1)
                    {
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
            if (cmbViewTypeSelector.SelectedIndex == -1)
                return;
            if (cmbViewTypeSelector.SelectedIndex == 0)
            {
                dpTo.Visibility = Visibility.Visible;
                lblTo.Visibility = Visibility.Visible;
                lblFrom.Content = "From";
                
                
            }
            else if (cmbViewTypeSelector.SelectedIndex == 1)
            {
                dpTo.Visibility = Visibility.Hidden;
                lblTo.Visibility = Visibility.Hidden;
                lblFrom.Content = "DATE";
            }
        }
    }
}
