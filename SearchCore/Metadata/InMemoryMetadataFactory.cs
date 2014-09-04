namespace SearchCore.Metadata
{
    public sealed class InMemoryMetadataFactory : IMetadataFactory
    {
        public IMetadata Create()
        {
            return new InMemoryMetadata();
        }
    }
}
