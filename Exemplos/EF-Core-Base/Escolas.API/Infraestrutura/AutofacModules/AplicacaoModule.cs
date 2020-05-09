using Autofac;
using Escola.Dominio.Alunos;
using Escolas.Infra.EF.Repositorios;

namespace Escolas.API.Infraestrutura.AutofacModules
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
