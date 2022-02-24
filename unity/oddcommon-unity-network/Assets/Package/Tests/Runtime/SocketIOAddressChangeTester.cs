using UnityEngine;
using OddCommon.Messaging;


namespace OddCommon.Network.Test
{
    public class SocketIOAddressChangeTester : MessagingBehaviour, INetworkConnectionStatusChanged
    {
        #region Methods
        #region INetworkConnectionStatusChanged
        public void NetworkConnectionStatusChanged(bool isConnected)
        {
            this.messagingManager.SocketIOAddressChangedEvent
            (
                Time.realtimeSinceStartup,
                "http://bmc-dev-145464832.us-east-2.elb.amazonaws.com",
                "80",
                "socket.io/"
            );
        }
        #endregion //INetworkConnectionStatusChanged
        #endregion //Methods
    }
}