using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCDCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDCalculator.Tests
{
    [TestClass()]
    public class GCDAlgorithmsTests
    {
        [TestMethod]
        public void FindGCDEuclidTest()
        {
            int u = 298467352;
            int v = 569484;
            int expected = 4;

            int actual = GCDAlgorithms.FindGCDEuclid(u, v, out long time);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FindGCDSteinTest()
        {
            int u = 232533435;
            int v = 234124;
            int expected = 4;

            int actual = GCDAlgorithms.FindGCDStein(u, v, out long time);
            Assert.AreEqual(expected, actual);
        }
    }
}