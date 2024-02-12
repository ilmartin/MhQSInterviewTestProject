using MartinHuiLoanApplicationApi.Model;

namespace MartinHuiLoanApplicationApi.Test.Data
{
    public class LoanProductData
    {
        public static List<LoanProduct> RandomLoanProducts = new List<LoanProduct>
        {
            Product31100MinSalary,Product36900MinSalary
        };
        public static List<LoanProduct> Above36900LoanProducts = new List<LoanProduct>
        {
            Product40000MinSalary,Product36900MinSalary
        };

        internal static LoanProduct Product30000MinSalary = new LoanProduct { ProductName = "Standard Credit Card", Issuer = "Bank A", InterestRate = 18.99M, AnnualFee = 0, MinimumAnnualSalary = 30000, MinimumCreditScore = 750, RewardsProgram = true, IntroductoryAPR = 0, IntroductoryPeriod = 0, CreditLimit = 5000, GracePeriod = 25, LatePaymentFee = 25, ForeignTransactionFee = 3, BalanceTransferFee = 3, CashAdvanceFee = 5, MinimumPaymentPercentage = 3, TermsAndConditions = "Standard terms and conditions apply." };
        internal static LoanProduct Product36900MinSalary = new LoanProduct { ProductName = "Gold Credit Card", Issuer = "Bank A", InterestRate = 16.99M, AnnualFee = 100, MinimumAnnualSalary = 36900, MinimumCreditScore = 700, RewardsProgram = true, IntroductoryAPR = 0, IntroductoryPeriod = 0, CreditLimit = 10000, GracePeriod = 30, LatePaymentFee = 35, ForeignTransactionFee = 2.5M, BalanceTransferFee = 2.5M, CashAdvanceFee = 4, MinimumPaymentPercentage = 4, TermsAndConditions = "Gold card benefits apply." };
        internal static LoanProduct Product40000MinSalary = new LoanProduct { ProductName = "Cashback Credit Card", Issuer = "Bank D", InterestRate = 18.99M, AnnualFee = 0, MinimumAnnualSalary = 40000, MinimumCreditScore = 750, RewardsProgram = true, IntroductoryAPR = 0, IntroductoryPeriod = 0, CreditLimit = 5000, GracePeriod = 25, LatePaymentFee = 25, ForeignTransactionFee = 3, BalanceTransferFee = 3, CashAdvanceFee = 5, MinimumPaymentPercentage = 3, TermsAndConditions = "Standard terms and conditions apply." };
        internal static LoanProduct Product31100MinSalary = new LoanProduct {  ProductName = "Standard Credit Card", Issuer = "Bank A", InterestRate = 18.99M, AnnualFee = 0, MinimumAnnualSalary = 31100, MinimumCreditScore = 650, RewardsProgram = true, IntroductoryAPR = 0, IntroductoryPeriod = 0, CreditLimit = 5000, GracePeriod = 25, LatePaymentFee = 25, ForeignTransactionFee = 3, BalanceTransferFee = 3, CashAdvanceFee = 5, MinimumPaymentPercentage = 3, TermsAndConditions = "Standard terms and conditions apply." };
}
}
