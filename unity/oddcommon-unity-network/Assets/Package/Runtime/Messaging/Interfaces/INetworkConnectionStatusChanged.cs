using OddCommon.Messaging;


namespace OddCommon.Network
{
    [MessagingInterface]
    public interface INetworkConnectionStatusChanged
    {
        void NetworkConnectionStatusChanged(bool isConnected);
    }
}