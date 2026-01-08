using System;

#region SRP classes

// 1. ПІБ студента
class PersonName
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? Lastname { get; set; }
}

// 2. Адреса
class Address
{
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public int HouseNumber { get; set; }
    public char Korpus { get; set; }
    public int PostalCode { get; set; }
}

// 3. Дата народження
class BirthInfo
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string? ZodiacSign { get; set; }
}

// 4. Навчальна інформація
class EducationInfo
{
    public int StartDay { get; set; }
    public int StartMonth { get; set; }
    public int StartYear { get; set; }
    public int Course { get; set; }
    public string? GroupName { get; set; }
    public string? Specialization { get; set; }
}

// 5. Відвідування
class Attendance
{
    public int LessonsVisited { get; set; }
    public int LessonsLate { get; set; }
}

// 6. Оцінки
class Grades
{
    public int[]? HomeworkRates { get; set; }
    public float HomeworkAverage { get; set; }

    public int[]? PracticeRates { get; set; }
    public float PracticeAverage { get; set; }

    public int[]? ExamRates { get; set; }
    public float ExamAverage { get; set; }

    public int[]? ZachetRates { get; set; }
    public int ZachetCount { get; set; }
    public float ZachetAverage { get; set; }

    public double TotalAverage { get; set; }
}

// 7. Предмет і викладач
class SubjectInfo
{
    public string? SubjectName { get; set; }
    public string? TeacherName { get; set; }
}

#endregion

#region Main Student class

// Головний клас Student
class Student
{
    public PersonName Name { get; set; } = new PersonName();
    public Address Address { get; set; } = new Address();
    public BirthInfo Birth { get; set; } = new BirthInfo();
    public EducationInfo Education { get; set; } = new EducationInfo();
    public Attendance Attendance { get; set; } = new Attendance();
    public Grades Grades { get; set; } = new Grades();
    public SubjectInfo Subject { get; set; } = new SubjectInfo();
}

#endregion

#region Program

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Student student = new Student();

        // ПІБ
        student.Name.FirstName = "Максим";
        student.Name.Surname = "Сергійович";
        student.Name.Lastname = "Мандрика";

        // Адреса
        student.Address.Country = "Україна";
        student.Address.Region = "Харківська область";
        student.Address.City = "Харків";
        student.Address.Street = "Сумська";
        student.Address.HouseNumber = 10;
        student.Address.PostalCode = 61052;

        // Дата народження
        student.Birth.Day = 14;
        student.Birth.Month = 2;
        student.Birth.Year = 1982;
        student.Birth.ZodiacSign = "Водолій";

        // Навчання
        student.Education.Course = 2;
        student.Education.GroupName = "СПР-411";
        student.Education.Specialization = "Комп'ютерні науки";

        // Відвідування
        student.Attendance.LessonsVisited = 120;
        student.Attendance.LessonsLate = 3;

        // Оцінки
        student.Grades.TotalAverage = 9.6;

        // Предмет
        student.Subject.SubjectName = "Програмування";
        student.Subject.TeacherName = "Загоруйко О.Д.";

        // Вивід
        Console.WriteLine("=== Дані студента ===");
        Console.WriteLine($"{student.Name.Lastname} {student.Name.FirstName}");
        Console.WriteLine($"Дата народження: {student.Birth.Day}.{student.Birth.Month}." +
            $"{student.Birth.Year}р.");
        Console.WriteLine($"Місто: {student.Address.City}");
        Console.WriteLine($"Курс: {student.Education.Course}");
        Console.WriteLine($"Середній бал: {student.Grades.TotalAverage}");
        Console.WriteLine($"Предмет: {student.Subject.SubjectName}");
        Console.WriteLine($"Викладач: {student.Subject.TeacherName}");
    }
}

#endregion
