using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        XDocument doc = new XDocument(
            new XElement("bookstore",
                new XElement("book",
                    new XAttribute("category", "COOKING"),
                    new XElement("title", new XAttribute("lang", "en"), "Everyday Italian"),
                    new XElement("author", "Giada De Laurentiis"),
                    new XElement("year", "2005"),
                    new XElement("price", "30.00")
                ),
                new XElement("book",
                    new XAttribute("category", "CHILDREN"),
                    new XElement("title", new XAttribute("lang", "en"), "Harry Potter"),
                    new XElement("author", "J.K. Rowling"),
                    new XElement("year", "2005"),
                    new XElement("price", "29.99")
                ),
                new XElement("book",
                    new XAttribute("category", "WEB"),
                    new XElement("title", new XAttribute("lang", "en"), "Learning XML"),
                    new XElement("author", "Erik T. Ray"),
                    new XElement("year", "2003"),
                    new XElement("price", "39.95")
                )
            )
        );

        string filePath = "bookstore.xml";
        doc.Save(filePath);
        Console.WriteLine($"Đã export cây XML thành công ra file: {filePath}\n");
        Console.WriteLine("Nội dung XML:");
        Console.WriteLine(doc.ToString());
        Console.WriteLine("\n--------------------------------------------------\n");

        XElement root = doc.Root;
        Console.WriteLine($"Phần tử gốc: <{root.Name}>");

        int totalDescendants = root.Descendants().Count();
        int totalBooks = root.Elements("book").Count();
        Console.WriteLine($"Thống kê số lượng phần tử:");
        Console.WriteLine($"   - Tổng số phần tử con/cháu (descendants): {totalDescendants}");
        Console.WriteLine($"   - Số lượng sách (<book>): {totalBooks}");

        var books = root.Elements("book").Select(b => new {
            Title = b.Element("title")?.Value,
            Category = b.Attribute("category")?.Value,
            Price = decimal.TryParse(b.Element("price")?.Value, out decimal p) ? p : 0
        }).ToList();

        if (books.Any())
        {
            var minPriceBook = books.OrderBy(b => b.Price).First();
            var maxPriceBook = books.OrderByDescending(b => b.Price).First();

            Console.WriteLine($"Thông tin giá sách:");
            Console.WriteLine($"   - Sách có giá THẤP NHẤT: '{minPriceBook.Title}' ({minPriceBook.Category}) - Giá: {minPriceBook.Price}");
            Console.WriteLine($"   - Sách có giá CAO NHẤT: '{maxPriceBook.Title}' ({maxPriceBook.Category}) - Giá: {maxPriceBook.Price}");
        }

        Console.ReadKey();
    }
}