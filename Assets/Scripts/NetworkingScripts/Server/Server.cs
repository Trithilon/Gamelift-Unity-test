using System;
using System.Diagnostics;
using Aws.GameLift.Server.Model;
using Cysharp.Threading.Tasks;
using MLAPI;
using NetworkingScripts.Api;
using NetworkingScripts.Extensions;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace NetworkingScripts.Server {
  public class Server {
    private readonly GameLiftAdapter gameLiftAdapter = new GameLiftAdapter();
    private readonly Transform transform;

    public Server(Transform transform) {
      this.transform = transform;
      gameLiftAdapter.GameSessionStarted += HandleGameLiftSessionStarted;
      gameLiftAdapter.Init();
    }

    public void Start() {
      NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
      NetworkingManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
      NetworkingManager.Singleton.StartServer();
    }

    private void HandleGameLiftSessionStarted(object sender, GameSession gameSession) {
      // Get ip and port from onStartGameSession and set
      Debug.Log($":) Game session details {gameSession.Port} {gameSession.IpAddress} {gameSession.MatchmakerData}");
      var enetTransport = (EnetTransport.EnetTransport)NetworkingManager.Singleton.NetworkConfig.NetworkTransport;
      enetTransport.Address = gameSession.IpAddress;
      enetTransport.Port = (ushort)gameSession.Port;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId,
      NetworkingManager.ConnectionApprovedDelegate callback) {
      var playerSessionId = connectionData.GetString();
      // TODO: check if the outcome that's returned from this is sync or whether we should be awaiting
      Debug.Log($":) Approval check {clientId} - {connectionData.GetString()}");
      var approved = gameLiftAdapter.ConnectPlayer(clientId, playerSessionId);

      //If approve is true, the connection gets added. If it's false. The client gets disconnected
      // null playerPrefabHash spawns default
      callback(true, null, approved, transform.position, transform.rotation);
    }

    private void HandleClientDisconnect(ulong clientId) {
      gameLiftAdapter.DisconnectPlayer(clientId);
    }
  }
}
