using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Index;

namespace CommonTests
{
    [TestClass]
    public class IndexTest
    {
        [TestMethod]
        public void InMemoryIndexAppendFewWordTest()
        {
            var index = new InMemoryIndex();
            const int testedFileId = 0;

            index.AppendWord("word1", testedFileId);
            index.AppendWord("word2", testedFileId);
            index.AppendWord("word1", testedFileId);

            Assert.AreEqual(1, index.GetFilesWithWord("word1").Count());
            Assert.AreEqual(1, index.GetFilesWithWord("word2").Count());

            Assert.AreEqual(2, index.GetWordFrequency("word1", testedFileId));
            Assert.AreEqual(1, index.GetWordFrequency("word2", testedFileId));
        }

        public void InMemoryFileSeveralFilesTest()
        {
            var index = new InMemoryIndex();

            const int testFile1 = 0;
            const int testFile2 = 1;

            index.AppendWord("word1", testFile1);
            index.AppendWord("word2", testFile1);
            index.AppendWord("word3", testFile1);

            index.AppendWord("word1", testFile2);
            index.AppendWord("word2", testFile2);
            index.AppendWord("word2", testFile2);

            Assert.AreEqual(2, index.GetFilesWithWord("word1").Count());
            Assert.AreEqual(2, index.GetFilesWithWord("word2").Count());
            Assert.AreEqual(1, index.GetFilesWithWord("word3").Count());


            Assert.AreEqual(1, index.GetWordFrequency("word1", testFile1));
            Assert.AreEqual(1, index.GetWordFrequency("word2", testFile1));
            Assert.AreEqual(1, index.GetWordFrequency("word3", testFile1));

            Assert.AreEqual(1, index.GetWordFrequency("word1", testFile1));
            Assert.AreEqual(2, index.GetWordFrequency("word2", testFile1));
        }
    }
}
