namespace ProcessManager.Messages
{
    public class EstablishCreditScoreForLoanRateQuote
    {
        public string CreditProcessingReferenceId { get; set; }
        public string TaxId { get; set; }
        public int Score { get; set; }

        public EstablishCreditScoreForLoanRateQuote(string creditProcessingReferenceId, string taxId, int score)
        {
            CreditProcessingReferenceId = creditProcessingReferenceId;
            TaxId = taxId;
            Score = score;
        }

        public override string ToString()
        {
            return $"CreditProcessingReferenceId: {CreditProcessingReferenceId}, TaxId: {TaxId}, Score: {Score}";
        }
    }
}