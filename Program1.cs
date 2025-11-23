using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;


class Student
{
    private string name;
    private string middleName;
    private string lastName;
    private DateTime dateOfBirth;
    private string address;
    private string phoneNumber;

    private int[] credits;
    private int[] coursework;
    private int[] exams;

    //властивості
    public string Name
    {
        get => name;
        set => name = value;
    }

    public string MiddleName
    {
        get => middleName;
        set => middleName = value;
    }

    public string LastName
    {
        get => lastName;
        set => lastName = value;
    }

    public DateTime DateOfBirth
    {
        get => dateOfBirth;
        set => dateOfBirth = value;
    }

    // вік обчислюється автоматично
    public int Age => DateTime.Now.Year - dateOfBirth.Year;

    // середній бал 
    public double AverageGrade
    {
        get
        {
            if (exams.Length == 0) return 0;
            double sum = 0;
            foreach (var e in exams) sum += e;
            return sum / exams.Length;
        }
    }

    public string Address
    {
        get => address;
        set => address = value;
    }

    public string PhoneNumber
    {
        get => phoneNumber;
        set => phoneNumber = value;
    }

    public int[] Credits
    {
        get => credits;
        set => credits = value;
    }

    public int[] Coursework
    {
        get => coursework;
        set => coursework = value;
    }

    public int[] Exams
    {
        get => exams;
        set => exams = value;
    }

    // конструктор без параметрів.
    public Student() : this("", "", "", DateTime.MinValue, "", "",
                            new int[0], new int[0], new int[0])
    {
    }


    // конструктор з параметрами.
    public Student(string name, string middleName, string lastName,
                   DateTime dateOfBirth, string address, string phoneNumber,
                   int[] credits, int[] coursework, int[] exams)
    {
        Name = name;
        MiddleName = middleName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Address = address;
        PhoneNumber = phoneNumber;
        Credits = credits;
        Coursework = coursework;
        Exams = exams;
    }

    // конструктор копіювання.
    public Student(Student other)
        : this(
            other.Name,
            other.MiddleName,
            other.LastName,
            other.DateOfBirth,
            other.Address,
            other.PhoneNumber,
            (int[])other.Credits.Clone(),
            (int[])other.Coursework.Clone(),
            (int[])other.Exams.Clone()
          )
    {

    }

    //перевантаження операторів.
    public static bool operator ==(Student s1, Student s2)
    {
        if (ReferenceEquals(s1, s2)) return true;
        if (s1 is null || s2 is null) return false;
        return s1.AverageGrade == s2.AverageGrade;
    }

    public static bool operator !=(Student s1, Student s2) => !(s1 == s2);

    // перевизначення методів Equals і GetHashCode
    public override bool Equals(object obj)
    {
        if (obj is Student other)
            return this == other;
        return false;
    }

    public override int GetHashCode() => AverageGrade.GetHashCode();

    // ToString
    public override string ToString()
    {
        return $"{LastName} {Name} | Бал: {AverageGrade:F1}";
    }
}


class Group
{
    private Student[] students;
    private string groupName;
    private string faculty;
    private int courseNumber;

    public Student[] Students
    {
        get => students;
        set => students = value;
    }

    public string GroupName
    {
        get => groupName;
        set => groupName = value;
    }

    public string Faculty
    {
        get => faculty;
        set => faculty = value;
    }

    public int CourseNumber
    {
        get => courseNumber;
        set => courseNumber = value;
    }

    // властивість для отримання кількості студентів у групі.
    public int Count => students.Length;

    // індексатор для доступу до студентів за індексом.
    public Student this[int index]
    {
        get => students[index];
        set => students[index] = value;
    }

    // конструктор без параметрів.
    public Group() : this(new Student[0], "", "", 0)
    {
    }

    // конструктор з параметрами.
    public Group(Student[] students, string groupName, string faculty, int courseNumber)
    {
        Students = students;
        GroupName = groupName;
        Faculty = faculty;
        CourseNumber = courseNumber;
    }

