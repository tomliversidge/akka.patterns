namespace ProcessManager.Messages
{
    public class BestLoanRateQuoted
    {
        public string BankId { get; }
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }
        public int CreditScore { get; }
        public double InterestRate { get; }

        public BestLoanRateQuoted(string bankId, string loanRateQuoteId, string taxId, int amount, int termInMonths, int creditScore, double interestRate)
        {
            BankId = bankId;
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
            CreditScore = creditScore;
            InterestRate = interestRate;
        }

        public override string ToString()
        {
            return $"BankId: {BankId}, LoanRateQuoteId: {LoanRateQuoteId}, TaxId: {TaxId}, Amount: {Amount}, TermInMonths: {TermInMonths}, CreditScore: {CreditScore}, InterestRate: {InterestRate}";
        }
    }
}