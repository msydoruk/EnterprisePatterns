using TableModule;
using TableModule.Repositories.Implementation;

var recognitionService = new RecognitionService(new OrderRepository(), new RevenueRecognitionRepository());

var orderIds = new List<int> {3354, 4550, 5630};

foreach (var orderId in orderIds)
{
    recognitionService.CalculateRevenueRecognitions(orderId);
}