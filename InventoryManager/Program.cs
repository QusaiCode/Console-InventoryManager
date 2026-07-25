
using InventoryManager.Services;

namespace InventoryManager
{
    class Program
    {
        public static void Main()
        {

            // Inventroy service instance ot the program
            var service = new InventoryService();

            // Track the running state of the program
            var active = true ;

            // Initial Render of Products
            Renderer.RenderProducts( service.GetProducts() );


            // Main app loop
            while(active){

                // Take input from user
                Console.Write("Enter Command :");
                // User Raw Input as String
                var input = Console.ReadLine() ?? "Empty";

                // decide what each command do
                switch ( input.Trim().ToLower() )
                {
                    case "restock":{
                        // take product id and Quantity from user and then validate them.
                        Console.Write(" Product ID :");

                        if( ! TryParseId( Console.ReadLine()!, out int pid, out string idMsg ))
                            {
                                Renderer.RenderMsg(idMsg);
                                Console.ReadKey();
                                
                                Renderer.RenderProducts( service.GetProducts() );
                                continue;
                            }

                        Console.Clear();
                        

                        if ( TryGetProduct(service, pid, out Product product ,out string productMsg))
                            {
                                Renderer.RenderProduct(product);
                            }
                            else
                            {
                                Renderer.RenderMsg(productMsg);
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );

                                continue;
                            }

                        Console.Write(" Add Quantity :");
                        if ( !int.TryParse(  Console.ReadLine() , out int pQty ) || pQty < 0 )
                            {
                                Renderer.RenderMsg(" invalid Quantity ! ");
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
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
                                
                                Renderer.RenderMsg(idMsg);
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
                                continue;
                            }

                        Console.Clear();

                        if ( TryGetProduct(service, pid, out Product product ,out string productMsg))
                            {
                                Renderer.RenderProduct(product);
                            }
                            else
                            {
                                Renderer.RenderMsg(productMsg);
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
                                continue;
                            }

                        Console.Write(" Product Quantity :");
                        if ( !int.TryParse(  Console.ReadLine() , out int pQty ) || pQty < 0 )
                            {
                                Renderer.RenderMsg(" invalid Quantity ! ");
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
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
                        
                        Console.Clear();
                        Console.Write("Product Name :");
                        var pName = Console.ReadLine()!;

                        if (string.IsNullOrEmpty( pName.Trim() ))
                            {
                                
                                Renderer.RenderMsg( "Name can't be Empty" );
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
                                continue;
                            }

                        Console.WriteLine("Product Categories:");
                        for (int i = 0; i < Enum.GetNames<ProductCategory>().Length ; i++ )
                        {
                            Console.WriteLine($" {i+1} ] { Enum.GetName<ProductCategory>( (ProductCategory) i) }");
                        }

                        Console.Write("Category ID:");

                        // Category Id From Category Enum
                        if ( !int.TryParse(  Console.ReadLine() , out int CategoryId ) || CategoryId < 0 )
                            {       
                                Renderer.RenderMsg(  " invalid Id ! ");
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
                                continue;
                            }

                        Console.Write("Product Initial Quantity:");
                        if ( !int.TryParse(  Console.ReadLine() , out int pQty ) || pQty < 0 )
                            {
                                Renderer.RenderMsg( " invalid Quantity ! " );
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
                                continue;
                            }

                        Console.Write("Product Price :");
                        if ( !Decimal.TryParse(  Console.ReadLine() , out Decimal pPrice ) || pPrice < 0 )
                            {                          
                                Renderer.RenderMsg( " invalid Price ! " );
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
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
                                                                
                                Renderer.RenderMsg( idMsg );
                                Console.ReadKey();
                                Renderer.RenderProducts( service.GetProducts() );
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

    }
}