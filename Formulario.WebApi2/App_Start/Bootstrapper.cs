using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Formulario.Business;
using Formulario.Business.RepositoryPattern;
using Formulario.Data.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace Formulario.WebApi2.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            var builder = new Autofac.ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            var service = builder.RegisterType<FormularioService>()
                .As<IFormularioService>();

            var factory = builder.RegisterType<DatabaseFactoryFormulario>()
                .As<IDatabaseFactory>();

            var unit = builder.RegisterType<UnitOfWorkGeneric>()
                .As<IUnitOfWOrkGeneric<IDatabaseFactory>>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterWebApiModelBinderProvider();

            service.InstancePerRequest();
            factory.InstancePerRequest();
            unit.InstancePerRequest();


            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}