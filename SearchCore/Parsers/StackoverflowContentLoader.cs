using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using SearchCore.Utils;


namespace SearchCore.Parsers
{
    public sealed class StackoverflowContentLoader : IContentLoader
    {
        public PageContent LoadData(string content)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);
            var pageTitle = string.Empty;
            
            var titleTag = document
                .DocumentNode
                .Descendants(HtmlUtils.MainTitleTag)
                .FirstOrDefault();

            if (titleTag != null)
            {
                pageTitle = HtmlUtils.CleanTags(titleTag.InnerText);
            }

            var paragraphs = SelectTagInnerText(
                document
                    .DocumentNode
                    .Descendants(HtmlUtils.DivTag),
                ContentClass);

            var tags = SelectTagInnerText(
                document.DocumentNode.Descendants(HtmlUtils.LinkTag),
                TagsClass).Distinct().ToArray();

            var votes = SelectTagInnerText(
                document.DocumentNode.Descendants(HtmlUtils.SpanTag),
                VoteClass).ToArray();

            return new PageContent(
                pageTitle, 
                paragraphs, 
                tags,
                votes);
        }

        private static string[] SelectTagInnerText(
            IEnumerable<HtmlNode> nodes, 
            string tagClass)
        {
            return nodes.Where(x =>
                (x.Attributes.Contains("class") &&
                 x.Attributes["class"].Value.Split(' ').Contains(tagClass)))
                .Select(x => HtmlUtils.CleanTags(x.InnerText)).ToArray();
        }

        private const string ContentClass = "post-text";
        private const string TagsClass = "post-tag";
        private const string VoteClass = "vote-count-post";
    }
}
