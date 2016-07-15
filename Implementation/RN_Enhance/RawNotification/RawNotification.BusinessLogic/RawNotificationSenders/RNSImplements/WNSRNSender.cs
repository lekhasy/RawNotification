using RawNotification.BusinessLogic.RawNotificationSenders.RNSInterfaces;
using System;
using System.IO;
using System.Net;
using RawNotification.Models.ServerBusinessModels;
using log4net;
using RawNotification.Models.ServerBusinessModels.Parameters;
using RawNotification.Models.ClientCommunicatorModels;

namespace RawNotification.BusinessLogic.RawNotificationSenders.RNSImplements
{
    /// <summary>
    /// Lớp Windows10RawNotificationSender có nhiệm vụ gửi thông báo raw tới một ứng dụng qua một URI nhận được từ windows 10
    /// </summary>
    public class WNSRNSender : IWNSRNSender
    {
        private readonly WNSTokenAccess _Token;
        private readonly ILog _Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initialize new instance of WNSRNsender class
        /// </summary>
        /// <param name="packageSID">Package of windows phone app</param>
        /// <param name="secretKey">Secret key of windows phone app</param>
        /// <param name="logger">an IRNLogger used for logging</param>
        /// <exception cref="Exception">Renew token may throw various type of exceptions</exception>
        internal WNSRNSender(WNSParameter parameter)
        {
            _Token = new WNSTokenAccess(parameter);
        }

        /// <summary>
        /// Send an message
        /// </summary>
        /// <param name="notificationInfo">Notification that need to be send</param>
        public EnumWNSSendResult SendNotificationData(string DeviceURI, byte[] NotifiInfo)
        {
            while (true)
            {
                var request = PrepareRequest(DeviceURI);

                if (request == null) // this is bad URI
                {
                    _Logger.Info("Bad URI:" + DeviceURI);
                    return EnumWNSSendResult.BadURI;
                }

                // gửi yêu cầu có nội dung là mảng byte trên tới uri
                try
                {
                    using (Stream requestStream = request.GetRequestStream())
                        requestStream.Write(NotifiInfo, 0, NotifiInfo.Length);
                } catch (Exception ex) //TODO : recheck if this statement throw more than one exception
                {
                    _Logger.Error("Need Attention: this point need to be determine that what type of exception can be throw", ex);
                    return EnumWNSSendResult.NetWorkError;
                }

                try
                {   // nhận về response
                    HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                    return EnumWNSSendResult.Success;
                } catch (WebException webException)
                {
                    #region Phân tích exception

                    // việc gửi yêu cầu bị lỗi không phải do giao thức (các nguyên nhân khác, có thể do mạng hoặc do tường lửa)
                    if (webException.Status != WebExceptionStatus.ProtocolError)
                    {
                        _Logger.Error("Internet Error", webException);
                        return EnumWNSSendResult.NetWorkError;
                    }

                    // code chạy xuống dưới đây có nghĩa là lỗi giao thức

                    // lấy status code
                    HttpStatusCode status = ((HttpWebResponse)webException.Response).StatusCode;

                    if (status == HttpStatusCode.Unauthorized) // nếu là lỗi do token hết hạn
                    {
                        try
                        {
                            request.Headers["Authorization"] = _Token.RenewToken();
                            continue;
                        } catch (UnauthorizedAccessException uex)
                        {
                            _Logger.Error("PackageSID or SecretKey Wrong", uex);

                            return EnumWNSSendResult.OtherErrors;
                        } catch (WebException wex)
                        {
                            _Logger.Error("Internet Error", wex);
                            return EnumWNSSendResult.NetWorkError;
                        }
                    }
                    else if (status == HttpStatusCode.Gone || status == HttpStatusCode.NotFound || status == HttpStatusCode.Forbidden) // Lỗi phát sinh do URI hết hạn, không tồn tại hoặc sai nên server không được phép gửi tới
                    {
                        // we don't have to log about bad uri error.
                        // we just need to log after an send request (how many send success, how many failed...)
                        return EnumWNSSendResult.BadURI;
                    }
                    else if (status == HttpStatusCode.NotAcceptable) // Lỗi do gửi quá nhiều thông báo tới này trong một khoảng thời gian quá ngắn
                    {
                        _Logger.Error("Not Acceptable");
                        return EnumWNSSendResult.OtherErrors;
                    }
                    else // Lỗi do một nguyên nhân khác
                    {
                        _Logger.Error("Internet Error", webException);
                        return EnumWNSSendResult.NetWorkError;
                    };
                    #endregion
                }
            }
        }

        /// <summary>
        /// Prepare request for sending notification
        /// </summary>
        /// <param name="URI">Device URI</param>
        /// <returns>HTTP request</returns>
        private HttpWebRequest PrepareRequest(string URI)
        {
            HttpWebRequest request = null;
            try { request = HttpWebRequest.Create(URI) as HttpWebRequest; } catch { return null; }

            request.Method = "POST";
            request.Headers.Add("X-WNS-Type", "wns/raw");
            request.Headers.Add("X-WNS-Cache-Policy", "cache");
            request.Headers.Add("X-WNS-RequestForStatus", "true");
            request.ContentType = "application/octet-stream";
            request.Headers.Add("Authorization", String.Format("Bearer {0}", _Token.Token));
            return request;
        }
    }
}