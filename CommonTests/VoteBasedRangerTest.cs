using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Metadata;
using SearchCore.Ranger;
using SearchCore.Ranger.RangerFilter;

namespace CommonTests
{
    [TestClass]
    public class VoteBasedRangerTest
    {
        [TestMethod]
        public void TwoFileOneWithVoteTest()
        {
            var ranger = CreateTestedVoteBasedRanger();

            var findResults = new[] { 0, 1 };
            var expectedRanked = new[] { 1, 0 };

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, UserQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void NegativeVoteTest()
        {
            var ranger = CreateTestedVoteBasedRanger();

            var findResults = new[] { 1, 2 };
            var expectedRanked = new[] { 1, 2 };

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, UserQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void SeveralVotesInOneFileTest()
        {
            var ranger = CreateTestedVoteBasedRanger();

            var findResults = new[] { 0, 1, 2, 3 };
            var expectedRanked = new[] { 3, 1, 0, 2 };

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, UserQuery, findResults).SequenceEqual(expectedRanked));
        }

        private IRanger CreateTestedVoteBasedRanger()
        {
            return TestUtils.CreateSingleParameterRager(
                new VoteBasedRankCalculator(
                    CreateTestedMetadataPool()));
        }

        private IMetadataPool CreateTestedMetadataPool()
        {
            var metadataPool = new InMemoryMetadataPool();

            var file0Metadata = new InMemoryMetadata();

            file0Metadata.AppendVote(0);

            metadataPool.AppendMetadata(0, file0Metadata);

            var file1Metadata = new InMemoryMetadata();

            file1Metadata.AppendVote(1);

            metadataPool.AppendMetadata(1, file1Metadata);

            var file2Metadata = new InMemoryMetadata();

            file2Metadata.AppendVote(-1);

            metadataPool.AppendMetadata(2, file2Metadata);

            var file3Metadata = new InMemoryMetadata();

            file3Metadata.AppendVote(-2);
            file3Metadata.AppendVote(4);

            metadataPool.AppendMetadata(3, file3Metadata);

            return metadataPool;
        }

        const string UserQuery = "somequery";
    }
}
