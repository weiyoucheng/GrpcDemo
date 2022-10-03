using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ProtoBuf.Grpc.Configuration;
using System.ServiceModel;

namespace GrpcLibrary {
    /// <summary>
    /// 运单信息服务接口
    /// </summary>
    [ServiceContract]
    public interface IWaybillInfoService {

        /// <summary>
        /// 查询运单信息
        /// </summary>
        /// <param name="request">查询运单请求参数</param>
        /// <param name="context">
        /// 统一客户端和服务器gRPC调用上下文的API；API交集可直接使用-用于特定于客户端或特定于服务器的选项：
        /// 用于获取请求头信息及远程取消等等。
        /// </param>
        /// <returns>符合查询条件的运单列表</returns>
        [OperationContract]
        ValueTask<ResponseMessage<List<WaybillData>>> QueryWaybill(QueryWaybillRequest request,CallContext context);

    }

    /// <summary>
    /// 查询运单请求类
    /// </summary>
    [DataContract]
    public class QueryWaybillRequest {

        /// <summary>
        /// 寄件开始时间
        /// </summary>
        [DataMember(Order = 1)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 寄件截止时间
        /// </summary>
        [DataMember(Order = 2)]
        public DateTime EndDate { get; set; }

    }
}
