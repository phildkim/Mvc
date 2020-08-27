namespace app
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using MediatR;
    using MS.Data;
    using System.Reflection;
    using System.Web.Mvc;
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }
        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();
            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterType<DataContextFactory>().As<IDataContextFactory>().InstancePerRequest();
            // finally register our custom code (individually, or via assembly scanning)
            // - requests & handlers as transient, i.e. InstancePerDependency()
            // - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
            // - behaviors as transient, i.e. InstancePerDependency()
            builder.RegisterAssemblyTypes(typeof(MS.Features.Home).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(MS.Features.Query).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(MS.Features.Create).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(MS.Features.Update).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(MS.Features.Delete).GetTypeInfo().Assembly).AsImplementedInterfaces();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}