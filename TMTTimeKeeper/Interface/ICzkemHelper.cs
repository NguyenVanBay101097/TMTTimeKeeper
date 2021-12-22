namespace TMTTimeKeeper.Interface
{
    public interface ICzkemHelper
    {
        bool GetConnectState();
        bool Connect(string ip, string port);
    }
}
