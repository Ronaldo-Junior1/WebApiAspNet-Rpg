using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using WebApi.Models;
using WebApi.Services.CharacterService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
       
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Get()
        { 
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character newCharacter) 
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
    }
}
