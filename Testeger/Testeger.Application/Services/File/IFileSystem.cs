namespace Testeger.Application.Services.File;

public interface IFileSystem
{
    bool FileExists(string path);
    Stream OpenRead(string path);
}
