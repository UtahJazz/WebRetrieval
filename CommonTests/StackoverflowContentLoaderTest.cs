using System.IO;
using CommonTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Parsers;

namespace CommonTests
{
    [TestClass]
    public class StackoverflowContentLoaderTest
    {
        [TestMethod]
        public void LoadTitleTest()
        {
            var loader = new StackoverflowContentLoader();
            var testText = File.ReadAllText("TestData/TestPage.htm");

            var pageContent = loader.LoadData(testText);

            Assert.AreEqual(TestPageOriginalData.TextTitle, pageContent.Title);
        }

        [TestMethod]
        public void LoadContentTest()
        {
            var loader = new StackoverflowContentLoader();
            var testText = File.ReadAllText("TestData/TestPage.htm");

            var pageContent = loader.LoadData(testText);

            Assert.AreEqual(TestPageOriginalData.TextBlocks.Length, pageContent.Paragraphs.Length);

            for (var i = 0; i < pageContent.Paragraphs.Length; i++)
            {
                Assert.AreEqual(TestPageOriginalData.TextBlocks[i], pageContent.Paragraphs[i]);
            }
        }

        [TestMethod]
        public void LoadTagsTest()
        {
            var loader = new StackoverflowContentLoader();
            var testText = File.ReadAllText("TestData/TestPage.htm");

            var pageContent = loader.LoadData(testText);

            Assert.AreEqual(TestPageOriginalData.TextTags.Length, pageContent.Tags.Length);

            for (var i = 0; i < pageContent.Tags.Length; i++)
            {
                Assert.AreEqual(TestPageOriginalData.TextTags[i], pageContent.Tags[i]);
            }
        }

        [TestMethod]
        public void LoadVotesTest()
        {
            var loader = new StackoverflowContentLoader();
            var testText = File.ReadAllText("TestData/TestPage.htm");

            var pageContent = loader.LoadData(testText);

            Assert.AreEqual(TestPageOriginalData.TextVotes.Length, pageContent.Votes.Length);

            for (var i = 0; i < pageContent.Votes.Length; i++)
            {
                Assert.AreEqual(TestPageOriginalData.TextVotes[i], pageContent.Votes[i]);
            }
        }
    }
}
