namespace EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

public class UserForListWithAllFieldsDto: UserForListWithOnlyIdDto
{
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
}