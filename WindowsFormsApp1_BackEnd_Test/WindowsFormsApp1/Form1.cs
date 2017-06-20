using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isClosed = false;
        

        
        //открытия окон принажатии в меню пунктов, не используется
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 pr = new Form3();
            pr.Show();
        }

        private void настройкиДистанцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 pr = new Form4();
            pr.Show();
        }
        Form5 pr5 = new Form5();
        public void расположениеУстройстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pr5.Show();
        }

        //когда форма загружатеся
        private void Form1_Load(object sender, EventArgs e)
        {
            //обновление какие COM устройства есть
            foreach (string str in SerialPort.GetPortNames())
            {
                MessageBox.Show(str);
                comboBox1.Items.Add(str);
            }

            

            //запуск потоков
            Thread myThread1 = new Thread(OutDist);
            Thread myThread2 = new Thread(OutTemp);
            Thread myThread3 = new Thread(OutHumi);
            Thread myThread4 = new Thread(OutX);
            Thread myThread5 = new Thread(StepGorb);

            myThread1.Start();
            myThread2.Start();
            myThread3.Start();
            myThread4.Start();
            myThread5.Start();
        }

        bool triggerAutoStart=false; //включена ли автозагрузка, переключатель

        //автозагрузка
        private void включитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (triggerAutoStart == false)
            {
                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
                key.SetValue("DotNull", Application.ExecutablePath);
                включитьToolStripMenuItem.Text = "Отключить";
                triggerAutoStart = true;
            }
            else
            {
                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
                key.DeleteValue("HealthBack");
                включитьToolStripMenuItem.Text = "Включить";
                triggerAutoStart = false;
            }
        }

        //некоторые настройки COM 
        string PortName;
        int BaudRate = 19200;

        //инициализация подключения
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PortName = comboBox1.SelectedItem.ToString();
                serialPort1.PortName = PortName;
                serialPort1.BaudRate = BaudRate;
                serialPort1.ReadTimeout = 1000;
                serialPort1.Open();
                //if (serialPort1.IsOpen)
                //{
                //    MessageBox.Show("Открыт!");
                //}
            }
            catch
            {

            }
        }

        //просчет степени горбатости
        int r = 0, s = 0;
        int dist1 = 0;
        int dist2 = 0;
        void StepGorb()
        {
            while (isClosed == false)
            {
                try
                {
                    //отделение данных из COM, проверка ID и затем использование данных для обработки
                    string rsp = label3.Text;
                    String[] outr = rsp.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Convert.ToInt32(outr[0]) == 1)
                    {
                        dist1 = Convert.ToInt32(outr[1]);
                        r++;
                    }
                    if (Convert.ToInt32(outr[0]) == 4)
                    {
                        dist2 = Convert.ToInt32(outr[1]);
                        s++;
                    }
                    if ((dist1 != 0) && (dist2 != 0))
                    {
                        if (dist1 < dist2)
                        {
                            if (label1.InvokeRequired)
                                label1.Invoke(new Action(() => outp(5, 0)));
                        }
                        else
                        {
                            if (label1.InvokeRequired)
                                label1.Invoke(new Action(() => outp(5, 1)));
                        }
                    }
                }
                catch
                {

                }
            }
        }
        //дистанция
        void OutDist()
        {
            while (isClosed == false)
            {
                //MessageBox.Show("work");
                try
                {
                    //Thread.Sleep(1000);
                    string rsp = label3.Text;
                    if (rsp.Contains("1_")) //иная вариация поиска индекса
                    {
                        //отделение данных из COM, проверка ID и затем использование данных для обработки
                        String[] outr = rsp.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                        if (Convert.ToInt32(outr[0]) == 1)
                        {
                            int dist = Convert.ToInt32(outr[1]);
                            //MessageBox.Show(Convert.ToString(dist));
                            //проверка условий
                            if (dist >= 1 && dist < 50)
                            {
                                if (label4.InvokeRequired)
                                    label4.Invoke(new Action(() => outp(4, 1)));
                            }
                            if (dist >= 50 && dist < 75)
                            {
                                if (label4.InvokeRequired)
                                    label4.Invoke(new Action(() => outp(4, 2)));
                            }
                            if (dist >= 75)
                            {
                                if (label4.InvokeRequired)
                                    label4.Invoke(new Action(() => outp(4, 3)));
                            }
                        }
                    }
                }
                catch
                {

                }

            }
        }
        //температура
        void OutTemp()
        {
            while (isClosed == false)
            {
                //Thread.Sleep(1000); временные данные
                //MessageBox.Show("work2");
                try
                {
                    //отделение данных из COM, проверка ID и затем использование данных для обработки
                    string rsp2 = label3.Text;
                    String[] outr = rsp2.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if(Convert.ToInt32(outr[0]) == 2)
                    {
                        int tmp = Convert.ToInt32(outr[1]);
                        if (label6.InvokeRequired)
                            label6.Invoke(new Action(() => outp(2, tmp)));
                    }
                }
                catch
                {

                }
            }
        }
        //влажность
        void OutHumi()
        {
            while (isClosed == false)
            {
                //MessageBox.Show("3");
                try
                {
                    Thread.Sleep(1000);
                    string rsp3 = label3.Text;
                    String[] outr = rsp3.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Convert.ToInt32(outr[0]) == 3)
                    {
                        int hmt = Convert.ToInt32(outr[1]);
                        if (label8.InvokeRequired)
                            label8.Invoke(new Action(() => outp(3, hmt)));
                    }
                }
                catch
                {

                }
            }
        }
        //вывод
        void outp(int i, int numberto_out)
        {
            try
            {
                if (i == 1)
                {
                    //(x)
                    label3.Text = serialPort1.ReadLine();
                }
                if (i == 2)
                {
                    //temp
                    label6.Text = numberto_out.ToString();
                }
                if (i == 3)
                {
                    label8.Text = numberto_out.ToString();
                }
                if ((i == 4) && (numberto_out == 1))
                {
                    label4.Text = "Близко";
                    //вывод уведомлений в случае свернутого состояния
                    notifyIcon1.ShowBalloonTip(5000, "Здоровая осанка", "Вы сидите слишком близко", ToolTipIcon.Error);
                }
                if ((i== 4) && (numberto_out == 2))
                {
                    label4.Text = "Нормально";
                }
                if ((i== 4) && (numberto_out == 3))
                {
                    label4.Text = "Далеко";
                    notifyIcon1.ShowBalloonTip(5000, "Здоровая осанка", "Вы сидите слишком далеко", ToolTipIcon.Error);
                }
                if ((i==5 )&& (numberto_out == 0)){
                    label1.Text = "Вы сидите криво";
                    notifyIcon1.ShowBalloonTip(5000, "Здоровая осанка", "Вы сидите криво", ToolTipIcon.Error);
                }
                if ((i==5) && (numberto_out == 1))
                {
                    label1.Text = "Вы сидите нормально";
                }
            }
            catch
            {

            }
        }

        //просто вывод значений COM поступающих данных
        void OutX()
        {
            while (isClosed == false)
            {
                //MessageBox.Show("work4");
                try
                {
                    Thread.Sleep(1000);
                    if(label3.InvokeRequired)
                    label3.Invoke(new Action(()=> outp(1, 0) ));

                }
                catch
                {

                }
            }
        }

        //не используется
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                
            }
            catch
            {

            }
        }
        //меняем потоки
        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(0);
        }

        //проверка, ушла ли наша форма в минимальное состояние
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }

        //раскрываем форму из иконки-состояния
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        //в случае если форма закрывается, закрываются и потоки
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isClosed = true;
            }
            catch
            {

            }
        }
    }
}
