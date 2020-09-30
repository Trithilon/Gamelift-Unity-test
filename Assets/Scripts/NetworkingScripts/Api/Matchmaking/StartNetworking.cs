// using System;
// using System.Collections.Generic;
// using Api;
// using Cysharp.Threading.Tasks;
// using MLAPI;
// using Newtonsoft.Json;
// using UnityEngine;
// using ParrelSync;
// using Zenject;
//
// public class StartNetworking : MonoBehaviour {
//   public static StartNetworking Instance { get; private set; }
//
//   [SerializeField] public GameObject rpcObject;
//   private void OnEnable() {
//     if (Instance != null && Instance != this) {
//       Destroy(this.gameObject);
//     }
//     else {
//       Instance = this;
//     }
//   }
//
//   public void DisconnectFromServer() {
//     if (NetworkingManager.Singleton.IsHost) {
//       NetworkingManager.Singleton.StopHost();
//     }
//     else if (NetworkingManager.Singleton.IsClient) {
//       NetworkingManager.Singleton.StopClient();
//     }
//     else if (NetworkingManager.Singleton.IsServer) {
//       NetworkingManager.Singleton.StopServer();
//     }
//   }
//
//
//   public async UniTask ConnectToGameLiftServer() {
//     // GETTING MATCHMAKING (IP and port are hardcoded locally while testing MLAPI)
//     var playerSession = new PlayerSession {ipAddress = "127.0.0.1", port = 7777};
//     NetworkingManager.Singleton.GetComponent<EnetTransport.EnetTransport>().Address = playerSession.ipAddress;
//     NetworkingManager.Singleton.GetComponent<EnetTransport.EnetTransport>().Port = (ushort)playerSession.port;
//
//     if (ClonesManager.IsClone()) {
//       NetworkingManager.Singleton.StartClient();
//     }
//     else {
//       NetworkingManager.Singleton.StartHost();
//       var go = Instantiate(rpcObject);
//       go.GetComponent<NetworkedObject>().Spawn();
//       // rpc = go.GetComponent<RtsClient>();
//     }
//   }
// }
