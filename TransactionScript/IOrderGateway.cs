using System.Data;

namespace TransactionScript
{
    public interface IOrderGateway
    {
        DataRow GetOrder(int orderId);

        void InsertRecognition(int orderId, decimal amount, DateTime recognitionDate);
    }
}
