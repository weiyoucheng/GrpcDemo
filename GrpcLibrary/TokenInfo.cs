using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GrpcLibrary {
    /// <summary>
    /// Token信息
    /// </summary>
    [DataContract]
    public class TokenInfo {
        /// <summary>
        /// Token值
        /// </summary>
        [DataMember(Order = 1)]
        public string Token { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [DataMember(Order = 2)]
        public DateTime Expiration { get; set; }

        public TokenInfo() {
            Token      = string.Empty;
            Expiration = DateTime.Now;
        }

        public TokenInfo(DateTime expiration) {
            Token = string.Empty;
            Expiration = expiration;
        }
    }
}
