
using InventoryManager.Services;

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


            // displayes the app title
            // Console.WriteLine("Inventory Manager v1.0");
            // Console.WriteLine(new String('-',50) );

            // // Displayes the Existed Products if any
            // if (service.GetProducts().Count > 0 )
            // {
            //     Console.WriteLine("Products:");
            //     foreach (var product in service.GetProducts() )
            //     {
            //         Console.WriteLine($"- {product.Id}: {product.Name}, Qty: {product.Quantity}, Price: {product.Price}");
            //     }
            // }
            // else
            // {
            //     Console.WriteLine("   No Products yet !");
            // }
            // Replaced by Renderer Function
            Renderer.RenderProducts( service.GetProducts() );


            // Main app loop
            while(active){

                // Take input from user
                Console.Write("Enter Command :");
                var input = Console.ReadLine() ?? "Empty";

                // decide what each command do
                switch ( input.Trim() )
                {
                    case "restock":{
                        // take product id and Quantity from user and then validate them.
                        Console.Write(" Product ID :");

                        if( ! TryParseId( Console.ReadLine()!, out int pid, out string idMsg ))
                            {
                                
                                continue;
                            }

                        // if ( !int.TryParse(  Console.ReadLine() , out int pid ) )
                        //     {
                        //         Console.WriteLine(" invalid Id ! ");
                        //         continue;
                        //     }
                        if ( TryGetProduct(service, pid, out Product product ,out string productMsg))
                            {
                                Console.WriteLine($" ID : {product.Id} - NAME : { product.Name } - QUANTITY : {product.Quantity} - Price :{product.Price} ");
                            }
                            else
                            {
                                continue;
                            }

                        // if ( service.GetProduct(pid) != null )
                        //     {
                        //         // var product = service.GetProduct(pid)! ;
                        //         // Console.WriteLine($" ID : {product.Id} - NAME : { product.Name } - QUANTITY : {product.Quantity} - Price :{product.Price} ");
                        //     }
                        // else
                        //     {
                        //         Console.WriteLine(" Product not exist ! ");
                        //         continue;
                        //     }

                        Console.Write(" Add Quantity :");
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

                        Renderer.RenderProducts(
                            service.GetProducts() ,
                            restocked ? "Product Restocked. " : "Restock Failed."
                        );

                        break;

                    }

                    case "sell":{

                        Console.Write(" Product ID :");
                        if( ! TryParseId( Console.ReadLine()!, out int pid, out string idMsg ))
                            {
                                //TODO: Make Renderer Show Error idMsg
                                continue;
                            }

                        if ( TryGetProduct(service, pid, out Product product ,out string productMsg))
                            {
                                //TODO: Make renderer Show Selected Product
                                Console.WriteLine($" ID : {product.Id} - NAME : { product.Name } - QUANTITY : {product.Quantity} - Price :{product.Price} ");
                            }
                            else
                            {
                                //TODO: Make renderer Show Erorr productMsg
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

                        // Console.WriteLine(sold ? "Product Sold." : "Selling Failed");
                        Renderer.RenderProducts(
                            service.GetProducts(),
                            sold ? "Product Sold. " : "Selling Failed."
                        );

                        break;

                    }

                    case "transactions":{
                        Renderer.RenderTransactions(
                            service.GetTransactions()
                        );
                        break;

                    }
                    case "products":{

                        Renderer.RenderProducts(
                            service.GetProducts()
                        );
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

                        Renderer.RenderProducts(
                            service.GetProducts(),
                            added ? "Product Added" : "Adding Failed" 

                        );

                        break;

                    }


                    case "delete":
                        {
                            Console.Write(" Product ID :");
                            if( ! TryParseId( Console.ReadLine()!, out int pid, out string idMsg ))
                            {
                                continue;
                            }

                            var deleted = service.DeleteProduct(pid);

                            Renderer.RenderProducts(
                                service.GetProducts(),
                                deleted ? "Product Deleted" : "Product not exist !" 

                            );
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


        private static bool TryGetProduct(InventoryService service, int id, out Product product, out string msg )
        {   
            if (service.GetProduct(id) == null )
            {
                msg = $"No Product Found With ID {id}";
                product = null!;
                return false;
            }
            product = service.GetProduct(id)!;
            msg = "Product Exists !";
            return true;
        }

        private static bool TryParseId ( string input, out int id, out string msg, int min = 0)
        {
            if ( !int.TryParse(input, out int result) || result < min )
            {
                msg = "Invalid Input. Please enter Posetive Number !";
                id = 0;
                return false;
            }
            id = result;
            msg = "";
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