using DDDTest.Domain.Framework.SeedWork;
using EventStore.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.DataAccess.Infrastrutures {
    public static class MediatorExtension {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx, EventStoreClient eventStoreClient) {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents) {
                //await mediator.Publish(domainEvent);
                var jsonString = JsonConvert.SerializeObject(domainEvent,
                   new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

                var jsonPayload = Encoding.UTF8.GetBytes(jsonString);

                var eventStoreDataType = new EventData(Uuid.NewUuid(), domainEvent.GetType().Name, jsonPayload);

                await eventStoreClient.AppendToStreamAsync(domainEvent.GetType().Name, StreamState.Any, new[] { eventStoreDataType });
            }





        }
    }
}
