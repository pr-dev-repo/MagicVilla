using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repo.IRepo;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaNumberAPI")] // code route hard to avoid maintanance issues
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IVillaNumberRepo _villaNumberRepo;
        private readonly IVillaRepo _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaNumberAPIController(IMapper mapper, ILogger<VillaAPIController> logger, IVillaNumberRepo repo, IVillaRepo villaRepo)
        {
            _logger = logger;
            _villaNumberRepo = repo;
            _mapper = mapper;
            _response = new();
            _villaRepo = villaRepo;
        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                _logger.LogInformation("getting villas");

                _response.Result = await _villaNumberRepo.GetAll(includeProps: "Villa");
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };

            }
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError($"GetVilla Erorr with id {id}");
                    return BadRequest();
                }

                var villaNum = await _villaNumberRepo.Get(i => i.VillaNo == id);

                if (villaNum == null)
                    return NotFound();

                _response.Result = villaNum;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };

            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumbers([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                if (await _villaNumberRepo.Get(n => n.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("Custom", "Villa Already Exists");
                    return BadRequest(ModelState);
                }

                if(await _villaRepo.Get(x => x.Id == createDTO.VillaID) == null)
                {
                    ModelState.AddModelError("Custom", "Villa ID is unavailable");
                    return BadRequest(ModelState);
                }
                 

                if (createDTO == null)
                    return BadRequest(createDTO);

                var model = _mapper.Map<VillaNumber>(createDTO);

                await _villaNumberRepo.Create(model);

                _response.Result = _mapper.Map<VillaNumberDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = model.VillaNo }, _response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };

            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaNumbers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumbers(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                var villa = await _villaNumberRepo.Get(i => i.VillaNo == id);

                if (villa == null)
                    return NotFound();

                await _villaNumberRepo.Remove(villa);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };

            }
            return _response;

        }


        [HttpPut("{id:int}", Name = "UpdateVillaNumbers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumbers(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.VillaNo)
                    return BadRequest();

                if (await _villaRepo.Get(x => x.Id == updateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("Custom", "Villa ID is unavailable");
                    return BadRequest(ModelState);
                }

                var model = _mapper.Map<VillaNumber>(updateDTO);

                await _villaNumberRepo.Update(model);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };

            }
            return _response;
        }
    }
}
