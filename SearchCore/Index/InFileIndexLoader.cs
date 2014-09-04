using System.IO;
using ProtoBuf;

namespace SearchCore.Index
{
    public sealed class InFileIndexLoader : IIndexLoader
    {
        public InFileIndexLoader(string folder)
        {
            _folder = folder;
        }

        public void Save(IIndex indexDictionary)
        {
            if (File.Exists(IndexFilePath))
            {
                File.Delete(IndexFilePath);    
            }

            using (var file = File.Create(IndexFilePath))
            {
                Serializer.Serialize(file, indexDictionary);
            }
        }

        public IIndex Load()
        {
            if (!File.Exists(IndexFilePath))
            {
                throw new FileNotFoundException("There is no index file:", IndexFilePath);
            }

            using (var file = File.Open(IndexFilePath, FileMode.Open))
            {
                return Serializer.Deserialize<InMemoryIndex>(file);
            }
        }

        private string IndexFilePath
        {
            get { return Path.Combine(_folder, IndexFileName); }
        }

        private readonly string _folder;
        private const string IndexFileName = "SearchIndex";
    }
}
