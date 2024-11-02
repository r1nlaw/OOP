using System;
using System.Windows;

namespace GCDCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FindGCD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int firstNumber = int.Parse(txtFirstNumber.Text);
                int secondNumber = int.Parse(txtSecondNumber.Text);

                long timeEuclid;
                int gcdEuclid = GCDAlgorithms.FindGCDEuclid(firstNumber, secondNumber, out timeEuclid);

                long timeStein;
                int gcdStein = GCDAlgorithms.FindGCDStein(firstNumber, secondNumber, out timeStein);

                resultEuclid.Content = $"Евклид: {gcdEuclid}, Время (тики): {timeEuclid}";
                resultStein.Content = $"Штейн: {gcdStein}, Время (тики): {timeStein}";
            }
            catch (FormatException)
            {
                resultEuclid.Content = "Введите корректные числа.";
                resultStein.Content = "";
            }
            
        }

        private void FindLargestPrime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int n = int.Parse(txtN.Text);
                int largestPrime = FindLargestPrimeLessThan(n);

                resultPrime.Content = $"Самое большое простое число меньше {n}: {largestPrime}";

                // Представление с помощью побитового сдвига
                if (largestPrime >= 2)
                {
                    int shift = (int)Math.Floor(Math.Log(largestPrime) / Math.Log(2));
                    resultBinary.Content = $"2 << ({shift}) = {1 << shift} (приблизительно {largestPrime})";
                }
                else
                {
                    resultBinary.Content = "Нет подходящего простого числа.";
                }
            }
            catch (FormatException)
            {
                resultPrime.Content = "Введите корректное число.";
                resultBinary.Content = "";
            }
            
        }

        private int FindLargestPrimeLessThan(int n)
        {
            for (int i = n - 1; i >= 2; i--)
            {
                if (IsPrime(i))
                    return i;
            }
            return 1; // Возвращаем 1, если нет простых чисел
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true; // 2 - простое число
            if (number % 2 == 0) return false; // Четные числа больше 2 не простые

            for (int i = 3; i * i <= number; i += 2)    // если число не делится ни на одно из числел до его квадратного корня то оно является простым
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }



}

