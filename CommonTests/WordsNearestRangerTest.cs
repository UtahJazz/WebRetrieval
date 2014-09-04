using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Metadata;
using SearchCore.Ranger;
using SearchCore.Ranger.RangerFilter;
using SearchCore.TextFilter;

namespace CommonTests
{
    [TestClass]
    public class WordsNearestRangerTest
    {
        [TestMethod]
        public void RangeTwoFilesInSameParagraph()
        {
            var ranger = CreateTestedWordNearestRanger();

            var findResults = new[] {0, 1};
            var expectedRanked = new[] {1, 0};

            var userQuery = "word1 word2";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void RangeTwoFilesInDiffrentParagraph()
        {
            var ranger = CreateTestedWordNearestRanger();

            var findResults = new[] { 2, 1 };
            var expectedRanked = new[] { 1, 2 };

            var userQuery = "word1 word2";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void RangeThreeFirstFiles()
        {
            var ranger = CreateTestedWordNearestRanger();

            var findResults = new[] { 0, 1, 2 };
            var expectedRanked = new[] { 1, 0, 2 };

            var userQuery = "word1 word2";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void RageFileWithMultiOccurrence()
        {
            var ranger = CreateTestedWordNearestRanger();

            var findResults = new[] { 4, 3 };
            var expectedRanked = new[] { 3, 4 };

            var userQuery = "word1 word2";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void AllFilesRageTest()
        {
            var ranger = CreateTestedWordNearestRanger();

            var findResults = new[] { 0, 1, 2, 3, 4 };
            var expectedRanked = new[] { 1, 3, 0, 4, 2 };

            var userQuery = "word1 word2";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void TreeWordQuerySameParagraph()
        {
            var ranger = CreateTestedWordNearestRanger();

            var findResults = new[] { 0, 1 };
            var expectedRanked = new[] { 1, 0 };

            var userQuery = "word1 word2 word3";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void TreeWordQueryDiffrentParagraphs()
        {
            var ranger = CreateTestedWordNearestRanger();

            var findResults = new[] { 0, 2 };
            var expectedRanked = new[] { 0, 2 };

            var userQuery = "word1 word2 word3";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        private IRanger CreateTestedWordNearestRanger()
        {
            return TestUtils.CreateSingleParameterRager(
                new WordNearestParameterCalculator(
                    CreateTestMetadataPool(),
                    new PunctuationTextFilter()));
        }

        private IMetadataPool CreateTestMetadataPool()
        {
            var metadataPool = new InMemoryMetadataPool();

            var file1Metadata = new InMemoryMetadata();
            file1Metadata.AppendWord("word1", new WordPosition(0, 0));
            file1Metadata.AppendWord("word3", new WordPosition(0, 1));
            file1Metadata.AppendWord("word3", new WordPosition(0, 2));
            file1Metadata.AppendWord("word3", new WordPosition(0, 3));
            file1Metadata.AppendWord("word2", new WordPosition(0, 10));

            file1Metadata.AppendTag("word1");

            metadataPool.AppendMetadata(0, file1Metadata);

            var file2Metadata = new InMemoryMetadata();
            file2Metadata.AppendWord("word1", new WordPosition(0, 0));
            file2Metadata.AppendWord("word2", new WordPosition(0, 1));
            file2Metadata.AppendWord("word3", new WordPosition(0, 3));

            file2Metadata.AppendTag("word1");

            metadataPool.AppendMetadata(1, file2Metadata);

            var file3Metadata = new InMemoryMetadata();
            file3Metadata.AppendWord("word1", new WordPosition(0, 0));
            file3Metadata.AppendWord("word2", new WordPosition(1, 0));
            file3Metadata.AppendWord("word3", new WordPosition(1, 3));

            file3Metadata.AppendTag("word1");

            metadataPool.AppendMetadata(2, file3Metadata);

            var file4Metadata = new InMemoryMetadata();
            file4Metadata.AppendWord("word1", new WordPosition(0, 0));
            file4Metadata.AppendWord("word2", new WordPosition(0, 10));
            file4Metadata.AppendWord("word1", new WordPosition(0, 12));
            file4Metadata.AppendWord("word3", new WordPosition(0, 8));
            file4Metadata.AppendWord("word3", new WordPosition(0, 9));

            file4Metadata.AppendTag("word1");

            metadataPool.AppendMetadata(3, file4Metadata);


            var file5Metadata = new InMemoryMetadata();
            file5Metadata.AppendWord("word1", new WordPosition(0, 0));
            file5Metadata.AppendWord("word2", new WordPosition(0, 10));
            file5Metadata.AppendWord("word1", new WordPosition(1, 12));
            file5Metadata.AppendWord("word3", new WordPosition(0, 9));

            file5Metadata.AppendTag("word1");

            metadataPool.AppendMetadata(4, file5Metadata);
            return metadataPool;
        }
    }
}
