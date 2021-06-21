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
        private Boolean escapeEnabled = false;
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

            int tmpTenPerc = (interval * 30) / 100;
            int tmpinteval = new Random().Next(interval - tmpTenPerc, interval);//random 
            myTimer.Interval = tmpinteval * 1000;
            labelAct.Text = (tmpinteval).ToString();
            myTimer.Start();
            // mytimerLbl.Text = myTimer.Interval.ToString();
        }
        private void ReSetTimer()
        {
            myTimer.Stop();
            int tmpTenPerc = (interval * 30) / 100;
            int tmpinteval = new Random().Next(interval - tmpTenPerc, interval);//random 
            myTimer.Interval = tmpinteval * 1000;
            labelAct.Text = (tmpinteval).ToString();
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

            int flag = new Random().Next(0, 2);//random 
            if (rightClickEnabled && flag > 0)
            {
                DoMouseClick();
                if (escapeEnabled)
                {
                    SendKeys.Send("{ESC}");//closing right click menu
                }
            }

            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);
            _currentElapsedTime = timeSinceStartTime + _totalElapsedTime;
            label1.Text = _currentElapsedTime.ToString();

            //int tmpinteval = new Random().Next(interval - 4, interval + 4);//random 

            int tmpTenPerc = new Random().Next((interval / 2), interval);//50%
            int tmpFourtyPerc = new Random().Next((interval / 3), interval);//30%

            int tmpinteval = new Random().Next(interval - tmpTenPerc, interval + tmpFourtyPerc);//random 

            myTimer.Interval = tmpinteval * 1000;
            //labelAct.Text += ","+ (tmpinteval).ToString();
            int kindex = labelAct.Text.IndexOf(',');
            labelAct.Text = labelAct.Text.Split(',').Length > 4
                ? labelAct.Text.Substring(kindex + 1) +" , "+ (tmpinteval).ToString()
                : labelAct.Text + " , " + (tmpinteval).ToString();
            //labelAct.Text = (tmpinteval).ToString();
            //mytimerLbl.Text = myTimer.Interval.ToString();

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
            if (((RadioButton)sender).Checked)
            {
                RadioButton rb = (RadioButton)sender;
                interval = Convert.ToInt32(rb.Tag);
                lblInterval.Text = interval.ToString() + " Second(s)";
                //MessageBox.Show("interval::" + interval.ToString());
                ReSetTimer();
            }
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            rightClickEnabled = checkBox1.Checked;
        }

        private void chkEscape_CheckedChanged(object sender, EventArgs e)
        {
            escapeEnabled = chkEscape.Checked;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            myTimer.Stop();

        }
    }
}
