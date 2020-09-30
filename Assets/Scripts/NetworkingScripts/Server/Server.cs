using System;
using Cysharp.Threading.Tasks;
using MLAPI;
using NetworkingScripts.Api.Matchmaking;
using NetworkingScripts.Api.Matchmaking.Models;
using UnityEngine;

namespace NetworkingScripts.Server
{
    public class Server : MonoBehaviour
    {
        private GameLiftServer server;

        public void Awake()
        {
            server = new GameLiftServer();
        }

        public void Start()
        {
            server.Start();
            // Need to wait for init and process ready and start game session i think?
            // Get ip and port from onStartGameSession and set
            NetworkingManager.Singleton.StartHost();
        }
        
        
        public bool ConnectPlayer(int playerIdx, string playerSessionId)
        {
            return server.ConnectPlayer(playerIdx, playerSessionId);
        }

        public void DisconnectPlayer(int playerIdx)
        {
            server.DisconnectPlayer(playerIdx);
        }

    }
}