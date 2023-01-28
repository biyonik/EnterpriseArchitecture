namespace EnterpriseArchitecture.Business.Authentication.Constants;

public class AuthMessages
{
    public static readonly string RegisterSuccess = "Kullanıcı kaydı başarıyla tamamlandı.";
    public static readonly string RegisterFail = "Kullanıcı kaydı başarısız!.";
    public static readonly string UserNotFound = "Böyle bir kullanıcı bulunamadı!";
    public static readonly string UserInfoWrong = "Kullanıcı bilgileriniz yanlış, lütfen tekrar deneyiniz.";
    public static readonly string EmailAlreadyUsed = "Bu mail adresi daha önce kullanılmış!";
    public static string ImageSizeLimitError(string sizeLimit, string sizeType = "MB") 
        => $"Yüklediğiniz resim boyutu en fazla {sizeLimit} {sizeType} olabilir.";

    public static readonly string WrongFileFormat = "Yüklediğiniz dosya formatı, uyumlu bir format değildi!";
    public static readonly string FileNotReaded = "Dosya okunamadı!";
}