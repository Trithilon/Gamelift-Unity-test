using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NetworkingScripts.Api.Models;
using NetworkingScripts.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UnityEngine.Networking;

namespace NetworkingScripts.Api {
  public abstract class Api {
    private readonly string baseUrl;
    private readonly JsonSerializerSettings settings = new JsonSerializerSettings {
      ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
      Converters = new List<JsonConverter> { new StringEnumConverter() }
    };

    protected Api(string baseUrl) {
      this.baseUrl = baseUrl;
    }

    protected virtual void AddRequestHeaders(IRequest request) {
      // No-op 
    }

    private async UniTask AddRequestBody(IRequest request, object body) {
      if (body != null) {
        await UniTask.SwitchToThreadPool();
        var bodyString = JsonConvert.SerializeObject(body, settings);
        await UniTask.SwitchToMainThread(); // Can only create an UploadHandler on the main thread
        request.UploadHandler = new UploadHandlerRaw(bodyString.GetBytes()) {
          contentType = "application/json"
        };
      }
    }

    public async UniTask<Response> SendRequest(RequestOptions requestOptions, IRequest request) {
      using (request.UnityWebRequest) {
        request.Url = baseUrl + requestOptions.Url;
        request.Method = requestOptions.Method;
        request.DownloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        AddRequestHeaders(request);
        foreach (var header in requestOptions.Headers) {
          request.SetRequestHeader(header.Key, header.Value);
        }
        await AddRequestBody(request, requestOptions.Body);

        await request.SendWebRequest();

        var response = new Response(request);
        if (!response.IsNetworkError && !response.IsHttpError) {
          return response;
        }
        throw new RequestException(response.Error, response.IsHttpError, response.IsNetworkError, response.StatusCode, response.Text);
      }
    }

    private async UniTask<Response> Request(RequestOptions requestOptions) => await SendRequest(requestOptions, new Request());

    private async UniTask<TResponse> Request<TResponse>(RequestOptions requestOptions) {
      var response = await SendRequest(requestOptions, new Request());
      var responseText = response.Text;
      await UniTask.SwitchToThreadPool();
      return JsonConvert.DeserializeObject<TResponse>(responseText, settings);
    }

    public async UniTask<Response> Get(string url) {
      return await Get(new RequestOptions { Url = url });
    }

    public async UniTask<Response> Get(RequestOptions options) {
      options.Method = UnityWebRequest.kHttpVerbGET;
      return await Request(options);
    }

    public async UniTask<T> Get<T>(string url) {
      return await Get<T>(new RequestOptions { Url = url });
    }

    public async UniTask<T> Get<T>(RequestOptions options) {
      options.Method = UnityWebRequest.kHttpVerbGET;
      return await Request<T>(options);
    }

    public async UniTask<Response> Post(string url, object body = null) {
      return await Post(new RequestOptions { Url = url, Body = body });
    }

    public async UniTask<Response> Post(RequestOptions options) {
      options.Method = UnityWebRequest.kHttpVerbPOST;
      return await Request(options);
    }

    public async UniTask<T> Post<T>(string url, object body = null) {
      return await Post<T>(new RequestOptions { Url = url, Body = body });
    }
    public async UniTask<T> Post<T>(RequestOptions options) {
      options.Method = UnityWebRequest.kHttpVerbPOST;
      return await Request<T>(options);
    }

    public async UniTask<Response> Put(string url, object body = null) {
      return await Put(new RequestOptions { Url = url, Body = body });
    }

    public async UniTask<Response> Put(RequestOptions options) {
      options.Method = UnityWebRequest.kHttpVerbPUT;
      return await Request(options);
    }

    public async UniTask<T> Put<T>(string url, object body = null) {
      return await Put<T>(new RequestOptions { Url = url, Body = body });
    }

    public async UniTask<T> Put<T>(RequestOptions options) {
      options.Method = UnityWebRequest.kHttpVerbPUT;
      return await Request<T>(options);
    }

    public async UniTask<Response> Delete(string url) {
      return await Delete(new RequestOptions { Url = url });
    }

    public async UniTask<Response> Delete(RequestOptions options) {
      options.Method = UnityWebRequest.kHttpVerbDELETE;
      return await Request(options);
    }
  }
}
