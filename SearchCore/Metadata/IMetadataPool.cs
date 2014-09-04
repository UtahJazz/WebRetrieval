using SearchCore.DirectoryProvider;
using SearchCore.FileIdMatcher;

namespace SearchCore.Metadata
{
    public interface IMetadataPool
    {
        IMetadata GetMetadata(int fileId);

        void AppendMetadata(int fileId, IMetadata metadata);

        void Load(
            IDirectoryProvider metadataDirectoryProvider,
            IFileIdMatcher fileIdMatcher,
            IMetadataLoader metadataLoader);
    }
}
