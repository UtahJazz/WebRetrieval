using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using SearchCore.DirectoryProvider;
using SearchCore.FileIdMatcher;

namespace SearchCore.Metadata
{
    [Serializable, ProtoContract]
    public sealed class InMemoryMetadataPool : IMetadataPool
    {
        public InMemoryMetadataPool()
        {
            _pool = new Dictionary<int, IMetadata>();
        }

        public void Load(
            IDirectoryProvider metadataDirectoryProvider,
            IFileIdMatcher fileIdMatcher,
            IMetadataLoader metadataLoader)
        {
            foreach (var metadataId in metadataDirectoryProvider
                    .GetFiles()
                    .Select(fileIdMatcher.MatchFile))
            {
                _pool.Add(
                    new KeyValuePair<int, IMetadata>(
                        metadataId,
                        metadataLoader.Load(metadataId)));
            }
        }

        public IMetadata GetMetadata(int fileId)
        {
            lock (_pool)
            {
                return _pool[fileId];
            }
        }

        public void AppendMetadata(int fileId, IMetadata metadata)
        {
            _pool.Add(fileId, metadata);
        }

        [ProtoMember(1)]
        private readonly IDictionary<int, IMetadata> _pool = 
            new Dictionary<int, IMetadata>();
    }
}
