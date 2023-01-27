using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using EnterpriseArchitecture.Business.Authentication;
using EnterpriseArchitecture.Business.Repositories.OperationClaimRepository;
using EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository;
using EnterpriseArchitecture.Business.Repositories.UserRepository;
using EnterpriseArchitecture.Business.Repositories.Utilities;
using EnterpriseArchitecture.Core.Utilities.Interceptors;
using EnterpriseArchitecture.DataAccess.Concrete.EntityFrameworkCore;
using EnterpriseArchitecture.DataAccess.Repositories.OperationClaimRepository;
using EnterpriseArchitecture.DataAccess.Repositories.UserOperationClaimRepository;
using EnterpriseArchitecture.DataAccess.Repositories.UserRepository;

namespace EnterpriseArchitecture.Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();
        builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
        builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

        builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<FileManager>().As<IFileService>();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
            new ProxyGenerationOptions
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}