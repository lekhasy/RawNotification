using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.ServiceLib.ThreadPoolHelper
{
    /*Author  SyLK*/
    /// <summary>
    /// This class help you wait a threadpool thread til it finish.
    /// ** Inportant note: remember call wait function
    /// </summary>
    public class ThreadPoolTask
    {
        System.Threading.ManualResetEvent mResetEvent;

        public static ThreadPoolTask run(Action action)
        {
            System.Threading.ManualResetEvent resetEvent = new System.Threading.ManualResetEvent(false);
            System.Threading.ThreadPool.QueueUserWorkItem((o) =>
            {
                action();
                resetEvent.Set();
            });
            return new ThreadPoolTask(resetEvent);
        }

        internal ThreadPoolTask(System.Threading.ManualResetEvent resetevent)
        {
            mResetEvent = resetevent;
        }

        /// <summary>
        /// Call this function where you want to wait this task to finish.
        /// If you forget calling this function, this thread in pool will lock ifself forever.
        /// And if too many ZThreadPoolTask instance exist, threadpool will run out of number of async thread 
        /// => all tasks that using threadpool will be run synchronously
        /// </summary>
        public virtual void wait()
        {
            mResetEvent.WaitOne();
        }
    }
}
