using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using SearchCore.DirectoryProvider;
using SearchCore.FileIdMatcher;
using SearchCore.Metadata;
using SearchCore.Parsers;
using SearchCore.Ranger;
using SearchCore.SnippetBuilder;
using SearchCore.TextFilter;
using SearchCore.UserQueryProcesser;
using SearchCore.UserStatistics;

namespace SearchCore.Searcher
{
    public sealed class DocumentSearcher : ISearcher
    {
        public DocumentSearcher(
            IUserQueryProcesser userQueryProcesser,
            IRanger ranger,
            ISnippetBuilder snippetBuilder,
            IFileIdMatcher forwardIndexIdMatcher,
            IDirectoryProvider farwardIndexProvider,
            IMetadataPool metadataPool,
            IUserStatisticsLoader userStatisticsLoader)
        {
            _userQueryProcesser = userQueryProcesser;
            _ranger = ranger;
            _snippetBuilder = snippetBuilder;
            _forwardIndexIdMatcher = forwardIndexIdMatcher;
            _farwardIndexProvider = farwardIndexProvider;
            _metadataPool = metadataPool;
            _userStatisticsLoader = userStatisticsLoader;
        }

        public SearchResult Search(
            string userQuery,
            int page,
            Guid userId,
            bool isNeedCustomRange)
        {
            var matchedFiles = _userQueryProcesser.GetMatchedFiles(userQuery).ToArray();

            var rangedFiles = _ranger.Rank(
                    GetUserId(userId, isNeedCustomRange),
                    userQuery,
                    matchedFiles).ToArray();

            var resultFiles = GetPageFiles(rangedFiles, page);

            //var filteredUserQuery = _punctuationFilter.Filter(userQuery);
            var filteredUserQuery = userQuery;
            var snippets = resultFiles.Select(
                resultFile =>
                    CreateFileSnippet(resultFile, filteredUserQuery))
                .ToList();

            return new SearchResult(snippets, matchedFiles.Count());
        }

        public void MarkAsLiked(Guid userId, int fileId)
        {
            var userStatistics = _userStatisticsLoader.Load(userId);

            foreach (var fileTag in _metadataPool.GetMetadata(fileId).GetTags())
            {
                userStatistics.AppendTag(fileTag);
            }

            _userStatisticsLoader.Save(userId, userStatistics);
        }

        private Snippet CreateFileSnippet(int fileId, string filteredUserQuery)
        {
            return
                new Snippet(
                    snippetText:
                        _snippetBuilder.BuildSnippet(filteredUserQuery, fileId),
                    snippetUrl:
                        MicrotestParser.Parse(
                            _farwardIndexProvider.GetFileContent(
                                _forwardIndexIdMatcher.GetFileById(fileId))).GetUrl(),
                    tags:
                        _metadataPool.GetMetadata(fileId).GetTags().ToArray(),
                    fileId:
                        fileId);
        }

        private static Guid GetUserId(Guid userId, bool isNeedCustomRange)
        {
            return isNeedCustomRange ? userId : new Guid();
        }

        private static IEnumerable<int> GetPageFiles(int[] files, int page)
        {
            var countDelta = files.Count() - page * SearchConstants.ResultPerPage;
            if (countDelta <= 0)
            {
                return new int[0];
            }

            var resultFilesCount = countDelta > SearchConstants.ResultPerPage
                ? SearchConstants.ResultPerPage
                : countDelta;

            return files
                .Skip(page * SearchConstants.ResultPerPage)
                .Take(resultFilesCount);
        }

        private readonly ITextFilter _punctuationFilter = new PunctuationTextFilter();
        private readonly IUserQueryProcesser _userQueryProcesser;
        private readonly IRanger _ranger;
        private readonly ISnippetBuilder _snippetBuilder;
        private readonly IFileIdMatcher _forwardIndexIdMatcher;
        private readonly IDirectoryProvider _farwardIndexProvider;
        private readonly IMetadataPool _metadataPool;
        private readonly IUserStatisticsLoader _userStatisticsLoader;
    }
}
