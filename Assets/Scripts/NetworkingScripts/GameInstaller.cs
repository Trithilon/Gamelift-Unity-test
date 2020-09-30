using UnityEngine;
using Zenject;

namespace NetworkingScripts {
  public class GameInstaller : MonoInstaller {
    public override void InstallBindings() {
      Container.Bind<CustomMessages>().AsSingle();
      Container.Bind<ChatHandler>().AsSingle();
    }
  }
}
