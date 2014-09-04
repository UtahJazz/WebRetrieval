namespace SearchCore.SnippetBuilder
{
    public interface ISnippetStyler
    {
        string Styled(string snippetText, string userQuery);
    }
}
