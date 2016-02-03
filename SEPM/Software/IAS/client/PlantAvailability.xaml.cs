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

namespace ias.client
{
    /// <summary>
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : UserControl,IScreen
    {
        
        PlantAvailability plantAvailability;
        

        public Summary()
        {
            InitializeComponent();
     
            plantAvailability = new PlantAvailability();
            
 
        }
        

        public void update()
        {
            plantAvailability.refresh();
            tbDate.Content = DateTime.Now.ToString("dd-MM-yyyy") ;

            btnBreakdown.Height =
                (BreakdownGrid.RowDefinitions[0].ActualHeight) *
                (Convert.ToDouble(plantAvailability.BreakdownHours) / Convert.ToDouble(plantAvailability.RunHours));

            if (btnBreakdown.Height > 0)
                btnBreakdown.Visibility = Visibility.Visible;
            else btnBreakdown.Visibility = Visibility.Hidden;


            btnQuality.Height =
                (QualityGrid.RowDefinitions[0].ActualHeight) *
                (Convert.ToDouble(plantAvailability.QualitydownHours) / Convert.ToDouble(plantAvailability.RunHours));

            if (btnQuality.Height > 0)
                btnQuality.Visibility = Visibility.Visible;
            else btnQuality.Visibility = Visibility.Hidden;


            btnPS.Height =
                (PSGrid.RowDefinitions[0].ActualHeight) *
                (Convert.ToDouble(plantAvailability.PartshortagedownHours) / Convert.ToDouble(plantAvailability.RunHours));

            if (btnPS.Height > 0)
                btnPS.Visibility = Visibility.Visible;
            else btnPS.Visibility = Visibility.Hidden;

            tbBreakdown.Text = Convert.ToDouble(plantAvailability.BreakdownHours).ToString() + " Hrs";
            tbQuality.Text = Convert.ToDouble(plantAvailability.QualitydownHours).ToString() + " Hrs";
            tbPS.Text = Convert.ToDouble(plantAvailability.PartshortagedownHours).ToString() + " Hrs";

            this.DataContext = null;
            this.DataContext = plantAvailability;
        }




    }


    public class PlantAvailability 
    {
        public String RunHours { get; set; }
        public String Downtime { get; set; }
        public String Availability { get; set; }

        public String BreakdownHours { get; set; }
        public String QualitydownHours { get; set; }
        public String PartshortagedownHours { get; set; }




        public ObservableCollection<LineAvailability> Lines {get;set;}

        DataAccess dataAccess;

        public PlantAvailability()
        {
            RunHours = String.Empty;
            Downtime = String.Empty;
            Availability = String.Empty;
            Lines = new ObservableCollection<LineAvailability>();
            dataAccess = new DataAccess();
        }


        public void initialize(String date)
        {
        }

        public void refresh()
        {

            Lines = dataAccess.getAvailability();

            TimeSpan rH = new TimeSpan(0,0,0);
            TimeSpan dT = new TimeSpan(0, 0, 0);
            TimeSpan bd = new TimeSpan(0, 0, 0);
            TimeSpan qa = new TimeSpan(0, 0, 0);
            TimeSpan ps = new TimeSpan(0, 0, 0);
            foreach (LineAvailability l in Lines)
            {

                rH += l.RunHours;
                dT += l.getTotalDownTime();
                bd += l.breakdownDowntime;
                qa += l.qualityDowntime;
                ps += l.partShortageDowntime;
            }
            RunHours = (rH.Hours + rH.Minutes / 60.0).ToString("F2");
            Downtime = (dT.Hours + dT.Minutes / 60.0).ToString("F2");
            Availability = ((((rH - dT).Hours + (rH - dT).Minutes / 60.0)
                / (rH.Hours + rH.Minutes / 60.0))*100).ToString("F2");

            BreakdownHours = (bd.Hours + bd.Minutes / 60.0).ToString("F2");
            QualitydownHours = (qa.Hours + qa.Minutes / 60.0).ToString("F2");
            PartshortagedownHours = (ps.Hours + ps.Minutes / 60.0).ToString("F2");
            
        }

    }






    public class LineAvailability : INotifyPropertyChanged
    {
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

