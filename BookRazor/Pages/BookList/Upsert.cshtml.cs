using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id == null)
            {
                // create
                return Page();
            }

            // edit
            Book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    _db.Books.Add(Book);
                }
                else
                {
                    _db.Books.Update(Book); // if wanna update every prop

                    //var book = await _db.Books.FindAsync(Book.Id);
                    //book.Name = Book.Name;
                    //book.Author = Book.Author;
                    //book.ISBN = Book.ISBN;
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}