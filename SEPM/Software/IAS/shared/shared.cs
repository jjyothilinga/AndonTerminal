using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.ComponentModel;

namespace ias.shared
{

        [ValueConversion(typeof(int), typeof(Brush))]
        public class statusToBackgroundConv : IValueConverter
        {
           
            Brush background = Brushes.White;
            bool backgroundFlag = false;
            public object Convert(object value, Type targetType, object obj, CultureInfo culInfo)
            {
                if (targetType != typeof(Brush)) return null;
                if ((int)value > 0)
                {
                    if (backgroundFlag == true)
                    {
                        background = Brushes.Red;
                        backgroundFlag = false;
                    }
                    else
                    {
                        background = Brushes.White;
                        backgroundFlag = true;
                    }


                }
                else
                    background = Brushes.LimeGreen ;

                return background;
            }


            public object ConvertBack(object value, Type targetType, object obj, CultureInfo culInfo)
            {
                throw new NotImplementedException();
            }
        }


        public class Shift
        {
            public int ID { get; set; }
            public string Name { get; set; }
            TimeSpan startTime;
            public string StartTime
            {
                get { return startTime.ToString(); }
                set
                {
                    if (value == String.Empty)
                    {

                    }
                    else
                    {
                        try
                        {
                            String[] timeparams = value.Split(':');
                            startTime = new TimeSpan(int.Parse(timeparams[0]), int.Parse(timeparams[1]),
                                                int.Parse(timeparams[2]));
                        }
                        catch (Exception e)
                        {
                            return;
                        }
                    }
                }
            }

            TimeSpan endTime;
            public string EndTime
            {
                get { return endTime.ToString(); }
                set
                {
                    if (value == String.Empty)
                    {

                    }
                    else
                    {
                        try
                        {
                            String[] timeparams = value.Split(':');
                            endTime = new TimeSpan(int.Parse(timeparams[0]), int.Parse(timeparams[1]),
                                                int.Parse(timeparams[2]));
                        }
                        catch (Exception e)
                        {
                            return;
                        }
                    }
                }
            }

            public SessionCollection Sessions;

            public Shift()
            {
            }

            public Shift(int id, string description, string startTime, string endTime)
            {
                ID = id;
                Name = description;
                StartTime = startTime;
                EndTime = endTime;
                Sessions = new SessionCollection();



            }


            public Shift(int id, string description, TimeSpan startTime, TimeSpan endTime)
            {
                ID = id;
                Name = description;
                this.startTime = startTime;
                this.endTime = endTime;
                Sessions = new SessionCollection();
  


            }

   

            public Session getSession(TimeSpan time)
            {
                foreach (Session s in Sessions)
                {
                    if (s.IsWithin(time) == true)
                        return s;
                }
                return null;
            }

            public bool IsWithin(TimeSpan ts)
            {
                TimeSpan start = startTime;
                TimeSpan end = endTime;


                if (end < startTime)
                {
                    if (ts <= startTime && ts < endTime)
                        return true;
                    return false;
                }

                if (ts >= startTime && ts < endTime)
                    return true;
                return false;
        
            }
        }

