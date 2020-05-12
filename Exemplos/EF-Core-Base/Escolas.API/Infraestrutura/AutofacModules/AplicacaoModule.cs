using Autofac;
using Escola.Dominio.Alunos;
using Escola.Infra.EF.Repositorios;

namespace Escola.API.Infraestrutura.AutofacModules
{
    public sealed class AplicacaoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AlunosRepositorio>()
                .As<IAlunosRepositorio>()
                .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
