using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMTTimeKeeper.Models
{
    public class TimeKeeperSave
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string IPAddress { get; set; }
        public string TCPPort { get; set; }
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
    }
}
