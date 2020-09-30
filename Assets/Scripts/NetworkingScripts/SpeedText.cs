using MLAPI;
using UnityEngine;
using UnityEngine.UI;

namespace NetworkingScripts {
  public class SpeedText : NetworkedBehaviour {
    [SerializeField] private Text playerSpeedsText;
    private Rigidbody rb;

    private void Update() {
      if (NetworkedVar.Instance == null) return;
      playerSpeedsText.text = "Player Speed: \n";
      foreach (var keyValuePair in NetworkedVar.Instance.playerSpeeds) {
        playerSpeedsText.text += $"{keyValuePair.Key} - {keyValuePair.Value}\n";
      }
    }

    private void Awake() {
      rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
      if (NetworkedVar.Instance == null) return;
      if (IsLocalPlayer) {
        NetworkedVar.Instance.playerSpeeds[OwnerClientId] = rb.velocity.magnitude;
      }
    }
  }
}
