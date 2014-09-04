using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchCore.TextFilter;
using HtmlAgilityPack;


namespace SearchCore.SnippetBuilder
{
    public static class SnippetsCompetition
    {
        public static string GetBestSnippet(List<string> snippets, string userQuery)
        {
            if (snippets == null || snippets.Count == 0)
                return string.Empty;

            ITextFilter _punctuationFilter = new PunctuationTextFilter();
            string filteredQuery = _punctuationFilter.Filter(userQuery), filteredSnippet;
            string[] separator = new string[] { " " };
            string[] queryWords = filteredQuery.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<double, string> d = new Dictionary<double, string>();

            foreach (string snippet in snippets)
            {
                filteredSnippet = _punctuationFilter.Filter(snippet);
                string[] snippetWords = filteredSnippet.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                double mark = Test1(queryWords, snippetWords);

                if (!d.ContainsKey(mark))
                    d.Add(mark, snippet);
                else
                {
                    d[mark] = d[mark] + "... " + snippet;
                }
            }

            string best = d.OrderBy(r => r.Key).Last().Value;

            if (best.Length < 100 && d.Count > 1)
            {
                double k = d.OrderBy(r => r.Key).Last().Key;
                d.Remove(k);
                best = best + "... " + d.OrderBy(r => r.Key).Last().Value;
            }


            return best;
        }

        public static string Comparer(Dictionary<int, string> d)
        {

            return string.Empty;
        }

        public static double Test1(String[] queryWords, String[] snippetWords) // доля слов из запроса, присутствующих в сниппете
        {
            int entriesCount = 0;
            foreach (String queryWord in queryWords)
            {
                foreach (String snippetWord in snippetWords)
                {
                    if (queryWord == snippetWord)
                        entriesCount++;
                }
            }

            double result = (double)entriesCount / (double)snippetWords.Length;
            return result;
        }
    }
}
