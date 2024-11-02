using System;
using System.Windows;

namespace SquareRootCalculator
{
    public partial class MainWindow : Window
    {
        private decimal previousResult;
        private int iteration;
        private decimal delta = Convert.ToDecimal(Math.Pow(10, -28)); 
        private decimal numberDecimal;
        private decimal guess;

        public MainWindow()
        {
            InitializeComponent();
            ResetValues();
        }

        private void ResetValues()
        {
            previousResult = 0;
            iteration = 0;
            IterationTextBlock.Text = "Итерация: 0";
            ErrorTextBlock.Text = "Погрешность: 0";
            RootResultTextBlock.Text = "Значение корня: 0";
        }

        private void CalculateWithDotNetFramework(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(InputNumberTextBox.Text, out double number))
            {
                if (number < 0)
                {
                    MessageBox.Show("Введите положительное число.");
                    return;
                }
                double result = Math.Sqrt(number);
                DotNetResultTextBox.Text = result.ToString("F16");
            }
            else
            {
                MessageBox.Show("Введите корректное число.");
            }
        }

        private void CalculateWithNewton(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(InputNumberTextBox.Text, out numberDecimal) && decimal.TryParse(InitialGuessTextBox.Text, out guess))
            {
                if (guess == 0)
                {
                    guess = numberDecimal / 2; 
                }

                decimal result = (numberDecimal / guess + guess) / 2;
                previousResult = result;
                NewtonResultTextBox.Text = result.ToString("F28");
            }
            else
            {
                MessageBox.Show("Введите корректные значения.");
            }
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            ResetValues();
            CalculateWithNewton(sender, e); 
        }

        private void NextIteration(object sender, RoutedEventArgs e)
        {
            decimal result = (numberDecimal / previousResult + previousResult) / 2;
            decimal error = Math.Abs(result - previousResult);

            
            iteration++;
            IterationTextBlock.Text = $"Итерация: {iteration}";
            ErrorTextBlock.Text = $"Погрешность: {error:F30}";
            RootResultTextBlock.Text = $"Значение корня: {result:F28}";

            // Обновляем предположение
            guess = result;
            NewtonResultTextBox.Text = result.ToString("F28");

            // Проверка точности
            if (error < delta)
            {
                MessageBox.Show("Достигнута необходимая точность.");
            }
            previousResult = result;
        }

        private void InitialGuessTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Обработчик изменения текста в поле начального приближения
        }

        private void NewtonResultTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Обработчик изменения текста в поле результата метода Ньютона
        }
    }
}