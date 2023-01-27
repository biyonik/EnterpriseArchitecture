using EnterpriseArchitecture.Business.Abstract;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.Business.Concrete;

public class FileManager: IFileService
{
    public bool IsExists(IFormFile? file)
    {
        FileInfo fileInfo = new FileInfo(file.FileName);
        return fileInfo.Exists;
    }
    
    public string Save(IFormFile? file, params string[] path)
    {
        if (file != null)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileName = Guid.NewGuid().ToString();
            string fileFormat = fileInfo.Extension;
            fileName = $"{fileName}{fileFormat}";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), string.Join('/', path), fileName);
            using var stream = File.Create(filePath);
            file?.CopyTo(stream);
            return fileName;
        }

        throw new Exception("File not found!");
    }

    public string GetExtension(IFormFile? file)
    {
        if (file != null)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            return fileInfo.Extension;  
        }

        throw new Exception("File not found!");
    }

    public long GetSize(IFormFile? file)
    {
        if (file != null)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            return fileInfo.Length;
        }

        throw new Exception("File not found!");
    }

    public decimal Convert(IFormFile? file, decimal multiplier)
    {
        if (IsExists(file))
        {
            var imageSize = GetSize(file);
            var convertedSize = imageSize * 0.000001;
            return (decimal)convertedSize;
        }

        throw new Exception("File not found!");
    }
}