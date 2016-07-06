namespace ProcessManager.Messages
{
    public class BestLoadRateDenied
    {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }
        public int CreditScore { get; }

        public BestLoadRateDenied(string loanRateQuoteId, string taxId, int amount, int termInMonths, int creditScore)
        {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
            CreditScore = creditScore;
        }

        public override string ToString()
        {
            return $"LoanRateQuoteId: {LoanRateQuoteId}, TaxId: {TaxId}, Amount: {Amount}, TermInMonths: {TermInMonths}, CreditScore: {CreditScore}";
        }
    }
}