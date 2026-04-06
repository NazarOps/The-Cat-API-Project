using AutoMapper;
using Cat_API_Project.DTO;
using Cat_API_Project.Models;

namespace Cat_API_Project.Profiles
{
    public class BreedFactProfile : Profile
    {
        public BreedFactProfile() 
        {
            CreateMap<CreateBreedFactDTO, BreedFact>();

            CreateMap<BreedFact, BreedFactDTO>()
            .ForMember(dest => dest.BreedName,
                opt => opt.MapFrom(src => src.Breed.BreedName));
        }
    }
}
