using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace GrpcDesktopService {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        public Server Server { get; set; }

        //侦听地址
        private const string HOST = "192.168.3.13";

        //侦听端口
        private const int PORT = 5839;

        //服务启动状态
        private bool _State = false;    

        /// <summary>
        /// 停止服务
        /// </summary>
        private async void StopService() {
            if (! _State) {
                MessageBox.Show("请先启动服务");
                return; 
            }
            await Server.ShutdownAsync();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        private void StartupService() {
            if (_State) {
                MessageBox.Show("服务已经启动，请勿重复执行。");
                return; 
            }
            _State = true;
            Server = new Server
            {
                //这里未实现暂时不知道怎么实现
                //Services = { MathServer.BindService(new MathServer()) },
                Ports = { { HOST, PORT, ServerCredentials.Insecure } }
            };
            Server.Start();
            textLog.AppendText("启动Grpc服务...\r");
            textLog.AppendText($"侦听地址：{HOST}\t端口 {PORT}\r");
            
        }

        private void ButtStartupService_Click(object sender, RoutedEventArgs e) {
            StartupService();
        }

        private void ButtStopService_Click(object sender, RoutedEventArgs e) {
            StopService();
        }
    }
}
