using TransactionScript;

var recognitionService = new RecognitionService(new OrderGateway());

var orderIds = new List<int> {3354, 4550, 5630};

foreach (var orderId in orderIds)
{
    recognitionService.CalculateRevenueRecognitions(orderId);
}