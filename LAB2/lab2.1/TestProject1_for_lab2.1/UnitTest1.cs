using GCDCalculator;
using NUnit.Framework; 

namespace TestProject1_for_lab2._1
{
    public class GCDTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void FindGCDEuclid_Test1()
        {
            int a = 2806;
            int b = 345;
            int expected = 23;

            int actual = GCD.FindGCDEuclid(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindGCDEuclid_Test2()
        {
            int a = 0;
            int b = 10;
            int expected = 10;

            int actual = GCD.FindGCDEuclid(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindGCDEuclid_Test3()
        {
            int a = 25;
            int b = 100;
            int expected = 25;

            int actual = GCD.FindGCDEuclid(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindGCDEuclid_Test4()
        {
            int a = 27;
            int b = 100;
            int expected = 1;

            int actual = GCD.FindGCDEuclid(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindGCDEuclid_Test5()
        {
            int a = 26;
            int b = 100;
            int expected = 2;

            int actual = GCD.FindGCDEuclid(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
