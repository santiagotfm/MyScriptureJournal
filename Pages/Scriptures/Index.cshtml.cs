using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;


namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ScriptureBook { get; set; }
        public string DateSort { get; set; }
        public string BookSort { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            IQueryable<string> bookQuery = from s in _context.Scripture
                                            orderby s.Book
                                            select s.Book;

            var scriptures = from s in _context.Scripture
                         select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Note.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(ScriptureBook))
            {
                scriptures = scriptures.Where(x => x.Book == ScriptureBook);
            }
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());

            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            BookSort = sortOrder == "Book" ? "book_desc" : "Book";

            switch (sortOrder)
            {
                case "date_desc":
                    scriptures = scriptures.OrderByDescending(s => s.EntryDate);
                    break;
                case "Book":
                    scriptures = scriptures.OrderBy(s => s.Book);
                    break;
                case "book_desc":
                    scriptures = scriptures.OrderByDescending(s => s.Book);
                    break;
                default:
                    scriptures = scriptures.OrderBy(s => s.EntryDate);
                    break;
            }
            Scripture = await scriptures.ToListAsync();
        }
    }
}
