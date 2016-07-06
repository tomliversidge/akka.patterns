namespace ProcessManager.Messages
{
    public class CreditChecked
    {
        public string CreditProcessingReferenceId { get; }
        public string TaxId { get; }
        public int Score { get; }
        public CreditChecked(string creditProcessingReferenceId, string taxId, int score)
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