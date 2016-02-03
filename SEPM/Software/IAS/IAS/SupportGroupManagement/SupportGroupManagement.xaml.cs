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

namespace IAS
{
    /// <summary>
    /// Interaction logic for SupportGroupManagement.xaml
    /// </summary>
    public partial class SupportGroupManagement : PageFunction<ContactCollection>
    {
        ContactCollection contacts = null;
        DataAccess dataAccess = null;

        Contact currentContact = null;

        public SupportGroupManagement(ContactCollection contacts)
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            this.contacts= contacts;

            contactControl.DataContext = contacts;

            ((Label)contactControl.aMDGroupBox.Header).Content = "CONTACTS";


                
        }

        private void contactControl_selectionChanged(object sender, AddModifyDeleteSelectionChangedEventArgs e)
        {
            
            contactDetailsControl.DataContext = null;

            if (contactControl.dgItem.SelectedIndex == -1)
            {
                contactDetailsControl.tbPassword.Clear();
                gbContactDetails.Visibility = Visibility.Hidden;
                return;
            }
            currentContact = contacts[contactControl.dgItem.SelectedIndex];
            
            contactDetailsControl.DataContext = currentContact;
            contactDetailsControl.tbPassword.Password = currentContact.Password;
            gbContactDetails.Visibility = Visibility.Visible;
         }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if( contactDetailsControl.tbContactName.Text.Length == 0)
            {
                 MessageBox.Show("Please Enter Contact Name ","Info",
                     MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }

            if( contactDetailsControl.tbContactNumber.Text.Length == 0 )
            {
                 MessageBox.Show("Please Enter Contact Number ","Info",
                     MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }
            


          //  dataAccess.updateContact(currentContact);

            if (!contacts.Contains(currentContact))
            {
                currentContact.ID = contactDetailsControl.tbID.Text;
                currentContact.Password = contactDetailsControl.tbPassword.Password;
                dataAccess.insertContact(currentContact);
                contacts.Add(currentContact);
                
            }
            else 
            {
                Contact cur = new Contact(currentContact.ID, currentContact.Password, 
                    currentContact.Name, currentContact.Number,currentContact.IsProcurement);
                cur.LineAssociation = currentContact.LineAssociation;
                cur.ShiftAssociation = currentContact.ShiftAssociation;
                cur.DepartmentAssociation = currentContact.DepartmentAssociation;
                cur.EscalationAssociation = currentContact.EscalationAssociation;
                dataAccess.updateContact(currentContact,cur);
                MessageBox.Show("Contact Saved ", "Info",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            contactDetailsControl.DataContext = null;
            contactControl.dgItem.SelectedIndex = -1;

        }

        private void contactControl_addClicked(object sender, EventArgs e)
        {
            contactDetailsControl.tbPassword.Clear();
            currentContact = new Contact();
            contactDetailsControl.DataContext = null;
            DataAccess.createAssociation(currentContact);
            contactDetailsControl.DataContext = currentContact;
            contactDetailsControl.tbContactName.Focus();
            
            gbContactDetails.Visibility = Visibility.Visible;
        }

        private void contactControl_deleteClicked(object sender, EventArgs e)
        {
            if (contactControl.dgItem.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Contact", "Info",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            dataAccess.deleteContact(currentContact);
            contacts.Remove(currentContact);
            contactDetailsControl.DataContext = null;
            contactControl.dgItem.SelectedIndex = -1;    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        

    }



    public class Contact : INotifyPropertyChanged
    {
        public String ID { get; set; }
        public String Password { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        bool isProcurement;
        public bool IsProcurement
        {
            get { return isProcurement; }
            set
            {
                isProcurement = value;
                OnPropertyChanged("IsProcurement");

            }
        }
        public LineAssociationCollection LineAssociation { get; set; }
        public ShiftAssociationCollection ShiftAssociation { get; set; }
        public DepartmentAssociationCollection DepartmentAssociation { get; set; }
        public EscalationAssociationCollection EscalationAssociation { get; set; }

        
        public Contact()
        {
            ID = String.Empty;
            Name = String.Empty;
            Number = String.Empty;

            IsProcurement = false;
        }

        public Contact(string  id, string name,string number)
        {
            ID = id;
            Name = name;
            Number = number;
            IsProcurement = false;
            
        }

        public Contact(string id,string password, string name, string number)
        {
            ID = id;
            Password = password;
            Name = name;
            Number = number;
            IsProcurement = false;

        }

        public Contact(string id, string password, string name, string number,bool isProcurement)
        {
            ID = id;
            Password = password;
            Name = name;
            Number = number;
            IsProcurement = isProcurement;

        }

        public bool IsAssociatedWith(int line,int shift,int department ,int escalation)
        {
            bool lineAsso = false;
            bool shiftAsso = false;
            bool departmentAsso = false;
            bool escalationAsso = false;
            foreach( LineAssociationInfo l in LineAssociation)
            {
                if( l.ID == line)
                {
                    lineAsso = l.IsAssociated;
                    break;
                }

            }

            foreach( ShiftAssociationInfo s in ShiftAssociation)
            {
                if( s.ID == shift)
                {
                    shiftAsso = s.IsAssociated;
                    break;
                }
            }

            foreach( DepartmentAssociationInfo d in DepartmentAssociation)
            {
                if( d.ID == department)
                {
                    departmentAsso= d.IsAssociated;
                    break;
                }
            }
            foreach( EscalationAssociationInfo e in EscalationAssociation)
            {
                if( e.ID == escalation)
                {
                    escalationAsso = e.IsAssociated;
                    break;
                }
            }

            return ( (lineAsso) && (shiftAsso) &&(departmentAsso) && (escalationAsso));
        }


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

    public class ContactCollection : ObservableCollection<Contact>
    {

        public List<Contact> getContactList(int line, List<int> shifts, int department, int escalation)
        {
            List<Contact> contactList = new List<Contact>();
            foreach (int id in shifts)
            {
                IEnumerator<Contact> ce = this.GetEnumerator();
                while (ce.MoveNext())
                {
                    if (ce.Current.IsAssociatedWith(line, id, department, escalation) == true)
                    {
                        if(!contactList.Contains(ce.Current))
                        {
                            contactList.Add(ce.Current);
                        }
                    }

                }
            }
            return contactList;
        }
    }

}
