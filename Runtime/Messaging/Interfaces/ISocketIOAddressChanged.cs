using OddCommon.Messaging;


namespace OddCommon.Network.SocketIO
{
    [MessagingInterface]
    public interface ISocketIOAddressChanged
    {
        void SocketIOAddressChanged(float realtimeSinceStartup, string address, string port, string path);
    }
}