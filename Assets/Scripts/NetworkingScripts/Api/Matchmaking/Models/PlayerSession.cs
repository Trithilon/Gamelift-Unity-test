using Newtonsoft.Json;

namespace NetworkingScripts.Api.Matchmaking.Models {
  public class PlayerSession {
    public string status;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string id;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string ipAddress;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int port; // TODO: make ushort
  }
}
