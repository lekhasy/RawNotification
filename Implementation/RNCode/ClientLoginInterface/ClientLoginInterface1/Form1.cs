using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientNetworkPacketSharedModels.FromServer;
using ClientNetworkPacketSharedModels.FromClient;

namespace ClientLoginInterface
{

    public partial class Form1 : Form
    {
        TCPServer.TCPServer thisServer;
        RawNotification.SharedLiblary.MyBitConverter<FromClientPacket> _FromClientConverter = new RawNotification.SharedLiblary.MyBitConverter<ClientNetworkPacketSharedModels.FromClient.FromClientPacket>();
        RawNotification.SharedLiblary.MyBitConverter<FromServerPacket> _FromServerConverter = new RawNotification.SharedLiblary.MyBitConverter<ClientNetworkPacketSharedModels.FromServer.FromServerPacket>();
        public Form1()
        {
            InitializeComponent();
            thisServer = new TCPServer.TCPServer((int)num_ClientPort.Value);
            thisServer.OnPacketReceived += ThisServer_OnPacketReceived;
        }

        private void ThisServer_OnPacketReceived(object sender, TCPServer.TCPSeverEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Đã nhận được yêu cầu");
            FromClientPacket ReceivedPacket = null;
            try
            {
                ReceivedPacket = _FromClientConverter.BytesToObject(e.Data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Gói tin bị lỗi khi vận chuyển");
                e.RespondToClient(_FromServerConverter.ObjectToBytes(new FromServerPacket(FromServerPacketType.DataCorrupted, ex)));
                return;
            }
            switch (ReceivedPacket.Type)
            {
                case FromClientPacketType.Login:
                    {
                        e.RespondToClient(_FromServerConverter.ObjectToBytes(new FromServerPacket(FromServerPacketType.LoginResult, new FSLoginPacketData(LoginResult.Success, "d"))));
                        e.CloseConnection();
                    }
                    break;
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                thisServer.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Không thể khởi động server" + Environment.NewLine + ex.ToString());
                return;
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            thisServer.Stop();
        }
    }
}
