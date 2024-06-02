using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos.Character;
using WebApi.Models;

namespace WebApi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
             _context.Add(character);
            _context.SaveChanges();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbcharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id); ;
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbcharacter);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
               
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (dbCharacter is null)
                    throw new Exception($"Character with Id {updatedCharacter.Id} not found");

                dbCharacter.Name = updatedCharacter.Name;
                dbCharacter.HitPoints = updatedCharacter.HitPoints;
                dbCharacter.Strenght = updatedCharacter.Strenght;
                dbCharacter.Defense = updatedCharacter.Defense;
                dbCharacter.Intelligence = updatedCharacter.Intelligence;
                dbCharacter.Class = updatedCharacter.Class;

                _context.Characters.Update(dbCharacter);
                _context.SaveChanges();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (dbCharacter is null)
                    throw new Exception($"Character with Id {id} not found");

                _context.Characters.Remove(dbCharacter);
                _context.SaveChanges();

                serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }
    }
}
