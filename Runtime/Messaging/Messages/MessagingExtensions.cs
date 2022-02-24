// MessagingExtensions.cs
// Auto-Generated 4/26/2021 6:15:15 PM
using System.Collections.Generic;
using OddCommon.Messaging;
using OddCommon.Network;
using OddCommon.Network.SocketIO;


public static class MessagingExtensions
{
    #region ISocketIOAddressChanged
    public static void SocketIOAddressChangedEvent(this OddCommon.Messaging.MessagingManager messagingManager, System.Single realtimeSinceStartup, System.String address, System.String port, System.String path)
    {
        List<ISocketIOAddressChanged> allListeners = messagingManager.GetRegisteredListeners<ISocketIOAddressChanged>("SocketIOAddressChanged");
        foreach (ISocketIOAddressChanged listener in allListeners)
        {
            listener.SocketIOAddressChanged(realtimeSinceStartup, address, port, path);
        }
    }
    #endregion

    #region INetworkConnectionStatusChanged
    public static void NetworkConnectionStatusChangedEvent(this OddCommon.Messaging.MessagingManager messagingManager, System.Boolean isConnected)
    {
        List<INetworkConnectionStatusChanged> allListeners = messagingManager.GetRegisteredListeners<INetworkConnectionStatusChanged>("NetworkConnectionStatusChanged");
        foreach (INetworkConnectionStatusChanged listener in allListeners)
        {
            listener.NetworkConnectionStatusChanged(isConnected);
        }
    }
    #endregion

    #region ISocketIOSend
    public static void SocketIOSendEvent(this OddCommon.Messaging.MessagingManager messagingManager, System.Single realtimeSinceStartup, System.String eventName, System.Collections.Generic.Dictionary<System.String, System.Object> data)
    {
        List<ISocketIOSend> allListeners = messagingManager.GetRegisteredListeners<ISocketIOSend>("SocketIOSend");
        foreach (ISocketIOSend listener in allListeners)
        {
            listener.SocketIOSend(realtimeSinceStartup, eventName, data);
        }
    }
    #endregion

    #region ISocketIOConnected
    public static void SocketIOConnectedEvent(this OddCommon.Messaging.MessagingManager messagingManager, System.Single realtimeSinceStartup)
    {
        List<ISocketIOConnected> allListeners = messagingManager.GetRegisteredListeners<ISocketIOConnected>("SocketIOConnected");
        foreach (ISocketIOConnected listener in allListeners)
        {
            listener.SocketIOConnected(realtimeSinceStartup);
        }
    }
    #endregion

    #region ISocketIODisconnected
    public static void SocketIODisconnectedEvent(this OddCommon.Messaging.MessagingManager messagingManager, System.Single realtimeSinceStartup)
    {
        List<ISocketIODisconnected> allListeners = messagingManager.GetRegisteredListeners<ISocketIODisconnected>("SocketIODisconnected");
        foreach (ISocketIODisconnected listener in allListeners)
        {
            listener.SocketIODisconnected(realtimeSinceStartup);
        }
    }
    #endregion

    #region ISocketIOEventReceived
    public static void SocketIOEventReceivedEvent(this OddCommon.Messaging.MessagingManager messagingManager, System.Single realtimeSinceStartup, System.String eventName, System.Collections.Generic.Dictionary<System.String, System.Object> data)
    {
        List<ISocketIOEventReceived> allListeners = messagingManager.GetRegisteredListeners<ISocketIOEventReceived>("SocketIOEventReceived");
        foreach (ISocketIOEventReceived listener in allListeners)
        {
            listener.SocketIOEventReceived(realtimeSinceStartup, eventName, data);
        }
    }
    #endregion
}
