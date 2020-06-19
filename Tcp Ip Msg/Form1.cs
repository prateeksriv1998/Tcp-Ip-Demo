using SimpleTCP;
using System;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Tcp_Ip_Msg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13; //enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString.Trim();
                e.ReplyLine(string.Format($"You Said {e.MessageString}"));
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtStatus.Text += "Server Starting...";
            IPAddress iP = IPAddress.Parse(txtHost.Text.Trim());
            server.Start(iP, Convert.ToInt32(txtPort.Text));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted) server.Stop();
            if (btnStart.Enabled == false) btnStart.Enabled = true;
        }
    }
}
