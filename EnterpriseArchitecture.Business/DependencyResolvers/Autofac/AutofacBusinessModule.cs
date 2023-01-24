using Autofac;
using EnterpriseArchitecture.Business.Abstract;
using EnterpriseArchitecture.Business.Concrete;
using EnterpriseArchitecture.DataAccess.Abstract;
using EnterpriseArchitecture.DataAccess.Concrete.EntityFrameworkCore;

namespace EnterpriseArchitecture.Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
        builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();
        builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
        builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();
        builder.RegisterType<AuthManager>().As<IAuthService>();
    }
}