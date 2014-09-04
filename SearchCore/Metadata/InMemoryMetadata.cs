using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace SearchCore.Metadata
{
    [Serializable, ProtoContract]
    public sealed class InMemoryMetadata : IMetadata
    {
        public InMemoryMetadata()
        {
            _metadataDictionary = new Dictionary<string, IList<WordPosition>>();
        }

        public InMemoryMetadata(IDictionary<string, IList<WordPosition>> metadataDictionary)
        {
            _metadataDictionary = metadataDictionary;
        }

        public IEnumerable<WordPosition> GetWordPositions(string word)
        {
            if (!_metadataDictionary.ContainsKey(word))
            {
                return new WordPosition[0];
            }

            return _metadataDictionary[word];
        }

        public void AppendWord(string word, WordPosition position)
        {
            if (_metadataDictionary.ContainsKey(word))
            {
                if (_metadataDictionary[word].All(x => !x.Equals(position)))
                {
                    _metadataDictionary[word].Add(position);
                }
            }
            else
            {
                _metadataDictionary.Add(new KeyValuePair<string, IList<WordPosition>>(
                    word, 
                    new List<WordPosition>
                        {
                            position
                        }));
            }
        }

        public void AppendTag(string tag)
        {
            if (!_tags.Contains(tag))
            {
                _tags.Add(tag);
            }
        }

        public void AppendVote(int vote)
        {
            _totalVote += vote;
        }

        public IList<string> GetTags()
        {
            return _tags;
        }

        public int GetTotalVote()
        {
            return _totalVote;
        }

        public IDictionary<string, IList<WordPosition>> GetStatistics()
        {
            return _metadataDictionary;
        }

        [ProtoMember(1)]
        private readonly IDictionary<string, IList<WordPosition>> _metadataDictionary;

        [ProtoMember(2)]
        private readonly IList<string> _tags = new List<string>();

        [ProtoMember(3)]
        private int _totalVote;
    }
}
