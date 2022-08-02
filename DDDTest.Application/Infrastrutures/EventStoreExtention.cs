using DDDTest.Application.EventStoreHandlers;
using DDDTest.Domain.Events;
using EventStore.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Application.Infrastrutures {
    public static class EventStoreExtention {
        public static IServiceCollection AddEventStore(this IServiceCollection services ) {
            var settings = EventStoreClientSettings
.Create("esdb://127.0.0.1:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000");
            var client = new EventStoreClient(settings);



            services.AddSingleton(client);


            client.SubscribeToStreamAsync(nameof(UserAddedDomainEvent),
   EventStore.Client.StreamPosition.Start,
   async (subscription, evnt, cancellationToken) => {
       Console.WriteLine($"Received event {evnt.OriginalEventNumber}@{evnt.OriginalStreamId}");
       await  UserAddedHandler.HandleUserAddedEvent(evnt,services);


       

   });

            return services;


        }
    }
}
