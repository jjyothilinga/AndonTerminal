using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ReportingUtility
{
    class IssueInfo
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

        DateTime? date = null;
        public string Date
        {
            get { return date.Value.ToShortDateString(); }
            set
            {
                date =  DateTime.Parse (value);
                OnPropertyChanged("Line");
            }
        }

        private String line = String.Empty;
        public String Line
        {
            get { return line.ToString(); }
            set
            {
                line = value;
                OnPropertyChanged("Line");
            }
        }

        string station = String.Empty;
        public string Station
        {
            get { return station; }
            set
            {
                station = value;
                OnPropertyChanged("Station");
            }
        }


        string issue = String.Empty;
        public string Issue
        {
            get { return issue; }
            set
            {
                issue = value;
                OnPropertyChanged("Issue");
            }
        }

        string details = String.Empty;
        public string Details
        {
            get { return details; }
            set
            {
                details = value;
                OnPropertyChanged("Details");
            }
        }


        DateTime? raised = null;
        public string Raised
        {
            get { return raised.Value.ToShortDateString(); }
            set
            {
                raised = DateTime.Parse(value);
                OnPropertyChanged("Raised");
            }
        }


    }
}
