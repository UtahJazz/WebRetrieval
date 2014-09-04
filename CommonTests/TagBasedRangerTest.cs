using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Metadata;
using SearchCore.Ranger;
using SearchCore.Ranger.RangerFilter;
using SearchCore.TextFilter;

namespace CommonTests
{
    [TestClass]
    public class TagBasedRangerTest
    {
        [TestMethod]
        public void TwoFileOneWithTagTest()
        {
            var ranger = CreateTestedTagBasedRanger();

            var findResults = new[] { 0, 1 };
            var expectedRanked = new[] { 1, 0 };

            var userQuery = "word1 word2 tag1";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void TreeFileWithTagTest()
        {
            var ranger = CreateTestedTagBasedRanger();

            var findResults = new[] { 0, 1, 2 };
            var expectedRanked = new[] { 2, 1, 0 };

            var userQuery = "tag3 word1 word2 tag1";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        private IRanger CreateTestedTagBasedRanger()
        {
            return TestUtils.CreateSingleParameterRager(
                new TagBasedRankCalculator(
                    CreateTestedMetadataPool(),
                    new LightPunctuationFilter()));
        }

        private IMetadataPool CreateTestedMetadataPool()
        {
            var metadataPool = new InMemoryMetadataPool();

            var file0Metadata = new InMemoryMetadata();

            file0Metadata.AppendWord("tag1", new WordPosition(0, 0));
            file0Metadata.AppendTag("tag2");

            metadataPool.AppendMetadata(0, file0Metadata);

            var file1Metadata = new InMemoryMetadata();

            file1Metadata.AppendWord("tag1", new WordPosition(0, 0));
            file1Metadata.AppendWord("tag2", new WordPosition(0, 1));
            file1Metadata.AppendTag("tag1");

            metadataPool.AppendMetadata(1, file1Metadata);

            var file2Metadata = new InMemoryMetadata();

            file2Metadata.AppendWord("tag1", new WordPosition(0, 0));
            file2Metadata.AppendWord("tag2", new WordPosition(0, 1));
            file2Metadata.AppendTag("tag1"); 
            file2Metadata.AppendTag("tag3");

            metadataPool.AppendMetadata(2, file2Metadata);

            return metadataPool;
        }
    }
}
