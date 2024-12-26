using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice.Concrete
{
    public class DeviceController
    {
        private static DeviceController obj;
        private DeviceType measurementType;

        public DeviceController(DeviceType measurementType)
        {
            this.measurementType = measurementType;
        }

        public static DeviceController StartDevice(DeviceType measurementType)
        {
            obj = new DeviceController(measurementType);
            return obj;
        }

        public static void StopDevice()
        {
            obj = null;
        }

        public static int TakeMeasurement()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }
    public enum DeviceType
    {
        LENGTH,
        MASS
    }
}
