using System;
using UnityEngine;
using Zenject;

namespace NetworkingScripts {
  public class InputHandler : MonoBehaviour {
    [Inject] private readonly ChatHandler chatHandler;

    private void Update() {
      if (Input.GetKeyDown(KeyCode.P)) {
        chatHandler.SendWassup();
      }
    }
  }
}
