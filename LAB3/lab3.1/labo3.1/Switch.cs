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
                throw new PowerGeneratorCommsException("Сбой в сети при доступе к системе мониторинга электрогенератора");
            return SuccessFailureResult.Success;
        }

        // Метод для проверки основного охладительного контур
        public CoolantSystemStatus VerifyPrimaryCoolantSystem()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoolantTemperatureReadException("Не удалось определить температуру системы охлаждения первого контура");
            return CoolantSystemStatus.OK;
        }

        // Метод для проверки резервного охладительного контур
        public CoolantSystemStatus VerifyBackupCoolantSystem()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoolantTemperatureReadException("Не удалось определить температуру резервной системы охлаждения");
            return CoolantSystemStatus.OK;
        }

        // Метод для вставки управляющих стержней
        public SuccessFailureResult InsertRodCluster()
        {
            SuccessFailureResult result = SuccessFailureResult.Fail;
            if (rand.Next(1, 100) > 5)
                result = SuccessFailureResult.Success;

            if (rand.Next(1, 10) > 8)
                throw new RodClusterReleaseException("Неисправность датчика, не удается проверить высвобождение штока");

            return result;
        }

        // Метод для получения температуры ядра
        public double GetCoreTemperature()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoreTemperatureReadException("Не удалось определить температуру системы активной зоны реактора");
            return rand.NextDouble() * 1000; // Возвращает случайное значение температуры
        }

        // Метод для получения уровня радиации
        public double GetRadiationLevel()
        {
            if (rand.Next(1, 20) > 18)
                throw new CoreRadiationLevelReadException("Не удалось определить уровень радиации в активной зоне реактора");
            return rand.NextDouble() * 500; // Возвращает случайное значение уровня радиации
        }

        // Метод для сигнала о завершении отключения
        public void SignalShutdownComplete()
        {
            if (rand.Next(1, 20) > 18)
                throw new SignallingException("Сбой в сети при подключении к системам широковещания");
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
