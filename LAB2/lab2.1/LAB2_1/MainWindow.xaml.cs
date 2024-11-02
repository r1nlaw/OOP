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
                int firstNumber = Convert.ToInt32(txtFirstNumber.Text);
                int secondNumber = Convert.ToInt32(txtSecondNumber.Text);
                int gcdResult = GCD.FindGCDEuclid(firstNumber, secondNumber);
                resultEuclid.Content = String.Format("НОД: {0}", gcdResult);
            }
            catch (FormatException)
            {
                resultEuclid.Content = "Введите корректные числа.";
            }
            catch (Exception ex)
            {
                resultEuclid.Content = $"Ошибка: {ex.Message}";
            }
        }
    }

    public static class GCD
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
    }

}
