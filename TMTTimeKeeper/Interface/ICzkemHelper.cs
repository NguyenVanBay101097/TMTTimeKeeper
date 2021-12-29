using TMTTimeKeeper.Models;

namespace TMTTimeKeeper.Interface
{
    public interface ICzkemHelper
    {
        bool GetConnectState();
        bool Connect(string ip, string port);
        public string GetSeriNumber(int machineNumber);
        ReadLogResult ReadTimeGLogData(int machineNumber, string fromTime, string toTime);
    }
}
