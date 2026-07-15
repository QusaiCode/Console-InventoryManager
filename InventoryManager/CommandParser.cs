using System;

namespace InventoryManager
{
    public class CommandParser
    {
        public static (string Verb ,string Payload) Parse(string? input)
        {
            if ( string.IsNullOrEmpty(input) ) return ("Empty","");

            var parts = input.Trim().Split(' ',2).ToArray();
            
            var verb = parts.ElementAtOrDefault(0) ?? "Empty";
            var payload = parts.ElementAtOrDefault(1) ?? "";

            return (verb, payload );
        }
    }
}