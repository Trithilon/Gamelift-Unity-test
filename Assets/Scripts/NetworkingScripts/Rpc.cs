using System;
using MLAPI;
using MLAPI.Logging;
using MLAPI.Messaging;
using UnityEngine;

namespace NetworkingScripts {
  public class Rpc : NetworkedBehaviour {
    [ServerRPC]
    private void ServerOnCollide() {
      NetworkLog.LogInfoServer("HELLO THERE WAS A COLLISION");
    }

    [ClientRPC]
    private void OnCollide() {
      Debug.Log("Other Player: OUCH!");
    }

    private void OnCollisionEnter(Collision other) {
      if (!other.gameObject.CompareTag("Player")) return;

      var collisionClientId = other.gameObject.GetComponent<NetworkedObject>().OwnerClientId;
      InvokeClientRpcOnClient(OnCollide, collisionClientId);
      InvokeServerRpc(ServerOnCollide);
    }
  }
}
