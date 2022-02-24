using OddCommon.Messaging;


namespace OddCommon.Network.SocketIO
{
    [MessagingInterface]
    public interface ISocketIOConnected
    {
        void SocketIOConnected(float realtimeSinceStartup);
    }
}