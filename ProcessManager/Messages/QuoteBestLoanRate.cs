namespace ProcessManager.Messages
{
    public class QuoteBestLoanRate
    {
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }

        public QuoteBestLoanRate(string taxId, int amount, int termInMonths)
        {
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
        }

        public override string ToString()
        {
            return $"TaxId: {TaxId}, Amount: {Amount}, TermInMonths: {TermInMonths}";
        }
    }
}