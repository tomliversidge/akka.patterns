namespace ProcessManager.Messages
{
    public class LoanRateQuoteRecorded
    {
        public string LoanRateQuoteId { get; set; }
        public string TaxId { get; set; }
        public BankLoanRateQuote BankLoanRateQuote { get; }

        public LoanRateQuoteRecorded(string loanRateQuoteId, string taxId, BankLoanRateQuote bankLoanRateQuote)
        {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            BankLoanRateQuote = bankLoanRateQuote;
        }

        public override string ToString()
        {
            return $"LoanRateQuoteId: {LoanRateQuoteId}, TaxId: {TaxId}, BankLoanRateQuote: {BankLoanRateQuote}";
        }
    }
}