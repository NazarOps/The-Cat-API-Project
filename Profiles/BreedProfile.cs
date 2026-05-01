using AutoMapper;
using Cat_API_Project.DTO;
using Cat_API_Project.Models;

namespace Cat_API_Project.Profiles
{
    public class BreedProfile : Profile
    {
        public BreedProfile()
        {
            CreateMap<CreateBreedDTO, Breed>();
            CreateMap<UpdateBreedDTO, Breed>();
            CreateMap<Breed, BreedDTO>();
        }
    }
}
