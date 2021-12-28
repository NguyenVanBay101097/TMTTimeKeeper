using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMTTimeKeeper.Models
{
    public class PagedResult2<T>
    {
        public PagedResult2(long totalItems, long offset, long limit)
        {
            TotalItems = totalItems;
            Offset = offset;
            Limit = limit;
        }

        public long Offset { get; private set; }
        public long Limit { get; private set; }
        public long TotalItems { get; private set; }
        public IEnumerable<T> Items { get; set; }
    }

    public class TimeKeeperSave
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string IPAddress { get; set; }
        public string TCPPort { get; set; }
    }

    public class TimeKeeperDisplay
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string IPAddress { get; set; }
        public string TCPPort { get; set; }
        public string SeriNumber { get; set; }
        public Guid? CompanyId { get; set; }
    }
    public class TimeKeeperConnectReq
    {
        public string IPAddress { get; set; }
        public string TCPPort { get; set; }
    }

    public class ReadTimeGLogDataReq
    {
        public DateTime DateFrom { get;set; }
        public DateTime DateTo { get;set; }
        public string IPAddress { get; set; }
        public string TCPPort { get; set; }
        public Guid DeviceId { get; set; }
    }

    public class TimeKeeperSyncDataReq
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string IPAddress { get; set; }
        public string TCPPort { get; set; }
    }

    public class TimeKeeperPaged
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Search { get; set; }
    }
}
