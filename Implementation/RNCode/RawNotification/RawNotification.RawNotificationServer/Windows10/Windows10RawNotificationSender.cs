using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RawNotification.SharedLiblary;

namespace RawNotification.RawNotificationServer.Windows10
{
    /// <summary>
    /// Lớp Windows10RawNotificationSender có nhiệm vụ gửi thông báo raw tới một ứng dụng qua một URI nhận được từ windows 10
    /// đối tượng dữ liệu cần gửi đi có kích thước tối đa là 5kb.
    /// tuy nhiên điều đó không được kiểm tra.
    /// lập trình viên dùng đối tượng này phải tự đảm bảo điều đó, nếu không, lỗi sẽ xảy ra
    /// và hàm UnknowError trong param sẽ được gọi để lập trình viên bắt lỗi đó
    /// </summary>
    internal partial class Windows10RawNotificationSender<T>
    {
        #region Events And Attriutes

        Windows10TokenAccess token;

        MyBitConverter<T> Tconverter;

        internal string PackageSID { get; private set; }
        internal string SecretKey { get; private set; }

        /// <summary>
        /// Xảy ra khi có lỗi đường truyền
        /// </summary>
        internal EventHandler<Exception> InternetOrFirewallErrorOccurred { get; private set; }

        /// <summary>
        /// Xảy ra khi PackageSID hoặc SecretKey sai
        /// </summary>
        internal EventHandler<Exception> WrongPackageSIDOrSecretKey { get; private set; }

        /// <summary>
        /// Xảy ra khi TokenID không chính xác
        /// </summary>
        internal EventHandler<Exception> UnAuthorizedErrorOccurred { get; private set; }
        /// <summary>
        /// Xảy ra khi URI được cung cấp không đúng, nên xóa thiết bị đó khỏi cơ sở dữ liệu
        /// </summary>
        internal EventHandler<DelegatesAndEnums.BadURIEventArgs> BadURIErrorOccurred { get; private set; }
        /// <summary>
        /// Xảy ra khi gửi quá nhiều thông báo cho 1 URI vào 1 thời điểm
        /// </summary>
        internal EventHandler<Exception> NotAccecptableErrorOccurred { get; private set; }
        /// <summary>
        /// Xảy ra khi có một lỗi không xác định xảy ra. 
        /// Lỗi này không phải do đường truyền.
        /// Có khả năng do phía Microsoft hoặc do gói tin gửi đi quá lớn (>5KB).
        /// </summary>
        internal EventHandler<Exception> UnknowErrorOccurred { get; private set; }

        /// <summary>
        /// Xảy ra khi một thông báo được gửi thành công tới client
        /// </summary>
        internal EventHandler<DeviceNotification> SendSuccessed { get; private set; }

        #endregion

        #region Constructor
        internal Windows10RawNotificationSender(string packageSID, string secretKey,
            EventHandler<Exception> internetErrorcOccurred,
            EventHandler<DelegatesAndEnums.BadURIEventArgs> badURIErrorOccurred,
            EventHandler<Exception> notAccecptableErrorOccurred,
            EventHandler<Exception> unknowErrorOccurred,
            EventHandler<DeviceNotification> sendSuccessed,
            EventHandler<Exception> wrongPackageSIDOrSecretKey)
        {
            PackageSID = packageSID;
            SecretKey = secretKey;
            InternetOrFirewallErrorOccurred = internetErrorcOccurred;
            BadURIErrorOccurred = badURIErrorOccurred;
            NotAccecptableErrorOccurred = notAccecptableErrorOccurred;
            UnknowErrorOccurred = unknowErrorOccurred;
            SendSuccessed = sendSuccessed;
            WrongPackageSIDOrSecretKey = wrongPackageSIDOrSecretKey;

            token = new Windows10TokenAccess(PackageSID, SecretKey);
            Tconverter = new MyBitConverter<T>();
            // mới bật chương trình lên thì làm mới lại token bất đồng bộ để tránh cản trở chương trình khởi động
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            token.RenewToken();
            //            break;
            //        }
            //        catch (UnauthorizedAccessException uex)
            //        {
            //            if (WrongPackageSIDOrSecretKey(uex, new DelegatesAndEnums.ChoicesOnAuthenticationErrorOccurreddEventArgs(uex, ChangePackAgeSIDAndSecretKey)) == DelegatesAndEnums.TryAgainOrCancel.Cancel)
            //                break;
            //        }
            //        catch
            //        {
            //            break;
            //        }
            //    }
            //});
        }

