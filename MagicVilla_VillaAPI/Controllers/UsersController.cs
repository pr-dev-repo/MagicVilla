using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")] 
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private APIResponse _apiResponse;

        public UsersController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _apiResponse = new();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var response = await _userRepo.Login(request);

            if (string.IsNullOrWhiteSpace(response.Token))
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username or Password is not correct.");
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_apiResponse);
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.Result = response;

            return Ok(_apiResponse);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationRequestDTO request)
        {
            bool ifUserIsUnique = _userRepo.IsUniqueUser(request.Username);

            if (!ifUserIsUnique)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username already exists!");
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_apiResponse);
            }

            var user = await _userRepo.Register(request);

            if(user == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Error while registering");
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_apiResponse);
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.Result = user;
            return Ok(_apiResponse);

        }
    }
}
