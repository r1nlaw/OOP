using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

static void task_1() //  Неявное преобразование простых и ссылочных типов, в виде комментариев внести в программу таблицу неявных преобразований;
{

    // ------------------------------------------------------------------------------|
    // Исходный тип |                                                                |
    // ------------------------------------------------------------------------------|
    // sbyte        | short, int, long, float, double, decimal                       |
    // byte         | short, ushort, int, uint, long, ulong, float, double, decimal  |
    // short        | int, long, float, double, decimal                              |
    // ushort       | int, uint, long, ulong, float, double, decimal                 |
    // int          | long, float, double, decimal                                   |
    // uint         | long, ulong, float, double, decimal                            |
    // long         | float, double, decimal                                         |
    // ulong        | float, double, decimal                                         |
    // float        | double                                                         |
    // char         | ushort, int, uint, long, ulong, float, double, decimal         |
    // ------------------------------------------------------------------------------|
    sbyte a = 30;
    int b = a;
    Console.WriteLine($"sbyte = {a} | int ----> {b}");

    string str = "Hello";
    object obj = str;
    Console.WriteLine($"string {str} | obj ----> {obj}");

}

//task_1();

static void task_2()
{
    // Таблица явных преобразований в C#
    // ----------------------------------------------------------------------------------------------------------------------------|
    // Исходный тип | Целевой тип       | Примечание                                                                               |
    // ----------------------------------------------------------------------------------------------------------------------------|
    // sbyte        | byte, ushort, uint, ulong, char | Преобразование с потерей знака                                             |
    // byte         | sbyte, char       | Преобразование с потерей знака                                                           |
    // short        | sbyte, byte, ushort, uint, ulong, char | Преобразование с потерей знака и переполнением                      |
    // ushort       | sbyte, byte, short, char | Преобразование с потерей знака и переполнением                                    |
    // int          | sbyte, byte, short, ushort, uint, ulong, char | Преобразование с потерей знака и переполнением               |
    // uint         | sbyte, byte, short, ushort, int, char | Преобразование с потерей знака и переполнением                       |
    // long         | sbyte, byte, short, ushort, int, uint, ulong, char | Преобразование с потерей знака и переполнением          |
    // ulong        | sbyte, byte, short, ushort, int, uint, long, char | Преобразование с потерей знака и переполнением           |
    // float        | sbyte, byte, short, ushort, int, uint, long, ulong, char | Преобразование с потерей точности                 |
    // double       | sbyte, byte, short, ushort, int, uint, long, ulong, char, float | Преобразование с потерей точности          |
    // decimal      | sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double | Преобразование с потерей точности  |
    // char         | sbyte, byte, short | Преобразование с потерей знака и переполнением                                          |
    // ----------------------------------------------------------------------------------------------------------------------------|

    short a = 30000;
    ushort b = (ushort)a; // short -> ushort
    Console.WriteLine($"short {a} | ushort: {b}");

    string str = "Hello";
    object obj = (object)str;
    Console.WriteLine($"string {str} | object: {obj}");
}

//task_2();

static void task_3()
{
    string str = "Ello";
    object obj = str as object;
    if( obj != null)
    {
        Console.WriteLine($"{obj} <--- object");

    }
    else
    {
        Console.WriteLine($"{obj} != object");
    }
    if( obj is string b){
        Console.WriteLine($"{b} <--- string");
    }
    else
    {
        Console.WriteLine($"{obj} != string");
    }

}
//task_3();

static void task_4()
{
    Rub rub = new Rub(90);
    Dollars dollars = rub;
    Console.WriteLine(dollars.Amount);

    Dollars dollars1 = new Dollars(100);
    Rub rub1 = (Rub)dollars1;
    Console.WriteLine(rub1.Amount);

}

task_4();



static void task_5()
{
    {
        string str = "8";
        int x = Convert.ToInt32( str );
        Console.WriteLine(x);
    }
    {
        string str = "1b";
        try
        {
            int x = int.Parse(str);
            Console.WriteLine(x);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    {
        string str = "2";
        int y;
        bool x = int.TryParse(str, out y);
        if (x == true) {
            Console.WriteLine(y);
        }
        else
        {
            Console.WriteLine(false);
        }
    }
}

//task_5();

public class Rub
{
    public decimal Amount;

    public Rub(decimal amount)
    {
        Amount = amount;
    }

    
    public static implicit operator Dollars(Rub rubles)
    {
        return new Dollars(rubles.Amount / 3);
    }
}

public class Dollars
{
    public decimal Amount;
    public Dollars(decimal amount)
    {
        Amount = amount;
    }


    public static explicit operator Rub(Dollars dollars)
    {
        return new Rub(dollars.Amount * 3);
    }
}