using ApplicationService.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA = WebApiDataAccess;
namespace ApplicationService
{
    public static class AutomMapperInstaller
    {
        public static IMapper InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DA.Employee, Employee>();
            });

            return config.CreateMapper();
        }
    }
}
