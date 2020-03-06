using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrostWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FrostApp _app;


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int timeout = 1000;

            var selectedIp = textAddress.Text;
            string ipAddress = selectedIp;
            int portNumber = Convert.ToInt32(textPort.Text);
            int localPort = Convert.ToInt32(textConsolePort.Text)
;
            _app = new FrostApp();
            _app.SetupClient(ipAddress, portNumber, localPort);
            FrostAppReference.Client = _app.Client;

            //AppReference.Client.GetDatabases();

            // can do this also to get results
            var task = FrostAppReference.Client.GetDatabasesAsync();
            await task;

            var result = task.Result;
            foreach(var i in result)
            {
                listDatabases.Items.Add(i);
            }
        }
    }
}
