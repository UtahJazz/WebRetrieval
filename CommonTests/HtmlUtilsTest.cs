using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Utils;

namespace CommonTests
{
    [TestClass]
    public class HtmlUtilsTest
    {
        [TestMethod]
        public void ParseAttributeFindAfterEqualAttributeTest()
        {
            const string searchString = @"    <meta name='twitter:card' content='summary'>";

            var nameValue = HtmlUtils.GetAttributeValue(searchString, "name");

            Assert.AreEqual(nameValue, "twitter:card");
        }

        [TestMethod]
        public void ParseAttributeFindAfterEqualDoubleQuotsAttributeTest()
        {
            const string searchString = "    <meta name=\"twitter:card\" content=\"summary\">";

            var nameValue = HtmlUtils.GetAttributeValue(searchString, "name");

            Assert.AreEqual(nameValue, "twitter:card");
        }

        [TestMethod]
        public void CleanTagsTest()
        {
            const string tagText = "<pre><code>Some text</code></pre><a>Some other text</a>";
            const string textWithoutTag = "  Some text   Some other text ";

            Assert.AreEqual(HtmlUtils.CleanTags(tagText), textWithoutTag);
        }
    }
}
