using BusinessLayer.Products.Models;
using System.Text.Json;

namespace StubGenerator
{
    internal class Program
    {
        private readonly static string _filePath = "/Projects/TemplateApp/WebApi/StubData/StubDataLayer.json";

        static void Main(string[] args)
        {
            WriteData();
        }

        public static void WriteData()
        {
            var data = new RootStubModel
            {
                Products = new ()
                {
                    new () { Name = "StubDLApple", Description = "SomeDescription", CreatedDate = DateTimeOffset.UtcNow },
                    new () { Name = "StubDLXiomi", Description = "SomeDescription", CreatedDate = DateTimeOffset.UtcNow },
                }
            };

            var jsonString = JsonSerializer.Serialize(data);

            using (var writer = new StreamWriter(_filePath, false))
            {
                writer.Write(jsonString);
            }
        }
    }
}