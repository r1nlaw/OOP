using System;
using System.Linq;
using System.Threading.Tasks;

public enum Units
{
    Metric,
    Imperial
}

public interface IMeasuringDevice
{
    decimal MetricValue();
    decimal ImperialValue();
    void StartCollecting();
    void StopCollecting();
    int[] GetRawData();
}

public abstract class MeasureDataDevice : IMeasuringDevice
{
    protected Units unitsToUse;
    protected int[] dataCaptured;
    protected int mostRecentMeasure;
    protected DeviceController controller;
    protected DeviceType measurementType;

    public MeasureDataDevice(Units units)
    {
        unitsToUse = units;
        dataCaptured = new int[10];  // Массив для хранения данных
    }

    public abstract decimal MetricValue();
    public abstract decimal ImperialValue();

    public void StartCollecting()
    {
        controller = DeviceController.StartDevice(measurementType);
        CollectMeasurementsAsync();  // Асинхронный сбор данных
    }

    public void StopCollecting()
    {
        if (controller != null)
        {
            controller.StopDevice();
            controller = null;
        }
    }

    public int[] GetRawData() => dataCaptured;

    private async void CollectMeasurementsAsync()
    {
        int index = 0;
        Random random = new Random();

        while (controller != null)
        {
            await Task.Delay(random.Next(10, 50));  // Задержка между измерениями
            dataCaptured[index] = controller.TakeMeasurement();
            mostRecentMeasure = dataCaptured[index];
            index = (index + 1) % dataCaptured.Length;  // Цикличность индекса
        }
    }
}

public class MeasureLengthDevice : MeasureDataDevice
{
    public MeasureLengthDevice(Units units) : base(units)
    {
        measurementType = DeviceType.LENGTH;
    }

    public override decimal MetricValue()
    {
        return unitsToUse == Units.Metric ? mostRecentMeasure : mostRecentMeasure * 25.4m;
    }

    public override decimal ImperialValue()
    {
        return unitsToUse == Units.Imperial ? mostRecentMeasure : mostRecentMeasure * 0.03937m;
    }
}

public enum DeviceType
{
    LENGTH,
    MASS
}

public class DeviceController
{
    public static DeviceController StartDevice(DeviceType type)
    {
        return new DeviceController();
    }

    public void StopDevice()
    {
        // Остановка устройства
    }

    public int TakeMeasurement()
    {
        return new Random().Next(1, 100);  // Возвращает случайное число
    }
}

public class Program
{
    public static void Main()
    {
        MeasureLengthDevice device = new MeasureLengthDevice(Units.Metric);
        device.StartCollecting();
        System.Threading.Thread.Sleep(2000); // Ждем, пока будут собраны данные
        device.StopCollecting();

        Console.WriteLine("Raw Data:");
        foreach (var data in device.GetRawData())
        {
            Console.WriteLine(data);
        }

        Console.WriteLine($"Metric Value: {device.MetricValue()}");
        Console.WriteLine($"Imperial Value: {device.ImperialValue()}");
    }
}
