using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.Logger;
using UnityEngine;
using BestHTTP.SocketIO;
using OddCommon.Debug;
using OddCommon.Messaging;
using OddCommon.Network.SocketIO;
using UnityEngine.Assertions;


namespace OddCommon.Network
{
    public class SocketIOClient : MessagingBehaviour, 
        ISocketIOAddressChanged, 
        INetworkConnectionStatusChanged, 
        ISocketIOSend
    {
        #region Fields
        #region Insepctor
        [Header("SocketIOClient")]
        [SerializeField] private string socketIOServerAddress;
        [SerializeField] private int socketIOServerPort;
        [SerializeField] private string socketIOServerPath;
        [SerializeField] private bool socketIOVerboseLogging = false;
        #endregion //Inspector
        
        #region Private
        private enum SocketIOClientState
        {
            Disconnected,
            Initializing,
            Connected
        }
        private SocketIOClientState currentState;
        private SocketManager socketManager;
        private Uri socketIOServerURI;
        #endregion //Private
        #endregion //Fields

        #region Methods
        #region Unity Messages
        private void Start()
        {
            Assert.IsNotNull(this.messagingManager);
            Assert.IsNotNull(this.socketIOServerAddress);
            
            this.currentState = SocketIOClientState.Disconnected;
        }

        protected override void OnDestroy()
        {
            switch (this.currentState)
            {
                case SocketIOClientState.Connected:
                case SocketIOClientState.Initializing:
                    this.DisconnectFromSocketIOServer();
                    goto default;
                default:
                    this.currentState = SocketIOClientState.Disconnected;
                    break;
            }
            base.OnDestroy();
        }
        #endregion //Unity Messages
        
        #region INetworkConnectionStatusChanged
        public void NetworkConnectionStatusChanged(bool isConnected)
        {
            switch (this.currentState)
            {
                case SocketIOClientState.Disconnected:
                    if (isConnected)
                    {
                        this.ConnectToSocketIOServer();
                        this.currentState = SocketIOClientState.Initializing;
                    }
                    break;
                case SocketIOClientState.Initializing:
                case SocketIOClientState.Connected:
                    if (!isConnected)
                    {
                        this.DisconnectFromSocketIOServer();
                        this.currentState = SocketIOClientState.Disconnected;
                    }
                    break;
            }
        }
        #endregion //INetworkConnectionStatusChanged

        #region ISocketIOAddressChanged
        public void SocketIOAddressChanged(float realtimeSinceStartup, string address, string port, string path)
        {
            switch (this.currentState)
            {
                case SocketIOClientState.Disconnected:
                    this.socketIOServerURI = this.CreateSocketServerURI(address, port, path);
                    break;
                case SocketIOClientState.Initializing:
                    this.DisconnectFromSocketIOServer();
                    this.socketIOServerURI = this.CreateSocketServerURI(address, port, path);
                    this.ConnectToSocketIOServer();
                    break;
                case SocketIOClientState.Connected:
                    this.currentState = SocketIOClientState.Initializing;
                    goto case SocketIOClientState.Initializing;
            }
        }
        #endregion //ISocketIOAddressChanged

        #region Private
        private Uri CreateSocketServerURI(string address, string port, string path)
        {
            string uriString = address;
            this.socketIOServerAddress = address;
            
            if (!String.IsNullOrEmpty(port))
            {
                uriString += ":" + port;
                this.socketIOServerPort = Int32.Parse(port);
            }

            if (!String.IsNullOrEmpty(path))
            {
                uriString += "/" + path;
                this.socketIOServerPath = path;
            }
            
            return new Uri(uriString);
        }

        private void ConnectToSocketIOServer()
        {
            if (this.socketIOServerURI != null)
            {
                Logging.Log("Connecting to WebSocket server.");
                if (this.socketIOVerboseLogging)
                {
                    HTTPManager.Logger.Level = Loglevels.All;
                }
                this.socketManager = new SocketManager(this.socketIOServerURI);
                this.socketManager.Socket.On(SocketIOEventTypes.Connect, this.SocketIOServerConnectCallback);
                this.socketManager.Socket.On(SocketIOEventTypes.Error, this.SocketIOServerErrorCallback);
                this.socketManager.Socket.On(SocketIOEventTypes.Event, this.SocketIOServerEventCallback);
                this.socketManager.Open();   
            }
        }

        private void DisconnectFromSocketIOServer()
        {
            if (this.socketManager != null)
            {
                this.socketManager.Socket.Off(SocketIOEventTypes.Connect, this.SocketIOServerConnectCallback);
                this.socketManager.Socket.Off(SocketIOEventTypes.Error, this.SocketIOServerErrorCallback);
                this.socketManager.Socket.Off(SocketIOEventTypes.Event, this.SocketIOServerEventCallback);
                this.socketManager.Close();
                this.socketManager = null;   
            }
        }
        #endregion //Private

        #region ISocketIOSend
        public void SocketIOSend(float realtimeSinceStartup, string eventName, Dictionary<string, object> data)
        {
            if (this.currentState == SocketIOClientState.Connected)
            {
                this.socketManager.Socket.Emit(eventName, data);   
            }
        }
        #endregion //ISocketIOSend

        #region Event Callbacks
        private void SocketIOServerConnectCallback(Socket socket, Packet packet, params object[] args)
        {
            if (this.currentState == SocketIOClientState.Initializing)
            {
                Logging.Log("WebSocket connection to {0} opened.", socket.Manager.Uri.OriginalString);
                this.currentState = SocketIOClientState.Connected;
                this.messagingManager.SocketIOConnectedEvent(Time.realtimeSinceStartup);   
            }
        }

        private void SocketIOServerErrorCallback(Socket socket, Packet packet, params object[] args)
        {
            Error error = args[0] as Error;
            switch (error.Code)
            {
                case SocketIOErrors.User:
                    Logging.Warn("SocketIO exception in an event handler: {0}", error.Message);
                    break;
                case SocketIOErrors.Internal:
                    Logging.Warn("SocketIO internal error: {0}", error.Message);
                    break;
                default:
                    Logging.Log("SocketIO server error: {0}", error.Message);
                    break;
            }
        }
        
        private void SocketIOServerEventCallback(Socket socket, Packet packet, params object[] args)
        {
            Logging.Log("SocketIO event name: {0}", packet.EventName);
            Dictionary<string, object> data = null;
            if (args.Length > 0)
            {
                data = args[0] as Dictionary<string, object>;
            }
            this.messagingManager.SocketIOEventReceivedEvent(Time.realtimeSinceStartup, packet.EventName, data);
        }

        private void SocketIOServerCloseCallback(Socket socket, Packet packet, params object[] args)
        {
            Logging.Log("WebSocket connection to {0} closed by server.", socket.Manager.Uri.OriginalString);
            this.DisconnectFromSocketIOServer();
            this.ConnectToSocketIOServer();
            this.currentState = SocketIOClientState.Initializing;
        }
        #endregion //Event Callbacks
        #endregion //Methods
    }
}