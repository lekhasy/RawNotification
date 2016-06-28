using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawNotificationForServer.Windows10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RawNotificationForServer.Windows10.Tests
{
    [TestClass()]
    public class Windows10TokenAccessTests
    {
        static string packsid = "ms-app://s-1-15-2-2725197343-3336943269-1554551664-2344088653-1875215302-900213611-2750785773";
        static string secret = "mNDa99DTqAqc0y+nNtXxNAF3vf6zVLZb";
        [TestMethod()]
        public static void ReNewTokenAsyncTest(object obj)
        {
            
           
            Windows10TokenAccess token = new Windows10TokenAccess(packsid, secret);
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(((o) =>
                {
                    //token.RenewToken();
                }));
            }
            System.Diagnostics.Debug.WriteLine("Sleeping for 3 second");
            Thread.Sleep(3000);

            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(((o) =>
                {
                    //token.RenewToken();
                }));
            }

        }
    }
}