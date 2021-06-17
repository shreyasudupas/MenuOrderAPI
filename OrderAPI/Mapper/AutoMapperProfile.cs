using AutoMapper;
using BuisnessLayer.DBModels;
using OrderAPI.BuisnessLayer.Models;

namespace OrderAPI.Mapper
{
    public class AutoMapperProfile:Profile
    {
        /// <summary>
        /// create all the mappinng here before implementing in the code
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<tblUser,User>()
                .ForMember(dest=>dest.StateName,source=>source.MapFrom(s=>s.StateId != null ? s.State.StateNames:string.Empty))
                .ForMember(dest => dest.CityName, source => source.MapFrom(s => s.CityId != null ? s.City.CityNames : string.Empty))
                .ForMember(dest => dest.CreatedDate, source => source.MapFrom(s => s.CreatedDate.ToString("dddd, dd MMMM yyyy")))
                .ForMember(dest => dest.UpdatedDate, source => source.MapFrom(s => s.UpdatedDate.GetValueOrDefault().ToString("dddd, dd MMMM yyyy")));
        }
    }
}
