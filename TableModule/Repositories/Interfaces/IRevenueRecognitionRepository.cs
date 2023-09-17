using TableModule.Repositories.Entities;

namespace TableModule.Repositories.Interfaces
{
    public interface IRevenueRecognitionRepository
    {
        List<RevenueRecognition> GetRecognitions(int orderId);

        void InsertRecognition(int orderId, decimal amount, DateTime recognitionDate);
    }
}
