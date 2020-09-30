using System;
using System.Collections.Generic;
using System.Linq;
using MLAPI;
using MLAPI.Connection;
using MLAPI.Messaging;
using ModestTree;
using UnityEngine;
using Zenject;

namespace NetworkingScripts {
  public class ChatHandler {
    private readonly CustomMessages customMessages;
    private List<NetworkedClient> otherClients;

    private ChatHandler(CustomMessages customMessages) {
      this.customMessages = customMessages;
      customMessages.ChatReceived += OnChatReceived;
    }

    public void SendWassup() {
      if (otherClients == null || otherClients.IsEmpty()) {
        otherClients =
            NetworkingManager.Singleton.ConnectedClientsList.Where(client =>
                client.ClientId != NetworkingManager.Singleton.LocalClientId).ToList();
      }
      foreach (var networkedClient in otherClients) {
        customMessages.SendChatMessage(networkedClient.ClientId, "WASSUP");
      }
    }

    private static void OnChatReceived(object sender, string message) {
      Debug.Log($"Received chat!: {message}");
    }
  }
}
