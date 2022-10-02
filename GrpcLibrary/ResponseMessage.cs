using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GrpcLibrary {

    /// <summary>
    /// 相应消息
    /// </summary>
    [DataContract]
    public class ResponseMessage<T> {

        /// <summary>
        /// 状态码
        /// </summary>
        [DataMember(Order = 1)]
        public int StatusCode { get; set; }

        /// <summary>
        /// 状态消息内容
        /// </summary>
        [DataMember(Order = 2)]
        public string Message { get; set; }

        /// <summary>
        /// 请求是否成功
        /// </summary>
        [DataMember(Order = 3)]
        public string IsSuccess { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        [DataMember(Order = 4)]
        public T Result { get; set; }

        public ResponseMessage() {
            StatusCode = 0;
            Message    = string.Empty;
            IsSuccess  = string.Empty;
            Result     = default!;
        }
        public ResponseMessage(int statusCode, string message, string isSuccess,T result) {
            StatusCode = statusCode;
            Message    = message;
            IsSuccess  = isSuccess;
            Result = result;
        }
    }

}
