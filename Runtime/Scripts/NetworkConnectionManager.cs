using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using OddCommon.Debug;
using OddCommon.Lifecycle;
using OddCommon.Messaging;


namespace OddCommon.Network
{
    public class NetworkConnectionManager : MessagingBehaviourSingle<NetworkConnectionManager>, ILifecycle
    {
        #region Fields
        #region Inspector
        [Header("NetworkConnectionManager")]
        [SerializeField] [Range(0, 86400)] protected int networkTestFrequencyInSeconds = 0;
        [SerializeField] protected string[] networkURLs;
        #endregion //Inspector
        
        #region Public
        public enum NetworkConnectionState
        {
            Initial,
            Disconnected,
            Connected
        }
        #endregion //Public

        #region Private
        private List<UnityWebRequest> networkWebRequests;
        private WaitForSecondsRealtime pollDelay;
        private NetworkConnectionState currentConnectionsState;
        private enum NetworkConnectionManagerState
        {
            Inactive,
            CheckingConnectionState,
            PollingDelay
        }
        private NetworkConnectionManagerState currentState;
        #endregion //Private
        #endregion //Fields

        #region Methods
        #region Unity Messages
        private void Start()
        {
            Assert.IsNotNull(this.messagingManager);
            
            this.currentConnectionsState = NetworkConnectionState.Initial;
            this.currentState = NetworkConnectionManagerState.Inactive;
            this.pollDelay =
                this.networkTestFrequencyInSeconds > 0.0f ? 
                    new WaitForSecondsRealtime(this.networkTestFrequencyInSeconds) : 
                    null;
        }

        private void OnValidate()
        {
            this.pollDelay = this.networkTestFrequencyInSeconds > 0.0f ? 
                             new WaitForSecondsRealtime(this.networkTestFrequencyInSeconds) :
                             null;
        }
        #endregion //Unity Messages

        #region ILifecycle
        public void LifecycleStart(float timeSinceStartup)
        {
            if (this.currentState == NetworkConnectionManagerState.Inactive)
            {
                this.networkWebRequests = 
                    new List<UnityWebRequest>(this.networkURLs == null ? 0 : this.networkURLs.Length);
                #if UNITY_EDITOR || UNITY_STANDALONE
                this.StartCoroutine(this.PollNetworkStatus());
                #endif
            }
        }

        public void LifecycleForeground(float timestamp)
        {
            #if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            if (this.currentState == NetworkConnectionManagerState.Inactive)
            {
                this.StartCoroutine(this.PollNetworkStatus());
            }
            #endif
        }
        
        public void LifecycleBackground(float timestamp)
        {
            #if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            this.StopAllCoroutines();
            this.DestroyUnityWebRequests();
            this.currentState = NetworkConnectionManagerState.Inactive;
            this.ChangeNetworkConnectionState(false);
            #endif
        }

        public void LifecycleQuit(float timestamp)
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            this.StopAllCoroutines();
            this.DestroyUnityWebRequests();
            this.currentState = NetworkConnectionManagerState.Inactive;
            this.ChangeNetworkConnectionState(false);
            #endif
            this.networkWebRequests = null;
        }
        #endregion //ILifecycle

        #region Internal Methods
        private IEnumerator PollNetworkStatus()
        {
            while (true)
            {
                switch (this.currentState)
                {
                    case NetworkConnectionManagerState.Inactive:
                        this.InitializeUnityWebRequests();
                        for (int i = 0; i < this.networkWebRequests.Count; i++)
                        {
                            this.networkWebRequests[i].SendWebRequest();
                        }
                        this.currentState = NetworkConnectionManagerState.CheckingConnectionState;
                        break;
                    case NetworkConnectionManagerState.CheckingConnectionState:
                        bool allRequestsFinished = this.networkWebRequests.Count == 0;
                        foreach (UnityWebRequest request in this.networkWebRequests)
                        {
                            allRequestsFinished &= request.isDone;
                        }
                        if (allRequestsFinished)
                        {
                            bool isConnected = this.networkWebRequests.Count == 0;
                            foreach (UnityWebRequest request in this.networkWebRequests)
                            {
                                isConnected |= !request.isNetworkError;
                            }
                            this.DestroyUnityWebRequests();
                            this.ChangeNetworkConnectionState(isConnected);
                            this.currentState = NetworkConnectionManagerState.PollingDelay;
                        }
                        break;
                    case NetworkConnectionManagerState.PollingDelay:
                        yield return pollDelay;
                        this.currentState = NetworkConnectionManagerState.Inactive;
                        break;
                }
                yield return null;
            }	
        }
        
        private void InitializeUnityWebRequests()
        {
            for (int i = 0; i < this.networkURLs.Length; i++)
            {
                UnityWebRequest request = UnityWebRequest.Head(this.networkURLs[i]);
                request.timeout = 5;
                this.networkWebRequests.Add(request);
            }
        }

        private void DestroyUnityWebRequests()
        {
            foreach (UnityWebRequest request in this.networkWebRequests)
            {
                request.Abort();
                request.Dispose();
            }
            this.networkWebRequests.Clear();
        }

        private void ChangeNetworkConnectionState(bool isConnected)
        {   
            switch (this.currentConnectionsState)
            {
                case NetworkConnectionState.Initial:
                    if (isConnected)
                    {
                        Logging.Log
                        (
                            "[{0}] Network connected on startup.",
                            nameof(NetworkConnectionManager)
                        );
                        this.messagingManager.NetworkConnectionStatusChangedEvent(true);
                        this.currentConnectionsState = NetworkConnectionState.Connected;
                    }
                    else
                    {
                        Logging.Log
                        (
                            "[{0}] Network disconnected on startup.",
                            nameof(NetworkConnectionManager)
                        );
                        this.messagingManager.NetworkConnectionStatusChangedEvent(false);
                        this.currentConnectionsState = NetworkConnectionState.Disconnected;
                    }
                    break;
                case NetworkConnectionState.Connected:
                    if (!isConnected)
                    {
                        Logging.Log
                        (
                            "[{0}] Network changed to disconnected.",
                            nameof(NetworkConnectionManager)
                        );
                        this.messagingManager.NetworkConnectionStatusChangedEvent(false);
                        this.currentConnectionsState = NetworkConnectionState.Disconnected;
                    }
                    break;
                case NetworkConnectionState.Disconnected:
                    if (isConnected)
                    {
                        Logging.Log
                        (
                            "[{0}] Network changed to connected",
                            nameof(NetworkConnectionManager)
                        );
                        this.messagingManager.NetworkConnectionStatusChangedEvent(true);
                        this.currentConnectionsState = NetworkConnectionState.Connected;
                    }
                    break;
            }
        }
        #endregion //Internal Methods
        #endregion //Methods
    }
}