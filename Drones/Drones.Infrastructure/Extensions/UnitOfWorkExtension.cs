using Drones.Domain;
using Drones.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Drones.Infrastructure.Repositories;

public static class UnitOfWorkExtension
{
    public static IServiceCollection SetupUnitOfWork([NotNullAttribute] this IServiceCollection serviceCollection)
    {        
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>(f =>
        {
            var scopeFactory = f.GetRequiredService<IServiceScopeFactory>();
            var context = f.GetService<ApiContext>();
            return new UnitOfWork(
                context,
                new DroneRepository(context.Drones),
                new MedicationRepository(context.Medications),
                new DroneMedicationRepository(context.DroneMedications)
            );         
        });
        return serviceCollection;
    }
}
