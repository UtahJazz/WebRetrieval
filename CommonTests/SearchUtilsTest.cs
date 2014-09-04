using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Utils;

namespace CommonTests
{
    [TestClass]
    public class SearchUtilsTest
    {
        [TestMethod]
        public void SplitByLinesTest()
        {
            const string text = "First line.\r\n  Second line;\r\n\tThird line.";

            var lines = StringUtils.SplitByLines(text).ToArray();
            Assert.AreEqual(lines.Length, 3);
            Assert.AreEqual(lines[0], "First line.");
            Assert.AreEqual(lines[1], "  Second line;");
            Assert.AreEqual(lines[2], "\tThird line.");
        }

        [TestMethod]
        public void SplitByWordsTest()
        {
            const string text = "Tom and I or Tom and me?";

            var words = StringUtils.SplitByWord(text).ToArray();
            Assert.AreEqual(words.Length, 7);
            Assert.AreEqual(words[0], "Tom");
            Assert.AreEqual(words[1], "and");
            Assert.AreEqual(words[2], "I");
            Assert.AreEqual(words[3], "or");
            Assert.AreEqual(words[4], "Tom");
            Assert.AreEqual(words[5], "and");
            Assert.AreEqual(words[6], "me?");
        }
    }
}
