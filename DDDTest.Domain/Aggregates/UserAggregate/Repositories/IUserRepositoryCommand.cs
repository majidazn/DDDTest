using DDDTest.Domain.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Domain.Aggregates.UserAggregate.Repositories {
    public interface IUserRepositoryCommand :  IWriteRepository<Entities.User>{

    }
}
