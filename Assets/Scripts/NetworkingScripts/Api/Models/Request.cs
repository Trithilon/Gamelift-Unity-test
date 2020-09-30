using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace NetworkingScripts.Api.Models {
  public interface IRequest {
    string Error { get; }
    bool IsHttpError { get; }
    bool IsNetworkError { get; }
    string Url { get; set; }
    long ResponseCode { get; }
    string Method { get; set; }
    DownloadHandler DownloadHandler { get; set; }
    UploadHandler UploadHandler { get; set; }
    UnityWebRequest UnityWebRequest { get; set; }
    UniTask SendWebRequest();
    void SetRequestHeader(string name, string value);
  }

  public class Request : IRequest {
    public Request() {
      UnityWebRequest = new UnityWebRequest();
    }

    public UnityWebRequest UnityWebRequest { get; set; }

    public bool IsNetworkError => UnityWebRequest.isNetworkError;

    public bool IsHttpError => UnityWebRequest.isHttpError;

    public string Error => UnityWebRequest.error;
    public long ResponseCode => UnityWebRequest.responseCode;

    public string Url {
      get => UnityWebRequest.url;
      set => UnityWebRequest.url = value;
    }

    public string Method {
      get => UnityWebRequest.method;
      set => UnityWebRequest.method = value;
    }

    public UploadHandler UploadHandler {
      get => UnityWebRequest.uploadHandler;
      set => UnityWebRequest.uploadHandler = value;
    }

    public DownloadHandler DownloadHandler {
      get => UnityWebRequest.downloadHandler;
      set => UnityWebRequest.downloadHandler = value;
    }

    public async UniTask SendWebRequest() {
      await UnityWebRequest.SendWebRequest();
    }

    public void SetRequestHeader(string name, string value) {
      UnityWebRequest.SetRequestHeader(name, value);
    }
  }
}
