using System.Collections.Generic;

namespace SearchCore.Index
{
    public interface IIndex
    {
        int GetWordFrequency(string word, int fileId);

        IEnumerable<int> GetFilesWithWord(string word);

        IEnumerable<int> GetFilesWithTag(string tag);

        int GetDocumentsCountWithWord(string word);

        void AppendWord(string word, int fileId);

        void AppendTag(string tag, int fileId);

        void AppendDocument();

        int DocumentCount { get; }
    }
}
