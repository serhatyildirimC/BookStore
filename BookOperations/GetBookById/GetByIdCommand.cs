using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookById
{
    public class GetByIdCommand
    {
        
        private readonly BookStoreDbContext _dbContext;

        public GetByIdCommand (BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BooksViewModel Handle(int id)
       {
             var book = _dbContext.Books.SingleOrDefault(x=>x.Id == id);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil");
            }
            var model = new BooksViewModel()
                {
                    Title = book.Title,
                    PageCount=book.PageCount,
                    PublishDate=book.PublishDate.Date.ToString("dd/mm/yyyy"),
                    Genre=((GenreEnum)book.GenreId).ToString()
                };
            return model;


        }
    }
    public class BooksViewModel
    {
            public string? Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
    }
    
}