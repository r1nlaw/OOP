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
                Assert.Pass("���� ������� �������."); // ���� ���������� ���������
            }
            else
            {
                Assert.Fail($"���� �� �������. ���������: {expected}, ��������: {actual}"); // ���� ���������� �� ���������
            }
        }

    }
}