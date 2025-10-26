using Shared;

namespace Linq;

public class DataConversion()
{
    private static string[] fruits = { "manga", "apple", "banana", "apple", "orange", "strawberry" };

    public static void AsEnumerable()
    {
        Utils.printTitle(title: "Data Conversion ( AsEnumerable )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<string> fruitsAsEnumerable = fruits.AsEnumerable();

        fruitsAsEnumerable.Print("Fruits as Enumerable");
    }

    public static void AsQueryable()
    {
        Utils.printTitle(title: "Data Conversion ( AsQueryable )", color: ConsoleColor.Blue, width: 70);

        IQueryable<string> fruitsAsQueryable = fruits.AsQueryable().Where(x => x.Length > 5);

        fruitsAsQueryable.Print("Fruits as Queryable");

        Console.WriteLine($"\nExpression: {fruitsAsQueryable.Expression}");
    }

    private static Person[] persons =
    {
        new Student(){id =0, age=20, name="student01", seat_number="2732739291"},
        new Employee() {id =1, age=25, name="Employee01", salary=2500},
        new Student(){id =2, age=21, name="student02", seat_number="6476282811"},
        new Employee() {id =3, age=25, name="Employee02", salary=5000},
        new Student(){id =4, age=22, name="student03", seat_number="3777271119"},
        new Employee() {id =5, age=25, name="Employee03", salary=3000},
    };

    public static void Cast()
    {
        Utils.printTitle(title: "Data Conversion ( Cast )", color: ConsoleColor.Blue, width: 70);

        // IEnumerable<Student> students = persons.Cast<Student>(); // InvalidCastException: Unable to cast object of type 'Employee' to type 'Student'.
        
        IEnumerable<Student> students = persons.Where(x => x.GetType() == typeof(Student))
                                                .Cast<Student>();

        students.Print("Students");
    }

    public static void OfType()
    {
        // same as Cast + Where

        Utils.printTitle(title: "Data Conversion ( OfType )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<Employee> employees = persons.OfType<Employee>();

        employees.Print("Employees");
    }

    public static void ToArray()
    {
        Utils.printTitle(title: "Data Conversion ( ToArray )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<Person> personsArray = persons.ToArray();

        personsArray.Print("Persons as Array");
    }

    public static void ToList()
    {
        Utils.printTitle(title: "Data Conversion ( ToList )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<Person> personsList = persons.ToList();

        personsList.Print("Persons as List");
    }

    public static void ToDictionary()
    {
        Utils.printTitle(title: "Data Conversion ( ToDictionary )", color: ConsoleColor.Blue, width: 70);

        Dictionary<int, Person> personsDict01 = persons.ToDictionary(p => p.id);
        personsDict01.Print("Persons as Dictionary (id, Person)");

        Dictionary<int, string> personsDict02 = persons.ToDictionary(p => p.id, p => p.name);
        personsDict02.Print("Persons as Dictionary (id, Name)");
    }
}