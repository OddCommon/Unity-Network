using OddCommon.Messaging;


namespace OddCommon.Network.SocketIO
{
    [MessagingInterface]
    public interface ISocketIODisconnected
    {
        void SocketIODisconnected(float realtimeSinceStartup);
    }
}