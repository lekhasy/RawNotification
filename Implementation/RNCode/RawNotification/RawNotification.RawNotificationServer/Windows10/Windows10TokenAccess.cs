using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.RawNotificationServer.Windows10
{
    public class Windows10TokenAccess
    {
        private string PackageSID;
        private string Secret;

        /// <summary>
        /// Khởi tạo một đối tượng TokenAccess cho windows 10 mà không làm mới token key
        /// </summary>
        /// <param name="PackageSID"></param>
        /// <param name="Secret"></param>
        public Windows10TokenAccess(string PackageSID, string Secret)
        {
            Token = "";
            this.PackageSID = PackageSID;
            this.Secret = Secret;
            TokenRefresher = new Liblary.ThreeRoomSync<string>(new Func<string>(() => { return GetAccessToken(Secret, PackageSID).AccessToken; }), "");
        }

        System.Threading.Semaphore Door1 = new System.Threading.Semaphore(1, 1);

        System.Threading.Semaphore Door2 = new System.Threading.Semaphore(1, 1);


        string insidetokenvalue = null;

        int registernumber = 0;

        Liblary.ThreeRoomSync<string> TokenRefresher;


        internal string RenewToken()
        {
            Token = TokenRefresher.RefreshResourceSync();
            return Token;
        }

        /// <summary>
        /// làm mới Token, cần phải catch các exception của method này
        /// </summary>
        /// <returns></returns>
        public string RenewToken1()
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
            if (insidetokenvalue!=null)
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
                Token = GetAccessToken(Secret, PackageSID).AccessToken;
                insidetokenvalue = Token;

                // đã lấy được token, băt đầu đóng cổng ngoài để các register vào lấy token ra
                Door1.WaitOne();
                Console.WriteLine("Clicked button");
                System.Diagnostics.Debug.WriteLine("Clicked button");
            }
            catch (WebException ex)
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



        public string Token { get; private set; }

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
    }
}
