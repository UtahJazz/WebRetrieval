using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public sealed class Snippet
    {
        public Snippet(
            string snippetText, 
            string snippetUrl,
            string[] tags,
            int fileId)
        {
            SnippetText = snippetText;
            SnippetUrl = snippetUrl;
            Tags = tags;
            FileId = fileId;
        }

        [DataMember]
        public string SnippetText { get; private set; }

        [DataMember]
        public string SnippetUrl { get; private set; }

        [DataMember]
        public string[] Tags { get; private set; }

        [DataMember]
        public int FileId { get; private set; }
    }
}
