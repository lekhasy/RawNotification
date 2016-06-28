using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.ServiceLib
{
    /// <summary>
    /// Một class cho phép truy cập tài nguyên một cách đồng bộ.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourceAccessSyncer<T>
    {

        System.Threading.Semaphore _PublicDoor = new System.Threading.Semaphore(1, 1);

        System.Threading.Semaphore _PrivateDoor = new System.Threading.Semaphore(1, 1);


        public T Resource { get; set; }

        bool MustRefreshResource = true;

        int RegisterCount = 0;

        Func<T> RefreshResourceMethod;

        public ResourceAccessSyncer(Func<T> refreshResourceMethod, T defaultResourceValue)
        {
            Resource = defaultResourceValue;
            RefreshResourceMethod = refreshResourceMethod;
        }

        public T RefreshResourceSync()
        {
            // nếu danh sách trước đang lấy resource thì danh sách mới sẽ phải chờ bên ngoài cửa public
            _PublicDoor.WaitOne();
            _PublicDoor.Release();

            // đăng ký vào danh sách nhận resource
            RegisterCount++;

            // chạy qua cửa private để vào làm mới resource
            _PrivateDoor.WaitOne();

            T dublicate;

            // nếu không cần làm mới resource thì lấy resource rồi đi ra
            if (!MustRefreshResource)
            {
                System.Diagnostics.Debug.WriteLine("get resource");
                dublicate = Resource;
                RegisterCount--;
                if (RegisterCount == 0) // tất cả các thread đăng ký đã lấy xong resource
                {
                    // đánh dấu là cần phải làm mới lại resource
                    MustRefreshResource = true;
                    // mở cửa 1 cho đợt thread mới vào
                    _PublicDoor.Release();
                }
                _PrivateDoor.Release();
                return dublicate;
            }

            try
            {
                Resource = RefreshResourceMethod();
                // đã refresh resource thành công rồi thì đánh dấu là tạm thời không cần làm mới nữa
                MustRefreshResource = false;
                System.Diagnostics.Debug.WriteLine("Refresh Resource");
            }
            catch
            {
                // lỗi thì mở cửa private cho thread tiếp theo vào thử Refresh lại resource
                _PrivateDoor.Release();
                throw;
            }

            // đã làm mới được resource, băt đầu đóng cổng public để các register vào lấy resource ra
            _PublicDoor.WaitOne();

            RegisterCount--;
            dublicate = Resource;
            if (RegisterCount == 0) // tất cả các thread đăng ký đã lấy xong resource
            {
                // đánh dấu là lần sau phải refresh lại resource
                MustRefreshResource = true;
                // mở cửa public cho đợt thread mới vào
                _PublicDoor.Release();
            }
            // mở cổng private cho các thread khác vào lấy
            _PrivateDoor.Release();
            return dublicate;
        }
    }
}
