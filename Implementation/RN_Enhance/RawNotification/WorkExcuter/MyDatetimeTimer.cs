using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkExcuter
{
    public class MyDatetimeTimer
    {
        System.Threading.Timer timer; // Timer lõi
        private DateTime _time; // Thời gian được hẹn

        public DateTime Time
        {
            get { return _time; }
            set
            {
                Stop();
                _time = value;
                Start();
            }
        }

        /// <summary>
        /// Sự kiện được bắn ra khi Timer lõi chạm tới thời gian được định
        /// </summary>
        private event System.Threading.TimerCallback callback;
        /// <summary>
        /// Sự kiện được bắn ra khi tới thời điểm được hẹn
        /// </summary>
        public event EventHandler RingRing;
        /// <summary>
        /// Sự kiện được bắn ra khi thời gian được hẹn đã trôi qua
        /// </summary>
        public event EventHandler TimePassed;
        public MyDatetimeTimer(DateTime ringtime)
        {
            callback += MyTimer_Tick;
            // khởi tạo timer lõi, timer lúc này chưa chạy.
            // các thông số chi tiết được gán vào trong hàm Start
            timer = new System.Threading.Timer(callback, new object(), -1, -1); 
            Time = ringtime;
            callback -= MyTimer_Tick;
        }
        /// <summary>
        /// Được gọi khi tới thời gian được hẹn
        /// </summary>
        /// <param name="state"></param>
        void MyTimer_Tick(object state)
        {
            // bắn ra sự kiện Ring
            RingRing(this, new EventArgs());
        }

        /// <summary>
        /// bắt đầu đếm giờ
        /// </summary>
        public void Start()
        {
            if (Time.CompareTo(DateTime.Now) == 1) // chưa quá giờ
            {
                timer.Change(Time - DateTime.Now, TimeSpan.Zero);
            }
            else
            {
                if (TimePassed != null)
                {
                    TimePassed(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Ngừng đếm giờ
        /// </summary>
        public void Stop()
        {
            timer.Change(-1, -1);
        }
    }
}
