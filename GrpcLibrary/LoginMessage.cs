using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ProtoBuf;

namespace GrpcLibrary {

    /// <summary>
    /// 登陆返回消息类
    /// </summary>
    [ProtoContract()]
    public class LoginMessage: ResponseMessage<string> {

        [ProtoMember(1)]
        public string Token { get; set; }

        public LoginMessage() : base() {
            Token = string.Empty;
        }

    }
}
