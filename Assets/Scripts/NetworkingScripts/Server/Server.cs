using System;
using System.Diagnostics;
using Aws.GameLift.Server.Model;
using Cysharp.Threading.Tasks;
using MLAPI;
using NetworkingScripts.Api;
using NetworkingScripts.Extensions;
using UnityEngine;

namespace NetworkingScripts.Server {
  public class Server {
    private readonly GameLiftAdapter gameLiftAdapter = new GameLiftAdapter();
    private readonly Transform transform;

    public Server(Transform transform) {
      this.transform = transform;
      gameLiftAdapter.GameSessionStarted += HandleGameLiftSessionStarted;
      gameLiftAdapter.Init();
      // TestInit().Forget();
    }

    // private async UniTaskVoid TestInit() {
    //   await UniTask.Delay(TimeSpan.FromSeconds(1));
    //   HandleGameLiftSessionStarted(this, new GameSession() { IpAddress = "127.0.0.1", Port = 7777});
    // }

    public void Start() {
      NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
      NetworkingManager.Singleton.StartServer();
    }

    private void HandleGameLiftSessionStarted(object sender, GameSession gameSession) {
      // Get ip and port from onStartGameSession and set
      var enetTransport = (EnetTransport.EnetTransport)NetworkingManager.Singleton.NetworkConfig.NetworkTransport;
      enetTransport.Address = gameSession.IpAddress;
      enetTransport.Port = (ushort)gameSession.Port;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId,
      NetworkingManager.ConnectionApprovedDelegate callback) {
      var playerSessionId = connectionData.GetString();
      // TODO: check if the outcome that's returned from this is sync or whether we should be awaiting
      var approved = gameLiftAdapter.ConnectPlayer(clientId, playerSessionId);

      //If approve is true, the connection gets added. If it's false. The client gets disconnected
      // null playerPrefabHash spawns default
      callback(true, null, approved, transform.position, transform.rotation);
    }
  }
}
