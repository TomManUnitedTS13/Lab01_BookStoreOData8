namespace Lab01_BookStoreOData8.Models
{
    public static class DataSource
    {
        private static IList<Book> ListBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if (ListBooks != null)
            {
                return ListBooks;
            }
            ListBooks = new List<Book>();
            Book book = new Book
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    category = Category.Book,
                }
            };
            ListBooks.Add(book);
            Book book2 = new Book
            {
                Id = 2,
                ISBN = "978-0-735-21909-0",
                Title = "Where The Crawdads Sing",
                Author = "Delia Owens",
                Price = 15.49m,
                Location = new Address
                {
                    City = "Hanoi",
                    Street = "K3, Nam Tu Liem District"
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "G.P. Putnam's Sons",
                    category = Category.Book
                }
            };
            ListBooks.Add(book2);
            Book book3 = new Book
            {
                Id = 3,
                ISBN = "978-0-446-54147-3",
                Title = "Start-Up Nation",
                Author = "Dan Senor, Saul Singer",
                Price = 10.99m,
                Location = new Address
                {
                    City = "Danang",
                    Street = "J3, Lien Chieu District"
                },
                Press = new Press
                {
                    Id = 3,
                    Name = "Twelve",
                    category = Category.Book
                }
            };
            ListBooks.Add(book3);
            return ListBooks;
        }
    }
}
