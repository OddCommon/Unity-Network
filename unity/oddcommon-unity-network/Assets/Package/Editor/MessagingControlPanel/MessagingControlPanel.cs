#if UNITY_EDITOR
// MessagingControlPanel.cs
// Auto-Generated 3/25/2021 9:47:59 PM
using UnityEngine;
using UnityEditor;
using OddCommon;
using OddCommon.Debug;
using OddCommon.Messaging;
using OddCommon.Messaging.Editor;
using System;
using System.Collections.Generic;



namespace OddCommon.Network.Editor
{
    public class MessagingControlPanel : EditorWindow
    {
        #region Class
        #region Methods
        [MenuItem("OddCommon/Messaging/Network Messaging Control Panel", false, 1)]
        internal static void ControlPanelWindow()
        {
            OddCommon.Network.Editor.MessagingControlPanel lifecycleControlPanel =
                (OddCommon.Network.Editor.MessagingControlPanel) EditorWindow.GetWindow
                (
                    typeof(OddCommon.Network.Editor.MessagingControlPanel),
                    false,
                    "Lifecycle Messaging Control Panel",
                    true
                );
            if (lifecycleControlPanel != null)
            {
                lifecycleControlPanel.Show();
            }
        }
        #endregion //Methods
        #endregion //Class

        #region Instance
        #region Fields
        #region Private
        #region Editor Window
        private Vector2 guiScrollPosition = Vector2.zero;
        #endregion //Editor Window

        #region Message Parameters
        #region INetworkConnectionStatusChanged Parameters
        private bool foldoutToggleINetworkConnectionStatusChanged = false;
        [SerializeField] private Boolean NetworkConnectionStatusChangedisConnected;
        #endregion //INetworkConnectionStatusChanged Parameters

        #region ISocketIOSend Parameters
        private bool foldoutToggleISocketIOSend = false;
        [SerializeField] private Single SocketIOSendrealtimeSinceStartup;
        [SerializeField] private String SocketIOSendeventName;
        [SerializeField] private Dictionary<string, System.Object> SocketIOSenddata;
        #endregion //ISocketIOSend Parameters

        #region ISocketIOConnected Parameters
        private bool foldoutToggleISocketIOConnected = false;
        [SerializeField] private Single SocketIOConnectedrealtimeSinceStartup;
        #endregion //ISocketIOConnected Parameters

        #region ISocketIODisconnected Parameters
        private bool foldoutToggleISocketIODisconnected = false;
        [SerializeField] private Single SocketIODisconnectedrealtimeSinceStartup;
        #endregion //ISocketIODisconnected Parameters

        #region ISocketIOEventReceived Parameters
        private bool foldoutToggleISocketIOEventReceived = false;
        [SerializeField] private Single SocketIOEventReceivedrealtimeSinceStartup;
        [SerializeField] private String SocketIOEventReceivedeventName;
        [SerializeField] private Dictionary<string, System.Object> SocketIOEventReceiveddata;
        #endregion //ISocketIOEventReceived Parameters

        #endregion //Message Parameters
        #endregion //Private
        #endregion //Fields

        #region Methods
        #region Unity Messages
        private void OnEnable()
        {
            this.guiScrollPosition = Vector2.zero;
        }

