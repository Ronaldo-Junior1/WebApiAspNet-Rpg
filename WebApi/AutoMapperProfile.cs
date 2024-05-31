using AutoMapper;
using WebApi.Dtos.Character;
using WebApi.Models;

namespace WebApi
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
        }
    }
}
