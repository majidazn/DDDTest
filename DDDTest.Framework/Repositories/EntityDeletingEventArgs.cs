namespace DDDTest.Domain.Framework.Repositories;
public class EntityDeletingEventArgs<T> : EventArgs {
    public T SavedEntity;

}

