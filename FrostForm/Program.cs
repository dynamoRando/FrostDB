using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrostForm
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var args = Environment.GetCommandLineArgs();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new formFrost());
        }

        public static void Main(string ipAddress, int dataPort, int consolePort)
        {
            //Application.Run(new formFrost(ipAddress, dataPort, consolePort));
            var form = new formFrost(ipAddress, dataPort, consolePort);
            form.Show();
        }
    }
}
