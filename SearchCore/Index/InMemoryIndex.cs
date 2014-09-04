using System;
using System.Collections.Generic;
using ProtoBuf;

namespace SearchCore.Index
{
    [Serializable]
    [ProtoContract]
    public sealed class InMemoryIndex : IIndex
    {
        public int GetWordFrequency(string word, int fileId)
        {
            if (!_indexDictionary.ContainsKey(word))
            {
                return 0;
            }
            lock (_indexDictionary)
            {
                return _indexDictionary[word].GetFileFrequency(fileId);
            }
        }

        public IEnumerable<int> GetFilesWithWord(string word)
        {
            if (!_indexDictionary.ContainsKey(word))
            {
                return new int[0];
            }

            return _indexDictionary[word].Files;
        }

        public IEnumerable<int> GetFilesWithTag(string tag)
        {
            if (!_tagIndex.ContainsKey(tag))
            {
                return new int[0];
            }

            return _tagIndex[tag].Files;
        }

        public int GetDocumentsCountWithWord(string word)
        {
            if (!_indexDictionary.ContainsKey(word))
            {
                return 0;
            }

            return _indexDictionary[word].Files.Count;
        }

        public void AppendWord(string word, int fileId)
        {
            AppendToIndex(_indexDictionary, word, fileId);
        }

        public void AppendTag(string tag, int fileId)
        {
            AppendToIndex(_tagIndex, tag, fileId);
        }

        public void AppendDocument()
        {
            DocumentCount++;
        }

        [ProtoMember(1)]
        public int DocumentCount { get; private set; }

        private static void AppendToIndex(IDictionary<string, WordStatistics> index, string word, int fileId)
        {
            lock (index)
            {
                word = word.ToLower();
                if (index.ContainsKey(word))
                {
                    index[word].AddFile(fileId);
                }
                else
                {
                    index.Add(word, new WordStatistics(fileId));
                }
            }
        }

        [ProtoMember(2)]
        private readonly IDictionary<string, WordStatistics> _indexDictionary = new Dictionary<string, WordStatistics>();

        [ProtoMember(3)]
        private readonly IDictionary<string, WordStatistics> _tagIndex = new Dictionary<string, WordStatistics>();
    
    }
}
