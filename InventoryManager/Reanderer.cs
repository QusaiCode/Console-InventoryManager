using System;

namespace InventoryManager
{
    class Renderer
    {
        public static void RenderProducts(List<Product> products,string msg = "")
        {
            Console.Clear();

            Console.WriteLine($"{"Inventory Manager v1.0",20}");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("  Products ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"  {"ID",5}    {"NAME",10}     {"PRICE",15}   ");
            Console.WriteLine("--------------------------------------");
            
            if ( products.Count > 0)
            {
                
                foreach (var p in products)
                {
                    Console.WriteLine($"{p.Id,5}{p.Name,10} {p.Price,15}");
                }
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine("        No Products yet !       ");
                Console.WriteLine(" ");
            }
            
            Console.WriteLine("-------------------------------------");
            RenderHelper();
            Console.WriteLine(msg);



            // Console.ReadKey();
        }
        public static void RenderTransactions(List<Transaction> transactions,string msg = "")
        {
            Console.Clear();

            Console.WriteLine($"{"Inventory Manager v1.0",20}");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"{"Tranactions",20}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"{"ID",5} {"TYPE",10} {"DESCRIPTION",20}     ");
            Console.WriteLine("---------------------------------------");
            
            if ( transactions.Count > 0)
            {
                
                foreach (var t in transactions)
                {
                    Console.WriteLine($"{t.Id,5} {t.Type,10} - ${t.Description,20}");
                }
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine("        No Transactions yet !       ");
                Console.WriteLine(" ");
            }
            
            Console.WriteLine("-------------------------------------------");
            RenderHelper();
            Console.WriteLine(msg);



            // Console.ReadKey();
        }

        public static void RenderProduct(Product product , string msg = "")
        {
            Console.Clear();
            Console.WriteLine($"{"Inventory Manager v1.0",20}");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("  Product ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"  ID:      Name:   Price:        Category: ");
            Console.WriteLine($"  {product.Id}    {product.Name}   {product.Price}     {product.Category} ");
            Console.WriteLine("--------------------------------------");
            RenderHelper();
            Console.WriteLine(msg);



        }
        public static void RenderHelper()
        {
            Console.WriteLine("Commands :");
            Console.WriteLine("  add");
            Console.WriteLine("  delete");
            Console.WriteLine("  restock");
            Console.WriteLine("  sell");
            Console.WriteLine("  Products");
            Console.WriteLine("  Transactions");
            
        }
    }
}