using System;
using System.Windows;

namespace Vector3DApp
{
    public partial class MainWindow : Window
    {
        // Масса студента (кг)
        private const double studentMass = 70.0;
        // Скорость студента (м/с)
        private const double studentSpeed = 7.0;
        // Угловая скорость Земли (рад/с)
        private const double omega = 7.2921159e-5;
        // Широта Симферополя (градусы)
        private const double latitude = 44.95;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки "Вычислить"
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Чтение данных из текстовых полей
                double x1 = Convert.ToDouble(X1TextBox.Text);
                double y1 = Convert.ToDouble(Y1TextBox.Text);
                double z1 = Convert.ToDouble(Z1TextBox.Text);
                double x2 = Convert.ToDouble(X2TextBox.Text);
                double y2 = Convert.ToDouble(Y2TextBox.Text);
                double z2 = Convert.ToDouble(Z2TextBox.Text);

                // Создание векторов
                Vector3D v1 = new Vector3D(x1, y1, z1);
                Vector3D v2 = new Vector3D(x2, y2, z2);

                // Выполнение операций
                Vector3D sum = v1 + v2;
                Vector3D difference = v1 - v2;
                Vector3D crossProduct = Vector3D.CrossProduct(v1, v2);
                double magnitudeV1 = v1.Magnitude;
                double magnitudeV2 = v2.Magnitude;

                // Отображение результатов
                ResultTextBlock.Text = $"Сумма: {sum}\n" +
                                       $"Разность: {difference}\n" +
                                       $"Векторное произведение: {crossProduct}\n" +
                                       $"Модуль V1: {magnitudeV1:F2}\n" +
                                       $"Модуль V2: {magnitudeV2:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислениях: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик нажатия кнопки "Вычислить силу Кориолиса"
        private void CalculateCoriolisButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Вычисление силы Кориолиса
                double latitudeRad = latitude * Math.PI / 180.0; // Переводим широту в радианы
                double coriolisForce = 2 * studentMass * studentSpeed * omega * Math.Sin(latitudeRad);

                // Отображаем результат
                CoriolisResultTextBlock.Text = $"Сила Кориолиса: {coriolisForce:F2} Н";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислениях силы Кориолиса: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
