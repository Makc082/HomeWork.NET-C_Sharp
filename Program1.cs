using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// Клас Student
public class Student
{
    // властивості
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public double AverageGrade { get; set; }

    // перевантаження операторів 
    public static bool operator ==(Student a, Student b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.AverageGrade == b.AverageGrade;
    }

    public static bool operator !=(Student a, Student b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj is Student other)
            return this == other;
        return false;
    }

    public override int GetHashCode() => AverageGrade.GetHashCode();

    // компаратори
    public class AverageGradeComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Student is null");

            int gradeCompare = x.AverageGrade.CompareTo(y.AverageGrade);
            if (gradeCompare != 0)
                return gradeCompare;

            return string.Compare(
                x.Lastname + x.Name,
                y.Lastname + y.Name,
                StringComparison.OrdinalIgnoreCase
            );
        }
    }

    public class FullNameComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Student is null");

            int nameCompare = string.Compare(
                x.Lastname + x.Name,
                y.Lastname + y.Name,
                StringComparison.OrdinalIgnoreCase
            );

            if (nameCompare != 0)
                return nameCompare;

            return y.AverageGrade.CompareTo(x.AverageGrade);
        }
    }

    // події
    public event Action<Student> LectureMissed;
    public event Action<Student> AutomatReceived;
    public event Action<Student> ScholarshipAwarded;

    // Методи для виклику подій
    public void CheckTime()
    {
        TimeSpan now = DateTime.Now.TimeOfDay;
        TimeSpan lectureStart = new TimeSpan(16, 45, 0);

        if (now > lectureStart)
            LectureMissed?.Invoke(this);
    }

    public void CheckAutomat(int grade)
    {
        if (grade == 100)
            AutomatReceived?.Invoke(this);
    }

    public void CheckScholarship()
    {
        if (AverageGrade >= 10)
            ScholarshipAwarded?.Invoke(this);
    }
}

// Клас Group
public class Group : IEnumerable<Student>
{
    private List<Student> students = new List<Student>();

    public int Count => students.Count;
    public string Specialization { get; set; }
    public int Course { get; set; }

    public static bool operator ==(Group a, Group b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Count == b.Count;
    }

    public static bool operator !=(Group a, Group b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj is Group other)
            return this == other;
        return false;
    }

    public override int GetHashCode() => Count.GetHashCode();

    // індексатор
    public Student this[int index]
    {
        get => students[index];
        set => students[index] = value;
    }

    public void Add(Student student)
    {
        students.Add(student);
    }

    public IEnumerator<Student> GetEnumerator()
    {
        return new GroupEnumerator(students);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class GroupEnumerator : IEnumerator<Student>
    {
        private List<Student> _students;
        private int index = -1;

        public GroupEnumerator(List<Student> students)
        {
            _students = students;
        }

        public Student Current => _students[index];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            index++;
            return index < _students.Count;
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose() { }
    }

    // події групи
    public event Action<Group> GroupPartyPlanned;
    public event Action<Group> SessionSurvived;

    public void CheckSessionResults(List<int> grades)
    {
        bool allExcellent = true;

        foreach (int g in grades)
        {
            if (g < 90)
            {
                allExcellent = false;
                break;
            }
        }

        if (allExcellent)
        {
            GroupPartyPlanned?.Invoke(this);
            SessionSurvived?.Invoke(this);
        }
    }
}

// Головна програма
class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Group group = new Group()
        {
            Specialization = "Комп'ютерні науки",
            Course = 2
        };

        // студенти
        var s1 = new Student { Name = "Іван", Lastname = "Петренко", Age = 19, AverageGrade = 87.5 };
        var s2 = new Student { Name = "Олег", Lastname = "Коваль", Age = 18, AverageGrade = 91.2 };
        var s3 = new Student { Name = "Андрій", Lastname = "Бондар", Age = 20, AverageGrade = 91.2 };
        var s4 = new Student { Name = "Марія", Lastname = "Антонюк", Age = 18, AverageGrade = 75.4 };

        group.Add(s1);
        group.Add(s2);
        group.Add(s3);
        group.Add(s4);

        // підписка на події Student
        foreach (var st in group)
        {
            st.LectureMissed += s =>
                Console.WriteLine($"{s.Name} {s.Lastname}: Ти запізнився! Швидко вмикай онлайн-трансляцію!");

            st.AutomatReceived += s =>
                Console.WriteLine($"{s.Name} {s.Lastname}: Вітаю з автоматом! Пора святкувати !!!");

            st.ScholarshipAwarded += s =>
                Console.WriteLine($"{s.Name} {s.Lastname}: Вітаємо! Ви отримуєте стипендію!");
        }

        // підписка на події Group
        group.GroupPartyPlanned += g =>
            Console.WriteLine($"Група {g.Specialization}, курс {g.Course}: Піца та пиво на всіх!");

        group.SessionSurvived += g =>
            Console.WriteLine($"Група {g.Specialization}, курс {g.Course}: Ура, сесія позаду! Відпочинок у парку!");


        // виклик подій Student
        Console.WriteLine("\n*** Перевірка подій Student ***");
        s1.CheckTime();
        s1.CheckAutomat(100);
        s1.CheckScholarship();


        // виклик подій Group
        Console.WriteLine("\n*** Перевірка подій Group ***");
        group.CheckSessionResults(new List<int> { 95, 100, 98 });

        // сортування 
        Console.WriteLine("\n*** Студенти (оригінальний порядок) ***");
        foreach (var st in group)
            Console.WriteLine($"{st.Lastname} {st.Name}: {st.AverageGrade}");

        group = SortGroup(group, new Student.AverageGradeComparer());

        Console.WriteLine("\n*** Сортування за середнім балом ***");
        foreach (var st in group)
            Console.WriteLine($"{st.Lastname} {st.Name}: {st.AverageGrade}");

        group = SortGroup(group, new Student.FullNameComparer());

        Console.WriteLine("\n*** Сортування за ПІБ ***");
        foreach (var st in group)
            Console.WriteLine($"{st.Lastname} {st.Name}: {st.AverageGrade}");
    }

    // метод сортування
    static Group SortGroup(Group group, IComparer<Student> comparer)
    {
        var list = new List<Student>();

        foreach (var s in group)
            list.Add(s);

        list.Sort(comparer);

        Group newGroup = new Group()
        {
            Specialization = group.Specialization,
            Course = group.Course
        };

        foreach (var s in list)
            newGroup.Add(s);

        return newGroup;
    }
}
