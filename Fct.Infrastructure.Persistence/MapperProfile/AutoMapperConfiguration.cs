using System;
using System.Collections.Generic;
using System.Text;

namespace Fct.Infrastructure.Persistence.MapperProfile
{
    using AutoMapper;

    public class AutoMapperConfiguration
    {
        public MapperConfiguration GetAutoMapperProfiles()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(typeof(ProductProfile));
                }
            );
            return config;

        }

        public IMapper GetIMapper()
        {
            IMapper mapper = new Mapper(this.GetAutoMapperProfiles());
            return mapper;
        }
    }
}
