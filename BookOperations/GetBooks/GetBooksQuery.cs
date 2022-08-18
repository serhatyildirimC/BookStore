using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery (BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BooksViewModel> Handle()
        {
            var books =_dbContext.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach(var i in books)
            {
                vm.Add(new BooksViewModel(){
                    Title = i.Title,
                    PageCount=i.PageCount,
                    PublishDate=i.PublishDate.Date.ToString("dd/mm/yyyy"),
                    Genre=((GenreEnum)i.GenreId).ToString()
                });
            }
            return vm;
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