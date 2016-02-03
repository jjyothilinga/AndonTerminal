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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Contact c;
        DataAccess dataAccess;

        public Window1()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            tbLineID.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            c = dataAccess.getContact(tbLineID.Text, tbPassword.Password);

            if (c == null)
            {
                MessageBox.Show("Invalid User ID or password",
                    "Login Failure", MessageBoxButton.OK
                    , MessageBoxImage.Error);
                return;
            }

            baseGrid.Children.Clear();
            baseGrid.Children.Add(new Home(c));

        }
    }
}
