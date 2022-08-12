﻿using MagicVilla_Utility;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;
        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new MagicVilla_VillaAPI.Models.APIRequest
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl + "/api/VillaAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new MagicVilla_VillaAPI.Models.APIRequest
            {
                ApiType = SD.ApiType.DELETE,
                URL = villaUrl + "/api/VillaAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new MagicVilla_VillaAPI.Models.APIRequest
            {
                ApiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new MagicVilla_VillaAPI.Models.APIRequest
            {
                ApiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new MagicVilla_VillaAPI.Models.APIRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                URL = villaUrl + "/api/VillaAPI/" + dto.Id,
                Token = token
            }); ;
        }
    }
}
