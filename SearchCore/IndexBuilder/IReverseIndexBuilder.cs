using SearchCore.Index;
using SearchCore.Parsers;

namespace SearchCore.IndexBuilder
{
    public interface IReverseIndexBuilder
    {
        void AppendFile(PageContent fileContent, int fileId);

        IIndex GetIndex();
    }
}
