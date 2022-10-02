using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcLibrary {

    /// <summary>
    /// 相应消息
    /// </summary>
    public class ResponseMessage {

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 状态消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 请求是否成功
        /// </summary>
        public string IsSuccess { get; set; }

        public ResponseMessage() {
            StatusCode = 0;
            Message    = string.Empty;
            IsSuccess  = string.Empty;
        }
        public ResponseMessage(int statusCode, string message, string isSuccess) {
            StatusCode = statusCode;
            Message    = message;
            IsSuccess  = isSuccess;
        }
    }
}
