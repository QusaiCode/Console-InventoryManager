using System;
using InventoryManager.Services;

namespace InventoryManager
{
    class Program
    {
        public static void Main()
        {


            var service = new InventoryService();

            bool added = service.AddProduct("Laptop", ProductCategory.Electronics, 5, 999.99m);
            Console.WriteLine("AddProduct: " + added);

            bool restocked = service.ProcessTransaction(1, TransactionType.Restock, 3);
            Console.WriteLine("Restock: " + restocked);

            bool sold = service.ProcessTransaction(1, TransactionType.Sell, 2);
            Console.WriteLine("Sell: " + sold);

            Console.WriteLine("Products:");
            foreach (var product in service.GetProducts())
            {
                Console.WriteLine($"- {product.Id}: {product.Name}, Qty: {product.Quantity}, Price: {product.Price}");
            }

            Console.WriteLine("Transactions:");
            foreach (var transaction in service.GetTransactions())
            {
                Console.WriteLine($"- {transaction.Id}: {transaction.Type}, Qty: {transaction.QuantityChanged}");
            }
            // Mental Thinking : 
            // 
            // Restock 
            // - check if product exisits => increase quantity 
            // - if not create new product and assign its quantity
            // - then create a (restock) transaction 
            // - add this transaction to history  with text 
            // 
            // sell 
            // - check if product isn't 0 and exisit in stock 
            // - if so :
            // -  create a (sell) transaction 
            // - add this transaction to history  with text 
            // 
            //
            // Approach :
            // 
            // data models : Product(id,name,cat,qty...) | Transacation(type,description)
            // 
            // services : 
            // - stockservice( ) handels creating, modifing,delelting products
            // - 
            // 
            // 
            // 
            // 
            // 
            // 
            // /
            Console.ReadKey();
        }
    }
}