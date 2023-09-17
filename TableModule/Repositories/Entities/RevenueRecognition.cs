namespace TableModule.Repositories.Entities
{
    public class RevenueRecognition
    {
        public RevenueRecognition(int orderId, decimal amount, DateTime recognitionDate)
        {
            OrderId = orderId;
            Amount = amount;
            RecognitionDate = recognitionDate;
        }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime RecognitionDate { get; set; }
    }
}