    // конструктор копіювання.
    public Group(Group other)
        : this(
            CloneStudents(other.Students),
            other.GroupName,
            other.Faculty,
            other.CourseNumber
          )
    {
    }

    // метод для глибокого копіювання масиву студентів.
    private static Student[] CloneStudents(Student[] arr)
    {
        if (arr == null) return new Student[0];
        Student[] result = new Student[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            // якщо елемент null — залишаємо null, інакше створюємо копію
            result[i] = arr[i] == null ? null : new Student(arr[i]);
        }
        return result;
    }

    // перевантаження операторів 
    public static bool operator ==(Group g1, Group g2)
    {
        if (ReferenceEquals(g1, g2)) return true;
        if (g1 is null || g2 is null) return false;
        return g1.Count == g2.Count;
    }

    public static bool operator !=(Group g1, Group g2) => !(g1 == g2);

    public override bool Equals(object obj)
    {
        if (obj is Group other)
            return this == other;
        return false;
    }

    public override int GetHashCode() => Count.GetHashCode();

    // показ всіх студентів у групі.
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Група: {groupName}");
        sb.AppendLine($"Спеціальність: {faculty}");
        sb.AppendLine($"Курс: {courseNumber}");
        sb.AppendLine($"Кількість студентів: {Count}");
        sb.AppendLine("Студенти:");

        for (int i = 0; i < students.Length; i++)
            sb.AppendLine($"{i + 1}. {students[i]}");

        return sb.ToString();
    }

    // додавання студента до групи.
    public void AddStudent(Student student)
    {
        Array.Resize(ref students, students.Length + 1);
        students[students.Length - 1] = student;
    }

    // переведення студента до іншої групи.
    public void TransferStudent(Student student, Group newGroup)
    {
        int index = Array.IndexOf(students, student);
        if (index >= 0)
        {
            newGroup.AddStudent(student);

            for (int i = index; i < students.Length - 1; i++)
                students[i] = students[i + 1];

            Array.Resize(ref students, students.Length - 1);
        }
    }

    // виключення студентів з незадовільними оцінками.
    public void ExpelStudents()
    {
        students = Array.FindAll(students, student =>
        {
            foreach (int exam in student.Exams)
            {
                if (exam < 60)
                    return false;
            }
            return true;
        });
    }


    // виключення студента з найгіршим середнім балом.
    public void ExpelWorstStudent()
    {
        if (students.Length == 0) return;
        int worstIndex = 0;
        double worstAverage = CalculateAverage(students[0]);
        for (int i = 1; i < students.Length; i++)
        {
            double average = CalculateAverage(students[i]);
            if (average < worstAverage)
            {
                worstAverage = average;
                worstIndex = i;
            }
        }

        for (int i = worstIndex; i < students.Length - 1; i++)
            students[i] = students[i + 1];
        Array.Resize(ref students, students.Length - 1);
    }

    // метод для обчислення середнього балу студента.
    private double CalculateAverage(Student s)
    {
        double sum = 0;
        int count = 0;

        foreach (int mark in s.Exams)
        {
            sum += mark;
            count++;
        }

        return count > 0 ? sum / count : 0;
    }
}

class Program
    {
        static void Main()
        {
        Student s1 = new Student("Іван", "Іванович", "Петренко",
           new DateTime(2003, 5, 12), "Київ", "1234567",
           new int[] { 80, 90 }, new int[] { 85, 75 }, new int[] { 90, 95 });

        Student s2 = new Student("Марія", "Ігорівна", "Сидоренко",
            new DateTime(2004, 2, 25), "Львів", "7654321",
            new int[] { 60, 55 }, new int[] { 70, 60 }, new int[] { 50, 40 });

        Group g = new Group(new Student[] { s1, s2 }, "ІП-23", "Інформатика", 2);

        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine(g.ToString());

        g.ExpelStudents(); // відраховує тих, хто не склав
        Console.WriteLine("\nПісля відрахування тих, хто не склав:");
        Console.WriteLine(g.ToString());
    }
}

