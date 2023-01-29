namespace EnterpriseArchitecture.Business.Repositories.UserOperationClaimRepository.Constants;

public static class UserOperationClaimMessages
{
    public static readonly string AddNewUserOperationClaimSuccess = "Kullanıcı yetki kaydı başarıyla eklendi.";
    public static readonly string AddnewUserOperationClaimFailed = "Kullanıcı yetki kaydı ekleme başarısız!";
    public static readonly string UpdateUserOperationClaimSuccess = "Kullanıcı yetki kaydı başarıyla güncellendi.";
    public static readonly string UpdateUserOperationClaimFailed = "Kullanıcı yetki kaydı güncelleme başarısız!";
    public static readonly string DeleteUserOperationClaimSuccess = "Kullanıcı yetki kaydı başarıyla silimdi.";
    public static readonly string DeleteUserOperationClaimFailed = "Kullanıcı yetki kaydı silme başarısız!";
    public static readonly string UserOperationClaimNotFound = "Sistemde kayıtlı herhangi bir kullanıcı yetkisi olmadığı için kayıt getirilemedi";
    public static readonly string OperationClaimAlreadySet = "Bu kullanıcıya, bu yetki daha önce atanmış";
}