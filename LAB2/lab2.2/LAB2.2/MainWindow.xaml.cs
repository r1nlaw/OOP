using System;
using System.Linq;
using System.Windows;

namespace GCDCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FindGCDFromString_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = txtInput.Text; // текстовое поле для ввода
                int result = GCDAlgorithms.FindGCDEuclidFromString(input);
                resultEuclid.Content = String.Format("Euclid: {0}", result);
            }
            catch (FormatException)
            {
                resultEuclid.Content = "Введите корректные числа через пробел.";
            }
            catch (Exception ex)
            {
                resultEuclid.Content = $"Ошибка: {ex.Message}";
            }
        }
    }

    public static class GCDAlgorithms
    {
        public static int FindGCDEuclid(int a, int b)
        {
            if (a == 0) return b;
            while (b != 0)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }
            return a;
        }

        public static int FindGCDEuclid(int a, int b, int c)
        {
            return FindGCDEuclid(FindGCDEuclid(a, b), c);
        }

        public static int FindGCDEuclid(int a, int b, int c, int d)
        {
            return FindGCDEuclid(FindGCDEuclid(a, b, c), d);
        }

        public static int FindGCDEuclid(int a, int b, int c, int d, int e)
        {
            return FindGCDEuclid(FindGCDEuclid(a, b, c, d), e);
        }

        public static int FindGCDEuclid(params int[] numbers)
        {
            if (numbers.Length == 0) return 0;
            int result = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                result = FindGCDEuclid(result, numbers[i]); // рекурсия для последовательного вычисления НОД (передаем в другую перегрузку этого метода)
            }
            return result;
        }

        public static int FindGCDEuclidFromString(string input)
        {
            var numbers = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) // разделяем входную строку input на массив строк, также пустые строки игнорируются
                               .Select(int.Parse) // парсим строки в целые числа
                               .ToArray(); // собираем все преобразованные числа в массив
            return FindGCDEuclid(numbers);
        }
    }
}
