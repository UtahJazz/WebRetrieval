using System.Collections.Generic;

namespace SearchCore.DirectoryProvider
{
    public interface IDirectoryProvider
    {
        IEnumerable<string> GetFiles();

        string GetFilePath(int file);

        string GetFileContent(string file);
    }
}
