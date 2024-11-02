using GCDCalculator;
using System.Numerics;

namespace TestProject1
{
    public class Tests
    {
      

        [Test]
        public void FindGCDEuclid_Test1()
        {
            int a = 2806;
            int b = 345;
            int expected = 23;

            int actual = GCD.FindGCDEuclid(a, b);

            if (actual == expected)
            {
                Assert.Pass("Тест пройден успешно."); // Если результаты совпадают
            }
            else
            {
                Assert.Fail($"Тест не пройден. Ожидалось: {expected}, получено: {actual}"); // Если результаты не совпадают
            }
        }

    }
}