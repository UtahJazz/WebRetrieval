using System.IO;
using ProtoBuf;

namespace SearchCore.Metadata
{
    public sealed class MetadataPoolLoader
    {
        public MetadataPoolLoader(string folder)
        {
            _folder = folder;
        }

        public void Save(IMetadataPool metadata)
        {
            if (File.Exists(MetadataFilePath))
            {
                File.Delete(MetadataFilePath);
            }

            using (var file = File.Create(MetadataFilePath))
            {
                Serializer.Serialize(file, metadata);
            }
        }

        public IMetadataPool Load()
        {
            if (!File.Exists(MetadataFilePath))
            {
                throw new FileNotFoundException("There is no index file:", MetadataFilePath);
            }

            using (var file = File.Open(MetadataFilePath, FileMode.Open))
            {
                return Serializer.Deserialize<IMetadataPool>(file);
            }
        }

        private string MetadataFilePath
        {
            get { return Path.Combine(_folder, IndexFileName); }
        }

        private readonly string _folder;
        private const string IndexFileName = "MetadataPool";
    }
}
