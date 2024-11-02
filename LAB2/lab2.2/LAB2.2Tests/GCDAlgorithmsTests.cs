using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCDCalculator;

namespace GCDCalculator.Tests
{
    [TestClass]
    public class GCDAlgorithmsTests
    {
        [TestMethod]
        public void FindGCDEuclid_TwoNumbers_ReturnsCorrectGCD()
        {
            // Arrange
            int a = 13;
            int b = 17;
            int expected = 1;


            int actual = GCDAlgorithms.FindGCDEuclid(a, b);

            // Assert
            if (actual != expected)
            {
                Assert.Fail($"Expected {expected}, but got {actual} for inputs {a}, {b}.");
            }
        }
        [TestMethod]
        public void FindGCDEuclid_ThreeNumbers_ReturnsCorrectGCD()
        {
            // Arrange
            int a = 7396;
            int b = 1978;
            int c = 1204;
            int expected = 86;

            
            int actual = GCDAlgorithms.FindGCDEuclid(a, b, c);

            // Assert
            if (actual != expected)
            {
                Assert.Fail($"Expected {expected}, but got {actual} for inputs {a}, {b}, {c}.");
            }
        }

        [TestMethod]
        public void FindGCDEuclid_FourNumbers_ReturnsCorrectGCD()
        {
            // Arrange
            int a = 0;
            int b = 14;
            int c = 28;
            int d = 42;
            int expected = 14;

           
            int actual = GCDAlgorithms.FindGCDEuclid(a, b, c, d);

            // Assert
            if (actual != expected)
            {
                Assert.Fail($"Expected {expected}, but got {actual} for inputs {a}, {b}, {c}, {d}.");
            }
        }

        [TestMethod]
        public void FindGCDEuclid_FiveNumbers_ReturnsCorrectGCD()
        {
            // Arrange
            int a = 12;
            int b = 18;
            int c = 30;
            int d = 42;
            int e = 60;
            int expected = 6;

            
            int actual = GCDAlgorithms.FindGCDEuclid(a, b, c, d, e);

            // Assert
            if (actual != expected)
            {
                Assert.Fail($"Expected {expected}, but got {actual} for inputs {a}, {b}, {c}, {d}, {e}.");
            }
        }

    }
}
