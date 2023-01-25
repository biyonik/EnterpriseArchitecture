using EnterpriseArchitecture.Entities.Abstract;

namespace EnterpriseArchitecture.Entities.Concrete;

public sealed class User: IEntityWithId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }

    public UserOperationClaim UserOperationClaim { get; set; }
}