using AutoMapper;
using Lucky.Entitys;
using LuckyJobsMarcoAlarcon.DTOs;

namespace LuckyJobsMarcoAlarcon.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PersonalCreacionDTO, Personal>();
            CreateMap<PersonalEliminacionDTO, Personal>();
            CreateMap<Personal, PersonalReporteDTO>();
            CreateMap<Hijo, HijoReporteDTO>();
            CreateMap<HijoEliminacionDTO, Hijo>();
        }
    }
}
