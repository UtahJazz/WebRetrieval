using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Index;
using SearchCore.IndexBuilder;
using SearchCore.Parsers;
using SearchCore.TextFilter;

namespace CommonTests
{
    [TestClass]
    public class IndexBuilderTest
    {
        [TestMethod]
        public void SmallFileIndexTest()
        {
            var testedPage = new PageContent(
                string.Empty,
                new []{ "like One,Three,Seven,One Month etc.These" },
                new string[0],
                new string[0]);

            const int testedFileId = 0;

            _reverseIndexBuilder.AppendFile(testedPage, testedFileId);
            var index = _reverseIndexBuilder.GetIndex();

            Assert.AreEqual(index.GetFilesWithWord("like").Count(), 1);
            Assert.AreEqual(index.GetFilesWithWord("one").Count(), 1);
            Assert.AreEqual(index.GetFilesWithWord("three").Count(), 1);
            Assert.AreEqual(index.GetFilesWithWord("seven").Count(), 1);
            Assert.AreEqual(index.GetFilesWithWord("month").Count(), 1);
            Assert.AreEqual(index.GetFilesWithWord("etc").Count(), 1);
            Assert.AreEqual(index.GetFilesWithWord("these").Count(), 1);

            Assert.AreEqual(index.GetFilesWithWord("like").First(), testedFileId);
            Assert.AreEqual(index.GetFilesWithWord("one").First(), testedFileId);
            Assert.AreEqual(index.GetFilesWithWord("three").First(), testedFileId);
            Assert.AreEqual(index.GetFilesWithWord("seven").First(), testedFileId);
            Assert.AreEqual(index.GetFilesWithWord("month").First(), testedFileId);
            Assert.AreEqual(index.GetFilesWithWord("etc").First(), testedFileId);
            Assert.AreEqual(index.GetFilesWithWord("these").First(), testedFileId);

            Assert.AreEqual(index.GetWordFrequency("like", testedFileId), 1);
            Assert.AreEqual(index.GetWordFrequency("one", testedFileId), 2);
            Assert.AreEqual(index.GetWordFrequency("three", testedFileId), 1);
            Assert.AreEqual(index.GetWordFrequency("seven", testedFileId), 1);
            Assert.AreEqual(index.GetWordFrequency("month", testedFileId), 1);
            Assert.AreEqual(index.GetWordFrequency("etc", testedFileId), 1);
            Assert.AreEqual(index.GetWordFrequency("these", testedFileId), 1);
        }

        [TestMethod]
        public void FewSeveralFileTest()
        {
            var testedPage1 = new PageContent(
                string.Empty,
                new[] { "I,One. And" },
                new string[0],
                new string[0]);

            var testedPage2 = new PageContent(
                string.Empty,
                new[] { "No;One die" },
                new string[0],
                new string[0]);

            const int testedFile1Id = 0;
            const int testedFile2Id = 1;

            _reverseIndexBuilder.AppendFile(testedPage1, testedFile1Id);
            _reverseIndexBuilder.AppendFile(testedPage2, testedFile2Id);
            var index = _reverseIndexBuilder.GetIndex();

            Assert.AreEqual(1, index.GetFilesWithWord("i").Count());
            Assert.AreEqual(2, index.GetFilesWithWord("one").Count());
            Assert.AreEqual(1, index.GetFilesWithWord("and").Count());
            Assert.AreEqual(1, index.GetFilesWithWord("die").Count());

            Assert.AreEqual(1, index.GetWordFrequency("i", testedFile1Id));
            Assert.AreEqual(0, index.GetWordFrequency("i", testedFile2Id));
            Assert.AreEqual(1, index.GetWordFrequency("one", testedFile1Id));
            Assert.AreEqual(1, index.GetWordFrequency("one", testedFile2Id));

            Assert.AreEqual(1, index.GetWordFrequency("and", testedFile1Id));
            Assert.AreEqual(1, index.GetWordFrequency("die", testedFile2Id));
        }

        private readonly IReverseIndexBuilder _reverseIndexBuilder = 
            new ReverseIndexBuilder(
                new InMemoryIndex(),
                new PunctuationTextFilter(),
                new LightPunctuationFilter());

    }
}
