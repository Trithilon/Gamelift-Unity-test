using System.Collections.Generic;

namespace NetworkingScripts.Api.Models {
  public class RequestOptions {
    public string Url { get; set; }

    public string Method { get; set; }

    public object Body { get; set; }

    private Dictionary<string, string> headers;
    public Dictionary<string, string> Headers {
      get => headers ?? new Dictionary<string, string>();
      set => headers = value;
    }
  }
}
