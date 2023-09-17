using System.Data;

namespace TransactionScript
{
    public class OrderGateway : IOrderGateway
    {
        public DataRow GetOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public void InsertRecognition(int orderId, decimal amount, DateTime recognitionDate)
        {
            throw new NotImplementedException();
        }
    }
}
