using Recruiter.Data;
using Recruiter.Models;
using Recruiter.Repositories;
using System;

namespace Recruiter
{
    class Program
    {

        static void Main(string[] args)
        {
            ConfigurationHelper configurationHelper = ConfigurationHelper.Instance();
            var dbConnectionString = configurationHelper.GetProperty<string>("DbConnectionString");

            using (RecruitmentContext context = new RecruitmentContext(dbConnectionString))
            {
                EmployeeRepository employeeRepository = new EmployeeRepository(context);
                Console.Write("Enter Employee Code:");
                string code = Console.ReadLine();
                Employee existingEmployee = new Employee();
                try
                {
                    existingEmployee = employeeRepository.FindByCode(code);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    System.Environment.Exit(1);
                }
                Employee position = employeeRepository.EmployeePosition(existingEmployee);
                Console.WriteLine($"Employee Code: {code}");
                Console.WriteLine($"First Name: {existingEmployee.VFirstName}");
                Console.WriteLine($"Last Name: {existingEmployee.VLastName}");
                Console.WriteLine($"Birth date: {existingEmployee.DBirthDate}");
                Console.WriteLine($"Current Position: {existingEmployee.CCurrentPosition}");
                employeeRepository.EmployeeSkills(code);
                employeeRepository.Annualfees(code);
                employeeRepository.MonthlySalary(code);
            }

        }
    }
}
