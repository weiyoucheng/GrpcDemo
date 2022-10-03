namespace GrpcServer.Utility {
    public interface IJWTService {

        /// <summary>
        /// 获取Token信息
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <returns></returns>
        public string GetToken(string userName);

    }
}
