using System.Net.Http.Headers;
using GP.Utilix;

namespace GP.ECommerce1.BlazorWebApp.Services.ClientServices;

public abstract class ClientServiceBase
{
    protected HttpClient HttpClient { get; }

    protected ClientServiceBase(HttpClient httpClient)
    {
        HttpClient = httpClient;
        var acceptJsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
        var acceptTextHeader = new MediaTypeWithQualityHeaderValue("text/plain");
        HttpClient.DefaultRequestHeaders.Accept.Add(acceptJsonHeader);
        HttpClient.DefaultRequestHeaders.Accept.Add(acceptTextHeader);
    }
    
    protected virtual async Task<Result<TValue>> SendRequest<TValue>(HttpMethod method, string path, object? body=null)
    {
        try
        {
            var responseMessage = await SendRequestInternal(method, path, body);
            var result = await responseMessage.Content.ReadFromJsonAsync<Result<TValue>>();
            if (result != null)
            {
                return result;
            }
            var text = await responseMessage.Content.ReadAsStringAsync();
               
                return new Result<TValue>{Error = text, IsSuccess = false};
        }
        catch (Exception e)
        {
            
            return new Result<TValue>
            {
                IsSuccess = false,
                Error = $"Exception: {e.Message}"
            };
        }
    }

    protected virtual async Task<Result> SendRequest(HttpMethod method, string uri, object? body=null)
    {
        try
        {
            var responseMessage = await SendRequestInternal(method, uri, body);
            var result = await responseMessage.Content.ReadFromJsonAsync<Result>();
            if (result != null)
            {
                return result;
            }
            var text = await responseMessage.Content.ReadAsStringAsync();
            
            return new Result{IsSuccess = false, Error = text};
        }
        catch (Exception e)
        {
            return new Result{IsSuccess = false, Error = e.Message};
        }
    }

    protected virtual async Task<HttpResponseMessage> SendRequestInternal(HttpMethod method, string path, object? body)
    {
        HttpResponseMessage responseMessage;

        if (method == HttpMethod.Get)
            responseMessage = await HttpClient.GetAsync(path);
        else if (method == HttpMethod.Post)
            responseMessage = await HttpClient.PostAsJsonAsync(path, body);
        else if (method == HttpMethod.Put)
            responseMessage = await HttpClient.PutAsJsonAsync(path, body);
        else if (method == HttpMethod.Delete)
            responseMessage = await HttpClient.DeleteAsync(path);
        else
            throw new ArgumentException(
                $"Http Method for {nameof(SendRequestInternal)} should be either get, post, put, or delete.");

        return responseMessage;
    }
}