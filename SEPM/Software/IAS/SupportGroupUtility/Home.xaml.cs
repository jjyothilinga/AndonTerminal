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
using ias.shared;

namespace SupportGroupUtility
{
    /// <summary>
    /// Interaction logic for Hometab.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        Contact c;

        public Home( Contact c)
        {
            InitializeComponent();
            this.c = c;

            monitorTab.Content = new Monitor(c);
            reportsTab.Content = new Reports(c);

            if (c.IsAssociatedWithLine(9))
            {
                procurementTab.Content = new Procurement(c);
                procurementTab.Visibility = Visibility.Visible;
                
            }

            
        }
    }
}
