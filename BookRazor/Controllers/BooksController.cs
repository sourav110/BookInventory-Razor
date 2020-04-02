using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRazor.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookRazor.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Books.ToList() });
        }
    }
}