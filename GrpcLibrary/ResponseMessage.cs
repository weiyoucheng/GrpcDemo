using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ProtoBuf;
using Microsoft.VisualBasic;

namespace GrpcLibrary {

    /// <summary>
    /// 相应消息
    /// </summary>
    [DataContract]
    [ProtoInclude(6, typeof(LoginMessage))]
    public class ResponseMessage<T> {

        /// <summary>
        /// 状态码
        /// </summary>
        [DataMember(Order = 1)]
        public System.Net.HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 状态消息内容
        /// </summary>
        [DataMember(Order = 2)]
        public string Message { get; set; }

        /// <summary>
        /// 请求是否成功
        /// </summary>
        [DataMember(Order = 3)]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        [DataMember(Order = 4)]
        public T Result { get; set; }

        public ResponseMessage() {
            StatusCode = System.Net.HttpStatusCode.OK;
            Message = string.Empty;
            IsSuccess = false;
            Result = default!;
        }
        public ResponseMessage(System.Net.HttpStatusCode statusCode, string message, bool isSuccess, T result) {
            StatusCode = statusCode;
            Message = message;
            IsSuccess = isSuccess;
            Result = result;
        }
    }

}
