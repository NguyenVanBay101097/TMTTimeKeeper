namespace TMTTimeKeeper.Interface
{
    public interface ICzkemHelper
    {
        bool GetConnectState();
        int Connect(string ip, string port);
    }
}
