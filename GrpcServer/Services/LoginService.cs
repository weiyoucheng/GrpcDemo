using GrpcLibrary;
using GrpcServer.Utility;
using ProtoBuf.Grpc;

namespace GrpcServer.Services {
    public class LoginService : ILoginService {
        private IJWTService _jwt;
        public LoginService(IJWTService jwt) {
            _jwt = jwt;
        }

        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="userInfo">要登录的用户信息</param>
        /// <param name="context">
        /// 统一客户端和服务器gRPC调用上下文的API；API交集可直接使用-用于特定于客户端或特定于服务器的选项：
        /// 用于获取请求头信息及远程取消等等。
        /// </param>
        /// <returns>返回登陆信息</returns>
        public ValueTask<LoginMessage> LoginAsync(UserInfo userInfo, CallContext context = default) {
            Console.WriteLine($"用户：{userInfo.UserName}\t所属网点\r{userInfo.SiteName}\t登陆IP：{context.ServerCallContext?.Peer}");
            var result = new LoginMessage();
            //获取用户信息（这里使用测试数据生成环境应改为数据库数据）
            var userInfos = GetUserInfos();
            //判断用户名和密码
            if (userInfos.Any(s => s.UserName == userInfo.UserName && s.Password == userInfo.Password)) {
                result.Message = "登陆成功";
                result.IsSuccess = true;
                result.Token = _jwt.GetToken(userInfo.UserName);
            } else {
                result.Message = "登陆失败，用户名或密码错误。";
            }
            return new ValueTask<LoginMessage>(result);
        }

        /// <summary>
        /// 获取测试用户信息
        /// </summary>
        internal static List<UserInfo> GetUserInfos() {
            var results = new List<UserInfo>();
            results.Add(new UserInfo("5500030001", "123456", "杭州桐庐"));
            results.Add(new UserInfo("5500030001", "123456", "杭州萧山"));
            results.Add(new UserInfo("3000020001", "123456", "江苏南京"));
            results.Add(new UserInfo("6800010001", "123456", "深州宝安"));
            results.Add(new UserInfo("8700080001", "123456", "广州白云"));
            return results;
        }

        public ValueTask<ResponseMessage<string>> Login2Async(UserInfo userInfo, CallContext context = default) {
            var result = new ResponseMessage<string>();
            var userInfos = GetUserInfos();
            if (userInfos.Any(s => s.UserName == userInfo.UserName && s.Password == userInfo.Password)) {
                result.Message = "登陆成功";
                result.IsSuccess = true;
            } else {
                result.Message = "登陆失败";
            }
            return new ValueTask<ResponseMessage<string>>(result);
        }

        public ValueTask<string> Login2Async(string userName, string password, CallContext context = default) {
            return new ValueTask<string>(userName+password);
        }
    }
}
