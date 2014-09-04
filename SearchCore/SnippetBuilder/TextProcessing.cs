using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCore.SnippetBuilder
{
    public struct Entry
    {
        public int start;
        public int end;

        public Entry(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
    }

    public static class TextProcessing
    {
        public static bool IsTextContainQueryWords(string[] queryWords, string filteredText)
        {
            bool exists = false;
            foreach(string word in queryWords)
            {
                if (filteredText.Contains(word))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        public static List<string> GetSnippetsFromText(string[] queryWords, string rawText, string filteredText, int windowWidth)
        {
            List<string> snippets = new List<string>();
            List<int> entriesOfSeparateWords = GetEntriesFromText(queryWords, filteredText);

            if (entriesOfSeparateWords == null)
                return null;

            snippets = GetSnippetsByCoordinates(GetSnippetsCoordinates(entriesOfSeparateWords, windowWidth), filteredText, 7);

            return snippets;
        }

        public static List<Entry> GetSnippetsCoordinates(List<int> entries, int windowWidth)
        {
            if (windowWidth <= 0)
                return null;

            if (entries.Count == 0)
                return null;

            List<Entry> textEntries = new List<Entry>();

            /*
            if (windowWidth > entries.Count)
            {
                windowWidth = entries.Count;
            }
            */

            for (int i = entries.Count - 1; i >= 0; i--)
            {
                bool isEntry = false;
                if (i == 0)
                    isEntry = true;
                int j = i - 1;

                while (j >= 0 && entries[i] - entries[j] + 1 <= windowWidth)
                {
                    j--;
                    isEntry = true;
                }

                if (j < 0)
                    j = 0;
                else if (isEntry && entries[i] - entries[j] + 1 > windowWidth)
                {
                    j++;
                }

                if (isEntry)
                {
                    textEntries.Add(new Entry(entries[j], entries[i]));
                }
            }

            if (textEntries.Count == 1)
            {
                for (int i = 0; i < entries.Count; i++ )
                    textEntries.Add(new Entry(entries[i], entries[i]));
            }
            
            return textEntries;
        }

        public static List<string> GetSnippetsByCoordinates(List<Entry> entries, string rawText, int n)
        {
            // n - количество слов в сниппете до и после слов из запроса

            List<string> snippets = new List<string>();
            string[] textWords = rawText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (entries == null || entries.Count == 0)
                return snippets;

            entries = FixEntriesBoundaries(entries);

            foreach(Entry e in entries)
            {
                int entryLen = e.end - e.start + 1;
                string sn = "";

                if (entryLen + 2 * n >= textWords.Length) // текст маленький, в сниппете будет весь текст
                {
                    for (int i = 0; i < textWords.Length; i++)
                    {
                        sn = sn + textWords[i] + " ";
                    }
                }
                else if (e.start <= n) // вхождение в начале текста
                {
                    for (int i = 0; i <= e.end + n; i++)
                    {
                        sn = sn + textWords[i] + " ";
                    }
                }
                else if (e.end + n >= textWords.Length - 1) // вхождение в конце текста
                {
                    for (int i = e.end - entryLen - n + 1; i < textWords.Length; i++)
                    {
                        sn = sn + textWords[i] + " ";
                    }
                }
                else // вхождение посреди текста, откусываем кусочки длины n слева и справа
                {
                    for (int i = e.end - entryLen - n + 1; i <= e.end + n; i++)
                    {
                        sn = sn + textWords[i] + " ";
                    }
                }
                snippets.Add(sn);
            }

            return snippets;
        }

        public static List<Entry> FixEntriesBoundaries(List<Entry> entries)
        {
            IOrderedEnumerable<Entry> l = entries.OrderBy(e => e.start);
            entries = l.ToList<Entry>();

            List<Entry> entriesWithoutIntersections = new List<Entry>();

            int lastAdded = -1;

            for (int i = 0; i < entries.Count - 1; i++)
            {
                if (entriesWithoutIntersections.Count != 0)
                    lastAdded = entriesWithoutIntersections[entriesWithoutIntersections.Count - 1].start;
                if (entries[i].start <= entries[i + 1].start && entries[i].start != lastAdded)
                {
                    entriesWithoutIntersections.Add(entries[i]);
                }
            }
            if (entries[entries.Count - 1].start != lastAdded)
                entriesWithoutIntersections.Add(entries[entries.Count - 1]);

            return entriesWithoutIntersections;
        }

        public static List<int> GetEntriesFromText(string[] queryWords, string filteredText)
        {
            List<int> entries = new List<int>();
            
            string[] textWords = filteredText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach(string queryWord in queryWords)
            {
                for (int i = 0; i < textWords.Length; i++)
                {
                    if (textWords[i] == queryWord)
                        entries.Add(i);
                }
            }

            if (entries.Count == 0)
            {
                foreach (string queryWord in queryWords)
                {
                    for (int i = 0; i < textWords.Length; i++)
                    {
                        if (textWords[i].Contains(queryWord))
                            entries.Add(i);
                    }
                }
            }

            if (entries.Count == 0)
                return null;

            entries.Sort();
            IEnumerable<int> distinctEntries = entries.Distinct();
            entries = distinctEntries.ToList();

            return entries;
        }



        #region snippets from code
        public static List<int> GetEntriesFromCode(string[] queryWords, string code)
        {
            TextGetters tg = new TextGetters();
            List<int> entries = new List<int>();

            string[] codeWords = code.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string queryWord in queryWords)
            {
                for (int i = 0; i < codeWords.Length; i++)
                {
                    if (codeWords[i] == queryWord)
                        entries.Add(i);
                }
            }

            return entries;
        }
        #endregion

    }
}
