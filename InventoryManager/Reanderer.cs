using System;

namespace InventoryManager
{
    class Renderer
    {
        public static void RenderProducts(List<Product> products,string msg = " ")
        {
            Console.Clear();

            Console.WriteLine($"{"Inventory Manager v1.0",10}");

            Console.WriteLine(" ");
            Console.WriteLine("  Products ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"{"ID",5}{"NAME",10}{"Quantity",15}{"PRICE",20}");
            Console.WriteLine("--------------------------------------");
            
            if ( products.Count > 0)
            {
                
                Console.WriteLine(" ");

                foreach (var p in products)
                {
                    Console.WriteLine($"{p.Id,5}{p.Name,10}{p.Quantity,15}{$"${p.Price}",20}");
                }
                
                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine("        No Products yet !       ");
                Console.WriteLine(" ");
            }
            
            Console.WriteLine("-------------------------------------");
            RenderHelper();
            // Console.WriteLine(msg);
            RenderMsg(msg);



            // Console.ReadKey();
        }
        public static void RenderTransactions(List<Transaction> transactions,string msg = " ")
        {
            Console.Clear();

            Console.WriteLine($"{"Inventory Manager v1.0",10}");

            Console.WriteLine(" ");
            Console.WriteLine($"{"Tranactions",10}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"{"ID",5} {"TYPE",10} {"DESCRIPTION",15}     ");
            Console.WriteLine("---------------------------------------");
            
            if ( transactions.Count > 0)
            {
                
                Console.WriteLine(" ");
                
                foreach (var t in transactions)
                {
                    Console.WriteLine($"{t.Id,5} {t.Type,10}  ${t.Description,15}");
                }

                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine("        No Transactions yet !       ");
                Console.WriteLine(" ");
            }
            
            Console.WriteLine("-------------------------------------------");
            RenderHelper();
            // Console.WriteLine(msg);
            RenderMsg(msg);



            // Console.ReadKey();
        }

        public static void RenderProduct(Product product )
        {
            // Console.Clear();
            Console.WriteLine($"{"Inventory Manager v1.0",10}");

            Console.WriteLine("");
            Console.WriteLine("  Product ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"{"ID:",0}{"Name:",5}{"Price:",10}{"Quantity:",15} ");
            Console.WriteLine($"{product.Id,2}{product.Name,7}{$"${product.Price}",13}{product.Quantity,17} ");
            Console.WriteLine("--------------------------------------");

        }

        public static void RenderMsg(string msg)
        {
            if ( string.IsNullOrWhiteSpace( msg.Trim() ) || string.IsNullOrEmpty( msg.Trim() )  ){
                // Console.WriteLine("no msg");
                return;

            }

            Console.WriteLine($"       {msg}");
            Console.WriteLine("--------------------------------------");
        }

        public static void RenderHelper()
        {
            Console.WriteLine("Commands :");
            Console.WriteLine("  add");
            Console.WriteLine("  delete");
            Console.WriteLine("  restock");
            Console.WriteLine("  sell");
            Console.WriteLine("  products");
            Console.WriteLine("  transactions");
            Console.WriteLine("--------------------------------------");
            
        }
    }
}