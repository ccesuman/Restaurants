using System.Runtime.CompilerServices;

namespace Encapsulation;

public static class InterViewPreprationQuestions
{
    public static void FindSecHiestSalary()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Alice", Salary = 50000, Age = 30, Company = "Company A", Department = "HR"},
            new Employee { Name = "Bob", Salary = 60000, Age = 35, Company = "Company A", Department = "Finance"},
            new Employee { Name = "Charlie", Salary = 55000, Age = 28, Company = "Company C", Department = "HR"},
            new Employee { Name = "David", Salary = 70000, Age = 40, Company = "Company D" , Department = "Finance"},
            new Employee { Name = "Eve", Salary = 65000, Age = 32, Company = "Company E", Department = "HR"},
             new Employee { Name = "Frank", Salary = 70000, Age = 45, Company = "Company F", Department = "Finance"}
             
        };

        var secHiestSalary = employees
            .Select(s=>s.Salary)
            .Distinct()
            .OrderByDescending(s => s)
            .Skip(1)
            .FirstOrDefault();

        var result = employees
            .Select(e =>e.Salary).Distinct()
            .OrderBy(emp => emp).Reverse().Skip(1).FirstOrDefault();
            

        //Console.WriteLine("using my desc: "+secHiestSalary);
        //Console.WriteLine("using asc: "+result);


        // Given a list of employees, group them by department and find the highest salary in each department.

        var result1 = employees.GroupBy(e => e.Department)
            .Select(g =>
            {
                var maxSalary = g.Max(e => e.Salary);
                return new
                {
                    Department = g.Key,
                    MaxSalary = g.Max(e => e.Salary),
                    EmployeeName = g.Where(e => e.Salary == maxSalary)
                };
            });

        

        foreach (var item in result1)
        {
            Console.WriteLine($"{item.Department} : {item.MaxSalary} : {item.EmployeeName}");
        }

    }

    public static void FrequecyOfCharacter()
    {
        string str = "Aarti";

        var charArray = str.ToLower().ToCharArray();

        var result = charArray.GroupBy(g => g)
                .Where(g => g.Count() > 1)
            .Select(x => new { Character = x.Key, Frequency = x.Count() });

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Character} : {item.Frequency}");
        }
    }
}