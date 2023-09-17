using TableModule.Repositories.Entities;
using TableModule.Repositories.Interfaces;

namespace TableModule.Repositories.Implementation
{
    public class RevenueRecognitionRepository : IRevenueRecognitionRepository
    {
        public List<RevenueRecognition> GetRecognitions(int orderId)
        {
            throw new NotImplementedException();
        }

        public void InsertRecognition(int orderId, decimal amount, DateTime recognitionDate)
        {
            throw new NotImplementedException();
        }
    }
}