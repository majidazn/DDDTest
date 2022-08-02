using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using DDDTest.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Application.EventStoreHandlers {
    public  class UserAddedHandler {
      
        public static  Task HandleUserAddedEvent(EventStore.Client.ResolvedEvent evnt, IServiceCollection services) {
            var esJsonData = Encoding.UTF8.GetString(evnt.OriginalEvent.Data.ToArray());
            var objState = JsonConvert.DeserializeObject<UserAddedDomainEvent>(esJsonData);


            // Build the intermediate service provider
            var sp = services.BuildServiceProvider();
            var _userRepositoryQuery = sp.GetService<IUserRepositoryQuery>();
            _userRepositoryQuery.CreateUser(objState);
            _userRepositoryQuery.SaveChangesAsync();



            return Task.CompletedTask;


        }
    }
}
