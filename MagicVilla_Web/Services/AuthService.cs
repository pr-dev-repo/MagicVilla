using MagicVilla_Utility;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO dto)
        {
            return SendAsync<T>(new MagicVilla_VillaAPI.Models.APIRequest
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl + "/api/v1/VillaAPI/login"
            });
        }
        public Task<T> RegisterAsync<T>(RegistrationRequestDTO dto)
        {
            return SendAsync<T>(new MagicVilla_VillaAPI.Models.APIRequest
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl + "/api/v1/VillaAPI/register"
            });
        }
    }
}
