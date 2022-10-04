using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GrpcExample.ViewModels {

    /// <summary>
    /// 主窗口菜单绑定模型
    /// </summary>
    public class MainWindowModel {

        //public ICommand QueryCmd { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        public MainWindowModel() {
            StartDate = new(2022, 9, 1);
            EndDate   = DateTime.Now;
        }
    }
}
