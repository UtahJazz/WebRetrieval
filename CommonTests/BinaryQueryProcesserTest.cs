using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Index;
using SearchCore.TextFilter;
using SearchCore.UserQueryProcesser;

namespace CommonTests
{
    [TestClass]
    public class BinaryQueryProcesserTest
    {
        [TestMethod]
        public void EmptyQueryTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("");
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CommonWordQueryTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("word1").ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[1]);
        }

        [TestMethod]
        public void UniqueWordTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("word0").ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0]);
        }

        [TestMethod]
        public void CommonTagQueryTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("tag1").ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[1]);
        }

        [TestMethod]
        public void UniqueTagQueryTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("tag2").ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0]);
        }

        [TestMethod]
        public void MixedCommonTagAndCommonWordQueryShouldFind()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("word1 tag1").ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[1]);
        }

        [TestMethod]
        public void MixedCommonTagAndUniqueWordShouldFinsOnlyUniqueTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("word2 tag1").ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(0, result[0]);
        }
        
        [TestMethod]
        public void MixedUniqueTagAndUniqueWordShouldFinsOnlyUniqueTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("tag2 word0").ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0]);
        }

        [TestMethod]
        public void PunctuationQueryTest()
        {
            var processer = CreateTestedProcesser();
            var result = processer.GetMatchedFiles("C#.C++,word1").ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(0, result[0]);
        }

        private static IUserQueryProcesser CreateTestedProcesser()
        {
            return new BinaryQueryProcesser(
                CreateTestIndex(),
                new PunctuationTextFilter(),
                new LightPunctuationFilter());
        }

        private static IIndex CreateTestIndex()
        {
            var index = new InMemoryIndex();

            index.AppendWord("word1", 0);
            index.AppendWord("word1", 0);
            index.AppendWord("word2", 0);
            index.AppendWord("word3", 0);
            index.AppendWord("word4", 0);

            index.AppendTag("tag1", 0);
            index.AppendTag("C#", 0);
            index.AppendTag("C++", 0);

            index.AppendWord("word1", 1);
            index.AppendWord("word0", 1);

            index.AppendTag("tag1", 1);
            index.AppendTag("tag2", 1);

            return index;
        }
    }
}
