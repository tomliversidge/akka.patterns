namespace ProcessManager.Messages
{
    public class BankLoanRateQuoted
    {
        public string BankId { get; }
        public string BankLoanRateQuoteId { get; }
        public string LoanQuoteReferenceId { get; }
        public string TaxId { get; }
        public double InterestRate { get; }

        public BankLoanRateQuoted(string bankId, string bankLoanRateQuoteId, string loanQuoteReferenceId, string taxId, double interestRate)
        {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            LoanQuoteReferenceId = loanQuoteReferenceId;
            TaxId = taxId;
            InterestRate = interestRate;
        }

        public override string ToString()
        {
            return $"BankId: {BankId}, BankLoanRateQuoteId: {BankLoanRateQuoteId}, LoanQuoteReferenceId: {LoanQuoteReferenceId}, TaxId: {TaxId}, InterestRate: {InterestRate}";
        }
    }
}