using System.Runtime.Serialization;

namespace GrpcLibrary {
    /// <summary>
    /// 运单数据
    /// </summary>
    [DataContract]
    public class WaybillData {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [DataMember(Order = 2)]
        public string EwbNo { get; set; }

        /// <summary>
        /// 寄件日期
        /// </summary>
        [DataMember(Order = 3)]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 发件地址
        /// </summary>
        [DataMember(Order = 4)]
        public string SendAddress { get; set; }

        /// <summary>
        /// 发件客户
        /// </summary>
        [DataMember(Order = 5)]
        public string SendCustomer { get; set; }

        /// <summary>
        /// 发件电话
        /// </summary>
        [DataMember(Order = 6)]
        public string SendPhone { get; set; }

        /// <summary>
        /// 收件客户
        /// </summary>
        [DataMember(Order = 7)]
        public string ReceiveCustomer { get; set; }

        /// <summary>
        /// 收件电话
        /// </summary>
        [DataMember(Order = 8)]
        public string ReceivePhone { get; set; }

        /// <summary>
        /// 收件地址
        /// </summary>
        [DataMember(Order = 9)]
        public string ReceiveAddress { get; set; }

        /// <summary>
        /// 结算重量
        /// </summary>
        [DataMember(Order = 10)]
        public decimal Weight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        [DataMember(Order = 11)]
        public decimal Volme { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember(Order = 12)]
        public string? Remark { get; set; }


        public WaybillData() {
            //Id            = 0;
            EwbNo           = string.Empty;
            //SendDate      = null!;
            SendAddress     = string.Empty;
            SendCustomer    = string.Empty;
            SendPhone       = string.Empty;
            ReceiveCustomer = string.Empty;
            ReceivePhone    = string.Empty;
            ReceiveAddress  = string.Empty;
            Weight          = 0m;
            Volme           = 0m;
            //Remark        = null;
        }
    }
}