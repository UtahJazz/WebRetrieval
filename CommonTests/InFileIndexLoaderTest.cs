using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Index;

namespace CommonTests
{
    [TestClass]
    public class InFileIndexLoaderTest
    {
        [TestMethod]
        public void SaveLoadRoundTest()
        {
            var fileIndexLoader = new InFileIndexLoader("");

            var index = new InMemoryIndex();

            index.AppendWord("word 1", 0);
            index.AppendWord("word 1", 1);
            index.AppendWord("word 2", 0);
            index.AppendWord("word 3", 0);

            fileIndexLoader.Save(index);

            var loadedIndex = fileIndexLoader.Load();

            Assert.AreEqual(index.DocumentCount, loadedIndex.DocumentCount);

            Assert.AreEqual(loadedIndex.GetFilesWithWord("word 1").First(), 0);
            Assert.AreEqual(loadedIndex.GetFilesWithWord("word 1").Last(), 1);
            Assert.AreEqual(loadedIndex.GetFilesWithWord("word 2").First(), 0);
            Assert.AreEqual(loadedIndex.GetFilesWithWord("word 3").First(), 0);
        }

        [TestMethod]
        public void SaveLoadTagsTest()
        {
            var fileIndexLoader = new InFileIndexLoader("");

            var index = new InMemoryIndex();

            index.AppendTag("tag 1", 0);
            index.AppendTag("tag 1", 1);
            index.AppendTag("tag 2", 0);
            index.AppendTag("tag 3", 0);

            fileIndexLoader.Save(index);

            var loadedIndex = fileIndexLoader.Load();

            Assert.AreEqual(loadedIndex.GetFilesWithTag("tag 1").First(), 0);
            Assert.AreEqual(loadedIndex.GetFilesWithTag("tag 1").Last(), 1);
            Assert.AreEqual(loadedIndex.GetFilesWithTag("tag 2").First(), 0);
            Assert.AreEqual(loadedIndex.GetFilesWithTag("tag 3").First(), 0);
        }
    }
}
