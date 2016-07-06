namespace ProcessManager.Messages
{
    public class BestLoanRateDenied
    {
        public string LoanRateQuoteId { get; set; }
        public string TaxId { get; set; }
        public int Amount { get; set; }
        public int TermInMonths { get; set; }
        public int Score { get; set; }

        public BestLoanRateDenied(string loanRateQuoteId, string taxId, int amount, int termInMonths, int score)
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