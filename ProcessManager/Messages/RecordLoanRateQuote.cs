namespace ProcessManager.Messages
{
    public class RecordLoanRateQuote
    {
        public string BankId { get; set; }
        public string BankLoanRateQuoteId { get; set; }
        public double InterestRate { get; set; }

        public RecordLoanRateQuote(string bankId, string bankLoanRateQuoteId, double interestRate)
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