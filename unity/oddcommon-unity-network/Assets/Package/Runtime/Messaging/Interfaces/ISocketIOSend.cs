using System.Collections.Generic;
using OddCommon.Messaging;


namespace OddCommon.Network
{
    [MessagingInterface]
    public interface ISocketIOSend
    {
        void SocketIOSend(float realtimeSinceStartup, string eventName, Dictionary<string, object> data);
    }
}