using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Template.Domain.Repositories;

namespace Template.Infrastructure.Services;

// Note: This Class Should not be used in any Repository Because It has nothing to do with database operations
public class FileService(IWebHostEnvironment environment) : IFileService
{
    /// <summary>
    ///     Note::
    ///     path: The relative path to Images/{path}
    /// </summary>
    public List<string> SaveFiles(List<IFormFile> files, string path, string[] allowedFileExtensions)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        //var prefixedPath = $"Images/{path}";

        string fileName;
        string filePath;
        List<string> filesPaths = [];

        foreach (var file in files)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            var extension = Path.GetExtension(file.FileName);

            if (!allowedFileExtensions.Contains(extension))
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");

            fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
            filePath = Path.Combine(environment.ContentRootPath, path, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            filesPaths.Add(Path.Combine(path, fileName));
        }

        return filesPaths;
    }

    public string SaveFile(IFormFile file, string path, string[] allowedFileExtensions)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        //var prefixedPath = $"Images/{path}";

        if (file == null) throw new ArgumentNullException(nameof(file));
        var extension = Path.GetExtension(file.FileName);

        if (!allowedFileExtensions.Contains(extension))
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");

        var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(environment.ContentRootPath, path, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyToAsync(stream);
        }

        var finalfilePaths = Path.Combine(path, fileName);

        return finalfilePaths;
    }

    public void DeleteFile(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

        if (!File.Exists(path)) throw new FileNotFoundException("Invalid file path");

        File.Delete(path);
    }
}