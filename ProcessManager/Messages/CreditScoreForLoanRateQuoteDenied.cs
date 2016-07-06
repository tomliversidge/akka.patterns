namespace ProcessManager.Messages
{
    public class CreditScoreForLoanRateQuoteDenied
    {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }
        public int Score { get; }

        public CreditScoreForLoanRateQuoteDenied(string loanRateQuoteId, string taxId, int amount, int termInMonths, int score)
        {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
            Score = score;
        }

        public override string ToString()
        {
            return $"LoanRateQuoteId: {LoanRateQuoteId}, TaxId: {TaxId}, Amount: {Amount}, TermInMonths: {TermInMonths}, Score: {Score}";
        }
    }
}