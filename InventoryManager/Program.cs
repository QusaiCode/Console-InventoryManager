using System;
using InventoryManager.Services;

namespace InventoryManager
{
    class Program
    {
        public static void Main()
        {


            var service = new InventoryService();

            var active = true ;

            Console.Clear();

            Console.WriteLine("Inventory Manager v1.0");
            Console.WriteLine(new String('-',50) );
            Console.WriteLine("Products:");
            foreach (var product in service.GetProducts())
            {
                Console.WriteLine($"- {product.Id}: {product.Name}, Qty: {product.Quantity}, Price: {product.Price}");
            }
            
            
            while(active){    
                Console.Write(">>");
                var input = Console.ReadLine();

                (string verb, string payload) = CommandParser.Parse(input);

                var args = payload.Split(' ').ToArray();

                
                switch (verb)
                {
                    case "restock":{

                        if ( !int.TryParse( args.ElementAtOrDefault(0) ,out int pid) ) 
                            {
                                Console.WriteLine(" invalid Arguments ! ");
                                continue;
                            }
                        if ( service.GetProduct(pid) == null)
                            {
                                Console.WriteLine(" Product not exist ! ");
                                continue ;
                            }
                        
                        bool restocked = service.ProcessTransaction (

                            Int32.Parse( args.ElementAtOrDefault(0) ?? "" ) ,
                            TransactionType.Restock ,
                            Int32.Parse( args.ElementAtOrDefault(1) ?? "0") 
                            // args.ElementAtOrDefault(0) ?? "" ,
                            // (ProductCategory) Int32.Parse( args.ElementAtOrDefault(1) ?? "0") ,
                            // Int32.Parse( args.ElementAtOrDefault(1) ?? "0") ,
                            // Int32.Parse( args.ElementAtOrDefault(1) ?? "0") 
                        );
                        Console.WriteLine("Product Restocked : " + restocked);
                        break;
                    }
                    case "sell":{

                        if ( 
                            !int.TryParse( args.ElementAtOrDefault(0) ,out int pid) ||
                            !int.TryParse( args.ElementAtOrDefault(0) ,out int qty)
                         ) 
                            {
                                Console.WriteLine(" invalid Arguments ! ");
                                continue;
                            }
                        if ( service.GetProduct(pid) == null )
                            {
                                Console.WriteLine(" Product not exist ! ");
                                continue; 
                            }
                        
                        bool sold = service.ProcessTransaction(
                            Int32.Parse( args.ElementAtOrDefault(0) ?? "" ) ,
                            TransactionType.Sell,
                            Int32.Parse( args.ElementAtOrDefault(1) ?? "0") 
                        );
                        Console.WriteLine("Sell: " + sold);
                        break;
                    }
                    case "transactions":{
                        Console.WriteLine("Transactions:");
                        foreach (var transaction in service.GetTransactions())
                        {
                            Console.WriteLine($"- {transaction.Id}: {transaction.Type}, Qty: {transaction.QuantityChanged}");
                        }
                        break;
                    }
                    case "products":{
                        Console.WriteLine("Products:");
                        foreach (var product in service.GetProducts())
                        {
                            Console.WriteLine($"- {product.Id}: {product.Name}, Qty: {product.Quantity}, Price: {product.Price}");
                        }
                        break;
                    }
                    // case "add":{
                    //     bool added = service.AddProduct(
                    //         "Laptop",
                    //         ProductCategory.Electronics,
                    //         5,
                    //         999.99m
                    //     );
                    //     Console.WriteLine("AddProduct: " + added);
                    //     break;
                    // }
                    case "exit":
                        {
                            active = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Command !");
                            break;
                        }
                    
                }
            }
            // bool added = service.AddProduct("Laptop", ProductCategory.Electronics, 5, 999.99m);
            // Console.WriteLine("AddProduct: " + added);

            // bool restocked = service.ProcessTransaction(1, TransactionType.Restock, 3);
            // Console.WriteLine("Restock: " + restocked);

            // bool sold = service.ProcessTransaction(1, TransactionType.Sell, 2);
            // Console.WriteLine("Sell: " + sold);

            // Console.WriteLine("Products:");
            // foreach (var product in service.GetProducts())
            // {
            //     Console.WriteLine($"- {product.Id}: {product.Name}, Qty: {product.Quantity}, Price: {product.Price}");
            // }

            // Console.WriteLine("Transactions:");
            // foreach (var transaction in service.GetTransactions())
            // {
            //     Console.WriteLine($"- {transaction.Id}: {transaction.Type}, Qty: {transaction.QuantityChanged}");
            // }
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
            Console.ReadKey();
        }
    }
}