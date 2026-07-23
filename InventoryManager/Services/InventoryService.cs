using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Services
{
    public class InventoryService
    {
        private readonly List<Product> _products = new();
        private readonly List<Transaction> _transactions = new();
        private int _productIdSeed = 1;
        private int _transactionIdSeed = 1;

        public List<Product> GetProducts()
        {
            return _products;
        }

        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }

        public bool AddProduct(string name, ProductCategory category, int quantity, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (quantity < 0)
                return false;

            if (price < 0)
                return false;

            var product = new Product
            {
                Id = _productIdSeed++,
                Name = name,
                Category = category,
                Quantity = quantity,
                Price = price
            };

            _products.Add(product);
            return true;
        }

        public Product? GetProduct(int pid)
        {
            var product = _products.FirstOrDefault(p => p.Id == pid);
            return product;
            
        }
        public bool DeleteProduct(int pid)
        {
            Product product = _products.FirstOrDefault(p => p.Id == pid)!;
            if (product != null )
            {
                _products.Remove(product);
                return true;
            }
            return false;
            
        }

        public bool ProcessTransaction(int productId, TransactionType type, int quantity)
        {
            if (quantity <= 0)
                return false;

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return false;

            if (type == TransactionType.Sell && product.Quantity < quantity)
                return false;

            if (type == TransactionType.Restock)
            {
                product.Quantity += quantity;
            }
            else
            {
                product.Quantity -= quantity;
            }

            var transaction = new Transaction
            {
                Id = _transactionIdSeed++,
                ProductId = product.Id,
                Type = type,
                QuantityChanged = quantity,
                Timestamp = DateTime.Now,
                Description = $"{type} {quantity} of {product.Name}"
            };

            _transactions.Add(transaction);
            return true;
        }
    }
}