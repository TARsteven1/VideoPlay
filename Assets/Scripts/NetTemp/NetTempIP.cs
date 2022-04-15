using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror
{
    public class NetTempIP : MonoBehaviour
    {
         NetworkManager manager;
        string LocalIP;
        public bool isClient;
        public Text tip;
        //public GameObject c;
        // Start is called before the first frame update
        void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }
        void Start()
        {
            if (isClient)
            {
                if (!NetworkClient.isConnected && !NetworkServer.active)
                {

                    StartClientBtn();
                }
                else
                {
                    StatusLabels();
                }
            }
            else
            {

            if (!NetworkClient.isConnected && !NetworkServer.active)
            {
                
                StartHostBtn();
            }
                else {
                
                    StatusLabels();
                }
            }
      
        }

        // Update is called once per frame
        //void Update()
        //{
        //    if (!NetworkClient.isConnected && !NetworkServer.active)
        //    {
        //        StartButtons();
        //    }
        //    else
        //    {
        //        StatusLabels();
        //    }

        //    // client ready
        //    if (NetworkClient.isConnected && !NetworkClient.ready)
        //    {
        //        if (GUILayout.Button("Client Ready"))
        //        {
        //            NetworkClient.Ready();
        //            if (NetworkClient.localPlayer == null)
        //            {
        //                NetworkClient.AddPlayer();
        //            }
        //        }
        //    }

        //    StopButtons();
        //}
        public string GetLocalIp()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    LocalIP = AddressIP;
                }
            }
            return AddressIP;

        }
        void StatusLabels()
        {
            // host mode
            // display separately because this always confused people:
            //   Server: ...
            //   Client: ...
            if (NetworkServer.active && NetworkClient.active)
            {
                //GUILayout.Label($"<b>Host</b>: running via {Transport.activeTransport}");
                tip.text = $"<b>Host</b>: running via {Transport.activeTransport}";
                // Debug.Log($"<b>Host</b>: running via {Transport.activeTransport}");
            }
            // server only
            else if (NetworkServer.active)
            {
                //Debug.Log($"<b>Server</b>: running via {Transport.activeTransport}");
                tip.text = $"<b>Server</b>: running via {Transport.activeTransport}";
            }
            // client only
            else if (NetworkClient.isConnected)
            {
                tip.text = $"<b>Client</b>: connected to {manager.networkAddress} via {Transport.activeTransport}";
            }
        }
         void StartButtons()
        {
            if (!NetworkClient.active)
            {
                // Server + Client
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    if (GUILayout.Button("Host (Server + Client)"))
                    {
                        manager.StartHost();
                    }
                }

                // Client + IP
                //GUILayout.BeginHorizontal();
                if (GUILayout.Button("Client"))
                {
                    manager.StartClient();
                }
                //指定读取IP
                manager.networkAddress = GUILayout.TextField(manager.networkAddress);
               // GUILayout.EndHorizontal();

                // Server Only
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    // cant be a server in webgl build
                    GUILayout.Box("(  WebGL cannot be server  )");
                }
                else
                {
                    if (GUILayout.Button("Server Only")) manager.StartServer();
                }
            }
            else
            {
                // Connecting
                GUILayout.Label($"Connecting to {manager.networkAddress}..");
                if (GUILayout.Button("Cancel Connection Attempt"))
                {
                    manager.StopClient();
                }
            }
        }
          void StopButtons()
        {
            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Host"))
                {
                    manager.StopHost();
                }
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Client"))
                {
                    manager.StopClient();
                }
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop Server"))
                {
                    manager.StopServer();
                }
            }
        }
        public void StartHostBtn()
        {
            if (!NetworkClient.active)
            {
                manager.StartHost();
                tip.text = "服务器已开启";
                //manager.StartServer();
            }
            }
        public void StopHostBtn() {
            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                manager.StopHost();
                //manager.StopServer();
            }
            }
        public void StartClientBtn()
        {
            if (!NetworkClient.active)
            {
                             
                showIP();
                manager.StartClient();
                if (manager.isNetworkActive)
                {
                    tip.text = "已连接";
                }
                else
                {
                    tip.text = "等待服务器开启";
                }
                //manager.networkAddress = manager.networkAddress;
            }

        }        
        public void StopClientBtn()
        {
            if (NetworkClient.isConnected)
            {
                
                    manager.StopClient();
                
            }
        }
    public void showIP()
        {
            manager.networkAddress = LocalIP;
            Debug.Log(manager.networkAddress);
        }
        public void reseriveIP(string ip)
        {

            LocalIP = ip;
        }

    }
   
}
