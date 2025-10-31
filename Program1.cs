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

    // конструктор без параметрів.
    public Student()
    {
        name = "";
        middleName = "";
        lastName = "";
        dateOfBirth = DateTime.MinValue;
        address = "";
        phoneNumber = "";
        credits = new int[0];
        coursework = new int[0];
        exams = new int[0];
    }

    // конструктор з параметрами.
    public Student(string name, string middleName, string lastName, DateTime dateOfBirth, string address, string phoneNumber, int[] credits, int[] coursework, int[] exams)
    {
        this.name = name;
        this.middleName = middleName;
        this.lastName = lastName;
        this.dateOfBirth = dateOfBirth;
        this.address = address;
        this.phoneNumber = phoneNumber;
        this.credits = credits;
        this.coursework = coursework;
        this.exams = exams;
    }

    // конструктор копіювання.
    public Student(Student other)
    {
        name = other.name;
        middleName = other.middleName;
        lastName = other.lastName;
        dateOfBirth = other.dateOfBirth;
        address = other.address;
        phoneNumber = other.phoneNumber;
        credits = (int[])other.credits.Clone();
        coursework = (int[])other.coursework.Clone();
        exams = (int[])other.exams.Clone();
    }

    // getter-и та setter-и для всіх полів.
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string MiddleName
    {
        get { return middleName; }
        set { middleName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set { dateOfBirth = value; }
    }

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public string PhoneNumber
    {
        get { return phoneNumber; }
        set { phoneNumber = value; }
    }

    public int[] Credits
    {
        get { return credits; }
        set { credits = value; }
    }

    public int[] Coursework
    {
        get { return coursework; }
        set { coursework = value; }
    }

    public int[] Exams
    {
        get { return exams; }
        set { exams = value; }
    }

    //показ даних студента.
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {name} {middleName} {lastName}");
        sb.AppendLine($"Date of Birth: {dateOfBirth.ToShortDateString()}");
        sb.AppendLine($"Address: {address}");
        sb.AppendLine($"Phone Number: {phoneNumber}");
        sb.AppendLine($"Credits: {string.Join(", ", credits)}");
        sb.AppendLine($"Coursework: {string.Join(", ", coursework)}");
        sb.AppendLine($"Exams: {string.Join(", ", exams)}");
        return sb.ToString();
    }
}


class Group
{
    private Student[] students;
    private string groupName;
    private string faculty;
    private int courseNumber;

    // конструктор без параметрів.
    public Group()
    {
        students = new Student[0];
        groupName = "";
        faculty = "";
        courseNumber = 0;
    }

    // конструктор з параметрами.
    public Group(Student[] students, string groupName, string faculty, int courseNumber)
    {
        this.students = students;
        this.groupName = groupName;
        this.faculty = faculty;
        this.courseNumber = courseNumber;
    }

    // конструктор копіювання.
    public Group(Group other)
    {
        students = new Student[other.students.Length];
        for (int i = 0; i < other.students.Length; i++)
        {
            students[i] = new Student(other.students[i]);
        }

        groupName = other.groupName;
        faculty = other.faculty;
        courseNumber = other.courseNumber;
    }

    // getter-и та setter-и для всіх полів.
    public Student[] Students
    {
        get { return students; }
        set { students = value; }
    }

    public string GroupName
    {
        get { return groupName; }
        set { groupName = value; }
    }

    public string Faculty
    {
        get { return faculty; }
        set { faculty = value; }
    }

    public int CourseNumber
    {
        get { return courseNumber; }
        set { courseNumber = value; }
    }

    // показ всіх студентів у групі.
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Group Name: {groupName}");
        sb.AppendLine($"Faculty: {faculty}");
        sb.AppendLine($"Course Number: {courseNumber}");
        sb.AppendLine("Students:");

        if (students == null || students.Length == 0)
        {
            sb.AppendLine("Немає студентів.");
            return sb.ToString();
        }

        Array.Sort(students, (s1, s2) => string.Compare(s1.LastName, s2.LastName));

        for (int i = 0; i < students.Length; i++)
            sb.AppendLine($"{i + 1}. {students[i].LastName} {students[i].Name}");

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

