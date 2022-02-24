using OddCommon.Messaging;


namespace OddCommon.Network.SocketIO
{
    [MessagingInterface]
    public interface ISocketIO : ISocketIOConnected, ISocketIOSend, ISocketIOEventReceived, ISocketIODisconnected
    {
        
    }
}