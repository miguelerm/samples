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
    public class BooksAsyncController : Controller
    {
        private readonly IBooksAsyncRepository books;

        public BooksAsyncController(IBooksAsyncRepository books)
        {
            this.books = books;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await books.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var book = await books.GetSingleAsync(id);

            if (book == null) {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateOrUpdateBookModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var title = model.Title;
            var author = model.Author;
            var year = model.Year;
            var id = await books.AddAsync(title, author, year);
            if (id <= 0) {
                return StatusCode(500);
            }

            return CreatedAtAction("Get", new { id, title });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, CreateOrUpdateBookModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var title = model.Title;
            var author = model.Author;
            var year = model.Year;
            var updated = await books.UpdateAsync(id, title, author, year);

            if (!updated) {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await books.DeleteAsync(id);

            if (!deleted) {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
