namespace ProcessManager.Messages
{
    public class LoanRateBestQuoteFilled
    {
        public string LoanRateQuoteId { get; set; }
        public string TaxId { get; set; }
        public int CreditScore { get; set; }
        public int Amount { get; set; }
        public int TermInMonths { get; set; }
        public BankLoanRateQuote BestBankLoanRateQuote { get; }

        public LoanRateBestQuoteFilled(string loanRateQuoteId, string taxId, int creditScore, int amount, int termInMonths, BankLoanRateQuote bestBankLoanRateQuote)
        {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            CreditScore = creditScore;
            Amount = amount;
            TermInMonths = termInMonths;
            BestBankLoanRateQuote = bestBankLoanRateQuote;
        }

        public override string ToString()
        {
            return $"LoanRateQuoteId: {LoanRateQuoteId}, TaxId: {TaxId}, CreditScore: {CreditScore}, Amount: {Amount}, TermInMonths: {TermInMonths}, BestBankLoanRateQuote: {BestBankLoanRateQuote}";
        }
    }
}