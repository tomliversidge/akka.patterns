namespace ProcessManager.Messages
{
    public class StartLoanRateQuote
    {
        public int ExpectedLoanRateQuotes { get; }

        public StartLoanRateQuote(int expectedLoanRateQuotes)
        {
            ExpectedLoanRateQuotes = expectedLoanRateQuotes;
        }

        public override string ToString()
        {
            return $"ExpectedLoanRateQuotes: {ExpectedLoanRateQuotes}";
        }
    }
}