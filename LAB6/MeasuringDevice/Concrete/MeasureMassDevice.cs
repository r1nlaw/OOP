using MeasuringDevice.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice.Concrete
{
    public class MeasureMassDevice : MeasureDataDevice
    {
        protected override DeviceType measurementType => DeviceType.MASS;
        public MeasureMassDevice(Units.Units units)
        {
            unitsToUse = units;
            dataCaptured = new int[10];
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
