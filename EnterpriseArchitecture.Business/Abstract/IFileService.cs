using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.Business.Abstract;

public interface IFileService
{
    bool IsExists(IFormFile? file);
    string Save(IFormFile? file, params string[] path);
    string SaveToFtp(IFormFile? file);
    (string content, byte[] fileBytes) FileConvertToByteForDatabase(IFormFile? file);
    string GetExtension(IFormFile? file);
    long GetSize(IFormFile? file);
    decimal Convert(IFormFile? file, decimal multiplier);
}