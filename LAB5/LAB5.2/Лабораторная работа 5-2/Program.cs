using System;

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

    public abstract decimal MetricValue();
    public abstract decimal ImperialValue();

    public void StartCollecting()
    {
        controller = DeviceController.StartDevice(measurementType);
        GetMeasurements();
    }

    public void StopCollecting()
    {
        if (controller != null)
        {
            controller.StopDevice();
            controller = null;
        }
    }

    public int[] GetRawData()
    {
        return dataCaptured;
    }

    private void GetMeasurements()
    {
        dataCaptured = new int[10];
        System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
        {
            int x = 0;
            Random timer = new Random();
            while (controller != null)
            {
                System.Threading.Thread.Sleep(timer.Next(10, 50));
                dataCaptured[x] = controller != null ? controller.TakeMeasurement() : dataCaptured[x];
                mostRecentMeasure = dataCaptured[x];
                x++;
                if (x == 10)
                {
                    x = 0;
                }
            }
        });
    }
}

public class MeasureLengthDevice : MeasureDataDevice
{
    public MeasureLengthDevice(Units units)
    {
        unitsToUse = units;
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

public class MeasureMassDevice : MeasureDataDevice
{
    public MeasureMassDevice(Units units)
    {
        unitsToUse = units;
        measurementType = DeviceType.MASS;
    }

    public override decimal MetricValue()
    {
        return unitsToUse == Units.Metric ? mostRecentMeasure : mostRecentMeasure * 0.4536m;
    }

    public override decimal ImperialValue()
    {
        return unitsToUse == Units.Imperial ? mostRecentMeasure : mostRecentMeasure * 2.2046m;
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
    }

    public int TakeMeasurement()
    {
        return new Random().Next(1, 100);
    }
}

// Тестовые вызовы
public class Program
{
    public static void Main()
    {
        MeasureLengthDevice lengthDevice = new MeasureLengthDevice(Units.Metric);
        lengthDevice.StartCollecting();
        System.Threading.Thread.Sleep(2000); // Ждем, пока будут собраны данные
        lengthDevice.StopCollecting();

        Console.WriteLine("Length Device Raw Data:");
        foreach (var data in lengthDevice.GetRawData())
        {
            Console.WriteLine(data);
        }

        Console.WriteLine($"Length Device Metric Value: {lengthDevice.MetricValue()}");
        Console.WriteLine($"Length Device Imperial Value: {lengthDevice.ImperialValue()}");

        MeasureMassDevice massDevice = new MeasureMassDevice(Units.Metric);
        massDevice.StartCollecting();
        System.Threading.Thread.Sleep(2000); // Ждем, пока будут собраны данные
        massDevice.StopCollecting();

        Console.WriteLine("Mass Device Raw Data:");
        foreach (var data in massDevice.GetRawData())
        {
            Console.WriteLine(data);
        }

        Console.WriteLine($"Mass Device Metric Value: {massDevice.MetricValue()}");
        Console.WriteLine($"Mass Device Imperial Value: {massDevice.ImperialValue()}");
    }
}
