namespace ProcessManager.Messages
{
    public class LoanRateQuoteStarted
    {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }

        public LoanRateQuoteStarted(string loanRateQuoteId, string taxId)
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