        int id;
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        double availability;
        public double Availability
        {
            get { return availability; }
            set
            {
                availability = Convert.ToDouble(value);
                OnPropertyChanged("Availability");
            }
        }


        TimeSpan runHours;
        public TimeSpan RunHours
        {
            get { return runHours; }
            set
            {
                runHours = value;
                OnPropertyChanged("RunHours");
            }
        }

        public TimeSpan breakdownDowntime;
        public TimeSpan qualityDowntime;
        public TimeSpan partShortageDowntime;
        
        

        

        double availablePercentage;
        public double AvailablePercentage
        {
            get { return availablePercentage; }
            set
            {
                availablePercentage = value;
                OnPropertyChanged("AvailablePercentage");
            }
        }

        

        double breakdownPercentage;
        public double BreakdownPercentage
        {
            get { return breakdownPercentage; }
            set
            {
                breakdownPercentage = value;
                OnPropertyChanged("BreakdownPercentage");
            }
        }


        double qualityPercentage;
        public double QualityPercentage
        {
            get { return qualityPercentage; }
            set
            {
                qualityPercentage = Convert.ToDouble(value);
                OnPropertyChanged("QualityPercentage");
            }
        }


        double partshortagePercentage;
        public double PartshortagePercentage
        {
            get { return partshortagePercentage; }
            set
            {
                partshortagePercentage = Convert.ToDouble(value);
                OnPropertyChanged("PartshortagePercentage");
            }
        }


        string name = String.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        


        public LineAvailability(int id, String name, double ava, double rp, double bp, double qp, double psp)
        {

            this.ID = id;
            this.Name = name;
            this.Availability = ava;
            this.AvailablePercentage = rp;
            

            this.BreakdownPercentage = bp;
            this.QualityPercentage = qp;
            this.PartshortagePercentage = psp;

  
        }

        public LineAvailability(int id, String name, DateTime? logon, DateTime? logoff)
        {
            this.ID = id;
            this.Name = name;
            if (logoff == null)
            {
                logoff = DateTime.Now;
            }



            TimeSpan ts = (logoff.Value - logon.Value);

            this.RunHours = ts;
            updateAvailability();


        }

        public void updateRunHours(DateTime logon, DateTime logoff)
        {



            TimeSpan ts = logoff - logon;

            this.RunHours += ts;

            updateAvailability();
        }


        public void updateDowntime(String raised, String resolved, int department)
        {
            if (resolved == String.Empty)
            {
                resolved = DateTime.Now.ToString();
            }

            DateTime lgn = DateTime.Parse(raised);
            DateTime lgf = DateTime.Parse(resolved);

            TimeSpan ts = lgf - lgn;

            switch (department)
            {
                case 1:
                    breakdownDowntime += ts;
                    break;
                case 2:
                    qualityDowntime += ts;
                    break;

                case 3:
                    partShortageDowntime += ts;
                    break;
                default:
                    break;
            }

            updateAvailability();
        }

        private void updateAvailability()
        {
            TimeSpan av = runHours - breakdownDowntime - qualityDowntime - partShortageDowntime;
            Availability = av.Hours + (av.Minutes / 60.0);

            AvailablePercentage = (Availability / (runHours.Hours + runHours.Minutes / 60.0)) * 100;
            BreakdownPercentage = ((breakdownDowntime.Hours + breakdownDowntime.Minutes / 60.0) /
                (runHours.Hours + runHours.Minutes / 60.0) ) * 100;
            QualityPercentage = ((qualityDowntime.Hours + qualityDowntime.Minutes / 60.0)/
                (runHours.Hours + runHours.Minutes / 60.0)) * 100;


            PartshortagePercentage = ((partShortageDowntime.Hours + partShortageDowntime.Minutes / 60.0)/
                (runHours.Hours + runHours.Minutes / 60.0)) * 100;
        }

        public TimeSpan getTotalDownTime()
        {
            return breakdownDowntime + qualityDowntime + partShortageDowntime;
        }
    }


    //[ValueConversion(typeof(double), typeof(double))]
    public class Percentage2Width : IMultiValueConverter
    {
        double width;
        public Percentage2Width()
        {
            width = 0;
        }
        #region IMultiValueConverter Members

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((double)(values[0]) > 0.0)
            {
                width = (double)values[0] * (double)values[1];
            }
            return width;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    
}
