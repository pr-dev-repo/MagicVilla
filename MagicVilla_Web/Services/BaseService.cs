using MagicVilla_Utility;
using MagicVilla_VillaAPI.Models;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            ResponseModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest aPIRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                var msg = new HttpRequestMessage();
                msg.Headers.Add("Accept", "application/json");
                msg.RequestUri = new Uri(aPIRequest.URL);
                if(aPIRequest.Data != null)
                {
                    msg.Content = new StringContent(JsonConvert.SerializeObject(aPIRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (aPIRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        msg.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        msg.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        msg.Method = HttpMethod.Delete;
                        break;
                    default:
                        msg.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage? apiResponse = null;

                apiResponse = await client.SendAsync(msg);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(apiContent);

            }
            catch(Exception e)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                return JsonConvert.DeserializeObject<T>(res);
            }
        }
    }
}