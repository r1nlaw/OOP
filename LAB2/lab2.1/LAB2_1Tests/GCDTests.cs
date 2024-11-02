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
    public class GCDTests
    {
        [TestMethod()]
        public void FindGCDEuclid_Test1()
        {
            int a = 2806;
            int b = 345;
            int expected = 23;

            int actual = GCD.FindGCDEuclid(a, b);

            if (actual != expected)
            {
                Assert.Fail($"Тест не пройден. Ожидалось: {expected}, получено: {actual}"); // Если результаты не совпадают
            }
            
        }
        
    }
}