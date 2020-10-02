using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class MultiplayerPlayerTweaks : NetworkedBehaviour {
  // Start is called before the first frame update
  void Start() {
    if (!IsLocalPlayer) {
      GetComponentInChildren<Camera>().enabled = false;
      GetComponent<BasicBehaviour>().enabled = false;
      GetComponent<MoveBehaviour>().enabled = false;
      GetComponent<FlyBehaviour>().enabled = false;
    }
  }
}
