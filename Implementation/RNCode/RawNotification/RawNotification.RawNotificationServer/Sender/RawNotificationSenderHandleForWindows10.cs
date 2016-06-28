using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer;

namespace RawNotification.RawNotificationServer.Sender
{
    internal partial class RawNotificationSender
    {
        void InitializeWindows10RawNotificationSender()
        {
                windows10sender = new Windows10.Windows10RawNotificationSender<long>(_Params.Windows10Params.PackageSID, _Params.Windows10Params.SecretKey,
                OnInternetOrFireWallErrorOccurred,
                Windows10_BadURIErrorOccurred,
                Windows10_NotAccecptableErrorOccurred,
                Windows10_UnknowErrorOccurred,
                Windows10_SendSuccessed,
                Windows10_WrongPackageSIDOrSecretKey);
        }

        

        void  Windows10_WrongPackageSIDOrSecretKey(object sender, Exception ex)
        {
            _LastestServerInfo.ChangeErrorInfo(ex,  SenderServerErrorType.W10_WrongPackageSIDOrSecretKeyError);
            RegisterTimer();            
        }

        /// <summary>
        /// Hàm được gọi khi URI cần gửi tới không tồn tại
        /// </summary>
        void Windows10_BadURIErrorOccurred(object sender, DelegatesAndEnums.BadURIEventArgs e)
        {
            lock(db)
            {
                Receiver rcv = e.Device.Receiver;
                if (rcv.Devices.Count == 1) // người nhận này còn sử dụng 1 thiết bị, mà thiết bị này đang chuẩn bị bị xóa thì xóa người dùng đó khỏi csdl, dẫn đến thiết bị của người đó cũng bị xóa theo.
                {
                    db.Receivers.DeleteOnSubmit(rcv);
                }
                else
                {
                    // nếu người dùng còn nhiều hơn 1 thiết bị thì chỉ xóa mình thiết bị này thôi
                    db.Devices.DeleteOnSubmit(e.Device);
                }
                System.Diagnostics.Debug.WriteLine("URI sai, xóa thiết bị");
            }
        }


        // Hàm được gọi khi có lỗi do server gửi thông báo quá nhiều cùng 1 lúc
        void Windows10_NotAccecptableErrorOccurred(object sender, Exception ex)
        {
            _LastestServerInfo.ChangeErrorInfo(ex, SenderServerErrorType.W10_NotAcceptableError);
            RegisterTimer();
        }

        
        // Hàm được gọi khi có một lỗi không xác định xảy ra
        void Windows10_UnknowErrorOccurred(object sender, Exception ex)
        {
            _LastestServerInfo.ChangeErrorInfo(ex, SenderServerErrorType.W10_UnknowError);
            RegisterTimer();
        }

        void Windows10_SendSuccessed(object sender, DeviceNotification e)
        {
            lock(db)
            {
                db.DeviceNotifications.DeleteOnSubmit(e);
            }
        }
    }
}
