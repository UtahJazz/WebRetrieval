using System;
using System.Collections.Generic;
using System.Linq;
using SearchCore.TextFilter;
using HtmlAgilityPack;

namespace SearchCore.SnippetBuilder
{
    public sealed class SnippetBuilderStub : ISnippetBuilder
    {
        public string BuildSnippet(string userQuery, int fileId)
        {
            ITextFilter _punctuationFilter = new PunctuationTextFilter();
            string query = userQuery;

            string filteredQuery = _punctuationFilter.Filter(userQuery);
            int windowWidth = _punctuationFilter.Filter(query).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;

            TextGetters tg = new TextGetters();

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            htmlDoc.Load("../../../ForwardIndex/" + fileId.ToString() + ".htm");
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                return string.Empty;
            }

            try
            {
                string[] queryWordsFiltered = filteredQuery.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string[] queryWordsUnfiltered = query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                // получаем коды из вопроса
                List<string> qCodes = tg.GetQuestionCodes(htmlDoc);

                // получаем коды из ответов
                List<string> aCodes = tg.GetAnswersCodes(htmlDoc);

                // получаем текст вопроса и фильтруем его
                string qText = tg.GetQuestionText(htmlDoc);
                qText = qText.Replace("\r", "");
                qText = qText.Replace("\n", "");
                string filteredQText = _punctuationFilter.Filter(qText);

                // сниппеты из кодов вопроса
                List<string> snippetsQCodes = new List<string>();
                if (qCodes != null)
                {
                    foreach (string qCode in qCodes)
                    {
                        //List<string> currentTextSnippets = TextProcessing.GetSnippetsFromText(queryWords, qCode, _punctuationFilter.Filter(qCode), windowWidth);
                        List<string> currentTextSnippets = TextProcessing.GetSnippetsFromText(queryWordsUnfiltered, qCode,
                            qCode, windowWidth);
                        if (currentTextSnippets != null && currentTextSnippets.Count != 0)
                            foreach (string snippet in currentTextSnippets)
                                snippetsQCodes.Add(snippet);
                    }
                }


                // сниппеты из кодов ответов
                List<string> snippetsACodes = new List<string>();
                if (aCodes != null)
                {
                    int i = 0;
                    foreach (string aCode in aCodes)
                    {
                        i++;
                        //List<string> currentTextSnippets = TextProcessing.GetSnippetsFromText(queryWords, aCode, _punctuationFilter.Filter(aCode), windowWidth);
                        List<string> currentTextSnippets = TextProcessing.GetSnippetsFromText(queryWordsUnfiltered, aCode,
                            aCode, windowWidth);
                        if (currentTextSnippets != null && currentTextSnippets.Count != 0)
                            foreach (string snippet in currentTextSnippets)
                                snippetsACodes.Add(snippet);
                    }
                }


                // получаем тексты ответов
                List<string> aTexts = tg.GetAnswersTexts(htmlDoc);

                // сниппеты из текста вопроса
                List<string> snippetsQText = TextProcessing.GetSnippetsFromText(queryWordsFiltered, qText, filteredQText, windowWidth);

                // сниппеты из текста ответа
                List<string> snippetsATexts = new List<string>();
                if (aTexts != null)
                {
                    foreach (string aText in aTexts)
                    {
                        List<string> currentTextSnippets = TextProcessing.GetSnippetsFromText(queryWordsFiltered, aText,
                            _punctuationFilter.Filter(aText), windowWidth);
                        if (currentTextSnippets != null && currentTextSnippets.Count != 0)
                            foreach (string snippet in currentTextSnippets)
                                snippetsATexts.Add(snippet);
                    }
                }

                // заголовок
                string title = tg.GetTitle(htmlDoc);
                foreach (string queryWord in queryWordsFiltered)
                {
                    title = title.Replace(queryWord, "<b>" + queryWord + "</b>");
                }

                if (snippetsQText == null && snippetsATexts == null && snippetsQCodes == null && snippetsACodes == null)
                {
                    return title;
                }

                string bestSnippetQText = SnippetsCompetition.GetBestSnippet(snippetsQText, userQuery);
                string bestSnippetATexts = SnippetsCompetition.GetBestSnippet(snippetsATexts, userQuery);
                string bestSnippetQCodes = SnippetsCompetition.GetBestSnippet(snippetsQCodes, userQuery);
                string bestSnippetACodes = SnippetsCompetition.GetBestSnippet(snippetsACodes, userQuery);

                string result = "";
                if (bestSnippetQText != "")
                {
                    result = result + bestSnippetQText + "...";
                }
                if (bestSnippetATexts != "")
                {
                    result = result + bestSnippetATexts + "...";
                }

                if (result != "")
                    result = result + "<br>";

                if (bestSnippetQCodes != "")
                {
                    result = result + bestSnippetQCodes + "<br>";
                }
                if (bestSnippetACodes != "")
                {
                    result = result + bestSnippetACodes + "<br>";
                }

                SnippetStylerStub sss = new SnippetStylerStub();
                result = sss.Styled(result, userQuery);

                if (result != "")
                    return result;
                else
                    return title;
            }
            catch
            {
                string title = tg.GetTitle(htmlDoc);
                return title;
            }
            //return bestSnippetQText;
        }
    }
}
