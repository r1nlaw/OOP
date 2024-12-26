using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice.Interfaces
{
    public interface IMeasuringDevice
    {
        decimal MetricValue();
        decimal ImperialValue();
        void StartCollecting();
        void StopCollecting();

        int[] GetRawData();
        string GetLoggingFile();
        Units.Units UnitsToUse { get; }
        int[] DataCaptured { get; }
        int MostRecentMeasure { get; }
        string LoggingFileName { get; set; }
    }
}
