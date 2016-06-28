using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkExcuter
{
    /// <summary>
    /// Delegate này đại diện cho một hàm nào đó mà có giá trị trả về là Void và tham số là một dối tượng dữ liệu
    /// Lớp chứa hàm mà được Delegate này đại diện bắt buộc phải được gắn thẻ [Serializable] bởi vì hàm đó sẽ được mã hóa và lưu xuống file.
    /// nếu không đánh dấu thì chương trình sẽ bị lỗi
    /// </summary>
    /// <param name="Data">Dữ liệu đầu vào</param>
    /// <param name="Work">Work gọi xử lí</param>
    [Serializable]
    public delegate void CallBackDlg(object Data, Work Work);

    [Serializable]
    public class Work
    {
        // thời điểm cần thực thi
        DateTime time;

        // Cho biết có làm công việc này không nếu thời gian đã trôi qua
        bool _IsExecuteIfOutOfDate;

        /// <summary>
        /// Event này được bắn ra khi có một Work bị hủy bởi người dùng
        /// </summary>
        public static event EventHandler AWorkTerminatedByUser;

        public static event EventHandler AworkDone;
        
        int _WorkID;        

        public bool IsExecuteIfOutOfDate
        {
            get { return _IsExecuteIfOutOfDate; }
            set { _IsExecuteIfOutOfDate = value; }
        }
        public DateTime Time
        {
            get { return time; }
        }

        /// <summary>
        /// Do thời gian trên máy tính là tương đối, nên vẫn có sai số
        /// </summary>
        internal static TimeSpan _SaiSo;

        public int WorkID { get { return _WorkID; } }

        // Delegate này sẽ được gọi khi công việc cần được thực hiện
        CallBackDlg Work_Func;

        // Delegate này sẽ được gọi khi công việc bị bỏ qua do quá thời gian
        CallBackDlg OnNotExcuted;

        // Dữ liệu truyền vào hàm đó khi nó được gọi
        object Data;

        /// <summary>
        /// Hủy bỏ, không chạy công việc này nữa
        /// </summary>
        public void Terminate()
        {
            if (AWorkTerminatedByUser!=null)
            {
                AWorkTerminatedByUser(this, new EventArgs());
            }
        }

        // Xử lí công việc
        public void Execute()
        {
            /// Công việc được thực thi khi được phép thực thi khi Out date hoặc công việc vẫn trong thời gian sai số
            if ((DateTime.Now - Time) < _SaiSo || IsExecuteIfOutOfDate)
            {
                if (AworkDone != null)
                {
                    AworkDone(this, new EventArgs());
                }
                ///ta không thể biết trước công việc được giao có nặng quá không
                /// nếu như nó quá nặng, xử lí quá lâu thì công việc tiếp theo có thể sẽ bị coi là quá hạn
                /// và khi quá hạn mà quá cả thời gian sai số thì công việc tiếp theo sẽ bị bỏ qua
                /// Vì vậy, ta để công việc này vào một thread cho nó tự xử
                new Thread(new ThreadStart(() =>
                {
                    Work_Func(Data, this);
                })).Start();
            }
            else
            {
                if (AworkDone != null)
                {
                    AworkDone(this, new EventArgs());
                }
                OnNotExcuted(Data, this);
            }
        }
        /// <summary>
        /// Không được tùy tiện tạo Work.
        /// phải xin một đối tượng thông qua lớp WorkManager
        /// </summary>
        /// <param name="when"> thời điểm cần thực thi</param>
        /// <param name="Data_param"> Dữ liệu truyền vào hàm đó khi nó được gọi</param>
        /// <param name="what">Delegate này sẽ được gọi khi công việc cần được thực hiện</param>
        /// <param name="whenNotExcuted">Delegate này sẽ được gọi khi công việc bị bỏ qua do quá thời gian</param>
        /// <param name="ExecuteIfTimeOut">Cho biết có làm công việc này không nếu thời gian đã trôi qua</param>
        internal Work(DateTime when, object Data_param, CallBackDlg what, CallBackDlg whenNotExcuted, bool ExecuteIfTimeOut, int ID)
        {
            Data = Data_param;
            time = when;
            Work_Func = what;
            OnNotExcuted = whenNotExcuted;
            IsExecuteIfOutOfDate = ExecuteIfTimeOut;
            _WorkID = ID;
        }
    }

}
