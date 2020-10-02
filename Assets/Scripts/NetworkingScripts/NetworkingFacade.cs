// TODO: Probably use csc.rsp files to actually define a global UNITY_CLIENT directive. This only works per file i believe.

#if !UNITY_SERVER
#define UNITY_CLIENT
#endif

using System.Diagnostics;
using UnityEngine;

namespace NetworkingScripts {
  public class NetworkingFacade : MonoBehaviour {
#if UNITY_SERVER
    private Server.Server server;
#else
    private Client.Client client;
#endif

    private void Awake() {
#if UNITY_SERVER
      server = new Server.Server(transform);
#else
      client = new Client.Client();
#endif
    }

    private void Start() {
#if UNITY_SERVER
      server.Start();
#else
      client.Start();
#endif
    }
  }
}
