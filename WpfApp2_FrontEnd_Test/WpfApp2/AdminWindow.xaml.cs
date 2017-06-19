using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            border_exit.Background = Brushes.White;
            border_minim.Background = Brushes.White;

            dist_far.Text = Properties.Settings.Default.dist_far;
            dist_normal.Text = Properties.Settings.Default.dist_normal;
            dist_near.Text = Properties.Settings.Default.dist_near;
            COMPort.Text = Properties.Settings.Default.COMPort;
            ACheck = Properties.Settings.Default.Autorun;

        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();// Для перемещение ока
        }

        private void border_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            border_exit.Background = Brushes.Red;
        }

        private void border_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            border_exit.Background = Brushes.White;
        }

        private void border_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void border_minim_MouseEnter(object sender, MouseEventArgs e)
        {
            border_minim.Background = Brushes.Red;
        }

        private void border_minim_MouseLeave(object sender, MouseEventArgs e)
        {
            border_minim.Background = Brushes.White;
        }

        private void border_minim_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.dist_far = dist_far.Text;
            Properties.Settings.Default.dist_normal = dist_normal.Text;
            Properties.Settings.Default.dist_near = dist_near.Text;
            Properties.Settings.Default.COMPort = COMPort.Text;
            Properties.Settings.Default.Autorun = ACheck;
            Properties.Settings.Default.Save();
        }

        bool ACheck = false;

        private void Autorun_Checked(object sender, RoutedEventArgs e)
        {
            ACheck = true;

            var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            key.SetValue("HP", System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void Autorun_Unchecked(object sender, RoutedEventArgs e)
        {
            ACheck = false;

            var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            key.DeleteValue("HP");
        }
    }
}
