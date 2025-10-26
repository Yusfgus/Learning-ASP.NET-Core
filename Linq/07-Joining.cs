using Shared;

namespace Linq;

public class Joining
{
    public static List<(string name, int deptID)> employees
                = new() {new ("Emp1", 2), new ("Emp2", 1), new ("Emp3", 2), new ("Emp4", 3), new ("Emp5", 1),};

    public static List<(int id, string name)> dempartments
                = new() { new(1, "IT"), new(2, "HR"), new(3, "Accounting"), new(3, "Finance"), };

    public static void Join()
    {
        Utils.printTitle(title: "Joining ( Join )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<(string, string)> result01 = employees.Join(                     // outer join
                                                dempartments,                        // inner join
                                                e => e.deptID,                       // outer selector
                                                d => d.id,                           // inner selector
                                                (emp, dept) => (emp.name, dept.name) // result
                                            );

        result01.Print("Employee with his Department (Method Syntax)");

        IEnumerable<(string, string)> result02 = from emp in employees
                                                 join dept in dempartments
                                                 on emp.deptID equals dept.id
                                                 select (emp.name, dept.name);

        result02.Print("Employee with his Department (Query Syntax)");
    }

    public static void GroupJoin()
    {
        Utils.printTitle(title: "Joining ( GroupJoin )", color: ConsoleColor.Blue, width: 70);

        IEnumerable<(string, List<string>)> result01 = dempartments.GroupJoin(     // outer join
                                                    employees,                     // inner join
                                                    d => d.id,                     // outer selector
                                                    e => e.deptID,                 // inner selector
                                                    (dept, emps) => (dept.name, emps.Select(e => e.name).ToList()) // result
                                                );

        Utils.printTitle("Department with its Employees (Method Syntax)");
        Console.WriteLine("{");
        foreach((string dept, List<string> emps) in result01)
        {
            Console.Write($"\t{dept}: ");
            emps.Print();
        }
        Console.WriteLine("}");


        IEnumerable<(string, List<string>)> result02 = from dept in dempartments
                                                       join emp in employees
                                                       on dept.id equals emp.deptID 
                                                       into emps
                                                       select (dept.name, emps.Select(e => e.name).ToList());

        Utils.printTitle("Department with its Employees (Query Syntax)");
        Console.WriteLine("{");
        foreach((string dept, List<string> emps) in result02)
        {
            Console.Write($"\t{dept}: ");
            emps.Print();
        }
        Console.WriteLine("}");
    }
}