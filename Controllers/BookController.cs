using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CrateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CrateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext  _context;
        private readonly IMapper _mapper;
        public BookController (BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
         public IActionResult GetBooks()
        {
            GetBooksQuery query =   new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);


        }
        [HttpGet("{id}")]
         public  IActionResult GetById(int id)
        {
            try
            {
            GetByIdCommand command = new GetByIdCommand(_context,_mapper);
            var result = command.Handle(id);
            return  Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel  newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model=newBook;
                command.Handle();
                
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

            


        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook )
        {
           UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Model=updatedBook;
                command.Handle(id);
                
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        
            

    
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
                return BadRequest();
            else
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return Ok();
            }
            
        }


    }
   
}