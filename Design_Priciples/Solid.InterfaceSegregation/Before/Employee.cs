namespace Design_Principles.Solid.InterfaceSegregation.Before;

abstract class Employee
{
    public string EmployeeNo { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    protected abstract decimal CalculateNetSalary();
    public abstract string PrintSalarySlip();
}

