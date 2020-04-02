using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(book == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Delete Successful." });
        }
    }
}