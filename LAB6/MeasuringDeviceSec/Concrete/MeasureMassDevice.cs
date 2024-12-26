using MeasuringDevice.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice.Concrete
{
    public class HeartBeatEventArgs : EventArgs
    {
        public DateTime TimeStamp { get;  }

        public HeartBeatEventArgs() : base()
        {
            TimeStamp = DateTime.Now;
        }
    }
    public delegate void HeartBeatEventHandler(object sender, HeartBeatEventArgs args);
    public class MeasureMassDevice : MeasureDataDevice
    {
        public int heartBeatInterval;
        protected override DeviceType measurementType => DeviceType.MASS;
        public MeasureMassDevice(int heartBeatInterval, Units.Units units, string logFileName)
        {
            this.heartBeatIntervalTime = heartBeatInterval;
            unitsToUse = units;
            dataCaptured = new int[10];
            this.LoggingFileName = logFileName;
        }

        public override decimal MetricValue()
        {
            return unitsToUse == Units.Units.Metric ? mostRecentMeasure : mostRecentMeasure * 0.4536m;
        }

        public override decimal ImperialValue()
        {
            return unitsToUse == Units.Units.Imperial ? mostRecentMeasure : mostRecentMeasure * 2.2046m;
        }
    }
}
