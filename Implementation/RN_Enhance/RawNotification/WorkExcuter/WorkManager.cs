using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkExcuter
{
    public static class WorksManager
    {
        public static event EventHandler WorksChanged;
        static LinkedList<Work> Works = new LinkedList<Work>();
        internal static Semaphore _Smp = new Semaphore(1, 1);
        static string _FilePath;

        public static void Initialize(TimeSpan Saiso, string FilePath = "Settings/WorksManager.wmg")
        {
            Work._SaiSo = Saiso;
            _FilePath = FilePath;
            FileInfo file = new FileInfo(_FilePath);
            if (file.Exists)
            {
                Works = ObjectSerization.Deserization(_FilePath) as LinkedList<Work>;
                Work.AWorkTerminatedByUser += OnAWorkTerminated;
                Work.AworkDone += OnAWorkDone;
                WorkExcuter.Initialize();
            }
            else
            {
                SaveWorks();
                Initialize(Saiso);
            }
        }

        private static void OnAWorkDone(object sender, EventArgs e)
        {
            RemoveWork(sender as Work);
        }

        private static void OnAWorkTerminated(object sender, EventArgs e)
        {
            RemoveWork(sender as Work);
        }

        public static Work GetNewWork(DateTime Time, CallBackDlg what, CallBackDlg whenNotExcuted, object Data, bool OutOfDateExcute)
        {
            _Smp.WaitOne();
            Work newwork = new Work(Time, Data, what, whenNotExcuted, OutOfDateExcute, GetID());
            AddWork(newwork);
            _Smp.Release();
            return newwork;
        }

        /// <summary>
        /// Hàm này là hàm private, chỉ được sử dụng bởi hàm GetNewWork, vì vậy không cần semaphore
        /// </summary>
        /// <param name="newwork"></param>
        private static void AddWork(Work newwork)
        {
            if (Works.Count == 0)
            {
                // khi không có ai trong danh sách chèn vào đầu
                Works.AddFirst(newwork);
            }
            else
            {
                // không cần phải lo về việc chèn vào có sao không
                LinkedListNode<Work> item = Works.First;
                do
                {
                    if (newwork.Time.CompareTo(item.Value.Time) == -1) // nếu work tới sớm hơn item
                    {// thì chèn work vào trước item
                        Works.AddBefore(item, newwork);
                        break;
                    }
                    item = item.Next;
                } while (item != null);

                // item == null có nghĩa là chạy hết danh sách rồi mà vẫ ko tìm được việc vào tới trễ hơn
                // vậy thì thêm vào cuối danh sách
                if (item == null)
                {
                    Works.AddLast(newwork);
                }
            }
            // thêm xong rồi thì lưu lại
            SaveWorks();
        }
        private static void RemoveWork(Work wrk)
        {
            Works.Remove(wrk);
            SaveWorks();
        }

        private static int GetID()
        {
            int max = 0;
            foreach (var item in Works)
            {
                if (max <= item.WorkID)
                {
                    max = item.WorkID+1;
                }
            }
            return max;
        }

        public static Work GetWorkByID(int id)
        {
            try
            {
                return Works.Single(w => w.WorkID == id);
            }
            catch
            {
                return null;
            }
        }

        public static LinkedList<Work> AllWorks
        {
            get { return Works; }
        }

        private static void SaveWorks()
        {
            ObjectSerization.Serization(Works, _FilePath);
            if (WorksChanged!=null)
            {
                WorksChanged(Works, new EventArgs());
            }
        }
    }
}
