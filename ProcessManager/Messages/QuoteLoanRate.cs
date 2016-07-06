namespace ProcessManager.Messages
{
    public class QuoteLoanRate
    {
        public string LoanQuoteReferenceId { get; set; }
        public string TaxId { get; set; }
        public int CreditScore { get; set; }
        public int Amount { get; set; }
        public int TermInMonths { get; set; }

        public QuoteLoanRate(string loanQuoteReferenceId, string taxId, int creditScore, int amount, int termInMonths)
        {
            LoanQuoteReferenceId = loanQuoteReferenceId;
            TaxId = taxId;
            CreditScore = creditScore;
            Amount = amount;
            TermInMonths = termInMonths;
        }

        public override string ToString()
        {
            return $"LoanQuoteReferenceId: {LoanQuoteReferenceId}, TaxId: {TaxId}, CreditScore: {CreditScore}, Amount: {Amount}, TermInMonths: {TermInMonths}";
        }
    }
}