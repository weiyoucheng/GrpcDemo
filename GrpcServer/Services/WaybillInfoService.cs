using GrpcLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using System.Resources;
using System.IdentityModel.Tokens.Jwt;

namespace GrpcServer.Services {
    /// <summary>
    /// 运单信息查询服务
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WaybillInfoService : IWaybillInfoService {
        public ValueTask<ResponseMessage<List<WaybillData>>> QueryWaybill(QueryWaybillRequest request, CallContext context) {
            //获取
            var authorization = context.RequestHeaders?.GetValue("Authorization");
            if (authorization == null) { throw new Grpc.Core.RpcException(Grpc.Core.Status.DefaultSuccess, "无法获取验证信息"); }
            string token      = authorization.Replace(JwtBearerDefaults.AuthenticationScheme+" ", "");

            var jwtHander        = new JwtSecurityTokenHandler();
            var JwtSecurityToken = jwtHander.ReadJwtToken(token);
            var user             = JwtSecurityToken.Claims.FirstOrDefault(s=>s.Type == JwtRegisteredClaimNames.Name)?.Value;
            if(user == null) {
                throw new Grpc.Core.RpcException(Grpc.Core.Status.DefaultSuccess, "Token内容解析错误");
            }
            //获取所有用户信息
            var users    = LoginService.GetUserInfos();
            var siteName = users.First(s => s.UserName == user).SiteName;
            Console.WriteLine( $"用户：{user} 执行运单查询，IP：{context.ServerCallContext?.Peer}");
            var result        = new ResponseMessage<List<WaybillData>>();
            //获取测试数据
            var data          = GetWaybillDatas();
            result.IsSuccess  = true;
            result.StatusCode = System.Net.HttpStatusCode.OK;
            result.Message    = "查询成功";
            result.Result     = data.Where(s=> s.SiteName == siteName && s.SendDate >= request.StartDate && s.SendDate <= request.EndDate).ToList();
            return new ValueTask<ResponseMessage<List<WaybillData>>>(result);
        }

        /// <summary>
        /// 获取运单信息测试数据
        /// </summary>
        /// <returns></returns>
        internal static List<WaybillData> GetWaybillDatas() {
            var result    = new List<WaybillData>();
            var siteNames = new string[] { "杭州桐庐", "杭州萧山", "江苏南京", "深州宝安", "广州白云" };
            var names     = MyResource.Names  .Split("\r\n");
            var address   = MyResource.Address.Split("\r\n");
            var p         = new int[] { 3, 5, 7, 8, 9 };
            var random    = new Random();
            for (int i = 0; i < 100000; i++) {
                result.Add(new WaybillData()
                {
                    Id              = i+3,
                    EwbNo           = (750088881050+i).ToString(),
                    SiteName        = siteNames[random.Next(0, siteNames.Length-1)],
                    SendDate        = DateTime.Now.AddDays(-1000).AddDays(i/100),
                    SendCustomer    = names[random.Next(0,names.Length)],
                    SendPhone       = "1" + p[random.Next(0,5)] + random.Next(000000000,999999999).ToString(),
                    SendAddress     = address[random.Next(0, address.Length)],
                    ReceiveCustomer = names[random.Next(0, names.Length)],
                    ReceivePhone    = "1" + p[random.Next(0, 5)] + random.Next(000000000, 999999999).ToString(),
                    ReceiveAddress  = address[random.Next(0, address.Length)],
                    Weight          = 135.35m,
                    Volme           = 0.83m,
                });
            }
            return result;
        }
    }
}
