using System;
using System.Collections.Generic;
using System.Text;

namespace TMTTimeKeeper.Models
{
    public class ReadLogResult
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public IEnumerable<ReadLogResultData> Data { get; set; } = new List<ReadLogResultData>();
    }

    public class ReadLogResultData
    {
        /// <summary>
        /// user id
        /// </summary>
        public string UserId { get; set; } 
        /// <summary>
        /// ngày chấm công
        /// </summary>
        public string VerifyDate { get; set; } 
        public DateTime Date { get; set; }
        /// <summary>
        /// chễ độ xác minh: ngón tay, mật khẩu, id card
        /// </summary>
        public int VerifyType { get; set; }
        /// <summary>
        /// kiểu chấm công:  0-Check-In Default 1-Check-Out 2-Break-Out 3-Break-In 4-OT-In 5-OT-Ou
        /// </summary>
        public int VerifyState { get; set; } 

        public int WorkCode { get; set; }
    }
}
