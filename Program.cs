using System;
using System.Collections.Generic;


// Інтерфейси
public interface ISpell
{
    void Cast();
    int GetPower();
}

// Додатковий інтерфейс для темної магії
public interface IDarkMagic
{
    void DarkEffect();
}

// Generic spellbook
public class Spellbook<T>
    where T : class, ISpell, IComparable<T>, new()
{
    private List<T> spells = new List<T>();

    // Вивчити нове закляття
    public void LearnSpell(T spell)
    {
        foreach (var s in spells)
        {
            if (s.CompareTo(spell) == 0)
            {
                Console.WriteLine(" Закляття з такою силою вже вивчене!");
                return;
            }
        }

        spells.Add(spell);
        Console.WriteLine($" Вивчено нове закляття (сила: {spell.GetPower()})");
    }

    // Сортування за силою
    public void SortSpells()
    {
        spells.Sort();
    }

    // Кастування найсильнішого
    public void CastStrongest()
    {
        if (spells.Count == 0)
        {
            Console.WriteLine("Книга порожня!");
            return;
        }

        SortSpells();
        var strongest = spells[^1];

        Console.WriteLine("\n Кастуємо найсильніше закляття:");
        strongest.Cast();
    }

    // Темний ритуал
    public void InvokeRitual()
    {
        foreach (var spell in spells)
        {
            if (spell is not IDarkMagic)
                throw new InvalidOperationException(
                    " Ритуал неможливий! Не всі закляття є темною магією.");
        }

        Console.WriteLine("\n Темний ритуал розпочато...");
        foreach (var spell in spells)
        {
            ((IDarkMagic)spell).DarkEffect();
        }
    }
}


// Закляття

// Вогняна куля
public class Fireball : ISpell, IComparable<Fireball>
{
    public int Power { get; set; } = 70;

    public void Cast()
    {
        Console.WriteLine(" Fireball: Вибух вогню!");
    }

    public int GetPower() => Power;

    public int CompareTo(Fireball other)
    {
        return Power.CompareTo(other.Power);
    }
}

// Відновлення здоров'я
public class HealingWave : ISpell, IComparable<HealingWave>
{
    public int Power { get; set; } = 40;

    public void Cast()
    {
        Console.WriteLine(" Healing Wave: Відновлення здоровʼя!");
    }

    public int GetPower() => Power;

    public int CompareTo(HealingWave other)
    {
        return Power.CompareTo(other.Power);
    }
}

// Темне закляття
public class DarkSpell : ISpell, IDarkMagic, IComparable<DarkSpell>
{
    public int Power { get; set; }

    public DarkSpell()
    {
        Power = new Random().Next(80, 120);
    }

    public void Cast()
    {
        Console.WriteLine($" Темне закляття кастується (сила {Power})");
    }

    public void DarkEffect()
    {
        Console.WriteLine(" Темна енергія поглинає все навколо...");
    }

    public int GetPower() => Power;

    public int CompareTo(DarkSpell other)
    {
        return Power.CompareTo(other.Power);
    }
}


// Main
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("===  Звичайна книга заклять ===");

        var fireBook = new Spellbook<Fireball>();
        fireBook.LearnSpell(new Fireball { Power = 60 });
        fireBook.LearnSpell(new Fireball { Power = 80 });
        fireBook.LearnSpell(new Fireball { Power = 80 }); // дубль
        fireBook.LearnSpell(new Fireball { Power = 50 });
        fireBook.LearnSpell(new Fireball { Power = 90 });

        fireBook.CastStrongest();

        Console.WriteLine("\n===  Книга темної магії ===");

        var darkBook = new Spellbook<DarkSpell>();
        darkBook.LearnSpell(new DarkSpell());
        darkBook.LearnSpell(new DarkSpell());
        darkBook.LearnSpell(new DarkSpell());

        darkBook.CastStrongest();

        // Темний ритуал
        try
        {
            darkBook.InvokeRitual();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
