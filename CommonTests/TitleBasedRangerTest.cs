using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Metadata;
using SearchCore.Ranger;
using SearchCore.Ranger.RangerFilter;
using SearchCore.TextFilter;

namespace CommonTests
{
    [TestClass]
    public class TitleBasedRangerTest
    {
        [TestMethod]
        public void TwoFileWithOneInTitleTest()
        {
            var ranger = CreateTestedTitleBasedRanger();

            var findResults = new[] { 0, 1 };
            var expectedRanked = new[] { 1, 0 };

            var userQuery = "word1";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void OneWordCountDoesNotMatterTest()
        {
            var ranger = CreateTestedTitleBasedRanger();

            var findResults = new[] { 0, 1, 2 };
            var expectedRanked = new[] { 1, 2, 0 };

            var userQuery = "word1 word2";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        private static IRanger CreateTestedTitleBasedRanger()
        {
            return TestUtils.CreateSingleParameterRager(
                new TitleBasedRankCalculator(
                    CreateTestedMetadataPool(),
                    new PunctuationTextFilter()));
        }

        private static IMetadataPool CreateTestedMetadataPool()
        {
            var metadataPool = new InMemoryMetadataPool();

            var file0Metadata = new InMemoryMetadata();

            file0Metadata.AppendWord("word1", new WordPosition(1, 0));

            metadataPool.AppendMetadata(0, file0Metadata);

            var file1Metadata = new InMemoryMetadata();

            file0Metadata.AppendWord("word1", new WordPosition(0, 0));
            file0Metadata.AppendWord("word2", new WordPosition(0, 0));

            metadataPool.AppendMetadata(1, file1Metadata);

            var file2Metadata = new InMemoryMetadata();

            file2Metadata.AppendWord("word1", new WordPosition(0, 0));
            file2Metadata.AppendWord("word1", new WordPosition(0, 0));
            file2Metadata.AppendWord("word1", new WordPosition(0, 0));

            metadataPool.AppendMetadata(2, file2Metadata);

            return metadataPool;
        }
    }
}
