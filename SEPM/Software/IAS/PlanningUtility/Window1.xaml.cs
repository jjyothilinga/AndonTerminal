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
using System.ComponentModel;
using System.Timers;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;
using ias.shared;


namespace PlanningUtility
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataAccess dataAccess = null;
        DataTable[] lineInfoTable = null;
        DataTable[] modelInfoTable = null;
        DataTable dt = null, linesInfoTable;

        String[] lines = null;
        string[] lineIds = null;
        string[] seperator = { "," };
        String lineSelection = String.Empty;
        System.Timers.Timer timer = null;

        
        
        double updateInterval = 0;

        DoubleAnimation marqueeAnimation = null;
        DoubleAnimation issueMarqueeAnimation = null;

        String dataSeperator = ";";

        public enum DeviceCommand
        {
            SET_PLANNED_QUANTITY = 1, SET_SCHEDULE = 2, SET_SHIFT_TIMINGS = 3,
            SET_BREAK_TIMINGS = 4, SET_RTC = 5, SET_QA_PERIOD = 6, SET_LINE_NAME = 7,
            SET_LINE_MODEL = 8, UPDATE_LINE_DATA = 9
        };


        public enum TransactionStatus
        {
            NONE = 0, OPEN = 1, INPROCESS = 2,
            COMPLETE = 3, TIMEOUT = 4
        };

        TimeSpan[,] shiftTimings = new TimeSpan[3, 2];


        
        String customerLogoPath = String.Empty;
        List<Reference> references;

        double issueMarqueeSpeed = 0.0;
        double messageMarqueeSpeed = 0.0;

        public Window1()
        {
            InitializeComponent();

            try
            {

                //customerLogo.Source = new BitmapImage(new Uri(customerLogoPath,UriKind.RelativeOrAbsolute));
                dataAccess = new DataAccess();
            }


            catch (SqlException s)
            {
                MessageBox.Show("Unable to Connect to DataBase ", "ERROR",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }


            linesInfoTable = dataAccess.getProductionLineInfo();

            cmbProductionLineSelector.DataContext = linesInfoTable;

        }



        private int findShift()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now >= shiftTimings[0, 0]) && (now < shiftTimings[0, 1]))
            {
                return 1;
            }
            else if ((now >= shiftTimings[1, 0]) && (now < shiftTimings[1, 1]))
            {
                return 2;
            }
            else return 3;
        }



        private void setupShiftTime(ref TimeSpan ts, String t)
        {
            if (t == String.Empty)
            {
                return;
            }
            else
            {
                try
                {
                    String[] timeparams = t.Split(':');
                    ts = new TimeSpan(int.Parse(timeparams[0]), int.Parse(timeparams[1]),
                                        int.Parse(timeparams[2]));
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            dataAccess.close();
        }


        private void btnSet_Click(object sender, RoutedEventArgs e)
        {

            int selectedIndex = cmbProductionLineSelector.SelectedIndex;

            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select a line", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int lineId = (int)linesInfoTable.Rows[selectedIndex]["id"];

            Reference reference = references[ReferenceSelector.SelectedIndex];
            
            
            foreach (shiftConfig s in shiftConfigTable.Items)
            {
                //if ((int.Parse(s.PlannedQuantity) <= 0)||(int.Parse(s.Manpower)<=0))
                //{
                //    continue;
                //}



                DateTime date = PlanDate.SelectedDate.Value;

                if (date < DateTime.Today)
                {
                    MessageBox.Show("Plan must be for a future date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
 
                try
                {

                    dataAccess.updateTarget(lineId, 
                        s.Shift,Convert.ToInt32( s.PlannedQuantity), reference.ID, Convert.ToInt32(s.PlannedManpower), 
                        Convert.ToInt32(s.MaximumManpower), date.ToString());
                }
                catch (SqlException sq)
                {
                    MessageBox.Show("Error Setting Target Quantity\n Contact Administrator  ", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            MessageBox.Show("Plan Updated  ", "Info",
            MessageBoxButton.OK, MessageBoxImage.Information);

        }



        private void cmbProductionLineSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            shiftConfigTable.Items.Clear();

            if (cmbProductionLineSelector.SelectedIndex == -1)
            {
                return;
            }

            
            int selectedLine = (int)linesInfoTable.Rows[cmbProductionLineSelector.SelectedIndex]["id"];

            ShiftCollection shifts = dataAccess.getShifts();

            references = dataAccess.getReferences(selectedLine);
            ReferenceSelector.DataContext = references;

            foreach (Shift s in shifts)
            {
                shiftConfigTable.Items.Add(new shiftConfig(s.ID,s.Name));
            }

        }


        public class shiftConfig : IEditableObject, INotifyPropertyChanged
        {
            public int ID{get;set;}

            String shift;
            public String Shift
            {
                get { return shift; }
                set
                {
                    shift = value;
                    OnPropertyChanged("Shift");
                }
            }
            int plannedQuantity;
            public String PlannedQuantity
            {
                get { return plannedQuantity.ToString(); }
                set
                {
                    try
                    {
                        plannedQuantity = int.Parse(value);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid Input for Planned Quantity : " + Shift, "Error",
                                            MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                    OnPropertyChanged("PlannedQuantity");
                }

            }

            int plannedmanpower;
            public String PlannedManpower
            {
                get { return plannedmanpower.ToString(); }
                set
                {
                    try
                    {
                        plannedmanpower = int.Parse(value);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid Input for Manpower : " + Shift, "Error",
                                            MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                    OnPropertyChanged("PlannedManpower");
                }

            }


            int maximumManpower;
            public String MaximumManpower
            {
                get { return maximumManpower.ToString(); }
                set
                {
                    try
                    {
                        maximumManpower = int.Parse(value);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid Input for Manpower : " + Shift, "Error",
                                            MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                    OnPropertyChanged("MaximumManpower");
                }

            }



            public shiftConfig(int id, string name)
            {
                this.ID = id;
                this.Shift = name;

                plannedQuantity = 0;
                plannedmanpower = 0;
                maximumManpower = 0;

            }


            #region IEditableObjectMembers
            void IEditableObject.BeginEdit()
            {
            }
            void IEditableObject.CancelEdit()
            {
            }
            void IEditableObject.EndEdit()
            {
            }
            #endregion

            #region INotifyPropetyChangedHandler
            public event PropertyChangedEventHandler PropertyChanged;
            // Create the OnPropertyChanged method to raise the event
            protected void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
            #endregion
        }




        





        



        




    }
}