        public class ShiftCollection : ObservableCollection<Shift>
        {
            public List<Shift> getShifts(TimeSpan time)
            {
                List<Shift> shiftList = new List<Shift>();
                IEnumerator<Shift> enumerator = this.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.IsWithin(time))
                    {
                        shiftList.Add(enumerator.Current);
                    }

                }
                return shiftList;
            }

            

        }

        public class LineAssociationInfo : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public String Name { get; set; }
            bool isAssociated;
            public Boolean IsAssociated
            {
                get { return isAssociated; }
                set
                {
                    isAssociated = value;
                    OnPropertyChanged("IsAssociated");
                }
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
            public LineAssociationInfo()
            {
            }

            public LineAssociationInfo(int id, string name, Boolean asso)
            {
                ID = id;
                Name = name;
                IsAssociated = asso;
            }
        }

        public class LineAssociationCollection : ObservableCollection<LineAssociationInfo>
        {
            public LineAssociationCollection()
            {
            }

            public LineAssociationCollection(String tag)
            {
                Array a = tag.ToCharArray();

                foreach (char c in a)
                {

                    this.Add(new LineAssociationInfo(-1, String.Empty,
                        c == '0' ? false : true));
                }
            }
        }


           
        public class shiftInfo
        {
            public string Name { get; set; }

            public string StartTime { get; set; }
            public string EndTime { get; set; }


            public shiftInfo()
            {
            }
        }

        public class Session
        {
            public int Shift { get; set; }
            public int ID { get; set; }
            public string Name { get; set; }
            TimeSpan startTime;
            public string StartTime
            {
                get { return startTime.ToString(); }
                set
                {
                    if (value == String.Empty)
                    {

                    }
                    else
                    {
                        try
                        {
                            String[] timeparams = value.Split(':');
                            startTime = new TimeSpan(int.Parse(timeparams[0]), int.Parse(timeparams[1]),
                                                int.Parse(timeparams[2]));
                        }
                        catch (Exception e)
                        {
                            return;
                        }
                    }
                }
            }

            TimeSpan endTime;
            public string EndTime
            {
                get { return endTime.ToString(); }
                set
                {
                    if (value == String.Empty)
                    {

                    }
                    else
                    {
                        try
                        {
                            String[] timeparams = value.Split(':');
                            endTime = new TimeSpan(int.Parse(timeparams[0]), int.Parse(timeparams[1]),
                                                int.Parse(timeparams[2]));
                        }
                        catch (Exception e)
                        {
                            return;
                        }
                    }
                }
            }

            public Session()
            {
            }
            public Session(int shift, int id, string description, TimeSpan startTime, TimeSpan endTime)
            {
                Shift = shift;
                ID = id;
                Name = description;
                this.startTime = startTime;
                this.endTime = endTime;
            }


            public Session(int shift, int id, string description, String startTime, String endTime)
            {
                Shift = shift;
                ID = id;
                Name = description;
                StartTime = startTime;
                EndTime = endTime;
            }

            public bool IsWithin(TimeSpan ts)
            {
                TimeSpan start = startTime;
                TimeSpan end = endTime;


                if (end < startTime)
                {
                    if (ts <= startTime && ts < endTime)
                        return true;
                    return false;
                }

                if (ts >= startTime && ts < endTime)
                    return true;
                return false;
        
            }
        }

        public class SessionCollection : ObservableCollection<Session>
        {
            public Session getTargetSession(TimeSpan ts)
            {
                IEnumerator<Session> enumerator = this.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.IsWithin(ts))
                    {
                        return enumerator.Current;
                    }

                }
                return null;
            }

        }

        public class sessionInfo
        {
            public int ShiftIndex { get; set; }
            public int ID { get; set; }
            public string Name { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }

            public sessionInfo()
            {
            }
        }

        public class DepartmentAssociationInfo : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public String Name { get; set; }
            bool isAssociated;
            public Boolean IsAssociated
            {
                get { return isAssociated; }
                set
                {
                    isAssociated = value;
                    OnPropertyChanged("IsAssociated");
                }
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
            public DepartmentAssociationInfo()
            {
            }

            public DepartmentAssociationInfo(int id, string name, Boolean asso)
            {
                ID = id;
                Name = name;
                IsAssociated = asso;
            }
        }

        public class DepartmentAssociationCollection : ObservableCollection<DepartmentAssociationInfo>
        {
            public DepartmentAssociationCollection()
            {
            }

            public DepartmentAssociationCollection(String tag)
            {
                Array a = tag.ToCharArray();

                foreach (char c in a)
                {

                    this.Add(new DepartmentAssociationInfo(-1, String.Empty,
                        c == '0' ? false : true));
                }
            }
        }

        public class ShiftAssociationInfo : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public String Name { get; set; }
            bool isAssociated;
            public Boolean IsAssociated
            {
                get { return isAssociated; }
                set
                {
                    isAssociated = value;
                    OnPropertyChanged("IsAssociated");
                }
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
            public ShiftAssociationInfo()
            {
            }

            public ShiftAssociationInfo(int id, string name, Boolean asso)
            {
                ID = id;
                Name = name;
                IsAssociated = asso;
            }
        }

        public class ShiftAssociationCollection : ObservableCollection<ShiftAssociationInfo>
        {
            public ShiftAssociationCollection()
            {
            }

            public ShiftAssociationCollection(String tag)
            {
                Array a = tag.ToCharArray();

                foreach (char c in a)
                {

                    this.Add(new ShiftAssociationInfo(-1, String.Empty,
                        c == '0' ? false : true));
                }
            }
        }



        public class EscalationAssociationInfo : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public String Name { get; set; }
            bool isAssociated;
            public Boolean IsAssociated
            {
                get { return isAssociated; }
                set
                {
                    isAssociated = value;
                    OnPropertyChanged("IsAssociated");
                }
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
            public EscalationAssociationInfo()
            {
            }

            public EscalationAssociationInfo(int id, string name, Boolean asso)
            {
                ID = id;
                Name = name;
                IsAssociated = asso;
            }
        }

        public class EscalationAssociationCollection : ObservableCollection<EscalationAssociationInfo>
        {
            public EscalationAssociationCollection()
            {
            }

            public EscalationAssociationCollection(String tag)
            {
                Array a = tag.ToCharArray();

                foreach (char c in a)
                {

                    this.Add(new EscalationAssociationInfo(-1, String.Empty,
                        c == '0' ? false : true));
                }
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

            public Contact(string id, string name, string number)
            {
                ID = id;
                Name = name;
                Number = number;
                IsProcurement = false;

            }

            public Contact(string id, string password, string name, string number)
            {
                ID = id;
                Password = password;
                Name = name;
                Number = number;
                IsProcurement = false;

            }

            public Contact(string id, string password, string name, string number, bool isProcurement)
            {
                ID = id;
                Password = password;
                Name = name;
                Number = number;
                IsProcurement = isProcurement;

            }

            public bool IsAssociatedWith(int line, int shift, int department, int escalation)
            {
                bool lineAsso = false;
                bool shiftAsso = false;
                bool departmentAsso = false;
                bool escalationAsso = false;
                foreach (LineAssociationInfo l in LineAssociation)
                {
                    if (l.ID == line)
                    {
                        lineAsso = l.IsAssociated;
                        break;
                    }

                }

                foreach (ShiftAssociationInfo s in ShiftAssociation)
                {
                    if (s.ID == shift)
                    {
                        shiftAsso = s.IsAssociated;
                        break;
                    }
                }

                foreach (DepartmentAssociationInfo d in DepartmentAssociation)
                {
                    if (d.ID == department)
                    {
                        departmentAsso = d.IsAssociated;
                        break;
                    }
                }
                foreach (EscalationAssociationInfo e in EscalationAssociation)
                {
                    if (e.ID == escalation)
                    {
                        escalationAsso = e.IsAssociated;
                        break;
                    }
                }

                return ((lineAsso) && (shiftAsso) && (departmentAsso) && (escalationAsso));
            }


            public bool IsAssociatedWithLine(int line)
            {
                return LineAssociation[line].IsAssociated;
            }

            public String getLineAssociation()
            {
                String association = String.Empty;
                foreach (LineAssociationInfo l in LineAssociation)
                {
                    if (l.IsAssociated == true)
                    {
                        association += l.ID.ToString() + ",";
                    }

                }
                int li = association.LastIndexOf(",");
                association = association.Remove(li);


                return association;
            }


            public String getDepartmentAssociation()
            {
                String association = String.Empty;
                foreach (DepartmentAssociationInfo d in DepartmentAssociation)
                {
                    if (d.IsAssociated == true)
                    {
                        association += d.ID.ToString() + ",";
                    }

                }
                int li = association.LastIndexOf(",");
                association = association.Remove(li);


                return association;
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
                            if (!contactList.Contains(ce.Current))
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
