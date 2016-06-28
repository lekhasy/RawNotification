using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RawNotification.RawNotificationServer.Params;

namespace DemoServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        RawNotification.RawNotificationServer.RawNotificationServer ThisServer;

        private void Form1_Load(object sender, EventArgs e)
        {
            RawNotificationSenderParamForWindows10 Windows10Param =
            new RawNotificationSenderParamForWindows10(PackageSID.Text,SecretKey.Text);

            RawNotificationSenderParams SenderParam = new RawNotificationSenderParams(Windows10Param);

            RawNotificationClientCommunicatorParams ClientCommunicatorParam =
                new RawNotificationClientCommunicatorParams(
                    (int)ClientPort.Value,
                    CommunicatorUnhandledErrorOccurred,
                    CommunicatorDBInteractionErrorOccurred);


            RawNotificationServerParam NotificationSeverParam =
            new RawNotificationServerParam(
                SenderParam,
                ClientCommunicatorParam);

            ThisServer = new RawNotification.RawNotificationServer.RawNotificationServer(NotificationSeverParam, (int)ServerPort.Value, (int)ServerNotifyPort.Value);

        }
        public void CommunicatorUnhandledErrorOccurred(Exception ex)
        {
            Console.WriteLine("Internet Error");
        }

        public void CommunicatorDBInteractionErrorOccurred(Exception ex)
        {
            Console.WriteLine("DB interact error");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ThisServer.Start();
            MessageBox.Show("Server Start Successfull");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ThisServer.Stop();
            MessageBox.Show("Server Stop SuccessFull");
        }
    }
}
