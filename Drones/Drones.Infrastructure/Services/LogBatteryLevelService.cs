using Drones.Domain.Repositories;
using Drones.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Infrastructure.HostedServices
{
    public class LogBatteryLevelService : BackgroundService
    {
        private readonly ILoggerManager _logger; 
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public LogBatteryLevelService(ILoggerManager logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;    
            _serviceScopeFactory = serviceScopeFactory; 
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            await Task.Yield();

            while (token.IsCancellationRequested == false)
            {
                _logger.LogInformation($"{GetType().Name} is starting.");

                await Task.Delay(300000, token);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var droneRepository = scope.ServiceProvider.GetRequiredService<IDroneService>();
                    var drones = await droneRepository.GetDrones();

                    foreach (var drone in drones)
                    {
                        _logger.LogInformation($"The drone {drone.SerialNumber} has the battery level at {drone.BatteryCapacity}%");
                    }
                }

                _logger.LogInformation($"{GetType().Name} is finished doing its work in starting.");
            }
        }
    }
}
