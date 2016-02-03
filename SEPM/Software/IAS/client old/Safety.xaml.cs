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
using System.Timers;

namespace ias.client
{
    /// <summary>
    /// Interaction logic for Safety.xaml
    /// </summary>
    public partial class Safety : UserControl,IScreen
    {
        int days = 0;
        DataAccess dataAccess;
        Timer appTimer;
        int timerElapsedCount = -1;
        
        public Safety()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
           




        }
        public void update()
        {
            days = dataAccess.getDays();
            tbDays.Text = days.ToString();
        }


    }
}
