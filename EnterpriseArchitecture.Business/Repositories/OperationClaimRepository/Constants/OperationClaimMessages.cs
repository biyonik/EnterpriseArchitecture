namespace EnterpriseArchitecture.Business.Repositories.OperationClaimRepository.Constants;

public static class OperationClaimMessage
{
    public static readonly string AddNewOperationClaimSuccess = "Yetki kaydı başarıyla eklendi.";
    public static readonly string AddnewOperationClaimFailed = "Yetki kaydı ekleme başarısız!";
    public static readonly string UpdateOperationClaimSuccess = "Yetki kaydı başarıyla güncellendi.";
    public static readonly string UpdateOperationClaimFailed = "Yetki kaydı güncelleme başarısız!";
    public static readonly string DeleteOperationClaimSuccess = "Yetki kaydı başarıyla silimdi.";
    public static readonly string DeleteOperationClaimFailed = "Yetki kaydı silme başarısız!";
    public static readonly string OperationClaimNotFound = "Sistemde kayıtlı herhangi bir yetki olmadığı için kayıt getirilemedi";
}