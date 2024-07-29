namespace Postech8SOAT.FastOrder.Domain.Entities;
public abstract class Entity : IEntity
{

    public Guid Id { get; protected set; }
}

public interface IEntity
{
    public Guid Id { get; }
}

public interface IAggregateRoot : IEntity { }