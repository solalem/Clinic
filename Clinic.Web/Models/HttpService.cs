using Blazorise;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clinic.Web.Models
{
    public class HttpService
    {
        INotificationService NotificationService { get; set; }

        private readonly HttpClient _httpClient;
        public string ApiUrl { get => _httpClient.BaseAddress.AbsolutePath; }

        public HttpService(HttpClient client, INotificationService notificationService)
        {
            _httpClient = client;// new HttpClient() { BaseAddress = new Uri(baseUrl) };
            NotificationService = notificationService;
        }

        public async Task<T> HttpGet<T>(string uri)
            where T : class
        {
            var result = await _httpClient.GetAsync($"{ApiUrl}{uri}");
            if (!result.IsSuccessStatusCode)
            {
                await NotificationService.Error($"Can't get. Error: {result.ReasonPhrase}", "Error");
                return null;
            }

            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> HttpDelete<T>(string uri, Guid id)
            where T : class
        {
            var result = await _httpClient.DeleteAsync($"{ApiUrl}{uri}/{id}");
            if (!result.IsSuccessStatusCode)
            {
                await NotificationService.Error($"Can't delete. Error: {result.ReasonPhrase}", "Error");
                return null;
            }

            await NotificationService.Success($"Successfully deleted.", "Success");
            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> HttpPost<T>(string uri, object dataToSend, bool notifySuccess = true)
            where T : class
        {
            var content = ToJson(dataToSend);

            var result = await _httpClient.PostAsync($"{ApiUrl}{uri}", content);
            if (!result.IsSuccessStatusCode)
            {
                
                await NotificationService.Error($"Can't create. Error: {result.ReasonPhrase}", "Error");
                return null;
            }

            if(notifySuccess)
                await NotificationService.Success($"Successfully updated.", "Success");
            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> HttpPut<T>(string uri, object dataToSend)
            where T : class
        {
            var content = ToJson(dataToSend);

            var result = await _httpClient.PutAsync($"{ApiUrl}{uri}", content);
            if (!result.IsSuccessStatusCode)
            {
                await NotificationService.Error($"Can't update. {result.ReasonPhrase}", "Error");
                return null;
            }

            await NotificationService.Success($"Successfully updated.", "Success");
            return await FromHttpResponseMessage<T>(result);
        }

        private StringContent ToJson(object obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }

        private async Task<T> FromHttpResponseMessage<T>(HttpResponseMessage result)
        {
            var stringContent = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(stringContent)) return default(T);

            return JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
