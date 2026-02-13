namespace WebAPIPractice.Controllers;

public class CreateEmployeeDto  
{
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}