namespace SearchCore.Parsers
{
    public sealed class PageContent
    {
        public PageContent(
            string title, 
            string[] paragraphs, 
            string[] tags, 
            string[] votes)
        {
            Votes = votes;
            Tags = tags;
            Paragraphs = paragraphs;
            Title = title;
        }

        public string Title { get; private set; }

        public string[] Paragraphs { get; private set; }

        public string[] Tags { get; private set; }

        public string[] Votes { get; private set; }
    }
}
