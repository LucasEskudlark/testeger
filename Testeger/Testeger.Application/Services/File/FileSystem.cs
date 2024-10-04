using IoFile = System.IO.File;

namespace Testeger.Application.Services.File;

public class FileSystem : IFileSystem
{
    public bool FileExists(string path)
    {
        return IoFile.Exists(path);
    }

    public Stream OpenRead(string path)
    {
        return IoFile.OpenRead(path);
    }
}
