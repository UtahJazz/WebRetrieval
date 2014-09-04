using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.TextFilter;

namespace CommonTests
{
    [TestClass]
    public class TextFiltersTest
    {
        [TestMethod]
        public void PunctuationFilterTest()
        {
            var filter = new PunctuationTextFilter();
            const string testText = "Some text[><%']other text,one";
            const string filteredText = "Some text      other text one";

            Assert.AreEqual(filter.Filter(testText), filteredText);
        }

        [TestMethod]
        public void LightPunctuationFilterTest()
        {
            var filter = new LightPunctuationFilter();
            const string testText = "C#,C++";
            const string filteredText = "C# C++";

            Assert.AreEqual(filter.Filter(testText), filteredText);
        }
    }
}
