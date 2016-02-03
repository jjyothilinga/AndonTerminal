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
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Timers;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;
using ias.shared;
using System.Threading;


namespace ias.client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        System.Timers.Timer appTimer = null;
        System.Timers.Timer startTimer = null;
        double issueMarqueeSpeed = 0.0;
        double messageMarqueeSpeed = 0.0;

        DoubleAnimation marqueeAnimation = null;
        DoubleAnimation issueMarqueeAnimation = null;

        DataAccess dataAccess;

        bool marqueeLoaded = false;

        Summary summary;

        public Window1()
        {
            try
            {
                dataAccess = new DataAccess();
                InitializeComponent();
                messageMarqueeSpeed = Convert.ToDouble(ConfigurationSettings.AppSettings["MESSAGE_MARQUEE_SPEED"]);


                marqueeAnimation = new DoubleAnimation();
                marqueeAnimation.Completed += new EventHandler(marqueeAnimation_Completed);


                tbMain.Visibility = Visibility.Hidden;
                tbMain.Items.Add(new Safety());
                tbMain.Items.Add(new Monitor());

                summary = new Summary();
                tbMain.Items.Add(summary);

                appTimer = new System.Timers.Timer(10 * 1000);
                appTimer.AutoReset = false;
                appTimer.Elapsed += new ElapsedEventHandler(appTimer_Elapsed);

                startTimer = new System.Timers.Timer(100);
                startTimer.AutoReset = false;
                startTimer.Elapsed += new ElapsedEventHandler(startTimer_Elapsed);
                startTimer.Start();
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.ToString());
            }

            

            
        }

        void startTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            startTimer.Stop();
            startTimer.Elapsed -= startTimer_Elapsed;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                           new Action(() =>
                                           {
                                               tbMain.SelectedIndex = 0;
                                               tbMain.Visibility = Visibility.Visible;
                                               appTimer.Start();
                                               tbMarquee_Loaded();
                                               foreach (Object i in tbMain.Items)
                                               {
                                                   ((IScreen)i).update();
                                               }
                                               
                                               

                                           }));


        }


        void marqueeAnimation_Completed(object sender, EventArgs e)
        {
            tbMarquee_Loaded();
        }



        private void tbMarquee_Loaded()
        {

            tbMarquee.Text = dataAccess.getMarquee();

            tbMarquee.UpdateLayout();
            cMarquee.Width = this.Width;

            double height = cMarquee.ActualHeight - tbMarquee.ActualHeight;
            tbMarquee.Margin = new Thickness(0, height / 2, 0, 0);
            marqueeAnimation.From = -tbMarquee.ActualWidth;
            marqueeAnimation.To = cMarquee.ActualWidth;

            double duration = (marqueeAnimation.To.Value - marqueeAnimation.From.Value) / 20;

            marqueeAnimation.Duration = new Duration(TimeSpan.FromSeconds(messageMarqueeSpeed));
            tbMarquee.BeginAnimation(Canvas.RightProperty, marqueeAnimation);




        }





      



        void appTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            appTimer.Stop();
           
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                               new Action(() =>
                               {
                                   ((IScreen)tbMain.Items[tbMain.SelectedIndex]).update();
                                   if (tbMain.SelectedIndex >= (tbMain.Items.Count - 1))
                                       tbMain.SelectedIndex = 0;
                                   else ++tbMain.SelectedIndex;

                                   

                               }));
            appTimer.Start();
        }

       

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            appTimer.Stop();
            appTimer.Elapsed -= appTimer_Elapsed;
 
        }
        




    }
}
