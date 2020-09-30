using System;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.NetworkedVar.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NetworkingScripts {
  public class NetworkedVar : NetworkedBehaviour {
    public static NetworkedVar Instance { get; private set; }

    public readonly NetworkedDictionary<ulong, float> playerSpeeds = new NetworkedDictionary<ulong, float>(new NetworkedVarSettings {
      ReadPermission = NetworkedVarPermission.Everyone,
      WritePermission = NetworkedVarPermission.Everyone,
    });


    private void OnEnable() {
      if (Instance == null) {
        Instance = this;
      }
    }


  }
}
