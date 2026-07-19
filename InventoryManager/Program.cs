using System;
using InventoryManager.Services;
using Microsoft.VisualBasic;

namespace InventoryManager
{
    class Program
    {
        public static void Main()
        {

            // Create a inventroy service object
            var service = new InventoryService();
            // track the running state of the program
            var active = true ;

            Console.Clear();

            // displayes the app title
            Console.WriteLine("Inventory Manager v1.0");
            Console.WriteLine(new String('-',50) );
            
            // Displayes the Existed Products if any
            if (service.GetProducts().Count > 0 )
            { 
                Console.WriteLine("Products:");
                foreach (var product in service.GetProducts() )
                {
                    Console.WriteLine($"- {product.Id}: {product.Name}, Qty: {product.Quantity}, Price: {product.Price}");
                }
            }
            else
            {
                Console.WriteLine("   No Products yet !");
            }


            // Main app loop
            while(active){

                // Take input from user
                Console.Write("Enter Command :");
                var input = Console.ReadLine() ?? "Empty";

                // decide what each command doe
                switch ( input.Trim() )
                {
                    case "restock":{
                        // take product id and Quantity from user and then validate them.
                        Console.Write(" Product ID :");
                        if ( !int.TryParse(  Console.ReadLine() , out int pid ) )
                            {
                                Console.WriteLine(" invalid Id ! ");
                                continue;
                            }

                        if ( service.GetProduct(pid) != null )
                            {
                                var product = service.GetProduct(pid)! ;
                                Console.WriteLine($" Product : { product.Name } - Price :{product.Price} ");
                            }
                        else
                            {
                                Console.WriteLine(" Product not exist ! ");
                                continue;
                            }

                        Console.Write(" Product Quantity :");
                        if ( !int.TryParse(  Console.ReadLine() , out int pQty ) || pQty < 0 )
                            {
                                Console.WriteLine(" invalid Quantity ! ");
                                continue;
                            }

                        bool restocked = service.ProcessTransaction (

                            pid ,
                            TransactionType.Restock ,
                            pQty

                        );

                        Console.WriteLine(restocked ? "Product Restocked. " : "Restock Failed." );
                        break;

                    }

                    case "sell":{

                        Console.Write(" Product ID :");
                        if ( !int.TryParse(  Console.ReadLine() , out int pid ) )
                            {
                                Console.WriteLine(" invalid Id ! ");
                                continue;
                            }

                        if ( service.GetProduct(pid) != null )
                            {
                                var product = service.GetProduct(pid)! ;
                                Console.WriteLine($" Product : { product.Name } - Price :{product.Price} ");
                            }
                        else
                            {
                                Console.WriteLine(" Product not exist ! ");
                                continue;
                            }

                        Console.Write(" Product Quantity :");
                        if ( !int.TryParse(  Console.ReadLine() , out int pQty ) || pQty < 0 )
                            {
                                Console.WriteLine(" invalid Quantity ! ");
                                continue;
                            }

                        bool sold = service.ProcessTransaction(
                            pid,
                            TransactionType.Sell,
                            pQty
                        );
                        Console.WriteLine(sold ? "Product Sold." : "Selling Failed");
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

                    case "add":{
                        Console.Write("Product Name :");
                        var pName = Console.ReadLine()!;
                        if (string.IsNullOrEmpty( pName.Trim() ))
                            {
                                Console.WriteLine("Name can't be Empty");
                            }

                        Console.WriteLine("Product Categories:");
                        for (int i = 0; i < Enum.GetNames<ProductCategory>().Length ; i++ )
                        {
                            Console.WriteLine($" {i+1} ] { Enum.GetName<ProductCategory>( (ProductCategory) i) }");
                        }

                        Console.Write("Category ID:");
                        // var Category = Console.ReadLine();
                        if ( !int.TryParse(  Console.ReadLine() , out int CategoryId ) || CategoryId < 0 )
                            {
                                Console.WriteLine(" invalid Id ! ");
                                continue;
                            }

                        Console.Write("Product Initial Quantity:");
                        if ( !int.TryParse(  Console.ReadLine() , out int pQty ) || pQty < 0 )
                            {
                                Console.WriteLine(" invalid Quantity ! ");
                                continue;
                            }

                        Console.Write("Product Price :");
                        if ( !Decimal.TryParse(  Console.ReadLine() , out Decimal pPrice ) || pPrice < 0 )
                            {
                                Console.WriteLine(" invalid Quantity ! ");
                                continue;
                            }


                        bool added = service.AddProduct(
                            pName,
                            (ProductCategory) CategoryId,
                            pQty,
                            pPrice
                        );
                        Console.WriteLine(added ? "Product Added" : "Adding Failed" );
                        break;

                    }


                    case "delete":
                        {
                            Console.Write(" Product ID :");
                            if ( !int.TryParse(  Console.ReadLine() , out int pid ) )
                                {
                                    Console.WriteLine(" invalid Id ! ");
                                    continue;
                                }

                            if ( service.DeleteProduct(pid) )
                                {
                                    Console.WriteLine($" Product Deleted ...");
                                }
                            else
                                {
                                    Console.WriteLine(" Product not exist ! ");
                                    continue;
                                }
                            break;
                        }
                    
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
        }


        private bool IsValidObject( VariantType? obj )
        {
            if (obj == null)
            {
                return false;
            }
            return true;
        }

        private bool IsValidIntId ( string id, int min )
        {
            if ( !int.TryParse( id, out int result) || result < min )
            {
                return false;
            }
            return true;
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
            // Console.ReadKey();
        
    }
}