using System.Collections.Generic;
using ProtoBuf;

namespace SearchCore.Metadata
{
    [ProtoContract]
    public interface IMetadata
    {
        IEnumerable<WordPosition> GetWordPositions(string word);

        void AppendWord(string word, WordPosition position);

        void AppendTag(string tag);

        void AppendVote(int vote);

        IList<string> GetTags();

        int GetTotalVote();

        IDictionary<string, IList<WordPosition>> GetStatistics();
    }
}
