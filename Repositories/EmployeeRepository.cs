using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recruiter.Data;
using Recruiter.Models;

namespace Recruiter.Repositories
{
    internal class EmployeeRepository
    {
        public RecruitmentContext Context { get; set; }

        public EmployeeRepository(RecruitmentContext context)
        {
            this.Context = context;
        }

        public Employee FindByCode(string employeeCode)
        {
            var employee = this.Context.Employees
                .Where(e => e.CEmployeeCode.Equals(employeeCode))
                .FirstOrDefault();
                if (employee != null)
                {
                    return employee;
                }

            throw new Exception($"Employee with Employee code {employeeCode} doesn't exist");
        }

        public Employee EmployeePosition(Employee employee)
        {
            this.FindByCode(employee.CEmployeeCode);
            string code = employee.CCurrentPosition.ToString();
            var position = this.Context.Positions.Where(e => e.CPositionCode.Equals(code)).FirstOrDefault();
            if(position != null)
            {
                employee.CCurrentPosition = position.VDescription;
                return employee;
            }
            throw new Exception($"Employee with Current Position code {code} does'nt exist");
        }

        public void EmployeeSkills(string employeecode)
        {
            var skills = this.Context.EmployeeSkills
                .Join(Context.Skills,
                es => es.CSkillCode,
                s => s.CSkillCode,
                (es, s) => new
                {
                    EmployeeCode = es.CEmployeeCode,
                    SkillCode = es.CSkillCode,
                    Skill = s.VSkill

                })
                .Where(e => e.EmployeeCode.Equals(employeecode))
                .ToList();

            Console.WriteLine("***Skills***");
            foreach(var skill in skills)
            {
                Console.WriteLine(skill.Skill);
            }
        }

        public void Annualfees(string employeeCode)
        {
            var annualS = this.Context.AnnualSalaries
                .Where(e => e.CEmployeeCode.Equals(employeeCode))
                .ToList();


            Console.WriteLine("**Annual Salary Received**");
            foreach (var annual in annualS)
            {
                Console.WriteLine($"Annual Salary: {annual.MAnnualSalary}, Year: {annual.SiYear}");
            }
        }

        public void MonthlySalary(string employeeCode)
        {
            var monthlySalaries = this.Context.MonthlySalaries
                .Where(e => e.CEmployeeCode.Equals(employeeCode))
                .ToList();


            Console.WriteLine("**Monthly Salary Received**");
            foreach (var monthlySalary in monthlySalaries)
            {
                Console.WriteLine($"Monthly Salary: {monthlySalary.MMonthlySalary}, Pay Date: {monthlySalary.DPayDate}, Referral Bonus: {monthlySalary.MReferralBonus}");
            }
        }

    }
}
