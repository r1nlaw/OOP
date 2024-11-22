using System;
using System.Windows;
using ComplexNumberApp;
namespace ComplexNumberApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var c1 = new ComplexNumber(
                Convert.ToDouble(Real1TextBox.Text),
                Convert.ToDouble(Imaginary1TextBox.Text)
            );
            var c2 = new ComplexNumber(
                Convert.ToDouble(Real2TextBox.Text),
                Convert.ToDouble(Imaginary2TextBox.Text)
            );

            var result = c1 + c2;
            ResultTextBlock.Text = $"Result: {result}";
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            var c1 = new ComplexNumber(
                Convert.ToDouble(Real1TextBox.Text),
                Convert.ToDouble(Imaginary1TextBox.Text)
            );
            var c2 = new ComplexNumber(
                Convert.ToDouble(Real2TextBox.Text),
                Convert.ToDouble(Imaginary2TextBox.Text)
            );

            var result = c1 * c2;
            ResultTextBlock.Text = $"Result: {result}";
        }

        private void ToExponentialButton_Click(object sender, RoutedEventArgs e)
        {
            var c1 = new ComplexNumber(
                Convert.ToDouble(Real1TextBox.Text),
                Convert.ToDouble(Imaginary1TextBox.Text)
            );

            var (modulus, argument) = c1.ToExponential();
            ExponentialTextBlock.Text = $"Exponential form: {modulus} * exp(i * {argument})";
        }
    }
}
