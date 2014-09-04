using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SearchCore.SnippetBuilder
{
    class TextGetters
    {
        public TextGetters()
        {
            String[] separators = new String[] { " " };
            //String[] queryWords = "How comparison string".Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //ExhaustText(queryWords);
            //Logic();
        }

        void ExhaustText(String[] queryWords)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;

            htmlDoc.Load("1.htm");

            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                Console.WriteLine("Reading error");

            }
            else
            {
                if (htmlDoc.DocumentNode != null)
                {
                    HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");
                    HtmlNode titleNode = htmlDoc.DocumentNode.SelectSingleNode("//title");
                    HtmlNodeCollection bigNodes = bodyNode.SelectNodes("//div[@class='post-text']");
                    HtmlNodeCollection smallNodes = bodyNode.SelectNodes("//span[@class='comment-copy']");

                    /*
                    // printing
                    if (bodyNode != null)
                    {
                        using (StreamWriter sw = new StreamWriter("result.htm"))
                        {
                            sw.WriteLine("<html>");
                            sw.WriteLine("<head>");
                            sw.WriteLine("Title:");
                            sw.WriteLine(GetHTMLForTitle(titleNode.InnerText, queryWords));
                            sw.WriteLine("<br>");
                            sw.WriteLine("<br>");
                            foreach (HtmlNode node in bigNodes)
                            {
                                sw.WriteLine("----------");
                                sw.WriteLine("<br>");
                                sw.WriteLine(node.InnerText);
                                sw.WriteLine("<br>");
                            }
                            sw.WriteLine("----------");
                            sw.WriteLine("<br>");
                            sw.WriteLine("----------");
                            sw.WriteLine("<br>");
                            sw.WriteLine("----------");
                            sw.WriteLine("<br>");
                            foreach (HtmlNode node in smallNodes)
                            {
                                sw.WriteLine(node.InnerText);
                                sw.WriteLine("----------");
                                sw.WriteLine("<br>");
                            }
                            sw.WriteLine("</head>");
                            sw.WriteLine("</html>");
                        }
                    }
                    */
                }
            }
        }


        void Logic()
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            htmlDoc.Load("767999.htm");
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                Console.WriteLine("Reading error");
                return;
            }

            GetQuestionText(htmlDoc);
            //GetQuestionCodes(htmlDoc);
            //GetAnswersTexts(htmlDoc);
            //GetAnswersCodes(htmlDoc);
            //GetTags(htmlDoc);
        }

        public String GetTitle(HtmlDocument htmlDoc)
        {
            if (htmlDoc.DocumentNode == null)
                return String.Empty;

            HtmlNode titleNode = htmlDoc.DocumentNode.SelectSingleNode("//title");

            return titleNode.InnerText;
        }

        public String GetQuestionText(HtmlDocument htmlDoc)
        {
            if (htmlDoc.DocumentNode == null)
                return String.Empty;
            
            HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (bodyNode == null)
                return String.Empty;
            
            HtmlNode questionNode = bodyNode.SelectSingleNode("//div[@itemprop='description']");

            var codeNodes = questionNode.SelectNodes(".//pre");
            if (codeNodes != null)
            {
                foreach (HtmlNode node in codeNodes)
                {
                        questionNode.RemoveChild(node);
                }
            }

            /*
             //printing
            
            String result = questionNode.InnerText;

            result = result.Replace("\n", "<br>");
            
            using (StreamWriter sw = new StreamWriter("result.htm"))
            {
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine(result);
                sw.WriteLine("</head>");
                sw.WriteLine("</html>");
            }
            */
            return questionNode.InnerText;
        }

        public List<String> GetAnswersTexts(HtmlDocument htmlDoc) // возвращает массив строк с текстом ответов
        {
            if (htmlDoc.DocumentNode == null)
                return null;

            HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (bodyNode == null)
                return null;

            var rawAnswersNodes = bodyNode.SelectNodes("//td[@class='answercell']");
            HtmlNodeCollection answersNodes = new HtmlNodeCollection(null);

            foreach (HtmlNode rawAnswerNode in rawAnswersNodes)
            {
                var tmpNode = rawAnswerNode.SelectSingleNode(".//div[@class='post-text']");
                answersNodes.Add(tmpNode);
            }

            foreach (HtmlNode answerNode in answersNodes)
            {
                var codeNodes = answerNode.SelectNodes(".//pre");
                if (codeNodes != null)
                {
                    foreach (HtmlNode codeNode in codeNodes)
                    {
                        codeNode.ParentNode.RemoveChild(codeNode);
                    }
                }     
            }

            List<String> textsOfTheAnswers = new List<string>();

            foreach (HtmlNode answerNode in answersNodes)
            {
                textsOfTheAnswers.Add(answerNode.InnerText);
            }
            /*
             //printing 
            using (StreamWriter sw = new StreamWriter("result.htm"))
            {
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");

                foreach (String textOfAnswer in textsOfTheAnswers)
                {
                    String tmp = textOfAnswer.Replace("\n", "<br>");
                    sw.WriteLine(tmp);
                    sw.WriteLine("-------");
                }

                sw.WriteLine("</head>");
                sw.WriteLine("</html>");
            }
            */

            return textsOfTheAnswers;
        }

        public List<String> GetQuestionCodes(HtmlDocument htmlDoc)
        {
            if (htmlDoc.DocumentNode == null)
                return null;

            HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (bodyNode == null)
                return null;

            HtmlNode questionNode = bodyNode.SelectSingleNode("//div[@itemprop='description']");

            var codeNodes = questionNode.SelectNodes(".//pre");
            
            List<String> codes = new List<string>();
            if (codeNodes == null)
                return null;
            foreach (HtmlNode codeNode in codeNodes)
            {
                codes.Add(codeNode.InnerText);
            }

            /*
             //printing 
            using (StreamWriter sw = new StreamWriter("result.htm"))
            {
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                
                foreach(String code in codes)
                {
                    String tmp = code.Replace("\n", "<br>");
                    sw.WriteLine(tmp);
                    sw.WriteLine("-------");
                }

                sw.WriteLine("</head>");
                sw.WriteLine("</html>");
            }
            */
            return codes;
        }

        public List<String> GetAnswersCodes(HtmlDocument htmlDoc) // возвращает массив строк с кодом из ответов
        {
            if (htmlDoc.DocumentNode == null)
                return null;

            HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (bodyNode == null)
                return null;

            var rawAnswersNodes = bodyNode.SelectNodes("//td[@class='answercell']");
            HtmlNodeCollection answersNodes = new HtmlNodeCollection(null);
            HtmlNodeCollection codeNodes = new HtmlNodeCollection(null);
            if (rawAnswersNodes == null)
                return null;
            foreach (HtmlNode rawAnswerNode in rawAnswersNodes)
            {
                var tmpNode = rawAnswerNode.SelectSingleNode(".//div[@class='post-text']");
                answersNodes.Add(tmpNode);
            }

            foreach (HtmlNode answerNode in answersNodes)
            {
                var tmpCodeNodes = answerNode.SelectNodes(".//pre");
                if (tmpCodeNodes != null)
                {
                    foreach (HtmlNode codeNode in tmpCodeNodes)
                    {
                        codeNodes.Add(codeNode);
                    }
                }
                else
                {
                    return null;
                }
                
            }

            List<String> codes = new List<string>();
            foreach (HtmlNode codeNode in codeNodes)
            {
                codes.Add(codeNode.InnerText);
            }
            /*
             //printing 
            using (StreamWriter sw = new StreamWriter("result.htm"))
            {
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");

                foreach (String code in codes)
                {
                    String tmp = code.Replace("\n", "<br>");
                    sw.WriteLine(tmp);
                    sw.WriteLine("-------");
                    sw.WriteLine("<br>");
                }

                sw.WriteLine("</head>");
                sw.WriteLine("</html>");
            }
            */

            return codes;
        }

        public List<String> GetTags(HtmlDocument htmlDoc)
        {
            if (htmlDoc.DocumentNode == null)
                return null;

            HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

            if (bodyNode == null)
                return null;

            var tagsNode = bodyNode.SelectSingleNode("//div[@class='post-taglist']");
            var tagsRefs = tagsNode.SelectNodes(".//a");

            if (tagsRefs == null)
            {
                return null;
            }

            List<String> tags = new List<string>();

            foreach (HtmlNode tagRef in tagsRefs)
            {
                tags.Add(tagRef.InnerText);
            }
            /*
             //printing 
            using (StreamWriter sw = new StreamWriter("result.htm"))
            {
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");

                foreach (String tag in tags)
                {
                    sw.WriteLine(tag);
                    sw.WriteLine("-------");
                }

                sw.WriteLine("</head>");
                sw.WriteLine("</html>");
            }
            */

            return tags;
        }

        public String GetHTMLForTitle(String titleText, String[] queryWords)
        {
            foreach (String word in queryWords)
            {
                if (titleText.Contains(word))
                    titleText = titleText.Replace(word, "<b>" + word + "</b>");
            }
            return titleText;
        }

        public List<int> GetListOfEntries(String[] queryWords, String answerText)
        {
            List<int> entries = new List<int>();
            String[] separators = new String[] { " " };

            String[] answerWords = answerText.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (String word in queryWords)
            {
                for (int i = 0; i < answerWords.Length; i++)
                {
                    if (answerWords[i].Contains(word))
                        entries.Add(i);
                }
            }

            entries.Sort();

            return entries;
        }
    }
}
