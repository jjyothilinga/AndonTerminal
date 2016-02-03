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





}
