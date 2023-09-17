using TransactionScript.Extensions;

namespace TransactionScript
{
    public class RecognitionService
    {
        private readonly IOrderGateway orderGateway;

        public RecognitionService(IOrderGateway orderGateway)
        {
            this.orderGateway = orderGateway;
        }

        public void CalculateRevenueRecognitions(int orderId)
        {
            var order = orderGateway.GetOrder(orderId);

            var productType = order["ProductType"].ToString();
            var amount = (decimal)order["Amount"];
            var dateCreated = (DateTime)order["DateCreated"];

            if (productType == "Animals")
            {
                var amountParts = DistributeEvenlyExtension.DistributeEvenly(amount, 2);

                orderGateway.InsertRecognition(orderId, amountParts[0], dateCreated.AddDays(10));
                orderGateway.InsertRecognition(orderId, amountParts[1], dateCreated.AddDays(40));
            }
            else if (productType == "BusinessIndustrial")
            {
                var amountParts = DistributeEvenlyExtension.DistributeEvenly(amount, 3);

                orderGateway.InsertRecognition(orderId, amountParts[0], dateCreated.AddDays(4));
                orderGateway.InsertRecognition(orderId, amountParts[1], dateCreated.AddDays(10));
                orderGateway.InsertRecognition(orderId, amountParts[2], dateCreated.AddDays(15));
            }
            else if (productType == "Electronics")
            {
                orderGateway.InsertRecognition(orderId, amount, dateCreated.AddDays(14));
            }
            else
            {
                orderGateway.InsertRecognition(orderId, amount, dateCreated);
            }
        }
    }
}
