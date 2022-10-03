using GrpcLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;
using System.Resources;

namespace GrpcServer.Services {
    /// <summary>
    /// 运单信息查询服务
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WaybillInfoService : IWaybillInfoService {
        public ValueTask<ResponseMessage<List<WaybillData>>> QueryWaybill(QueryWaybillRequest request, CallContext context) {
            Console.WriteLine( $"用户：{1} 执行运单查询，IP：{context.ServerCallContext?.Peer}");
            var result = new ResponseMessage<List<WaybillData>>();
            //获取测试数据
            var data = GetWaybillDatas();
            result.IsSuccess = true;
            result.StatusCode = System.Net.HttpStatusCode.OK;
            result.Message = "查询成功";
            result.Result = data.Where(s=> s.SendDate >= request.StartDate && s.SendDate <= request.EndDate).ToList();
            return new ValueTask<ResponseMessage<List<WaybillData>>>(result);
        }

        /// <summary>
        /// 获取运单信息测试数据
        /// </summary>
        /// <returns></returns>
        private static List<WaybillData> GetWaybillDatas() {
            var result = new List<WaybillData>();
            result.Add(new WaybillData()
            {
                Id = 0,
                EwbNo = "750088880001",
                SendDate = new(2022,8,1),
                SendCustomer = "黄小姐",
                SendPhone = "13888888888",
                SendAddress = "浙江省杭州市桐庐县XX路XX号",
                ReceiveCustomer = "李先生",
                ReceivePhone = "15999999999",
                ReceiveAddress = "广东省深圳市宝安区XX路XX号",
                Weight = 85.35m,
                Volme = 0.35m,
                Remark = "不送货上门拒收"
            });
            result.Add(new WaybillData()
            {
                Id = 0,
                EwbNo = "750088880035",
                SendDate = new(2022, 8, 3),
                SendCustomer = "张无忌",
                SendPhone = "13888886666",
                SendAddress = "湖北省荆州市荆州区区东城街道详细北京西路518号",
                ReceiveCustomer = "李寻欢",
                ReceivePhone = "15966666666",
                ReceiveAddress = "广东省广州是白云区XX路XX号",
                Weight = 200m,
                Volme = 0.8m,
                Remark = "不送货上门拒收"
            });
            result.Add(new WaybillData()
            {
                Id = 0,
                EwbNo = "750088880055",
                SendDate = new(2022, 8, 6),
                SendCustomer = "特朗普",
                SendPhone = "13833331111",
                SendAddress = "四川省成都市温江区天府街道天府街道天府镇维新南街42号",
                ReceiveCustomer = "奥巴马",
                ReceivePhone = "15955557777",
                ReceiveAddress = "上海市松江区九亭镇潮富路8号",
                Weight = 135.35m,
                Volme = 0.83m,
                Remark = "不送货上门拒收"
            });

            var names = MyResource.Names.Split("\r\n");
            var address = MyResource.Address.Split("\r\n");
            var p = new int[] { 3, 5, 7, 8, 9 };
            var random = new Random();
            for (int i = 0; i < 1000; i++) {
                result.Add(new WaybillData()
                {
                    Id = i+3,
                    EwbNo = (750088881050+i).ToString(),
                    SendDate = new(2022, 3, 1 + i/10),
                    SendCustomer = names[random.Next(0,names.Length)],
                    SendPhone = "1" + p[random.Next(0,5)] + random.Next(000000000,999999999).ToString(),
                    SendAddress = address[random.Next(0, address.Length)],
                    ReceiveCustomer = names[random.Next(0, names.Length)],
                    ReceivePhone = "1" + p[random.Next(0, 5)] + random.Next(000000000, 999999999).ToString(),
                    ReceiveAddress = address[random.Next(0, address.Length)],
                    Weight = 135.35m,
                    Volme = 0.83m,
                });
            }
            return result;
        }
    }
}
