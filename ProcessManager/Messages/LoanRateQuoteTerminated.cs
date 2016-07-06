namespace ProcessManager.Messages
{
    public class LoanRateQuoteTerminated
    {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }

        public LoanRateQuoteTerminated(string loanRateQuoteId, string taxId)
        {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
        }

        public override string ToString()
        {
            return $"LoanRateQuoteId: {LoanRateQuoteId}, TaxId: {TaxId}";
        }
    }
}