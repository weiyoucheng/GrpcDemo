using Grpc.Core;
using Grpc.Net.Client;
using GrpcExample.ViewModels;
using GrpcLibrary;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace GrpcExample {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = new MainWindowModel();
        }

        /// <summary>
        /// 查询运单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butQuery_Click(object sender, RoutedEventArgs e) {

            //允许不安全的链接
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            //使用Grpc客户端 需引用 Grpc.Net.Client
            using var channel = GrpcChannel.ForAddress(
                "http://192.168.3.13:5838",
                new GrpcChannelOptions() {
                    HttpHandler = handler,
                    //设置允许最大发送数据大小（以字节为单位）,默认允许发送最大数据为4M大小如果不设置当数据超过4M大小时则引发异常
                    MaxSendMessageSize    = int.MaxValue,
                    //设置允许最大接收数据大小（以字节为单位）,默认允许接收最大数据为4M大小如果不设置当数据超过4M大小时则引发异常
                    MaxReceiveMessageSize = int.MaxValue
                    }
                );

            //要登录的用户信息
            var userInfo = new UserInfo
            {
                UserName = "5500030001",
                Password = "123456",
                SiteName = "杭州萧山"
            };

            //根据接口获取Grpc ILoginService 接口（引用GrpcLibrary项目） 
            var loginService = channel.CreateGrpcService<ILoginService>();
            var loginMsg = await loginService.LoginAsync(userInfo);

            //判断登陆是否成功,如果不成功则退出
            if (!loginMsg.IsSuccess) {
                MessageBox.Show(loginMsg.Message);
                return;
            }

            //获取Token
            var token = loginMsg.Token;

            //Grpc元数据，类似于 Http请求头
            var metadata = new Metadata
            {
                { "Authorization",$"Bearer {token}" }
            };
            
            var option = new CallOptions(metadata);
            var context = new CallContext(option, CallContextFlags.CaptureMetadata);

            var qryParam = (MainWindowModel)DataContext;
            //查询运单参数
            var qryWaybillReq = new QueryWaybillRequest()
            {
                StartDate = qryParam.StartDate,
                EndDate   = qryParam.EndDate
            };
            //根据接口获取Grpc IWaybillInfoService 接口（引用GrpcLibrary项目） 
            var waybillService = channel.CreateGrpcService<IWaybillInfoService>();
            var waybillMsg = await waybillService.QueryWaybill(qryWaybillReq,context);
            this.dataGrid.ItemsSource = waybillMsg.Result;
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
