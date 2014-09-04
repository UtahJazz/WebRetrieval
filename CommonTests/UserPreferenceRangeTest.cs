using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Metadata;
using SearchCore.Ranger;
using SearchCore.Ranger.RangerFilter;
using SearchCore.UserStatistics;

namespace CommonTests
{
    [TestClass]
    public class UserPreferenceRangeTest
    {
        [TestMethod]
        public void TwoFileOneWithPreferencesTag()
        {
            var userGuid = Guid.NewGuid();

            var userStatistics = new InMemoryUserStatistics();
            userStatistics.AppendTag("tag2");

            var statisticsLoader = new InMemoryUserStatisticsLoader();
            statisticsLoader.Save(userGuid, userStatistics);

            var ranger = CreateTestedTitleBasedRanger(statisticsLoader);

            var findResults = new[] { 0, 1 };
            var expectedRanked = new[] { 1, 0 };
            Assert.IsTrue(
                ranger.Rank(userGuid, UserQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void PreferenceTagCountDoesNotMatter()
        {
            var userGuid = Guid.NewGuid();

            var userStatistics = new InMemoryUserStatistics();
            userStatistics.AppendTag("tag1");
            userStatistics.AppendTag("tag2");

            var statisticsLoader = new InMemoryUserStatisticsLoader();
            statisticsLoader.Save(userGuid, userStatistics);

            var ranger = CreateTestedTitleBasedRanger(statisticsLoader);

            var findResults = new[] { 1, 2 };
            var expectedRanked = new[] { 1, 2 };
            Assert.IsTrue(
                ranger.Rank(userGuid, UserQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void OneTagMorePreferenceTest()
        {
            var userGuid = Guid.NewGuid();

            var userStatistics = new InMemoryUserStatistics();
            userStatistics.AppendTag("tag1");
            userStatistics.AppendTag("tag2");
            userStatistics.AppendTag("tag3");
            userStatistics.AppendTag("tag3");
            userStatistics.AppendTag("tag3");

            var statisticsLoader = new InMemoryUserStatisticsLoader();
            statisticsLoader.Save(userGuid, userStatistics);

            var ranger = CreateTestedTitleBasedRanger(statisticsLoader);

            var findResults = new[] { 2, 3 };
            var expectedRanked = new[] { 3, 2 };
            Assert.IsTrue(
                ranger.Rank(userGuid, UserQuery, findResults).SequenceEqual(expectedRanked));
        }

        private static IRanger CreateTestedTitleBasedRanger(IUserStatisticsLoader userStatisticsLoader)
        {
            return TestUtils.CreateSingleParameterRager(
                new UserPreferenceCalculator(
                    userStatisticsLoader,
                    CreateTestedMetadataPool()));
        }

        private static IMetadataPool CreateTestedMetadataPool()
        {
            var metadataPool = new InMemoryMetadataPool();

            var file0Metadata = new InMemoryMetadata();

            file0Metadata.AppendTag("tag1");

            metadataPool.AppendMetadata(0, file0Metadata);

            var file1Metadata = new InMemoryMetadata();

            file1Metadata.AppendTag("tag1");
            file1Metadata.AppendTag("tag2");

            metadataPool.AppendMetadata(1, file1Metadata);

            var file2Metadata = new InMemoryMetadata();

            file2Metadata.AppendTag("tag1");
            file2Metadata.AppendTag("tag1");
            file2Metadata.AppendTag("tag1");

            metadataPool.AppendMetadata(2, file2Metadata);

            var file3Metadata = new InMemoryMetadata();

            file3Metadata.AppendTag("tag3");

            metadataPool.AppendMetadata(3, file3Metadata);

            return metadataPool;
        }

        const string UserQuery = "word1";
    }
}
