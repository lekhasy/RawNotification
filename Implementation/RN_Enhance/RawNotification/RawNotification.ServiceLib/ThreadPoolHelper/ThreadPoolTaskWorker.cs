using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.ServiceLib.ThreadPoolHelper
{
    /*Author: SyLK */
    /// <summary>
    /// This class help you easier in waitting a number of task using threadpool before it finish
    /// ** Inportant note: remember call waitAll function
    /// </summary>
    public class ThreadPoolTaskWorker
    {
        List<ThreadPoolTask> mTasks;

        /// <summary>
        /// Create a ZThreadPoolTaskWorker instance.
        /// </summary>
        /// <param name="numberOfTasks">Number of task. If you specify this value, this class will give you better perfomance</param>
        public ThreadPoolTaskWorker(int numberOfTasks = 10)
        {
            mTasks = new List<ThreadPoolTask>(numberOfTasks);
        }


        public void run(Action action)
        {
            mTasks.Add(ThreadPoolTask.run(action));
        }



        /// <summary>
        /// Call this function where you want to wait all tasks finishing.
        /// If you forget calling this function, these threads in pool will lock themself forever.
        /// And if too many ZThreadPoolTask instance exist, threadpool will run out of number of async thread 
        /// => all tasks that using threadpool will be run synchronously
        /// </summary> 
        public void waitAll()
        {
            foreach (var task in mTasks)
            {
                task.wait();
            }
        }
    }
}
