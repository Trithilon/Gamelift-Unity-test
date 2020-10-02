using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace NetworkingScripts.Api.Matchmaking {
  public class MatchmakingClient {

    private readonly MatchmakingApi matchmakingApi;

    public MatchmakingClient() {
      matchmakingApi = new MatchmakingApi();
    }

    public async UniTask<string> StartMatchmaking() {
      var response = await matchmakingApi.Post(
        "/manual-gamelift-robot-lambda",
        new StartMatchmakingRequest());
      return response.Text;
      // return "foo";
    }

    private class StartMatchmakingRequest {
      public string type = "start-mlapi";
      public Dictionary<string, int> latencyMap = new Dictionary<string, int> { { "us-east-1", 10 } };
    }

    public async UniTask<Models.PlayerSession> CheckForPlayerSession(string ticketId) =>
      await matchmakingApi.Post<Models.PlayerSession>(
        "/manual-gamelift-robot-lambda",
        new MatchmakingTicketRequest { ticketId = ticketId }
      );
      // new Models.PlayerSession {ipAddress = "127.0.0.1", port = 7777, id = "psess-a832a627-1b85-418f-9b1e-b62e65045192", status = "COMPLETED"};

    private class MatchmakingTicketRequest {
      public string type = "check";
      public string ticketId;
    }

    private class MatchmakingApi : Api {
      public MatchmakingApi() : base("https://mja0nx8lgg.execute-api.us-east-1.amazonaws.com") {
      }
    }
  }
}
