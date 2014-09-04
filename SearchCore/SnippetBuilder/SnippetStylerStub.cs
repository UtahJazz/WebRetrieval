using System;
using System.Collections.Generic;
using System.Linq;
using SearchCore.TextFilter;
using HtmlAgilityPack;

namespace SearchCore.SnippetBuilder
{
    public sealed class SnippetStylerStub : ISnippetStyler
    {
        public string Styled(string snippetText, string userQuery)
        {
            ITextFilter _punctuationFilter = new PunctuationTextFilter();
            string[] queryWordsUnfiltered = userQuery.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string styledSnippet = snippetText;


            foreach (string queryWord in queryWordsUnfiltered)
            {
                if (styledSnippet.ToLower().Contains(queryWord))
                {
                    int i = styledSnippet.ToLower().IndexOf(queryWord);
                    string tmp = styledSnippet.Substring(i, queryWord.Length);
                    styledSnippet = styledSnippet.Replace(tmp, "<b>" + tmp + "</b>");
                }
                styledSnippet = styledSnippet.Replace(queryWord, "<b>" + queryWord + "</b>");
            }

            return styledSnippet;
        }
    }
}