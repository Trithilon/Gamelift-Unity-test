using Aws.GameLift.Server.Model;
using MLAPI;
using NetworkingScripts.Api;
using UnityEngine;

namespace NetworkingScripts.Server {
  public class Server : MonoBehaviour {
    private GameLiftAdapter gameLiftAdapter;

    public void Awake() {
      gameLiftAdapter = new GameLiftAdapter();
      gameLiftAdapter.GameSessionStarted += HandleGameLiftSessionStarted;
      gameLiftAdapter.Start();
    }

    private void HandleGameLiftSessionStarted(object sender, GameSession gameSession) {
      // Get ip and port from onStartGameSession and set
      var enetTransport = (EnetTransport.EnetTransport)NetworkingManager.Singleton.NetworkConfig.NetworkTransport;
      enetTransport.Address = gameSession.IpAddress;
      enetTransport.Port = (ushort)gameSession.Port;
    }

    public void Start() {
      NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
      NetworkingManager.Singleton.StartServer();
    }


    private void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkingManager.ConnectionApprovedDelegate callback) {
      var playerSessionId = connectionData.GetString();

      var approved = gameLiftAdapter.ConnectPlayer(clientId, playerSessionId); // TODO: check if the outcome that's returned from this is sync or whether we should be awaiting

      //If approve is true, the connection gets added. If it's false. The client gets disconnected
      // null playerPrefabHash spawns default
      var t = transform;
      callback(true, null, approved, t.position, t.rotation);
    }

  }
}
