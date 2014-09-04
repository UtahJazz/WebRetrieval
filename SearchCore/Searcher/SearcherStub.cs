using System;
using System.Collections.Generic;
using Common;

namespace SearchCore.Searcher
{
    public sealed class SearcherStub : ISearcher
    {
        public SearchResult Search(
            string userQuery, 
            int page, 
            Guid userId, 
            bool isNeedCustomRange)
        {
            return new SearchResult(new List<Snippet>
            {
                new Snippet("Yandex", "http://ya.ru", new []{ "Yandex" }, 0),
                new Snippet("Google", "http://google.com", new []{ "Google" }, 1),
                new Snippet("Greate server for Ultima online!", "http://uoo.ru", new []{ "MMO", "Games", "Server" }, 2),
                new Snippet("Some social web =/", "http://vk.com", new []{ "VK", "Server", "Web" }, 3),
                new Snippet("Another social web =/", "http://facebook.com", new []{ "Facebook", "Web" }, 4),
                new Snippet("Yes i can speak a lot about this site, but there is much more information you can get out of there.", "http://stackoverflow.com", new []{ "Tag", "IT" }, 5),
                new Snippet("One more usefull site!", "http://lenta.ru", new []{ "Big Brother", "News" }, 6),
                new Snippet("Best browser ever ^_^", "http://www.mozilla.org/ru/", new []{ "Mozilla", "Best Browser" }, 7),
                new Snippet("Best site about it industri!", "http://habrahabr.ru", new []{ "Web", "IT" }, 8),
                new Snippet("Great Russian compani kokokokokokokokokokokkokokokokokokokokokokkokokokokokokokokokokkokokokokokokokokokokkokokokokokokokokokokkokokokokokokokokokokkokokokokokokokokokokkokokokokokokokokokokkokokokokokokokokokok", "http://mail.ru", new []{ "Mail", "Games" }, 9),
            },
            resultsCount: 120);
        }

        public void MarkAsLiked(Guid userId, int fileId)
        {

        }
    }
}
