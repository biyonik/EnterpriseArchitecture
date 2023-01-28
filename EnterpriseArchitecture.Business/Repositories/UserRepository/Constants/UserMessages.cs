namespace EnterpriseArchitecture.Business.Repositories.UserRepository.Constants;

public static class UserMessages
{
    public static readonly string AddNewUserSuccess = "Kullanıcı kaydı başarıyla eklendi.";
    public static readonly string AddnewUserFailed = "Kullanıcı kaydı ekleme başarısız!";
    public static readonly string UpdateUserSuccess = "Kullanıcı kaydı başarıyla güncellendi.";
    public static readonly string UpdateUserFailed = "Kullanıcı kaydı güncelleme başarısız!";
    public static readonly string DeleteUserSuccess = "Kullanıcı kaydı başarıyla silimdi.";
    public static readonly string DeleteUserFailed = "Kullanıcı kaydı silme başarısız!";
    public static readonly string UserNotFound = "Sistemde kayıtlı herhangi bir kullanıcı olmadığı için kayıt getirilemedi";
    public static readonly string WrongCurrentPassword = "Şuanki parolanızı yanlış girdiniz!";
    public static readonly string PasswordChangeSuccess = "Şifre başarıyla değiştirildi";
    public static readonly string PasswordChangeFail = "Şifre değiştirme işlemi başarısız!";
}