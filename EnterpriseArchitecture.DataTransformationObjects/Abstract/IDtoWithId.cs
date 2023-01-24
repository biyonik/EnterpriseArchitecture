namespace EnterpriseArchitecture.DataTransformationObjects.Abstract;

public interface IDtoWithId<TKey>: IDto
{
    public TKey Id { get; set; }
}