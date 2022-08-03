using DDDTest.Application.EventStoreHandlers;
using DDDTest.Domain.Events;
using EventStore.Client;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTest.Application.Infrastrutures;
public static class EventStoreExtention {
    public static IServiceCollection AddEventStore(this IServiceCollection services) {
        var settings = EventStoreClientSettings
         .Create("esdb://127.0.0.1:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000");

        var client = new EventStoreClient(settings);
        services.AddSingleton(client);
        var persistClient = new EventStorePersistentSubscriptionsClient(settings);
        var userCredentials = new UserCredentials("admin", "changeit");

        var persistentSettings = new EventStore.Client.PersistentSubscriptionSettings();
        persistClient.CreateAsync(
           nameof(UserAddedDomainEvent),
           $"{nameof(UserAddedDomainEvent)}-group",
           persistentSettings,
           userCredentials: userCredentials);

        var subscription = persistClient.SubscribeToStreamAsync(
           nameof(UserAddedDomainEvent),
           $"{nameof(UserAddedDomainEvent)}-group",
           async (subscription, evnt, retryCount, cancellationToken) => {

               try {
                   await UserAddedHandler.HandleUserAddedEvent(evnt, services);
                   await subscription.Ack(evnt);
               }
               catch (Exception ex) {
                   await subscription.Nack(PersistentSubscriptionNakEventAction.Retry, ex.Message, evnt);
               }

           }, (subscription, dropReason, exception) => {
           });
        return services;
    }
}

