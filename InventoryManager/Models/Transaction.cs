using System;

namespace InventoryManager
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public TransactionType Type { get; set; }
        public int QuantityChanged { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}