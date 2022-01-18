using System;
using System.Collections.Generic;

namespace TMTTimeKeeper.Models.ApiRequestModels
{
    public class GetAttendanceSyncReq
    {
        public Guid? MachineId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string State { get; set; }
    }
    public class TimeAttendanceSyncLogDisplay
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public Guid? MachineId { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime AttendanceTime { get; set; }
    }

    public class AttenDanceSyncDataReq
    {
        public Guid? MachineId { get; set; }
        public IEnumerable<ReadLogResultData> Data { get; set; } = new List<ReadLogResultData>();
    }
}
