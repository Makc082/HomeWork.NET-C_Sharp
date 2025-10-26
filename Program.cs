//середнє арефметичне трьох чисел
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            int a = 15;
            int b = 20;
            int c = 25;
            double average = (a + b + c) / 3.0;
            Console.WriteLine("Середнє арефметичне трьох чисел: " + average);
        }
    }
}

// корінь лінійного рівняння.
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            double a = 5;
            double b = 10;
            if (a != 0)
            {
                double x = -b / a;
                Console.WriteLine("Корінь лінійного рівняння: " + x);
            }
            else
            {
                Console.WriteLine("Рівняння не має розв'язку, оскільки a дорівнює нулю.");
            }
        }
    }
}

// ступінь. 
using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть число: ");
            double number = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть ступінь: ");
            double exponent = Convert.ToDouble(Console.ReadLine());

            double result = Math.Pow(number, exponent);

            Console.WriteLine($"{number} у ступені {exponent} дорівнює {result}");
        }
    }
}

//площа і довжина кола.
using System;   
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            const double Pi = 3.14159;

            Console.Write("Введіть радіус кола: ");
            double radius = Convert.ToDouble(Console.ReadLine());
            double area = Pi * Math.Pow(radius, 2);
            double circumference = 2 * Pi * radius;

            Console.WriteLine($"Площа кола: {area}");
            Console.WriteLine($"Довжина кола: {circumference}");

        }
    }
}

// гривені в долари і євро.
using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            const double UsdToUahRate = 41.70; // курс долара до гривні
            const double EurToUahRate = 49.70; // курс євро до гривні

            Console.Write("Введіть кількість гривень: ");
            double uahAmount = Convert.ToDouble(Console.ReadLine());

            double usdAmount = uahAmount / UsdToUahRate;
            double eurAmount = uahAmount / EurToUahRate;

            Console.WriteLine($"{uahAmount} гривень дорівнює {usdAmount:F2} доларів.");
            Console.WriteLine($"{uahAmount} гривень дорівнює {eurAmount:F2} євро.");
        }
    }
}

//кілометри в сухопутні і морські милі.
using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            const double KmToLandMilesRate = 0.621371; // кількість кілометрів до сухопутних миль
            const double KmToNauticalMilesRate = 0.539957; // кількість кілометрів до морських миль

            Console.Write("Введіть кількість кілометрів: ");
            double kmAmount = Convert.ToDouble(Console.ReadLine());

            double landMilesAmount = kmAmount * KmToLandMilesRate;
            double nauticalMilesAmount = kmAmount * KmToNauticalMilesRate;

            Console.WriteLine($"{kmAmount} кілометрів дорівнює {landMilesAmount:F2} сухопутних миль.");
            Console.WriteLine($"{kmAmount} кілометрів дорівнює {nauticalMilesAmount:F2} морських миль.");
        }
    }
}

// відсоток від числа.
using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть число N: ");
            double n = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть відсоток P: ");
            double p = Convert.ToDouble(Console.ReadLine());

            double result = (p / 100) * n;
            Console.WriteLine($"{p}% від числа {n} дорівнює {result}");
        }
    }
}

//цельсії в фаренгейти і навпаки.
using System;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть температуру в Цельсіях: ");
            double celsius = Convert.ToDouble(Console.ReadLine());
            double fahrenheitFromCelsius = (celsius * 9 / 5) + 32;

            Console.WriteLine($"{celsius}°C дорівнює {fahrenheitFromCelsius}°F");

            Console.Write("Введіть температуру в Фаренгейтах: ");
            double fahrenheit = Convert.ToDouble(Console.ReadLine());
            double celsiusFromFahrenheit = (fahrenheit - 32) * 5 / 9;

            Console.WriteLine($"{fahrenheit}°F дорівнює {celsiusFromFahrenheit}°C");
        }
    }
}

