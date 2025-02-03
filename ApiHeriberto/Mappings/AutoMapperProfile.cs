using ApiHeriberto.Models.Domain;
using ApiHeriberto.Models.DTO;
using AutoMapper;

namespace ApiHeriberto.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            /**
             * Region mappings
             * */

            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();


            /**
             * Difficulty mappings
             */
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();


            /**
             * Walk Maps
             * **
             */
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();
        }


    }
}
