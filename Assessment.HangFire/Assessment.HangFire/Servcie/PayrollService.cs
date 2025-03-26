using System;
using System.Threading.Tasks;

public class PayrollService
{
    public async Task SendPayrollReportsAsync()
    {
        try
        {
            Console.WriteLine("Generating Payroll Reports...");
            await Task.Delay(5000);

            Console.WriteLine("Sending Payroll Reports via Email...");
            await Task.Delay(3000);

            Console.WriteLine("Payroll Reports Sent Successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Sending Payroll Reports: {ex.Message}");
            throw; 
        }
    }
}
