using System.IO;

namespace SearchCore.FileIdMatcher
{
    public class ByNameFileIdMatcher : IFileIdMatcher
    {
        public ByNameFileIdMatcher(string extension)
        {
            _extension = extension;
        }

        public int MatchFile(string fileName)
        {
// ReSharper disable AssignNullToNotNullAttribute
            return int.Parse(Path.GetFileNameWithoutExtension(fileName));
// ReSharper restore AssignNullToNotNullAttribute
        }

        public string GetFileById(int fileId)
        {
            return fileId + _extension;
        }

        public string GetMatchedExtention()
        {
            return _extension;
        }

        public void Load()
        {
        }

        public void Save()
        {
        }

        private readonly string _extension;
    }
}
