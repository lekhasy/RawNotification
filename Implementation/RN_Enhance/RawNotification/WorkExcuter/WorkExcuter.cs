using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkExcuter
{
    /// <summary>
    /// Trong class MyDatetimeTimer tự động gọi Start() khi được gán giá trin time cho nó.
    /// không cần gọi lại khi không cần thiết
    /// </summary>
    public static class WorkExcuter
    {
        static MyDatetimeTimer _Timer;
        static LinkedList<Work> _Works;  // danh sách này sắp xếp theo thứ tự ngày sắp đến tới ngày còn lâu mới đến
        static Work _CurrentWork;


        /// <summary>
        /// Gọi hàm này khi khởi động chương trình
        ///  - Khởi tạo Danh sách công việc
        ///  - khởi động bộ đếm thời gian nếu có công việc cần làm
        /// </summary>
        public static void Initialize()
        {
            _Works = WorksManager.AllWorks;
            _Timer = new MyDatetimeTimer(DateTime.Now);
            _Timer.RingRing += timer_RingRing;
            _Timer.TimePassed += timer_RingRing;
            SetTimerDueTime();
            WorksManager.WorksChanged += (o, e) =>
            {
                SetTimerDueTime();
            };
        }

        /// <summary>
        /// Hàm này có chức năng set cho timer chạy cho công việc đầu tiên trong list
        /// </summary>
        private static void SetTimerDueTime()
        {

            if (_Works.Count > 0)
            {
                // mỗi khi cập nhật lại timer thì  ta cũng cập nhật luôn cho work hiện tại, để khi excute dùng để so sánh
                _CurrentWork = _Works.First.Value;

                _Timer.Time = _CurrentWork.Time;
            }
            else
            {
                _Timer.Stop();
            }

        }

        /// <summary>
        /// Chạy mỗi khi Timer chạm tới thời điểm được hẹn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timer_RingRing(object sender, EventArgs e)
        {
            if (_Works.Count == 0) // đề phòng trường hợp sự kiện vừa phát sinh, chưa kịp khóa semaphore mà phần tử cuối cùng bị xóa
            {
                return;
            }
            Work executework = _Works.First.Value;

            //Kiểm tra lại xem có đúng không
            if (!Work.ReferenceEquals(executework, _CurrentWork))
            {
                return;
            }

            executework.Execute();
        }
    }
}
