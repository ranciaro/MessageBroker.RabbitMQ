using BrokerMessage.RabbitMQ.Core;
using BrokerMessage.RabbitMQ.Core.Messages;
using BrokerMessage.RabbitMQ.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silverback.Messaging;
using Silverback.Messaging.Configuration;

namespace BrokerMessage.RabbitMQ.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICustomerPublisher, CustomerPublisher>();

            services.AddSilverback()
                    .UseModel()
                    .WithConnectionToMessageBroker(options => options
                    .AddRabbit()
                    .AddOutboundConnector());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BusConfigurator busConfigurator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            busConfigurator
            .Connect(endpoints => endpoints
                .AddOutbound<CustomerMessage>(
                    new RabbitQueueProducerEndpoint("create-customer")
                    {
                        Connection = GetConnection(),
                        Queue = GetQueueConfiguration()
                    }));
        }
        private RabbitQueueConfig GetQueueConfiguration() => new RabbitQueueConfig
        {
            IsDurable = true,
            IsExclusive = false,
            IsAutoDeleteEnabled = false
        };

        private RabbitConnectionConfig GetConnection() => new RabbitConnectionConfig
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
    }
}
