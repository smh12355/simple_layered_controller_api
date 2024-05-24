namespace BookStore.Core.Models
{
// домейн модел
    public class Book
    {
        public const int MAX_TITLE_LENGHT = 250;
        private Book(Guid id, string title, string description, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }
        public static (Book Book, string Error) Create(Guid id, string title, string description, decimal price)
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGHT) 
            {
                error = "Title can not be empty or longer then 250 symbols";
                // по-нормальному еще нужно делать валидация для всех остальных полей
            }
            var book = new Book(id, title, description, price);
            return (book, error);
        }

        public Guid Id { get;}
        public string Title { get;} = String.Empty;
        public string Description { get;} = String.Empty;
        public decimal Price { get;}
    }
}
