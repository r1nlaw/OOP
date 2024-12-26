using MeasuringDevice.Concrete;
using MeasuringDevice.Interfaces;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;

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
        public event EventHandler<HeartBeatEventArgs> HeartBeat;

        // Свойства
        protected int heartBeatIntervalTime { get; set; }
        public int HeartBeatInterval
        {
            get { return heartBeatIntervalTime; }
            private set { heartBeatIntervalTime = value; }
        }

        // Приватные поля
        private BackgroundWorker heartBeatTimer;
        private BackgroundWorker dataCollector;
        private StreamWriter loggingFileWriter;
        private bool disposed = false;

        // Методы для управления сердечным ритмом
        private void StartHeartBeat()
        {
            heartBeatTimer = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };

            heartBeatTimer.DoWork += (o, args) =>
            {
                while (true)
                {
                    Thread.Sleep(HeartBeatInterval);
                    if (disposed) break;
                    heartBeatTimer.ReportProgress(0);
                }
            };

            heartBeatTimer.ProgressChanged += (o, args) =>
            {
                OnHeartBeat();
            };

            heartBeatTimer.RunWorkerAsync();
        }

        protected virtual void OnHeartBeat()
        {
            HeartBeat?.Invoke(this, new HeartBeatEventArgs());
        }

        // Методы для управления сбором данных
        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);
            InitializeLoggingFile();
            GetMeasurements();
            StartHeartBeat();
        }

        public void StopCollecting()
        {
            StopDeviceController();
            CancelDataCollector();
            CloseLoggingFile();
            DisposeHeartBeatTimer();
        }

        public int[] GetRawData()
        {
            return dataCaptured;
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
                Thread.Sleep(timer.Next(1000, 5000));
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

        protected virtual void OnNewMeasurementTaken()
        {
            NewMeasurementTaken?.Invoke(this, EventArgs.Empty);
        }

        // Методы для управления файлом логов
        private void InitializeLoggingFile()
        {
            if (loggingFileWriter == null)
            {
                LoggingFileName = "measurements.log";
                loggingFileWriter = new StreamWriter(LoggingFileName, true);
            }
        }

        private void LogMeasurement(int measurement)
        {
            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine($"Measurement - {measurement}");
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

        // Методы для управления устройством
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

        private void DisposeHeartBeatTimer()
        {
            if (heartBeatTimer != null)
            {
                heartBeatTimer.Dispose();
            }
        }

        public void Dispose()
        {
            DisposeDataCollector();
            DisposeLoggingFile();
            DisposeHeartBeatTimer();
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

        // Публичные свойства
        public int[] DataCaptured => dataCaptured;
        public int MostRecentMeasure => mostRecentMeasure;
        public string LoggingFileName { get; set; }
        Units.Units IMeasuringDevice.UnitsToUse => unitsToUse;

        public string GetLoggingFile()
        {
            return LoggingFileName;
        }
    }
}