using System;

namespace labo3._1
{
    public class ReactorSwitch
    {
        private Random rand = new Random();

        // Метод для отключения от генератора
        public SuccessFailureResult DisconnectPowerGenerator()
        {
            if (rand.Next(1, 20) > 18)
                throw new PowerGeneratorCommsException("Network failure accessing Power Generator monitoring system");
            return SuccessFailureResult.Success;
        }

        // Метод для проверки основного охладительного контур
        public CoolantSystemStatus VerifyPrimaryCoolantSystem()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoolantTemperatureReadException("Failed to read primary coolant system temperature");
            return CoolantSystemStatus.OK;
        }

        // Метод для проверки резервного охладительного контур
        public CoolantSystemStatus VerifyBackupCoolantSystem()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoolantTemperatureReadException("Failed to read backup coolant system temperature");
            return CoolantSystemStatus.OK;
        }

        // Метод для вставки управляющих стержней
        public SuccessFailureResult InsertRodCluster()
        {
            SuccessFailureResult result = SuccessFailureResult.Fail;
            if (rand.Next(1, 100) > 5)
                result = SuccessFailureResult.Success;

            if (rand.Next(1, 10) > 8)
                throw new RodClusterReleaseException("Sensor failure, cannot verify rod release");

            return result;
        }

        // Метод для получения температуры ядра
        public double GetCoreTemperature()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoreTemperatureReadException("Failed to read core reactor system temperature");
            return rand.NextDouble() * 1000; // Возвращает случайное значение температуры
        }

        // Метод для получения уровня радиации
        public double GetRadiationLevel()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoreRadiationLevelReadException("Failed to read core reactor system radiation levels");
            return rand.NextDouble() * 500; // Возвращает случайное значение уровня радиации
        }

        // Метод для сигнала о завершении отключения
        public void SignalShutdownComplete()
        {
            if (rand.Next(1, 20) > 18)
                throw new SignallingException("Network failure connecting to broadcast systems");
        }

    }

    // Перечисления и исключения
    public enum SuccessFailureResult { Success, Fail }
    public enum CoolantSystemStatus { OK, Check, Fail }

    public class PowerGeneratorCommsException : Exception
    {
        public PowerGeneratorCommsException(string message) : base(message) { }
    }

    public class CoolantTemperatureReadException : Exception
    {
        public CoolantTemperatureReadException(string message) : base(message) { }
    }

    public class CoreTemperatureReadException : Exception
    {
        public CoreTemperatureReadException(string message) : base(message) { }
    }

    public class RodClusterReleaseException : Exception
    {
        public RodClusterReleaseException(string message) : base(message) { }
    }

    public class CoreRadiationLevelReadException : Exception
    {
        public CoreRadiationLevelReadException(string message) : base(message) { }
    }

    public class SignallingException : Exception
    {
        public SignallingException(string message) : base(message) { }
    }
    public class CoolantPressureReadException : Exception
    {
        public CoolantPressureReadException(string message) : base(message) { }
    }

}
