using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Model
{
    public class ResultModel
    {
        public ResultModel()
        {
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