        private void OnGUI()
        {
            SerializedObject serializedEditorWindow = new SerializedObject(this as ScriptableObject);
            this.guiScrollPosition = EditorGUILayout.BeginScrollView(this.guiScrollPosition);
            MessagingManager messagingManager = GameObject.FindObjectOfType<MessagingManager>();

            this.foldoutToggleINetworkConnectionStatusChanged = EditorGUILayout.BeginFoldoutHeaderGroup(this.foldoutToggleINetworkConnectionStatusChanged, "INetworkConnectionStatusChanged");
            if(this.foldoutToggleINetworkConnectionStatusChanged)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("INetworkConnectionStatusChanged", MessageType.Info);
                SerializedProperty serialized_NetworkConnectionStatusChangedisConnected = serializedEditorWindow.FindProperty("NetworkConnectionStatusChangedisConnected");
                EditorGUILayout.PropertyField(serialized_NetworkConnectionStatusChangedisConnected, new GUIContent("isConnected"), true);;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * MessagingConstants.controlPanelButtonsOffset);
                if (Application.isPlaying & GUILayout.Button("Fire NetworkConnectionStatusChanged Event "))
                {
                    if (messagingManager != null)
                    {
                        messagingManager.NetworkConnectionStatusChangedEvent(this.NetworkConnectionStatusChangedisConnected);
                    }
                    else
                    {
                        Logging.Warn
                        (
                            "[{0}] MessagingManager must exist in the scene to fire message from control panel!",
                            nameof(MessagingControlPanelGenerator)
                        );
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            this.foldoutToggleISocketIOSend = EditorGUILayout.BeginFoldoutHeaderGroup(this.foldoutToggleISocketIOSend, "ISocketIOSend");
            if(this.foldoutToggleISocketIOSend)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("ISocketIOSend", MessageType.Info);
                SerializedProperty serialized_SocketIOSendrealtimeSinceStartup = serializedEditorWindow.FindProperty("SocketIOSendrealtimeSinceStartup");
                EditorGUILayout.PropertyField(serialized_SocketIOSendrealtimeSinceStartup, new GUIContent("realtimeSinceStartup"), true);;
                SerializedProperty serialized_SocketIOSendeventName = serializedEditorWindow.FindProperty("SocketIOSendeventName");
                EditorGUILayout.PropertyField(serialized_SocketIOSendeventName, new GUIContent("eventName"), true);;
                SerializedProperty serialized_SocketIOSenddata = serializedEditorWindow.FindProperty("SocketIOSenddata");
                EditorGUILayout.PropertyField(serialized_SocketIOSenddata, new GUIContent("data"), true);;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * MessagingConstants.controlPanelButtonsOffset);
                if (Application.isPlaying & GUILayout.Button("Fire SocketIOSend Event "))
                {
                    if (messagingManager != null)
                    {
                        messagingManager.SocketIOSendEvent(this.SocketIOSendrealtimeSinceStartup,this.SocketIOSendeventName,this.SocketIOSenddata);
                    }
                    else
                    {
                        Logging.Warn
                        (
                            "[{0}] MessagingManager must exist in the scene to fire message from control panel!",
                            nameof(MessagingControlPanelGenerator)
                        );
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            this.foldoutToggleISocketIOConnected = EditorGUILayout.BeginFoldoutHeaderGroup(this.foldoutToggleISocketIOConnected, "ISocketIOConnected");
            if(this.foldoutToggleISocketIOConnected)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("ISocketIOConnected", MessageType.Info);
                SerializedProperty serialized_SocketIOConnectedrealtimeSinceStartup = serializedEditorWindow.FindProperty("SocketIOConnectedrealtimeSinceStartup");
                EditorGUILayout.PropertyField(serialized_SocketIOConnectedrealtimeSinceStartup, new GUIContent("realtimeSinceStartup"), true);;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * MessagingConstants.controlPanelButtonsOffset);
                if (Application.isPlaying & GUILayout.Button("Fire SocketIOConnected Event "))
                {
                    if (messagingManager != null)
                    {
                        messagingManager.SocketIOConnectedEvent(this.SocketIOConnectedrealtimeSinceStartup);
                    }
                    else
                    {
                        Logging.Warn
                        (
                            "[{0}] MessagingManager must exist in the scene to fire message from control panel!",
                            nameof(MessagingControlPanelGenerator)
                        );
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            this.foldoutToggleISocketIODisconnected = EditorGUILayout.BeginFoldoutHeaderGroup(this.foldoutToggleISocketIODisconnected, "ISocketIODisconnected");
            if(this.foldoutToggleISocketIODisconnected)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("ISocketIODisconnected", MessageType.Info);
                SerializedProperty serialized_SocketIODisconnectedrealtimeSinceStartup = serializedEditorWindow.FindProperty("SocketIODisconnectedrealtimeSinceStartup");
                EditorGUILayout.PropertyField(serialized_SocketIODisconnectedrealtimeSinceStartup, new GUIContent("realtimeSinceStartup"), true);;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * MessagingConstants.controlPanelButtonsOffset);
                if (Application.isPlaying & GUILayout.Button("Fire SocketIODisconnected Event "))
                {
                    if (messagingManager != null)
                    {
                        messagingManager.SocketIODisconnectedEvent(this.SocketIODisconnectedrealtimeSinceStartup);
                    }
                    else
                    {
                        Logging.Warn
                        (
                            "[{0}] MessagingManager must exist in the scene to fire message from control panel!",
                            nameof(MessagingControlPanelGenerator)
                        );
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            this.foldoutToggleISocketIOEventReceived = EditorGUILayout.BeginFoldoutHeaderGroup(this.foldoutToggleISocketIOEventReceived, "ISocketIOEventReceived");
            if(this.foldoutToggleISocketIOEventReceived)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("ISocketIOEventReceived", MessageType.Info);
                SerializedProperty serialized_SocketIOEventReceivedrealtimeSinceStartup = serializedEditorWindow.FindProperty("SocketIOEventReceivedrealtimeSinceStartup");
                EditorGUILayout.PropertyField(serialized_SocketIOEventReceivedrealtimeSinceStartup, new GUIContent("realtimeSinceStartup"), true);;
                SerializedProperty serialized_SocketIOEventReceivedeventName = serializedEditorWindow.FindProperty("SocketIOEventReceivedeventName");
                EditorGUILayout.PropertyField(serialized_SocketIOEventReceivedeventName, new GUIContent("eventName"), true);;
                SerializedProperty serialized_SocketIOEventReceiveddata = serializedEditorWindow.FindProperty("SocketIOEventReceiveddata");
                EditorGUILayout.PropertyField(serialized_SocketIOEventReceiveddata, new GUIContent("data"), true);;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * MessagingConstants.controlPanelButtonsOffset);
                if (Application.isPlaying & GUILayout.Button("Fire SocketIOEventReceived Event "))
                {
                    if (messagingManager != null)
                    {
                        messagingManager.SocketIOEventReceivedEvent(this.SocketIOEventReceivedrealtimeSinceStartup,this.SocketIOEventReceivedeventName, this.SocketIOEventReceiveddata);
                    }
                    else
                    {
                        Logging.Warn
                        (
                            "[{0}] MessagingManager must exist in the scene to fire message from control panel!",
                            nameof(MessagingControlPanelGenerator)
                        );
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.EndScrollView();
            serializedEditorWindow.ApplyModifiedProperties();
        }
        #endregion //Unity Messages
        #endregion //Methods
        #endregion //Instance
    }
}
#endif //UNITY_EDITOR
