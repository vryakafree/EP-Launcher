using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace EP
{
    public partial class EPLauncher : Form
    {
        public EPLauncher()
        {
            InitializeComponent();
        }
        //gọi button
        private bool button1WasClicked = false;
        private bool button2WasClicked = false;
        private bool button3WasClicked = false;
        private bool button4WasClicked = false;

        public void ExecuteAsAdmin(string fileName)
        {
            //chạy form con
                Process p = new Process();
                p.StartInfo.FileName = fileName;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.Verb = "runas";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();

            //cho form con vào panel
            while (p.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(600);
                p.Refresh();
            }

            //thay đổi cách hiển thị form con
            IntPtr pFoundWindow = p.MainWindowHandle;
            int style = Win32API.GetWindowLong(pFoundWindow, Win32API.GWL_STYLE);
            //force a redraw
            Win32API.DrawMenuBar(p.MainWindowHandle);
            Win32API.SetWindowLong(pFoundWindow, Win32API.GWL_STYLE, (style & ~Win32API.WS_SYSMENU));

            //event click từng button, sau đó hiển thị ra màn hình
            if (button1WasClicked)
            {
                Win32API.SetParent(p.MainWindowHandle, this.fLP1.Handle);
                button1WasClicked = false;
            }
            else if (button2WasClicked)
            {
                Win32API.SetParent(p.MainWindowHandle, this.fLP2.Handle);
                button2WasClicked = false;
            }
            else if (button3WasClicked)
            {
                Win32API.SetParent(p.MainWindowHandle, this.fLP3.Handle);
                button3WasClicked = false;
            }
            else if (button4WasClicked)
            {
                Win32API.SetParent(p.MainWindowHandle, this.fLP4.Handle);
                button4WasClicked = false;
            }
            Win32API.MoveWindow(p.MainWindowHandle, 0, 0, this.Width, this.Height, true);
        }      
        //---------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            button1WasClicked = true;
            ExecuteAsAdmin("eParking.exe");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2WasClicked = true;
            ExecuteAsAdmin("brave.exe");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3WasClicked = true;
            ExecuteAsAdmin("vlc.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4WasClicked = true;
            ExecuteAsAdmin("notepad.exe");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            popup.Image = Properties.Resources.info;
            popup.TitleText = "Bấm F11 để thoát toàn màn hình.";
            popup.Popup();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
    }
}
