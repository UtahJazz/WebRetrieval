using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Parsers.BlockTextBuilder;

namespace CommonTests
{
    [TestClass]
    public class HtmlTextBlockBuilderTest
    {
        [TestMethod]
        public void BuildBlocksTest()
        {
            var htmlTextBlockBuilder = new HtmlTextBlockBuilder();
            const string firstBlock = "SomeText";
            const string secondBlock = "SomeOtherText";

            htmlTextBlockBuilder.AppendTextBlock(firstBlock);
            htmlTextBlockBuilder.AppendTextBlock(secondBlock);

            var resultString = htmlTextBlockBuilder.Build();

            Assert.IsTrue(resultString.Contains(firstBlock));
            Assert.IsTrue(resultString.Contains(secondBlock));
        }

        [TestMethod]
        public void RoundTest()
        {
            var htmlTextBlockBuilder = new HtmlTextBlockBuilder();
            const string firstBlock = "SomeText";
            const string secondBlock = "SomeOtherText";

            htmlTextBlockBuilder.AppendTextBlock(firstBlock);
            htmlTextBlockBuilder.AppendTextBlock(secondBlock);

            var resultString = htmlTextBlockBuilder.Build();

            var roundStrings = htmlTextBlockBuilder.SplitByBlocks(resultString);
            Assert.AreEqual(roundStrings[0], firstBlock);
            Assert.AreEqual(roundStrings[1], secondBlock);
        }
    }
}
