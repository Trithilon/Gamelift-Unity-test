using System;
using Cysharp.Threading.Tasks;
using MLAPI;
using NetworkingScripts.Api.Matchmaking;
using NetworkingScripts.Api.Matchmaking.Models;
using NetworkingScripts.Extensions;
using UnityEngine;

namespace NetworkingScripts.Client {
  public class Client {
    private readonly MatchmakingClient matchmakingClient = new MatchmakingClient();

    public async void Start() {
      var ticketId = await matchmakingClient.StartMatchmaking();
      // Initial delay before polling CheckMatchmaking
      await UniTask.Delay(1000);
      var playerSession = await WaitForPlayerSession(ticketId);

      await UniTask.SwitchToMainThread();
      var enetTransport = (EnetTransport.EnetTransport)NetworkingManager.Singleton.NetworkConfig.NetworkTransport;
      enetTransport.Address = playerSession.ipAddress;
      enetTransport.Port = (ushort)playerSession.port;
      // Extra data to authenticate with Gamelift
      NetworkingManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes(playerSession.id);
      await NetworkingManager.Singleton.StartClient(); // TODO: IDK if I made the custom awaiter correctly.
    }

    private async UniTask<PlayerSession> WaitForPlayerSession(string ticketId) {
      for (var checkAttempt = 0; checkAttempt < 12; checkAttempt++) {
        var playerSession = await matchmakingClient.CheckForPlayerSession(ticketId);
        if (playerSession.status == "COMPLETED") {
          return playerSession;
        }

        await UniTask.Delay(TimeSpan.FromSeconds(10));
      }

      throw new Exception("Too many attempts to checking for session");
    }

    public void OnApplicationQuit(ulong clientId) {
      NetworkingManager.Singleton.DisconnectClient(clientId);
    }
  }
}
