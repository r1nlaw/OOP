namespace GCDCalculator
{
    public static class PrimeUtils
    {
        public static int LargestPrimeLessThan(int n)
        {
            for (int i = n - 1; i >= 2; i--)
            {
                if (IsPrime(i))
                {
                    return i;
                }
            }
            return -1; 
        }

        private static bool IsPrime(int num)
        {
            if (num <= 1) return false;
            if (num <= 3) return true;

            if (num % 2 == 0 || num % 3 == 0) return false;

            for (int i = 5; i * i <= num; i += 6)
            {
                if (num % i == 0 || num % (i + 2) == 0) return false;
            }
            return true;
        }
    }
}
