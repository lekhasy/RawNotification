using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RawNotification.ServerClient.ServerInterface;
using TestInterface.SharedModels;

namespace TestInterface
{
    public partial class Form1 : Form
    {
        ServerInterface<NotificationDataPacket> RawNotificationInterface = new ServerInterface<NotificationDataPacket>();
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            RawNotificationInterface.ServerInfoChanged += RawNotificationInterface_ServerInfoChanged;
            RawNotificationInterface.LossConnectionToServer += () =>
            {
                MessageBox.Show("Đã mất kết nối tới máy chủ");
            };

            RawNotificationInterface.LossConnectionToNotifyServer += () =>
            {
                MessageBox.Show("Đã mất kết nối tới máy chủ thông báo");
            };
        }

        private void RawNotificationInterface_ServerInfoChanged(RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer.ServerInfo obj)
        {
            string message = "Server Info Changed" + Environment.NewLine + obj.ErrorExit.ToString() + Environment.NewLine;
            if (obj.ErrorExit)
            {
                message += obj.LastestErrorReason.ToString() + Environment.NewLine + obj.LastestException.ToString() + Environment.NewLine + obj.LastestErrorOccurredTime.ToString();
            }
            MessageBox.Show(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer.AddNotificationFSPacketData result = null;
            while (true)
            {
                try
                {
                    result = await RawNotificationInterface.AddNotificationAsync(new NotificationDataPacket(txt_line1.Text, txt_Line2.Text), new List<IReceiver> { new TestReceiver("d") });
                    break;
                }
                catch // lỗi khi gửi
                {
                    if (MessageBox.Show("Lỗi xảy ra khi gửi yêu cầu tới server\nThử lại?", "Lỗi khi gửi yêu cầu", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            if (result == null) // lỗi khi nhận gói tin trả về
            {
                MessageBox.Show("Đã gửi yêu cầu thành công, nhưng gặp lỗi khi nhận gói tin trả về", "Lỗi khi nhận hồi đáp", MessageBoxButtons.OK);
                return;
            }
            else
            {
                if (result.IsSuccess)
                {
                    MessageBox.Show("Thêm thông báo thành công");
                }
                else
                {
                    MessageBox.Show("thêm thông báo thất bại" + Environment.NewLine + result.ErrorType + Environment.NewLine + result.InnerException);
                }
            }


        }

        public class Receiver : IReceiver
        {
            public Receiver(string originalID)
            {
                _OriginalID = originalID;
            }
            string _OriginalID;
            public string RNReceiverOldID
            {
                get
                {
                    return _OriginalID;
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (await RawNotificationInterface.SendAllNotificationAsync())
            { MessageBox.Show("Gửi yêu cầu thành công"); }
            else { MessageBox.Show("Gửi yêu cầu thất bại"); }
        }
        
        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                    await RawNotificationInterface.ConnectToRawNotificationServerAsync(txt_ServerIP.Text, (int)num_Serverport.Value);
            }
            catch
            {
                MessageBox.Show("Kết nối tới Server thất bại");
                return;
            }

            try
            {
                    await RawNotificationInterface.ConnectToNotifyNotificationServerInfoAsync(txt_ServerIP.Text, (int)num_NotifyPort.Value);
            }
            catch
            {
                MessageBox.Show("Kết nối tới Notify server thất bại");
                return;
            }
            MessageBox.Show("connect ok");
        }
    }
}
