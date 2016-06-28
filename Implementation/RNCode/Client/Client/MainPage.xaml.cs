using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Net;
using System.Net.Sockets;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RawNotification.MobileClient.LiblaryForClient.RawNotificationClient ClientHelper = new RawNotification.MobileClient.LiblaryForClient.RawNotificationClient("115.77.25.17", 22112);
        UWPTCPClient.UWPTCPClient Client;
        RawNotification.SharedLiblary.MyBitConverter<ClientNetworkPacketSharedModels.FromClient.FromClientPacket> _FromClientConverter = new RawNotification.SharedLiblary.MyBitConverter<ClientNetworkPacketSharedModels.FromClient.FromClientPacket>();

        RawNotification.SharedLiblary.MyBitConverter<ClientNetworkPacketSharedModels.FromServer.FromServerPacket> _FromLoginServerConverter = new RawNotification.SharedLiblary.MyBitConverter<ClientNetworkPacketSharedModels.FromServer.FromServerPacket>();

        public MainPage()
        {
            this.InitializeComponent();
            Client = new UWPTCPClient.UWPTCPClient(txt_ServerName.Text, txt_Logig_PortName.Text);
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if (!await Client.ConnectAsync())
            {
                await new MessageDialog("Kết nối tới login server thất bại").ShowAsync();
                //return;
            }
            var result = await Client.SendAndRecieveAsync(
                _FromClientConverter.ObjectToBytes(
                    new ClientNetworkPacketSharedModels.FromClient.FromClientPacket
                    (
                        ClientNetworkPacketSharedModels.FromClient.FromClientPacketType.Login,
                        new ClientNetworkPacketSharedModels.FromClient.FCLoginPacketData(txt_UserName.Text, txt_Pasword.Text)
                        )));
            if (result.IsSuccess)
            {
                await new MessageDialog("Đăng nhập thành công").ShowAsync();
                var FromserverPacket = _FromLoginServerConverter.BytesToObject(result.Data);
                var PacketData = FromserverPacket.Data as ClientNetworkPacketSharedModels.FromServer.FSLoginPacketData;
                ClientHelper.RegisterUserNameWithRNS(PacketData.OldID);
                System.Diagnostics.Debug.WriteLine("register done");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Đăng nhập thất bại");
            }

            PushNotificationChannel chanel = await Notification_Helper_Client.Normal_Task.getChanel();
            System.Diagnostics.Debug.WriteLine(chanel.Uri);
            System.Diagnostics.Debug.WriteLine(Notification_Helper_Client.Device.ID);
            if (chanel.Uri == null) return;
            chanel.PushNotificationReceived += Chanel_PushNotificationReceived;
            txt_URI.Text = chanel.Uri;
        }

        private void Chanel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Raw received");
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await ClientHelper.RegisterBackgroundTaskAsync();
            await new MessageDialog(result.ToString()).ShowAsync();
            ClientHelper.ChangeServerName(txt_ServerName.Text);
            ClientHelper.ChangeServerPort(int.Parse(txt_RNS_PortName.Text));
            return;
        }
    }
}