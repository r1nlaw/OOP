using System.Diagnostics;

namespace GCDCalculator
{
    public static class GCDAlgorithms
    {
        public static int FindGCDEuclid(int u, int v, out long time)
        {
            time = 0;
            var sw = Stopwatch.StartNew();
            while (v != 0)
            {
                int temp = v;
                v = u % v;
                u = temp;
            }
            sw.Stop();
            time = sw.ElapsedTicks;
            return u;
        }

        public static int FindGCDStein(int u, int v, out long time)
        {
            time = 0;
            var sw = Stopwatch.StartNew();

            int k = 0;
            if (u == 0 || v == 0)
                return u | v;   // если true то возвращаем другое число

            for (k = 0; ((u | v) & 1) == 0; ++k) // проверяем являются ли оба числа четными
            {
                u >>= 1;    // сдвиг вправо (деление на 2)
                v >>= 1;
            }

            while ((u & 1) == 0)    // удаляем оставшиеся множители 2
                u >>= 1;

            do
            {
                while ((v & 1) == 0)
                    v >>= 1;

                if (u < v)
                {
                    v -= u;
                }
                else
                {
                    int diff = u - v;
                    u = v;
                    v = diff;
                }
                v >>= 1;    // удаляем множитель 2 который мог остаться после вычислений
            } while (v != 0);

            u <<= k;
            sw.Stop();
            time = sw.ElapsedTicks;
            return u;
        }
    }
}
