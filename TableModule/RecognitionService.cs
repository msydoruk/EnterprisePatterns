using TableModule.Extension;
using TableModule.Repositories.Entities;
using TableModule.Repositories.Interfaces;

namespace TableModule
{
    public class RecognitionService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IRevenueRecognitionRepository revenueRecognitionRepository;

        public RecognitionService(IOrderRepository orderRepository, IRevenueRecognitionRepository revenueRecognitionRepository)
        {
            this.orderRepository = orderRepository;
            this.revenueRecognitionRepository = revenueRecognitionRepository;
        }

        public void CalculateRevenueRecognitions(int orderId)
        {
            var order = orderRepository.GetOrder(orderId);

            var product = order.Product;
            var amount = order.Amount;
            var dateCreated = order.DateCreated;

            if (product.Type == ProductType.Animals)
            {
                var amountParts = DistributeEvenlyExtension.DistributeEvenly(amount, 2);

                revenueRecognitionRepository.InsertRecognition(orderId, amountParts[0], dateCreated.AddDays(10));
                revenueRecognitionRepository.InsertRecognition(orderId, amountParts[1], dateCreated.AddDays(40));
            }
            else if (product.Type == ProductType.BusinessIndustrial)
            {
                var amountParts = DistributeEvenlyExtension.DistributeEvenly(amount, 3);

                revenueRecognitionRepository.InsertRecognition(orderId, amountParts[0], dateCreated.AddDays(4));
                revenueRecognitionRepository.InsertRecognition(orderId, amountParts[1], dateCreated.AddDays(10));
                revenueRecognitionRepository.InsertRecognition(orderId, amountParts[2], dateCreated.AddDays(15));
            }
            else if (product.Type == ProductType.Electronics)
            {
                revenueRecognitionRepository.InsertRecognition(orderId, amount, dateCreated.AddDays(14));
            }
            else
            {
                revenueRecognitionRepository.InsertRecognition(orderId, amount, dateCreated);
            }
        }
    }
}
