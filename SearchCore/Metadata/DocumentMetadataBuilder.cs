using System.Linq;
using SearchCore.Parsers;
using SearchCore.TextFilter;
using SearchCore.Utils;

namespace SearchCore.Metadata
{
    public sealed class DocumentMetadataBuilder
    {
        public DocumentMetadataBuilder(
            ITextFilter filter,
            IMetadataFactory factory)
        {
            _filter = filter;
            _factory = factory;
        }

        public IMetadata Build(PageContent pageContent)
        {
            var metadata = _factory.Create();

            FillTitle(metadata, pageContent);
            FillContent(metadata, pageContent);
            FillTags(metadata, pageContent);
            FillVotes(metadata, pageContent);

            return metadata;
        }

        private void FillVotes(IMetadata metadata, PageContent pageContent)
        {
            foreach (var vote in pageContent.Votes)
            {
                metadata.AppendVote(int.Parse(vote));
            }
        }

        private void FillTags(IMetadata metadata, PageContent pageContent)
        {
            foreach (var tag in pageContent.Tags)
            {
                metadata.AppendTag(tag);
            }
        }

        private void FillContent(IMetadata metadata, PageContent pageContent)
        {
            for (var i = 0; i < pageContent.Paragraphs.Count(); i++)
            {
                var paragraphWordsBag = GetParagraphWordBag(pageContent.Paragraphs[i]);

                for (var wordPosition = 0; wordPosition < paragraphWordsBag.Count(); wordPosition++)
                {
                    metadata.AppendWord(
                        paragraphWordsBag[wordPosition],
                        new WordPosition(i + 1, wordPosition));
                }
            }
        }

        private void FillTitle(IMetadata metadata, PageContent pageContent)
        {
            var titleWordBag = GetParagraphWordBag(pageContent.Title);

            for (var wordPosition = 0; wordPosition < titleWordBag.Count(); wordPosition++)
            {
                metadata.AppendWord(
                    titleWordBag[wordPosition],
                    new WordPosition(0, wordPosition));
            }
        }

        private string[] GetParagraphWordBag(string paragraph)
        {
            return StringUtils.SplitByLowerWord(
                    _filter.Filter(paragraph)).ToArray();
        }

        private readonly ITextFilter _filter;
        private readonly IMetadataFactory _factory;
    }
}
