using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GrpcLibrary {
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    public class UserInfo {

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember(Order = 1)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember(Order = 2)]
        public string Password { get; set; }

        /// <summary>
        /// 所属网点
        /// </summary>
        [DataMember(Order = 3)]
        public string SiteName { get; set; }

        public UserInfo() {
            UserName = string.Empty;
            Password = string.Empty;
            SiteName = string.Empty;
        }

        public UserInfo(string userName,string password,string siteName) {
            UserName = userName;
            Password = password;
            SiteName = siteName;
        }

    }
}
