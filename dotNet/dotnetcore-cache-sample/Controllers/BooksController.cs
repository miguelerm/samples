using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Samples.CacheSample.Models;
using Samples.CacheSample.Repositories;

namespace Samples.CacheSample.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBooksRepository books;

        public BooksController(IBooksRepository books)
        {
            this.books = books;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(books.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = books.GetSingle(id);

            if (book == null) {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post(CreateOrUpdateBookModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var title = model.Title;
            var author = model.Author;
            var year = model.Year;
            var id = books.Add(title, author, year);
            if (id <= 0) {
                return StatusCode(500);
            }

            return CreatedAtAction("Get", new { id, title });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CreateOrUpdateBookModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var title = model.Title;
            var author = model.Author;
            var year = model.Year;
            var updated = books.Update(id, title, author, year);

            if (!updated) {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = books.Delete(id);

            if (!deleted) {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
