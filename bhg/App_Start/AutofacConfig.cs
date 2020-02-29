using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Core.Registration;
using AutoMapper;
using bhg.Models;
using bhg.Repositories;
using bhg.Interfaces;

namespace bhg
{
  public class AutofacConfig
  {
    public static void Register()
    {
      var bldr = new ContainerBuilder();
      var config = GlobalConfiguration.Configuration;
      bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
      RegisterServices(bldr);
      bldr.RegisterWebApiFilterProvider(config);
      bldr.RegisterWebApiModelBinderProvider();
      var container = bldr.Build();
      config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }

    private static void RegisterServices(ContainerBuilder bldr)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TreasureMapMappingProfile());
        });

        bldr.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();
        bldr.RegisterType<BhgContext>().InstancePerRequest();
        bldr.RegisterType<TreasureMapRepository>().As<ITreasureMapRepository>().InstancePerRequest();
    }
  }
}
