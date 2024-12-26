using MeasuringDevice.Concrete;
using MeasuringDevice.Interfaces;
using System;
using System.ComponentModel;
using System.IO;

namespace MeasuringDevice.Abstract
{
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice
    {
        // Поля
        protected Units.Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected abstract DeviceType measurementType { get; }

        // Абстрактные методы
        public abstract decimal MetricValue();
        public abstract decimal ImperialValue();

        // События
        public event EventHandler NewMeasurementTaken;

        // Приватные поля
        private BackgroundWorker dataCollector;
        private StreamWriter loggingFileWriter;

        // Публичные методы
        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);
            InitializeLoggingFile();
            GetMeasurements();
        }

        public void StopCollecting()
        {
            StopDeviceController();
            CancelDataCollector();
            CloseLoggingFile();
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }

        public void Dispose()
        {
            DisposeDataCollector();
            DisposeLoggingFile();
        }

        public string GetLoggingFile()
        {
            return LoggingFileName;
        }

        // Свойства
        public int[] DataCaptured => dataCaptured;
        public int MostRecentMeasure => mostRecentMeasure;
        public string LoggingFileName { get; set; }
        Units.Units IMeasuringDevice.UnitsToUse => unitsToUse;

        // Приватные методы
        private void InitializeLoggingFile()
        {
            if (loggingFileWriter == null)
            {
                LoggingFileName = "measurements.log";
                loggingFileWriter = new StreamWriter(LoggingFileName, true);
            }
        }

        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            dataCollector = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };

            dataCollector.DoWork += dataCollector_DoWork;
            dataCollector.ProgressChanged += dataCollector_ProgressChanged;
            dataCollector.RunWorkerAsync();
        }

        private void dataCollector_DoWork(object sender, DoWorkEventArgs e)
        {
            int x = 0;
            Random timer = new Random();

            while (!dataCollector.CancellationPending)
            {
                System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                dataCaptured[x] = controller != null ? DeviceController.TakeMeasurement() : dataCaptured[x];
                mostRecentMeasure = dataCaptured[x];

                LogMeasurement(mostRecentMeasure);

                dataCollector.ReportProgress(0);
                x++;
                if (x == 10) x = 0;
            }
        }

        private void dataCollector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }

        private void LogMeasurement(int measurement)
        {
            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine($"Measurement - {measurement}");
            }
        }

        private void StopDeviceController()
        {
            if (controller != null)
            {
                DeviceController.StopDevice();
                controller = null;
            }
        }

        private void CancelDataCollector()
        {
            if (dataCollector != null)
            {
                dataCollector.CancelAsync();
            }
        }

        private void CloseLoggingFile()
        {
            if (loggingFileWriter != null)
            {
                loggingFileWriter.Close();
                loggingFileWriter = null;
            }
        }

        private void DisposeDataCollector()
        {
            if (dataCollector != null)
            {
                dataCollector.Dispose();
            }
        }

        private void DisposeLoggingFile()
        {
            if (loggingFileWriter != null)
            {
                loggingFileWriter.Dispose();
                loggingFileWriter = null;
            }
        }

        // Виртуальные методы
        protected virtual void OnNewMeasurementTaken()
        {
            NewMeasurementTaken?.Invoke(this, EventArgs.Empty);
        }
    }
}