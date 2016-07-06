namespace ProcessManager.Messages
{
    public class CheckCredit
    {
        public string CreditProcessingReferenceId { get; }
        public string TaxId { get; }

        public CheckCredit(string creditProcessingReferenceId, string taxId)
        {
            CreditProcessingReferenceId = creditProcessingReferenceId;
            TaxId = taxId;
        }

        public override string ToString()
        {
            return $"CreditProcessingReferenceId: {CreditProcessingReferenceId}, TaxId: {TaxId}";
        }
    }
}