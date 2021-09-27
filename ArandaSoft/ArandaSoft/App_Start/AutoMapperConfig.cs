using ArandaSoft.Core.Model.ValueObjects;
using ArandaSoft.Model.InputModels;
using ArandaSoft.Model.OutputModels;
using ArandaSoft.Model.ValueObjects;
using AutoMapper;

namespace ArandaSoft
{
    public class AutoMapperConfig
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AppUserModel, AppUserOutput>();
                    cfg.CreateMap<AppUserModel, UserSession>();
                    cfg.CreateMap<AppUserInput, AppUserModel>();
                }
            );

            return config;
        }
    }
}