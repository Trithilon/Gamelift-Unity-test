using Cysharp.Threading.Tasks;
using MLAPI;

namespace NetworkingScripts {
  public static class NetworkAdapter {
    public static async UniTask<NetworkingManager> GetNetworkManager() {
      await UniTask.WaitUntil(() =>
          NetworkingManager.Singleton != null && NetworkingManager.Singleton.IsConnectedClient);
      return NetworkingManager.Singleton;
    }
  }
}
