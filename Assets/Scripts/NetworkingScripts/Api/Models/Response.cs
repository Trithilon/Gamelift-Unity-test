namespace NetworkingScripts.Api.Models {
  public class Response {
    public Response(IRequest request) {
      Text = request.DownloadHandler.text;
      StatusCode = request.ResponseCode;
      Error = request.Error;
      IsNetworkError = request.IsNetworkError;
      IsHttpError = request.IsHttpError;
    }
    public long StatusCode { get; }

    public string Text { get; }

    public string Error { get; }
    public bool IsNetworkError { get; }
    public bool IsHttpError { get; }
  }
}
