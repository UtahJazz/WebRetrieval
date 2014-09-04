using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace SearchCore.Index
{
    [Serializable]
    [ProtoContract]
    public sealed class WordStatistics
    {
        public WordStatistics()
        {
            Files = new List<int>();
            _frequency = new List<int>();
        }

        public WordStatistics(int fileId) : this()
        {
            Files.Add(fileId);
            _frequency.Add(1);
        }

        public int GetFileFrequency(int fileId)
        {
            var fileIndex = Files.IndexOf(fileId);
            return fileIndex == -1 ? 0 : _frequency[fileIndex];
        }

        public void AddFile(int fileId)
        {
            if (Files.Any(x => x == fileId))
            {
                var fileIndex = Files.IndexOf(fileId);
                _frequency[fileIndex]++;
            }
            else
            {
                Files.Add(fileId);
                _frequency.Add(1);
            }
        }

        [ProtoMember(1)]
        public IList<int> Files { get; private set; }

        [ProtoMember(2)]
        private readonly IList<int> _frequency;
    }
}
