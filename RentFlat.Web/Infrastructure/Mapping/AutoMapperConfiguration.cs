using RentFlat.Model;
using RentFlat.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentFlat.Web.Infrastructure.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                //config.CreateMap<ApplicationUser, ProfileViewModel>();
                //config.CreateMap<ProfileViewModel, ApplicationUser>();
            });
        }
    }
}