namespace SearchCore.SnippetBuilder
{
    public interface ISnippetBuilder
    {
        string BuildSnippet(string userQuery, int fileId);
    }
}
