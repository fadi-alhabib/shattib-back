using Microsoft.AspNetCore.Http;

namespace Template.Domain.Repositories;

public interface IFileService
{
    List<string>? SaveFiles(List<IFormFile> file, string path, string[] allowedFileExtensions);
	string SaveFile(IFormFile file, string path, string[] allowedFileExtensions);
	void DeleteFile(string fileNameWithExtension);
}