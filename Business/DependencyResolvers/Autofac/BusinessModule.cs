using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace Business.DependencyResolvers.Autofac
{
    public class BusinessModule : Module
    {
        protected override void Load(global::Autofac.ContainerBuilder builder)
        {


            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<TokenHelper>().As<ITokenHelper>();


            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

            base.Load(builder);
        }
    }
}
