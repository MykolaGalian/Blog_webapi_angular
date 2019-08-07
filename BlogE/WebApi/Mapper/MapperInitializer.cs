using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Mapper
{
    public static class MapperInitializer
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.AddProfile<MapViewModels>();
                config.AddProfile<MapDTOModels>();
            });
        }
    }
}