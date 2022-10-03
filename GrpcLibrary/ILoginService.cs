using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GrpcLibrary {

    /// <summary>
    /// 登陆服务接口
    /// </summary>
    [ServiceContract]
    public interface ILoginService {

        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="message">登陆用户信息</param>
        /// <param name="context">
        /// 统一客户端和服务器gRPC调用上下文的API；API交集可直接使用-用于特定于客户端或特定于服务器的选项：
        /// 用于获取请求头信息及远程取消等等。
        /// </param>
        /// <returns></returns>
        [OperationContract]
        ValueTask<LoginMessage> LoginAsync(UserInfo userInfo,CallContext context = default);

    }
}