        private void ChangePackAgeSIDAndSecretKey(string packageSID, string SecretKey)
        {
            token = new Windows10TokenAccess(packageSID, SecretKey);
        }

        #endregion

        /// <summary>
        /// Gửi một đối tượng dữ liệu bất kì tới WNS 
        /// </summary>
        /// <param name="device">Thiết bị cần nhận thông báo, giúp việc xóa thiết bị dễ dàng hơn khi xảy ra lỗi sai URI</param>
        /// <param name="sendobj">Object cần gửi</param>
        /// <param name="devicenoti">Đưa ra việc gửi cho thiết bị nào và gửi thông báo nào, giúp việc xóa nó sẽ dễ dàng hơn nếu gửi thông báo thành công</param>
        public void Send(Device device, T sendobj, DeviceNotification devicenoti)
        {
            bool flag = false;
            byte[] data = Tconverter.ObjectToBytes(sendobj);

            #region chuẩn bị http request và các header phù hợp để gửi raw notification
            HttpWebRequest request = null;
            try
            {
                 request = HttpWebRequest.Create(device.URI) as HttpWebRequest;
            }
            catch(Exception ex)
            {
                BadURIErrorOccurred(this, new DelegatesAndEnums.BadURIEventArgs(ex, device));
                return;
            }
            request.Method = "POST";
            request.Headers.Add("X-WNS-Type", "wns/raw");
            request.Headers.Add("X-WNS-Cache-Policy", "cache");
            request.Headers.Add("X-WNS-RequestForStatus", "true");
            request.ContentType = "application/octet-stream";
            request.Headers.Add("Authorization", String.Format("Bearer {0}", token.Token));
            #endregion

            // chuyển đối tượng cần gửi về dạng mảng byte để có thể gửi đi
            #region Gửi Notification
            try
            {
                // gửi yêu cầu có nội dung là mảng byte trên tới uri
                using (Stream requestStream = request.GetRequestStream())
                    requestStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi xảy ra");
                InternetOrFirewallErrorOccurred(this, ex);
                return;
            }
            #endregion

            #region nhận về Response
            try
            {   // nhận về response
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                SendSuccessed(this, devicenoti);
                System.Diagnostics.Debug.WriteLine(webResponse.Headers["X-WNS-Status"]);
                System.Diagnostics.Debug.WriteLine(webResponse.Headers["X-WNS-DeviceConnectionStatus"]);
                flag = true;
            }
            catch (WebException webException)
            {
                
                #region Phân tích exception

                // việc gửi yêu cầu bị lỗi không phải do giao thức (các nguyên nhân khác, có thể do mạng hoặc do tường lửa)
                if (webException.Status != WebExceptionStatus.ProtocolError)
                {
                    InternetOrFirewallErrorOccurred(this, webException);
                    return;
                }

                // code chạy xuống dưới đây có nghĩa là lỗi giao thức

                // lấy status code
                HttpStatusCode status = ((HttpWebResponse)webException.Response).StatusCode;

                if (status == HttpStatusCode.Unauthorized) // nếu là lỗi do token hết hạn
                {
                    try
                    {
                        token.RenewToken();
                    }
                    catch (UnauthorizedAccessException uex)
                    {
                        WrongPackageSIDOrSecretKey(this, uex);
                        return;
                    }
                    catch (WebException wex)
                    {
                        InternetOrFirewallErrorOccurred(this, wex);
                        return;
                    }

                    this.Send(device, sendobj, devicenoti);
                }
                else if (status == HttpStatusCode.Gone || status == HttpStatusCode.NotFound || status == HttpStatusCode.Forbidden) // Lỗi phát sinh do URI hết hạn, không tồn tại hoặc sai nên server không được phép gửi tới
                {
                    BadURIErrorOccurred(this, new DelegatesAndEnums.BadURIEventArgs(webException, device));
                    return;
                }
                else if (status == HttpStatusCode.NotAcceptable) // Lỗi do gửi quá nhiều thông báo tới này trong một khoảng thời gian quá ngắn
                {
                    NotAccecptableErrorOccurred(this, webException);
                    return;
                }
                else // Lỗi do một nguyên nhân khác
                {
                    UnknowErrorOccurred(webException, webException);
                    return;
                };
                #endregion
            }
            #endregion
        }
    }
}
