using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace CSC4151_TaskService.ASB
{
    public class EndpointInitializer : BackgroundService
    {
        private readonly ServiceBusClient _client;

        public EndpointInitializer(ServiceBusClient client)
        {
            _client = client;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.StartAsync();

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {

            await _client.StopAsync();

            await base.StopAsync(cancellationToken);
        }
    }
}

