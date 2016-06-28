using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using RawNotification.ServiceLib;
using RawNotification.Models.ServerBusinessModels.Parameters;

namespace RawNotification.BusinessLogic.RawNotificationSenders.RNSImplements
{
    public class WNSTokenAccess
    {
        private Action<string> _SetToken;
        private string _PackageSID;
        private string _Secret;
        private string _Token;
        public string Token { get { return _Token; }
            private set
            {
                _Token = value;
                _SetToken(Token);
            }
        }

        /// <summary>
        /// Khởi tạo một đối tượng TokenAccess cho windows 10 mà không làm mới token key
        /// </summary>
        /// <param name="PackageSID"></param>
        /// <param name="Secret"></param>
        public WNSTokenAccess(WNSParameter parameter)
        {
            this._PackageSID = parameter.WNSPackageSID;
            this._Secret = parameter.WNSSecretKey;
            this._SetToken = parameter.SetToken;
            Token = parameter.GetToken();
            TokenRefresher = new ResourceAccessSyncer<string>(new Func<string>(() => { return GetAccessToken(_Secret, _PackageSID).AccessToken; }), "");
        }

        
        ResourceAccessSyncer<string> TokenRefresher;

        /// <summary>
        /// làm mới token, 
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"></exception>
        internal string RenewToken()
        {
            Token = TokenRefresher.RefreshResourceSync();
            return Token;
        }
        
        [DataContract]
        protected class OAuthToken
        {
            [DataMember(Name = "access_token")]
            public string AccessToken { get; set; }
            [DataMember(Name = "token_type")]
            public string TokenType { get; set; }
        }

        private static OAuthToken GetOAuthTokenFromJson(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                var ser = new DataContractJsonSerializer(typeof(OAuthToken));
                var oAuthToken = (OAuthToken)ser.ReadObject(ms);
                return oAuthToken;
            }
        }

        protected static OAuthToken GetAccessToken(string secret, string sid)
        {
            var urlEncodedSecret = System.Web.HttpUtility.UrlEncode(secret);
            var urlEncodedSid = System.Web.HttpUtility.UrlEncode(sid);

            var body =
              String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com", urlEncodedSid, urlEncodedSecret);

            string response;
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                response = client.UploadString("https://login.live.com/accesstoken.srf", body);
            }
            return GetOAuthTokenFromJson(response);
        }

        #region Old Codes
        /// <summary>
        /// làm mới Token, đây là phiên bản cũ
        /// </summary>
        /// <returns></returns>
        private string RenewToken1()
        {
            // nếu danh sách trước đang lấy Token thì danh sách mới sẽ phải chờ ở đây
            Door1.WaitOne();
            Door1.Release();

            // đăng ký vào danh sách nhận token
            registernumber++;

            // chạy qua cửa 2 để vào bấm nút
            Door2.WaitOne();

            string dublicate;

            // nếu token đã có sẵn thì lấy token đó rồi đi ra
            if (insidetokenvalue != null)
            {
                dublicate = insidetokenvalue;
                registernumber--;
                if (registernumber == 0) // tất cả các thread đăng ký đã lấy xong token
                {
                    // xóa giá trị token đã lưu
                    insidetokenvalue = null;
                    // mở cửa 1 cho đợt thread mới vào
                    Door1.Release();
                }
                Door2.Release();
                System.Diagnostics.Debug.WriteLine("got token : " + dublicate.ToString());
                return dublicate;
            }



            try
            {
                Token = GetAccessToken(_Secret, _PackageSID).AccessToken;
                insidetokenvalue = Token;

                // đã lấy được token, băt đầu đóng cổng ngoài để các register vào lấy token ra
                Door1.WaitOne();
                Console.WriteLine("Clicked button");
                System.Diagnostics.Debug.WriteLine("Clicked button");
            } catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    throw new UnauthorizedAccessException();
                }
                else
                    throw new WebException();
            }

            registernumber--;
            dublicate = Token;
            if (registernumber == 0) // tất cả các thread đăng ký đã lấy xong token
            {
                // xóa giá trị token đã lưu
                insidetokenvalue = null;
                // mở cửa 1 cho đợt thread mới vào
                Door1.Release();
            }
            // mở cổng trong cho các thread khác vào lấy
            Door2.Release();
            return dublicate;
        }

        System.Threading.Semaphore Door1 = new System.Threading.Semaphore(1, 1);

        System.Threading.Semaphore Door2 = new System.Threading.Semaphore(1, 1);


        string insidetokenvalue = null;

        int registernumber = 0;


        #endregion
    }
}
