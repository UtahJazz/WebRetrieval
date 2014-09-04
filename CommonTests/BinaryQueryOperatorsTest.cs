using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.UserQueryProcesser;

namespace CommonTests
{
    [TestClass]
    public class BinaryQueryOperatorsTest
    {
        [TestMethod]
        public void AndOperatorEmptyTest()
        {
            var firstOperand = new int[0];
            var secondOperand = new int[0];

            Assert.AreEqual(0, BinaryQueryOperators.And(firstOperand, secondOperand).Length);
        }

        [TestMethod]
        public void SameSizeTest()
        {
            var firstOperand = new[]
                {
                    1, 2, 3
                };

            var secondOperand = new[]
                {
                    1, 2, 4
                };

            var result = new[]
                {
                    1, 2
                };

            Assert.IsTrue(BinaryQueryOperators.And(firstOperand, secondOperand).SequenceEqual(result));
        }

        [TestMethod]
        public void AndOperatorDiffrentSizeTest()
        {
            var firstOperand = new[]
                {
                    1, 2, 3, 4, 9
                };

            var secondOperand = new[]
                {
                    1, 2, 4, 7
                };

            var result = new[]
                {
                    1, 2, 4
                };

            Assert.IsTrue(BinaryQueryOperators.And(firstOperand, secondOperand).SequenceEqual(result));
        }

        [TestMethod]
        public void OrOperatorEmptyTest()
        {
            var firstOperand = new int[0];
            var secondOperand = new int[0];

            Assert.AreEqual(
                0, 
                BinaryQueryOperators.Or(
                    firstOperand, 
                    secondOperand).Length);
        }

        [TestMethod]
        public void OrOperator()
        {
            var firstOperand = new[]
                {
                    1, 2, 3
                };

            var secondOperand = new[]
                {
                    1, 2, 4
                };

            var result = new[]
                {
                    1, 2, 3, 4
                };

            Assert.IsTrue(BinaryQueryOperators.Or(firstOperand, secondOperand).SequenceEqual(result));
        }

        [TestMethod]
        public void OrOperatorDiffrentSizeTest()
        {
            var firstOperand = new[]
                {
                    1, 2, 3, 4, 9
                };

            var secondOperand = new[]
                {
                    1, 2, 4, 7
                };

            var result = new[]
                {
                    1, 2, 3, 4, 7, 9
                };

            Assert.IsTrue(
                BinaryQueryOperators.Or(
                    firstOperand, secondOperand).SequenceEqual(result));
        }
    }
}
