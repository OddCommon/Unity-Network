using System.Collections.Generic;
using OddCommon.Messaging;


namespace OddCommon.Network.SocketIO
{
    [MessagingInterface]
    public interface ISocketIOEventReceived
    {
        void SocketIOEventReceived(float realtimeSinceStartup, string eventName, Dictionary<string, object> data);
    }
}