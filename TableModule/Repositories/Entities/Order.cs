namespace TableModule.Repositories.Entities
{
    public class Order
    {
        public Product Product { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }

        public List<RevenueRecognition> RevenueRecognitions { get; set; }
    }
}
