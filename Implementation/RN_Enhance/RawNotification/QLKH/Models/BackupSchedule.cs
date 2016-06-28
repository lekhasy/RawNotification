using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace QLKH.Models
{
    [Serializable]
    public class BackupSchedule
    {
        bool _Repeat;
        bool[] _Thu;
        DateTime _Time;
        // khi schedule done thì không cần gán lại -1 làm gì cả, bởi vì đối tượng này sẽ bị hủy ngay sau đó do công việc của nó đã xong
        int[] _WorksID = new int[7] { -1, -1, -1, -1, -1, -1, -1 };
        int NoRepeatWork;
        public static event EventHandler AScheduleDone;

        public String Description
        {
            get { return ToString(); }
        }

        public int[] Works
        {
            get { return _WorksID; }
            set { _WorksID = value; }
        }

        public BackupSchedule(BackupSchedule schedule)
        {
            _Repeat = schedule._Repeat;
            _Time = schedule._Time;
            DateTime RealTime = DateTime.Today + schedule._Time.TimeOfDay;
            _Time = RealTime;
            if (_Repeat) // nếu cho phép repeat thì thêm các công việc cho từng ngày
            {
                _Thu = schedule._Thu;

                /// i chạy từ ngày hiện hành
                /// j là số ngày cần phải cộng thêm vào
                for (int i = (int)DateTime.Now.DayOfWeek, j = 0; j < 7; i++, j++)
                {

                    int Sttthu = i < 7 ? i : i - 7;
                    if (_Thu[Sttthu])
                    {
                        Works[Sttthu] = WorkExcuter.WorksManager.GetNewWork(RealTime.AddDays(j),
                            new WorkExcuter.CallBackDlg(CallBack), new WorkExcuter.CallBackDlg(OnNotExcuted), RealTime, false).WorkID;
                    }
                }
            }
            else
            {
                if (RealTime.CompareTo(DateTime.Now) < 0) // Realtime đã trôi qua
                {
                    RealTime = RealTime.AddDays(1);
                    _Time = RealTime;
                }
                NoRepeatWork = WorkExcuter.WorksManager.GetNewWork(RealTime, new WorkExcuter.CallBackDlg(CallBack), new WorkExcuter.CallBackDlg(OnNotExcuted), RealTime, false).WorkID;
            }
        }

        public BackupSchedule(DateTime time, bool repeat, bool cn, bool t2, bool t3, bool t4, bool t5, bool t6, bool t7)
        {
            _Repeat = repeat;
            _Time = time;
            DateTime RealTime = DateTime.Today + time.TimeOfDay;
            _Time = RealTime;
            if (_Repeat) // nếu cho phép repeat thì thêm các công việc cho từng ngày
            {
                _Thu = new bool[] { cn, t2, t3, t4, t5, t6, t7 };

                /// i chạy từ ngày hiện hành
                /// j là số ngày cần phải cộng thêm vào
                for (int i = (int)DateTime.Now.DayOfWeek, j = 0; j < 7; i++, j++)
                {

                    int Sttthu = i < 7 ? i : i - 7;
                    if (_Thu[Sttthu])
                    {
                        Works[Sttthu] = WorkExcuter.WorksManager.GetNewWork(RealTime.AddDays(j),
                            new WorkExcuter.CallBackDlg(CallBack), new WorkExcuter.CallBackDlg(OnNotExcuted), RealTime, false).WorkID;
                    }
                }
            }
            else
            {
                if (RealTime.CompareTo(DateTime.Now) < 0) // Realtime đã trôi qua
                {
                    RealTime = RealTime.AddDays(1);
                    _Time = RealTime;
                }
                NoRepeatWork = WorkExcuter.WorksManager.GetNewWork(RealTime, new WorkExcuter.CallBackDlg(CallBack), new WorkExcuter.CallBackDlg(OnNotExcuted), RealTime, false).WorkID;
            }
        }
        /// <summary>
        /// Hàm này sẽ cho biết trong quá trình lưu file xuống, nó có bị lỗi không. 
        /// Hàm này chỉ được gọi khi Package WorkExcuter đã Initialize
        /// </summary>
        /// <returns></returns>
        public bool IsError()
        {
            if (!_Repeat)
            {
                if (WorkExcuter.WorksManager.GetWorkByID(NoRepeatWork) == null)
                {
                    return true;
                }
                return false;
            }
            foreach (var item in Works)
            {
                if (item!=-1 && WorkExcuter.WorksManager.GetWorkByID(item) == null) // không tìm thấy work của mình có nghĩa là lỗi
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Hủy bỏ tác vụ này
        /// </summary>
        public void Terminate()
        {
            // Muốn hủy bỏ một tác vụ, đầu tiên ta phải dùng lớp để thông báo là nó đã xong việc
            if (AScheduleDone != null)
            {
                AScheduleDone(this, new EventArgs());
            }
            // sau khi thông báo rồi thì phải terminate tất cả các work bên trong Schedule này
            if (_Repeat)
            {
                foreach (var item in Works)
                {
                    WorkExcuter.Work work = WorkExcuter.WorksManager.GetWorkByID(item);
                    if (work != null)
                    {
                        work.Terminate();
                    }
                }
            }
            else
            {
                WorkExcuter.WorksManager.GetWorkByID(NoRepeatWork).Terminate();
            }
        }

        /// <summary>
        /// Xảy ra khi công việc hoàn thành
        /// </summary>
        /// <param name="Data">Dữ liệu đầu vào</param>
        private void CallBack(object Data, WorkExcuter.Work work)
        {
            DBDataContext db = new DBDataContext();
            if (!Directorys.IsFileExited())
            {
                db.BackUpDB(Directorys.GetBackupPath());
            }
            //Thay thế Work đã được thực thi bằng work mới nếu repeat true
            if (_Repeat)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (Works[i] == work.WorkID)
                    {
                        DateTime NextTime = work.Time.AddDays(7);
                        Works[i] = WorkExcuter.WorksManager.GetNewWork(NextTime, new WorkExcuter.CallBackDlg(CallBack),
                            new WorkExcuter.CallBackDlg(OnNotExcuted), NextTime, false).WorkID;
                    }
                }
            }
            else // Không repeat, chạy xong rồi thì nhờ class thông báo rằng mình đã xong việc
            {
                if (AScheduleDone != null)
                {
                    AScheduleDone(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Chạy khi có 1 Work không được chạy do quá thời gian
        /// </summary>
        /// <param name="sender">WorkID</param>
        /// <param name="e">EventArgs</param>
        private void OnNotExcuted(object sender, WorkExcuter.Work work)
        {
            // Nếu là lặp lại thì set work mới
            if (_Repeat)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (Works[i] == (int)sender)
                    {
                        int SoNgayDaQua = DateTime.Today.Day - work.Time.Day;
                        DateTime NextTime = work.Time.AddDays(7 - SoNgayDaQua);
                        Works[i] = WorkExcuter.WorksManager.GetNewWork(NextTime, new WorkExcuter.CallBackDlg(CallBack),
                            new WorkExcuter.CallBackDlg(OnNotExcuted), NextTime, false).WorkID;
                    }
                }
            }
            else // Không lặp lại thì thông báo là tác vụ backup đã hoàn thành
            {
                if (AScheduleDone != null)
                {
                    AScheduleDone(this, new EventArgs());
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Time: ");
            builder.Append(_Time);
            if (_Repeat)
            {
                builder.Append("  Repeat: ");
                builder.Append(" T2:"); builder.Append(_Thu[1]);
                builder.Append(" T3:"); builder.Append(_Thu[2]);
                builder.Append(" T4:"); builder.Append(_Thu[3]);
                builder.Append(" T5:"); builder.Append(_Thu[4]);
                builder.Append(" T6:"); builder.Append(_Thu[5]);
                builder.Append(" T7:"); builder.Append(_Thu[6]);
                builder.Append(" CN:"); builder.Append(_Thu[0]);
            }
            return builder.ToString();
        }
    }
}