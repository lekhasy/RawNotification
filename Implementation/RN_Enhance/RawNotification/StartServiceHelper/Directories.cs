using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartServiceHelper
{
    public static class Directories
    {
        public static readonly string RN_BuildFolder = InitializeRNBuild().FullName;


        private static DirectoryInfo InitializeRNBuild()
        {
            var CurrentDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath);
            DirectoryInfo info = new DirectoryInfo(CurrentDirectoryPath);
            info = info.Parent;
            return info;
        }
    }
}
