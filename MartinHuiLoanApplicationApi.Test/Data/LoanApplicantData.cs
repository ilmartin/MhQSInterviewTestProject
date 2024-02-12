using MartinHuiLoanApplicationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartinHuiLoanApplicationApi.Test.Data
{
    internal static class LoanApplicantData
    {
        internal static LoanApplicant Applicant33333AnnualSalary = new LoanApplicant()
        {
            FirstName = "abc",
            LastName = "kk",
            Address = $"101 street",
            City = "Leicester",
            Country = "gb",
            PostalCode = "LE1 1DD",
            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 1),
            AnnualIncome = 33333
        };
    }
}
