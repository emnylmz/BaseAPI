using System.Reflection;
using Autofac;
using BaseAPI.Core.Interfaces;
using BaseAPI.Data;
using BaseAPI.Service.Mapping;
using BaseAPI.Service.Services;
using Module = Autofac.Module;

namespace BaseAPI.API
{
    public class RepoServiceModule:Module
	{
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).
                As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(BaseService<>)).
                As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).
                Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).
                Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}

