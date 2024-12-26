using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MeasuringDevice;
using MeasuringDevice.Abstract;
using MeasuringDevice.Concrete;
using MeasuringDevice.Interfaces;
using MeasuringDevice.Units;


namespace WpfApp1
{
    
    public partial class MainWindow : Window
    {
        private EventHandler newMeasurementTaken;
        private IEventEnabledMeasuringDevice device;
        private Units unit = Units.Metric;

        public MainWindow()
        {
            InitializeComponent();
            device = new MeasureMassDevice(unit);
        }

        private void startCollecting_Click(object sender, RoutedEventArgs e)
        {
            newMeasurementTaken = new EventHandler(device_NewMeasurementTaken);
            device.NewMeasurementTaken += newMeasurementTaken;
            device.StartCollecting();
        }

        private void stopCollecting_Click(object sender, RoutedEventArgs e)
        {
            device.NewMeasurementTaken -= newMeasurementTaken;
            device.StopCollecting();
            resultLabel.Content += "Остановка сбора данных\n";
        }

        private void device_NewMeasurementTaken(object sender, EventArgs e)
        {
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
            }
        }

        private void CreateInstance_Click(object sender, RoutedEventArgs e)
        {
            device = new MeasureMassDevice(unit);
          
        }

        private void GetRawData_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content += string.Join(", ", device.GetRawData());
            resultLabel.Content += "\n";
        }

        private void GetMetricValue(object sender, RoutedEventArgs e)
        {
            resultLabel.Content += "Метрические данные:\n\t" + device.MetricValue();
            resultLabel.Content += "\n";
        }

        private void GetImperialValue(object sender, RoutedEventArgs e)
        {
            resultLabel.Content += "Имперские данные:\n\t" + device.ImperialValue();
            resultLabel.Content += "\n";
        }

        private void StopCollecting(object sender, RoutedEventArgs e)
        {
            device.StopCollecting();
            resultLabel.Content += "Остановка сбора данных\n";
        }

        private void measuring_system_Checked(object sender, RoutedEventArgs e)
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
        }
    }
}
