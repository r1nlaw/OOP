namespace MeasuringDevice
{
    public class MeasureMassDevice : MeasureDataDevice
    {
        // Конструктор, который принимает единицу измерения
        public MeasureMassDevice(Units units)
        {
            unitsToUse = units;
            measurementType = DeviceType.MASS;
        }

        // Реализация метода для получения значения в метрических единицах
        public override decimal MetricValue()
        {
            return unitsToUse == Units.Metric ? mostRecentMeasure : mostRecentMeasure * 0.4536m;
        }

        // Реализация метода для получения значения в имперских единицах
        public override decimal ImperialValue()
        {
            return unitsToUse == Units.Imperial ? mostRecentMeasure : mostRecentMeasure * 2.2046m;
        }
    }
}
