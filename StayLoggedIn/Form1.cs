using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        Cursor cur = new Cursor(Cursor.Current.Handle);
        Point p = new Point();
        Size s = new Size();
        int k = 0;
        int inteval = 5;
        int positionX = -100;
        int positionY = 100;

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
            myTimer.Tick += new EventHandler(MoveCursor);
            myTimer.Interval = inteval*1000;
            myTimer.Start();
        }
        private void MoveCursor(Object myObject, EventArgs myEventArgs)
        {
            // Set the Current cursor, move the cursor's Position,
            // and set its clipping rectangle to the form. 
            int num1 = new Random().Next(positionX, positionY);
            int num2 = new Random().Next(positionX, positionY);
            cur = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - num1, Cursor.Position.Y - num2);
            Cursor.Clip = new Rectangle(p, s);
            DoMouseClick();
            k += inteval;
            label1.Text =  k % 60 == 0 ? (k / 60).ToString()+" Minute(s)" : k+ " Seconds" ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myTimer.Stop();
            Application.Exit();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            inteval = 5;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            inteval = 10;
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
            positionY = 100;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            positionX = -200;
            positionY = 200;
        }
    }
}
