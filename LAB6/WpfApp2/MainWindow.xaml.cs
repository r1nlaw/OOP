using MeasuringDevice.Concrete;
using MeasuringDevice.Interfaces;
using MeasuringDevice.Units;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private EventHandler<HeartBeatEventArgs> heartBeatHandler;
        private IEventEnabledMeasuringDevice device;
        private Units unit = Units.Metric;
        private StreamWriter logFileWriter; 

        public MainWindow() => InitializeComponent();
        private EventHandler newMeasurementTaken;

        private void startCollecting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newMeasurementTaken = new EventHandler(device_NewMeasurementTaken);
                heartBeatHandler = (o, args) =>
                {
                    heartbeatLabel.Content = $"HeartBeat: {args.TimeStamp}";
                };
                device.HeartBeat += heartBeatHandler;
                device.NewMeasurementTaken += newMeasurementTaken;
                device.StartCollecting();

                LogToFile($"Сбор данных начат: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при запуске сбора данных");
            }
        }

        private void stopCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device == null)
            {
                return;
            }
            try
            {
                device.NewMeasurementTaken -= newMeasurementTaken;
                device.StopCollecting();
                resultLabel.Content += "Остановка сбора данных\n";

                LogToFile($"Сбор данных остановлен: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при остановке сбора данных");
            }
        }

        private void device_NewMeasurementTaken(object sender, EventArgs e)
        {
            try
            {
                resultLabel.Content = "";
                if (device != null)
                {
                    resultLabel.Content += device.MostRecentMeasure.ToString();
                    resultLabel.Content += "\n";
                    resultLabel.Content += device.MetricValue().ToString();
                    resultLabel.Content += "\n";
                    resultLabel.Content += device.ImperialValue().ToString();
                    resultLabel.Content += "\n";
                    resultLabel.Content += string.Join(", ", device.GetRawData());
                    resultLabel.Content += "\n";

                    LogToFile($"Новое измерение: {device.MostRecentMeasure}, Метрическое: {device.MetricValue()}, Имперское: {device.ImperialValue()}");
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при обработке нового измерения");
            }
        }

        private void CreateInstance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logFileWriter?.Close();

                logFileWriter = new StreamWriter("log.txt", true);

                device = new MeasureMassDevice(1000, unit, "log.txt");

                LogToFile($"Создан экземпляр устройства: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при создании экземпляра устройства");
            }
        }

        private void GetRawData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                resultLabel.Content += string.Join(", ", device.GetRawData());
                resultLabel.Content += "\n";

                LogToFile($"Получены необработанные данные: {string.Join(", ", device.GetRawData())}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при получении необработанных данных");
            }
        }

        private void GetMetricValue(object sender, RoutedEventArgs e)
        {
            try
            {
                resultLabel.Content += "Метрические данные:\n\t" + device.MetricValue();
                resultLabel.Content += "\n";

                LogToFile($"Получены метрические данные: {device.MetricValue()}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при получении метрических данных");
            }
        }

        private void GetImperialValue(object sender, RoutedEventArgs e)
        {
            try
            {
                resultLabel.Content += "Имперские данные:\n\t" + device.ImperialValue();
                resultLabel.Content += "\n";

                // Логирование в файл
                LogToFile($"Получены имперские данные: {device.ImperialValue()}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при получении имперских данных");
            }
        }

        private void measuring_system_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = sender as CheckBox;
                if (checkBox != null && checkBox.IsChecked == true)
                {
                    unit = Units.Imperial;
                }
                else
                {
                    unit = Units.Metric;
                }

                // Логирование в файл
                LogToFile($"Изменены единицы измерения: {unit}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при изменении единиц измерения");
            }
        }

        private void Dispose_Click(object sender, RoutedEventArgs e)
        {
            if (device == null)
            {
                return;
            }
            try
            {
                device.HeartBeat -= heartBeatHandler;
                device = null;
                LogToFile($"Объект устройства уничтожен: {DateTime.Now}");

                logFileWriter?.Close();
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при уничтожении объекта устройства");
            }
        }

        private void LogToFile(string message)
        {
            try
            {
                if (logFileWriter != null)
                {
                    logFileWriter.WriteLine($"{DateTime.Now}: {message}");
                    logFileWriter.Flush(); 
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при записи в файл");
            }
        }

        private void HandleError(Exception ex, string message)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            LogToFile($"Ошибка: {message} - {ex.Message}");
        }

        private void StopCollecting(object sender, RoutedEventArgs e)
        {
            try
            {
                if (device == null)
                {
                    return;
                }
                device.StopCollecting();
                resultLabel.Content += "Остановка сбора данных\n";

                // Логирование в файл
                LogToFile($"Сбор данных остановлен: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                HandleError(ex, "Ошибка при остановке сбора данных");
            }
        }
    }
}