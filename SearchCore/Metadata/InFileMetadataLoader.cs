using System.IO;
using ProtoBuf;
using SearchCore.DirectoryProvider;

namespace SearchCore.Metadata
{
    public sealed class InFileMetadataLoader : IMetadataLoader
    {
        public InFileMetadataLoader(
            IDirectoryProvider directoryProvider)
        {
            _directoryProvider = directoryProvider;
        }

        public void Save(int fileId, IMetadata metadata)
        {
            var fileMetadataPath = GetFileMetadataPath(fileId);
            if (File.Exists(fileMetadataPath))
            {
                File.Delete(fileMetadataPath);
            }

            using (var file = File.Create(fileMetadataPath))
            {
                Serializer.Serialize(file, metadata);
            }
        }

        public IMetadata Load(int fileId)
        {
            var fileMetadataPath = GetFileMetadataPath(fileId);
            if (!File.Exists(fileMetadataPath))
            {
                throw new FileNotFoundException("There is no index file:", fileMetadataPath);
            }

            using (var file = File.Open(fileMetadataPath, FileMode.Open))
            {
                return Serializer.Deserialize<InMemoryMetadata>(file);
            }
        }

        private string GetFileMetadataPath(int fileId)
        {
            return _directoryProvider.GetFilePath(fileId);
        }

        private readonly IDirectoryProvider _directoryProvider;
    }
}
