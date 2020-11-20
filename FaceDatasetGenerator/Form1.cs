using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceParser
{
    public partial class Form1 : Form
    {
        bool IsStart;
        int count;
        Uri uri;
        public Form1()
        {
            InitializeComponent();

            Directory.CreateDirectory("data");
            uri = new Uri("https://thispersondoesnotexist.com/image");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsStart = !IsStart;
            button1.Text = IsStart ? "Stop" : "Start";

            if (IsStart && !backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var client = new WebClient())
            {
                while (IsStart)
                {
                    client.DownloadFile(uri, $@"data\{DateTime.Now.Ticks}.jpg");

                    label1.BeginInvoke((MethodInvoker)(()=>label1.Text=$"Downloaded: {++count}"));

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
