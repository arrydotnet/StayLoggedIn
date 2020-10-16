using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Timer myTimer = new Timer();
        int interval = 30;
        int positionX = -100;
        int positionY = 100;

        private DateTime _startTime;
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;
        private TimeSpan _totalElapsedTime = TimeSpan.Zero;
        private Boolean rightClickEnabled = false;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rightClickEnabled = checkBox1.Checked;
            lblInterval.Text = interval.ToString() + " Second(s)";
            _startTime = DateTime.Now;
            myTimer.Tick += new EventHandler(MoveCursor);
            int tmpinteval = new Random().Next(interval - 3, interval);//random 
            myTimer.Interval = tmpinteval * 1000;
            myTimer.Start();

        }
        private void MoveCursor(Object myObject, EventArgs myEventArgs)
        {
            _totalElapsedTime = _currentElapsedTime;
            // Set the Current cursor, move the cursor's Position,
            // and set its clipping rectangle to the form. 
            int num1 = new Random().Next(positionX, positionY);
            int num2 = new Random().Next(positionX, positionY);
            Cursor.Position = new Point(Cursor.Position.X - num1, Cursor.Position.Y - num2);
            Cursor.Clip = new Rectangle(new Point(), new Size());

            if (rightClickEnabled)
            {
                DoMouseClick();
            }

            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);
            _currentElapsedTime = timeSinceStartTime + _totalElapsedTime;
            label1.Text = _currentElapsedTime.ToString();

            int tmpinteval = new Random().Next(interval - 4, interval + 4);//random 
            myTimer.Interval = tmpinteval * 1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myTimer.Stop();
            Application.Exit();
        }

        public void DoMouseClick()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            positionX = -100;
            positionY = 200;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            positionX = -200;
            positionY = 400;
        }

        private void radioButtonsGroup_CheckedChanged(object sender, EventArgs e)
        {
            interval = Convert.ToInt32(((System.Windows.Forms.Control)sender).Tag);
            lblInterval.Text = interval.ToString() + " Second(s)";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            rightClickEnabled = checkBox1.Checked;
        }
    }
}
