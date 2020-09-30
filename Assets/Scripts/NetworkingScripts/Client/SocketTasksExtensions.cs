using System;
using System.Runtime.CompilerServices;
using MLAPI.Transports.Tasks;

namespace NetworkingScripts.Client {
  public static class SocketTasksExtensions {
    public struct Awaiter : INotifyCompletion {
      private readonly SocketTasks socketTasks;

      public Awaiter(SocketTasks socketTasks) => this.socketTasks = socketTasks;

      public bool GetResult() => socketTasks.AnySuccess;

      public bool IsCompleted => socketTasks.IsDone;

      public void OnCompleted(Action continuation) {
      }
    }

    public static Awaiter GetAwaiter(this SocketTasks socketTasks) {
      return new Awaiter(socketTasks);
    }


  }
}
