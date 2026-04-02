using AutoMapper;
using Cat_API_Project.Models;
using Cat_API_Project.DTO;

namespace Cat_API_Project.Profiles
{
    public class CatProfile : Profile // inheret from automapper profile
    {
        public CatProfile()
        {
            CreateMap<CreateCatDTO, Cat>();

            CreateMap<Cat, CatDTO>()    //map from cat to catDTO automatically
                .ForMember(dest => dest.BreedName,
                opt => opt.MapFrom(src => src.Breed.BreedName));

            CreateMap<Breed, BreedDTO>();
        }
    }
}
