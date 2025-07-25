namespace Ambev.DeveloperEvaluation.Domain.Utils;

public static class EventQueueName
{
    public const string SaleCreated = "sales.sale-created";
    public const string SaleUpdated = "sales.sale-updated";
    public const string SaleCancelled = "sales.sale-cancelled";
    public const string ItemCancelled = "sales.item-cancelled";
}