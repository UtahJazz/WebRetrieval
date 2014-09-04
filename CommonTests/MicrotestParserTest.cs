using System.IO;
using CommonTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Parsers;

namespace CommonTests
{
    [TestClass]
    public class MicrotestParserTest
    {
        [TestMethod]
        public void ParseTest()
        {
            var text = File.ReadAllText("TestData/TestPage.htm");
            var microtestData = MicrotestParser.Parse(text);
            Assert.AreEqual(microtestData.GetUrl(), TestPageOriginalData.Url);
        }
    }
}
