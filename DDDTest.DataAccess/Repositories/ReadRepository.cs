using DDDTest.Domain.Framework.Repositories;

namespace DDDTest.DataAccess.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class {
    public ReadRepository() {

    }
}


