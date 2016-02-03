using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WareHouseUtility
{
    class OpenIssue
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

        private int slNo;
        public String RecordID
        {
            get { return slNo.ToString(); }
            set
            {
                slNo = Convert.ToInt32(value);
                OnPropertyChanged("RecordID");
            }
        }

        string partNo = String.Empty;
        public string PartNo
        {
            get { return partNo; }
            set
            {
                partNo = value;
                OnPropertyChanged("PartNo");
            }
        }


        string status = String.Empty;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }


        public OpenIssue(int slNo, String partNo,String status)
        {
            this.slNo = slNo;
            this.PartNo = partNo;
            this.Status = status;
        }
    }

    class OpenIssueCollection : ObservableCollection<OpenIssue>
    {
    }
}
