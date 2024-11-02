using System;
using System.Diagnostics;
using System.Windows;
using labo3._1;

namespace FailsafeApp
{
    public partial class MainWindow : Window
    {
        private ReactorSwitch switchDevice = new ReactorSwitch();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            // Отключение от генератора питания
            try
            {
                var result = switchDevice.DisconnectPowerGenerator();
                textBlock1.Text += "\nШаг 1: Отключение от генератора питания: " + result;
            }
            catch (PowerGeneratorCommsException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 1: {ex.Message}";
            }

            // Проверка статуса основной системы охлаждения
            try
            {
                var status = switchDevice.VerifyPrimaryCoolantSystem();
                textBlock1.Text += "\nШаг 2: Проверка основной системы охлаждения: " + status;
            }
            catch (CoolantPressureReadException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 2: {ex.Message}";
            }
            catch (CoolantTemperatureReadException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 2: {ex.Message}";
            }

            // Проверка статуса резервной системы охлаждения
            try
            {
                var status = switchDevice.VerifyPrimaryCoolantSystem();
                textBlock1.Text += "\nШаг 3: Проверка резервной системы охлаждения: " + status;
            }
            catch (CoolantPressureReadException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 3: {ex.Message}";
            }
            catch (CoolantTemperatureReadException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 3: {ex.Message}";
            }

            // Запись температуры ядра перед отключением
            try
            {
                double temperature = switchDevice.GetCoreTemperature();
                textBlock1.Text += $"\nШаг 4: Температура ядра: {temperature}";
            }
            catch (CoreTemperatureReadException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 4: {ex.Message}";
            }

            // Вставка управляющих стержней в реактор
            try
            {
                var result = switchDevice.InsertRodCluster();
                textBlock1.Text += "\nШаг 5: Вставка управляющих стержней: " + result;
            }
            catch (RodClusterReleaseException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 5: {ex.Message}";
            }

            // Запись температуры ядра после отключения
            try
            {
                double temperature = switchDevice.GetCoreTemperature();
                textBlock1.Text += $"\nШаг 6: Температура ядра после отключения: {temperature}";
            }
            catch (CoreTemperatureReadException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 6: {ex.Message}";
            }

            // Запись уровней радиации ядра после отключения
            try
            {
                double radiationLevel = switchDevice.GetRadiationLevel();
                textBlock1.Text += $"\nШаг 7: Уровень радиации: {radiationLevel}";
            }
            catch (CoreRadiationLevelReadException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 7: {ex.Message}";
            }

            // Сообщение о завершении отключения
            try
            {
                switchDevice.SignalShutdownComplete();
                textBlock1.Text += "\nШаг 8: Отключение завершено!";
            }
            catch (SignallingException ex)
            {
                textBlock1.Text += $"\n*** Исключение в шаге 8: {ex.Message}";
            }
        }
    }
}
