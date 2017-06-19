﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (IsRunAsAdmin == true)
            {
                AdminWindow w = new AdminWindow();
                w.Show();
                this.Close();
            }

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        internal bool IsRunAsAdmin
        {
            get
            {
                WindowsIdentity id = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        bool isClosed = false;
        //NotifyIcon nIcon = new NotifyIcon();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread myThread1 = new Thread(OutDist);
            Thread myThread4 = new Thread(OutDist2);
            Thread myThread5 = new Thread(OutDist3);
            Thread myThread2 = new Thread(OutTemp);
            Thread myThread3 = new Thread(OutHumi);
            //Thread myThread4 = new Thread(OutX); вывод Serial данных, не нужно пока что
            //Thread myThread5 = new Thread(StepGorb);
            Thread myThread6 = new Thread(OutContrastRoom);
            Thread myThread7 = new Thread(OutContrastDisplay);
            Thread myThread8 = new Thread(TIMEWORK);

            myThread1.Start();
            myThread2.Start();
            myThread3.Start();
            myThread4.Start();
            myThread5.Start();
            myThread6.Start();
            myThread7.Start();
            myThread8.Start();


            slider1.Minimum = 1;
            slider1.Maximum = 11;
            slider1.Value = 1;
            slider1.SmallChange = 1;
            slider1.LargeChange = 1;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(0);
        }

        void OUT(int i)
        {
            Random r = new Random();
            Random z = new Random();
            
            try
            {
                switch (i)
                {
                    case 1:
                        switch (r.Next(3))
                        {
                            case 0:
                                Distance_range.Text = "Нормально";
                                Distance_range.Foreground = Brushes.Green;
                                Distance_range.FontSize = 38;
                                break;
                            case 1:
                                Distance_range.Text = "Далеко";
                                Distance_range.Foreground = Brushes.Red;
                                Distance_range.FontSize = 40;
                                break;
                            case 2:
                                Distance_range.Text = "Близко";
                                Distance_range.Foreground = Brushes.Red;
                                Distance_range.FontSize = 40;
                                break;
                        }
                        break;
                    case 2:
                        switch (z.Next(2))
                        {
                            case 0:
                                Distance_landing.Text = "Ровно";
                                Distance_landing.Foreground = Brushes.Green;
                                break;
                            case 1:
                                Distance_landing.Text = "Криво";
                                Distance_landing.Foreground = Brushes.Red;                                break;
                        }
                        break;
                    case 3:
                        if (((Distance_landing.Text == "Ровно") && (Distance_range.Text == "Близко")) || ((Distance_landing.Text == "Ровно") && (Distance_range.Text == "Далеко")))
                        {
                            Distance_or_and.Text = "  но";
                        }
                        else if ((Distance_landing.Text == "Криво") && (Distance_range.Text == "Нормально"))
                        {
                            Distance_or_and.Text = "  но";
                        }
                        else
                        {
                            Distance_or_and.Text = " и";
                        }
                        break;
                    case 4:
                        Temp.Text = Convert.ToString(r.Next(100) + "°C");
                        break;
                    case 5:
                        Humi.Text = Convert.ToString(r.Next(100) + "%");
                        break;
                    case 6:
                        contrast_room.Text = Convert.ToString(r.Next(100) + "%");
                        break;
                    case 7:
                        contrast_display.Text = Convert.ToString(r.Next(100) + "%");
                        break;
                    case 8:
                        slider_age_text.Text = Convert.ToString(Math.Round(slider1.Value));
                        switch (Math.Round(slider1.Value))
                        {
                            case 1:
                                minutes_for_end.Text = "10";
                                break;
                            case 2:
                                minutes_for_end.Text = "10";
                                break;
                            case 3:
                                minutes_for_end.Text = "15";
                                break;
                            case 4:
                                minutes_for_end.Text = "20";
                                break;
                            case 5:
                                minutes_for_end.Text = "25";
                                break;
                            case 6:
                                minutes_for_end.Text = "30";
                                break;
                            case 7:
                                minutes_for_end.Text = "30";
                                break;
                            case 8:
                                minutes_for_end.Text = "35";
                                break;
                            case 9:
                                minutes_for_end.Text = "40";
                                break;
                            case 10:
                                minutes_for_end.Text = "45";
                                break;
                            case 11:
                                minutes_for_end.Text = "45";
                                break;
                        }
                        break;
                } 
            }
            catch
            {

            }
        }

        void OutDist()
        {
            while (isClosed == false)
            {
                try
                {
                    Thread.Sleep(1000);
                    Distance_range.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(1);
                    }));
                }
                catch
                {

                }
            }
        }
        void OutDist2()
        {
            while (isClosed == false)
            {
                try
                {
                    Thread.Sleep(1000);
                    Distance_landing.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(2);
                    }));
                }
                catch
                {

                }
            }
        }
        void OutDist3()
        {
            while (isClosed == false)
            {
                try
                {
                    Thread.Sleep(1000);
                    Distance_or_and.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(3);
                    }));
                }
                catch
                {

                }
            }
        }

        void OutTemp()
        {
            while (isClosed == false)
            {
                try
                {
                    Thread.Sleep(1000);
                    Temp.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(4);
                    }));
                }
                catch
                {

                }
            }
        }
        void OutHumi()
        {
            while (isClosed == false)
            {
                try
                {
                    Thread.Sleep(1000);
                    Humi.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(5);
                    }));
                }
                catch
                {

                }
            }
        }
        void OutContrastRoom()
        {
            while (isClosed == false)
            {
                try
                {
                    Thread.Sleep(1000);
                    contrast_room.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(6);
                    }));
                }
                catch
                {

                }
            }
        }
        void OutContrastDisplay()
        {
            while (isClosed == false) { 
                try
                {
                    Thread.Sleep(1000);
                    contrast_display.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(7);
                    }));
                }
                catch
                {

                }
        }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isClosed = true;
        }

        void TIMEWORK()
        {
            while (isClosed == false)
            {
                try
                {
                    Thread.Sleep(100);
                    minutes_for_end.Dispatcher.Invoke((Action)(() =>
                    {
                        OUT(8);
                    }));
                }
                catch
                {

                }
            }
        }

        private void CopyrightN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
