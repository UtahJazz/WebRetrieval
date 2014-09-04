using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchCore.Utils;

namespace CommonTests
{
    [TestClass]
    public class VectorUtilsTest
    {
        [TestMethod]
        public void SingleVectorTest()
        {
            var vector = new[] { 0 };

            Assert.AreEqual(0, VectorUtils.MinimalDistance(new[]
            {
                vector
            }));
        }

        [TestMethod]
        public void TwoShortVectorTest()
        {
            var vector1 = new[] { 0 };
            var vector2 = new[] { 1 };

            Assert.AreEqual(1, VectorUtils.MinimalDistance(new[]
            {
                vector1, vector2
            })); 
        }

        [TestMethod]
        public void ThreeSortVectorTest()
        {
            var vector1 = new[] { 0 };
            var vector2 = new[] { 1 };
            var vector3 = new[] { 2 };

            Assert.AreEqual(2, VectorUtils.MinimalDistance(new[]
            {
                vector1, vector2, vector3
            })); 
        }

        [TestMethod]
        public void TwoLongVectorTest()
        {
            var vector1 = new[] { 0, 4, 10 };
            var vector2 = new[] { 5, 13 };

            Assert.AreEqual(1, VectorUtils.MinimalDistance(new[]
            {
                vector1, vector2
            })); 
        }

        [TestMethod]
        public void ThreeLongVectorTest()
        {
            var vector1 = new[] { 0, 4, 10 };
            var vector2 = new[] { 5, 13 };
            var vector3 = new[] { 1, 100 };

            Assert.AreEqual(4, VectorUtils.MinimalDistance(new[]
            {
                vector1, vector2, vector3
            })); 
        }
    }
}
