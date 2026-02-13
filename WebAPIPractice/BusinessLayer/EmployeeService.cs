namespace BusinessLayer;

public class EmployeeService : IEmployee
{
    private List<Employee> _employees;
    public EmployeeService()
    {
        _employees = new()
        {
            new Employee(){Id = 1, Name = "Suman",Department = "HR",Salary = 60000},
            new Employee(){Id = 2,Name = "Satyam", Department = "FN", Salary = 70000},
            new Employee(){Id  = 3, Name = "Nutan", Department = "FN", Salary = 4500000},
            new Employee(){Id = 4, Name = "ASDFs", Department = "HR", Salary = 214555},
            new Employee(){Id = 5, Name = "sdfsd", Department = "ECE", Salary = 45787},
        };
    }
    public Employee GetEmployeeById(int id)
    {
        var employee = _employees.Find(x => x.Id == id);
        return employee ?? new Employee();
    }

    public List<Employee> GetAllEmployees()
    {
        return _employees;
    }

    public void AddEmployee(Employee employee)
    {
        employee.Id = _employees.Count + 1; // Simple ID assignment, in real scenarios consider using a more robust method
        _employees.Add(employee);
    }

    public bool RemoveEmployee(Employee employee)
    {
        var result = _employees.Find(x => x.Id == employee.Id);
        if (result != null)
          return  _employees.Remove(employee);

        return false;
    }
}