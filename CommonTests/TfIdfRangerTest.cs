using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Index;
using SearchCore.Ranger;
using SearchCore.Ranger.RangerFilter;
using SearchCore.TextFilter;

namespace CommonTests
{
    [TestClass]
    public sealed class TfIdfRangerTest
    {
        [TestMethod]
        public void TwoFilesRangeTest()
        {
            var ranger = CreateTestedTfIdfRanger();

            var findResults = new[] { 0, 1 };
            var expectedRanked = new[] { 1, 0 };

            var userQuery = "word1 word2";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        [TestMethod]
        public void ThreeFileRageTest()
        {
            var ranger = CreateTestedTfIdfRanger();

            var findResults = new[] { 5, 4, 3 };
            var expectedRanked = new[] { 3, 4, 5 };

            var userQuery = "word1 word2 word3";

            Assert.IsTrue(
                ranger.Rank(TestUtils.EmptyUserGuid, userQuery, findResults).SequenceEqual(expectedRanked));
        }

        private IRanger CreateTestedTfIdfRanger()
        {
            return TestUtils.CreateSingleParameterRager(
                new TfIdfParameterCalculator(
                    CreateTestedIndex(),
                    new PunctuationTextFilter()));
        }

        private static IIndex CreateTestedIndex()
        {
            var index = new InMemoryIndex();
            index.AppendDocument();
            index.AppendWord("word1", 0);
            index.AppendWord("word2", 0);

            index.AppendDocument();

            index.AppendWord("word1", 1);
            index.AppendWord("word2", 1);
            index.AppendWord("word2", 1);

            index.AppendDocument();
            index.AppendWord("word1", 2);
            index.AppendWord("word3", 2);

            index.AppendDocument();
            index.AppendWord("word1", 3);
            index.AppendWord("word2", 3);
            index.AppendWord("word3", 3);
            index.AppendWord("word3", 3);

            index.AppendDocument();
            index.AppendWord("word1", 4);
            index.AppendWord("word2", 4);
            index.AppendWord("word2", 4);
            index.AppendWord("word3", 4);

            index.AppendDocument();
            index.AppendWord("word1", 5);
            index.AppendWord("word2", 5);
            index.AppendWord("word3", 5);
            return index;
        }
    }
}
