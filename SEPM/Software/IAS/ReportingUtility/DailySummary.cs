using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ReportingUtility
{
    class DailySummary
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

        string line = String.Empty;
        public string Line
        {
            get { return line; }
            set
            {
                line = value;
                OnPropertyChanged("Line");
            }
        }

        private int producedQuantity;
        public String ProducedQuantity
        {
            get { return producedQuantity.ToString(); }
            set
            {
                producedQuantity = Convert.ToInt32(value);
                OnPropertyChanged("ProducedQuantity");
            }
        }

        string breakdown = String.Empty;
        public string Breakdown
        {
            get { return breakdown; }
            set
            {
                breakdown = value;
                OnPropertyChanged("Breakdown");
            }
        }


        string quality = String.Empty;
        public string Quality
        {
            get { return quality; }
            set
            {
                quality = value;
                OnPropertyChanged("Quality");
            }
        }

        string partShortage = String.Empty;
        public string PartShortage
        {
            get { return partShortage; }
            set
            {
                partShortage = value;
                OnPropertyChanged("PartShortage");
            }
        }

        public DailySummary()
        {
        }


    }

    class DailySummaryCollection : ObservableCollection<DailySummary>
    {
    }
}
