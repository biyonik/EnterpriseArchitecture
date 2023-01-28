using System.Net;
using Microsoft.AspNetCore.Http;

namespace EnterpriseArchitecture.Business.Repositories.Utilities;

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

        throw new Exception("File cannot be null!");
    }

    [Obsolete("Obsolete")]
    public string SaveToFtp(IFormFile? file)
    {
        if (file != null)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileName = Guid.NewGuid().ToString();
            string fileFormat = fileInfo.Extension;
            fileName = $"{fileName}{fileFormat}";
            FtpWebRequest? ftpWebRequest = WebRequest.Create($"Ftp Adresi + {file.FileName}") as FtpWebRequest;
            ftpWebRequest.Credentials = new NetworkCredential("Username", "Password");
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            using Stream ftpStream = ftpWebRequest.GetRequestStream();
            file.CopyTo(ftpStream);
            return fileName;
        }
        
        throw new Exception("File cannot be null!");
    }

    public (string content, byte[] fileBytes) FileConvertToByteForDatabase(IFormFile? file)
    {
        if (file != null)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            string readedContent = System.Convert.ToBase64String(fileBytes);
            return (readedContent, fileBytes);
        }

        throw new Exception("File cannot be null!");
    }

    public string GetExtension(IFormFile? file)
    {
        if (file != null)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            return fileInfo.Extension;  
        }

        throw new Exception("File cannot be null!");
    }

    public long GetSize(IFormFile? file)
    {
        if (file != null)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            return fileInfo.Length;
        }

        throw new Exception("File cannot be null!");
    }

    public decimal Convert(IFormFile? file, decimal multiplier)
    {
        if (IsExists(file))
        {
            var imageSize = GetSize(file);
            var convertedSize = imageSize * 0.000001;
            return (decimal)convertedSize;
        }

        throw new Exception("File cannot be null!");
    }

    public void Delete(string fileName, params string[] path)
    {
        string combinedPath = Path.Combine(Directory.GetCurrentDirectory(), string.Join('/', path), fileName);
        try
        {
            if (File.Exists(combinedPath))
            {
                File.Delete(combinedPath);
            }
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }
}