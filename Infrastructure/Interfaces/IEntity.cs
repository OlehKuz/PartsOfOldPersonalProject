namespace Infrastructure.Interfaces
{
    public interface IEntity<TKey>:IEntity
    {
        TKey Id { get; }
    }
    public interface IEntity
    {
    }
}