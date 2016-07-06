namespace ProcessManager.Messages
{
    public class BankLoanRateQuote
    {
        public string BankId { get; }
        public string BankLoanRateQuoteId { get; }
        public double InterestRate { get; }

        public BankLoanRateQuote(string bankId, string bankLoanRateQuoteId, double interestRate)
        {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            InterestRate = interestRate;
        }

        public override string ToString()
        {
            return $"BankId: {BankId}, BankLoanRateQuoteId: {BankLoanRateQuoteId}, InterestRate: {InterestRate}";
        }
    }
}