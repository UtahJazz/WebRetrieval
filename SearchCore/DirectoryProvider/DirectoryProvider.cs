using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SearchCore.FileIdMatcher;

namespace SearchCore.DirectoryProvider
{
    public sealed class DirectoryProvider : IDirectoryProvider
    {
        public DirectoryProvider(
            string directoryPath,
            IFileIdMatcher fileIdMather)
        {
            _directoryPath = directoryPath;
            _fileIdMather = fileIdMather;
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory
                .GetFiles(_directoryPath)
                .Where(x => 
                    String.Equals(
                    _fileIdMather.GetMatchedExtention(), 
                        Path.GetExtension(x), 
                        StringComparison.CurrentCultureIgnoreCase));
        }

        public string GetFilePath(int file)
        {
            return Path.Combine(_directoryPath, _fileIdMather.GetFileById(file));
        }

        public string GetFileContent(string file)
        {
            if (!File.Exists(file))
            {
                file = Path.Combine(_directoryPath, file);
            }

            return File.ReadAllText(file);
        }

        private readonly IFileIdMatcher _fileIdMather;

        private readonly string _directoryPath;
    }
}
