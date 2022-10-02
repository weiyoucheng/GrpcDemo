using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GrpcLibrary {

    /// <summary>
    /// 登陆返回消息类
    /// </summary>
    [DataContract]
    public class LoginMessage:ResponseMessage<string> {

        [DataMember(Order = 5)]
        public string Token { get; set; }

        public LoginMessage():base() {
            Token = string.Empty;
        }
    }
}
