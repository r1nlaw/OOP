using System;
using System.Threading;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IMeasuringDevice
    {
        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        // Абстрактные методы для получения значения в разных единицах
        public abstract decimal MetricValue();
        public abstract decimal ImperialValue();

        // Метод для запуска сбора данных
        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);
            GetMeasurements();
        }

        // Метод для остановки сбора данных
        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }
        }

        // Метод для получения сырых данных
        public int[] GetRawData()
        {
            return dataCaptured;
        }

        // Метод для сбора данных с устройства
        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();
                while (controller != null)
                {
                    Thread.Sleep(timer.Next(1000, 5000)); // Симуляция задержки между измерениями
                    dataCaptured[x] = controller != null ? controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasure = dataCaptured[x];
                    x++;
                    if (x == 10) x = 0;
                }
            });
        }
    }
}
