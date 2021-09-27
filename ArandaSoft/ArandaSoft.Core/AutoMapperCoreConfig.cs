using ArandaSoft.Core.Model.ValueObjects;
using ArandaSoft.EntityFramework.Model;
using AutoMapper;

namespace ArandaSoft.Core
{
    public class AutoMapperCoreConfig
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AppUser, AppUserModel>()
                        .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.AppRole.Name));
                    cfg.CreateMap<AppUserModel, AppUser>();
                    cfg.CreateMap<AppRole, AppRoleModel>();
                }
            );

            return config;
        }
    }
}