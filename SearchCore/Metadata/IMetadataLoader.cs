namespace SearchCore.Metadata
{
    public interface IMetadataLoader
    {
        void Save(int fileId, IMetadata metadataDictionary);

        IMetadata Load(int fileId);
    }
}
