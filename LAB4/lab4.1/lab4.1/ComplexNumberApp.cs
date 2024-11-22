using System;

namespace ComplexNumberApp
{
    public struct ComplexNumber
    {
        // Поля структуры: вещественная и мнимая части комплексного числа
        public double Real { get; }
        public double Imaginary { get; }

        // Конструктор для инициализации комплексного числа
        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // Свойство для вычисления модуля комплексного числа
        public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);

        // Перегрузка оператора сложения: (c1 + c2)
        public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2)
        {
            return new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        // Перегрузка оператора вычитания: (c1 - c2)
        public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2)
        {
            return new ComplexNumber(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        // Перегрузка оператора умножения: (c1 * c2)
        public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2)
        {
            double realPart = c1.Real * c2.Real - c1.Imaginary * c2.Imaginary;
            double imaginaryPart = c1.Real * c2.Imaginary + c1.Imaginary * c2.Real;
            return new ComplexNumber(realPart, imaginaryPart);
        }

        // Перегрузка оператора деления: (c1 / c2)
        public static ComplexNumber operator /(ComplexNumber c1, ComplexNumber c2)
        {
            double denominator = c2.Real * c2.Real + c2.Imaginary * c2.Imaginary;
            double realPart = (c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) / denominator;
            double imaginaryPart = (c1.Imaginary * c2.Real - c1.Real * c2.Imaginary) / denominator;
            return new ComplexNumber(realPart, imaginaryPart);
        }

        // Перегрузка оператора равенства: (c1 == c2)
        public static bool operator ==(ComplexNumber c1, ComplexNumber c2)
        {
            return c1.Real == c2.Real && c1.Imaginary == c2.Imaginary;
        }

        // Перегрузка оператора неравенства: (c1 != c2)
        public static bool operator !=(ComplexNumber c1, ComplexNumber c2)
        {
            return !(c1 == c2);
        }

        // Переопределение метода ToString для красивого отображения комплексного числа
        public override string ToString()
        {
            if (Imaginary >= 0)
                return $"{Real} + {Imaginary}i";
            else
                return $"{Real} - {-Imaginary}i";
        }

        // Метод для преобразования комплексного числа в экспоненциальную форму
        // Формула Эйлера: r * exp(iθ) = r * (cos(θ) + i * sin(θ))
        public (double modulus, double argument) ToExponential()
        {
            double modulus = Magnitude;
            double argument = Math.Atan2(Imaginary, Real);  // аргумент в радианах
            return (modulus, argument);
        }

        // Статический метод для преобразования из экспоненциальной формы в прямоугольную
        // r * exp(iθ) = r * (cos(θ) + i * sin(θ))
        public static ComplexNumber FromExponential(double modulus, double argument)
        {
            double real = modulus * Math.Cos(argument);
            double imaginary = modulus * Math.Sin(argument);
            return new ComplexNumber(real, imaginary);
        }

        // Переопределение метода Equals для корректной работы с == и !=
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return this == other;
            }
            return false;
        }

        // Переопределение GetHashCode
        public override int GetHashCode()
        {
            return (Real, Imaginary).GetHashCode();
        }
    }
}
