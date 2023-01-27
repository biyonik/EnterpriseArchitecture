namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class UserWithAllFields: UserWithIdDto
{
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
}