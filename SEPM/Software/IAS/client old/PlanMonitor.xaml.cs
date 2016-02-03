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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Sql;
using ias.shared;
namespace ias.client
{
    /// <summary>
    /// Interaction logic for Plan.xaml
    /// </summary>
    public partial class PlanMonitor : UserControl, IScreen
    {
        ObservableCollection<plan> plantPlan;
        DataAccess dataAccess;
        

        ShiftCollection shifts;
        public PlanMonitor()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            
            shifts = dataAccess.getShifts();
        }



        #region IScreen Members

        public void update()
        {
            PlanGrid.DataContext = null;

            List<Shift> s = shifts.getShifts(DateTime.Now.TimeOfDay);

             plantPlan =  dataAccess.getPlan(s);


             PlanGrid.DataContext = plantPlan;
        }

        #endregion

    }




    public class plan
    {
        public String Line { get; set; }
        public int Plan { get; set; }
        public int Manpower { get; set; }

        public plan()
        {
            Line = String.Empty;
            Plan = 0;
            Manpower = 0;
        }

        public plan(string line, int plan, int manpower)
        {
            this.Line = line;
            this.Plan = plan;
            this.Manpower = manpower;
        }
    }
}
