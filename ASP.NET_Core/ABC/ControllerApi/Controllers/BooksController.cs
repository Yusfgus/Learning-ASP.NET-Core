using System.Collections.Generic;
using System.Threading.Tasks;
using ABC.Controller.Models;
using ABC.Controller.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _bookService.GetAll());
        }
    }
}
