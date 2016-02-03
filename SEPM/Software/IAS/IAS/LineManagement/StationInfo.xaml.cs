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

namespace IAS
{
    /// <summary>
    /// Interaction logic for StationInfo.xaml
    /// </summary>
    public partial class StationInfo : PageFunction<stationInfo>
    {
        stationInfo _station = null;
        public StationInfo(stationInfo station)
        {
            InitializeComponent();
            if (station != null)
            {
                _station = station;
            }
            tbLineID.Focus();

        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_station == null)
                    _station = new stationInfo();
                _station.ID = Convert.ToInt32(tbLineID.Text);
                _station.Name = tbLineName.Text;
                OnReturn(new ReturnEventArgs<stationInfo>(_station));
            }
            catch (Exception s)
            {
                OnReturn(new ReturnEventArgs<stationInfo>(null));
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<stationInfo>(null));
        }
    }
}
