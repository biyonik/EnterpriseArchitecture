namespace EnterpriseArchitecture.Entities.Abstract;

public interface IEntityWithId<TKey>: IEntity
{
    public TKey Id { get; set; }
}