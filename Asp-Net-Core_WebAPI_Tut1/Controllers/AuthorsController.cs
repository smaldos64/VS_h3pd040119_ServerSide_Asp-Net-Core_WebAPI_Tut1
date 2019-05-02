using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp_Net_Core_WebAPI_Tut1.Models;
using Asp_Net_Core_WebAPI_Tut1.Models.Repository;
using Asp_Net_Core_WebAPI_Tut1.Models.DataManager;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Net_Core_WebAPI_Tut1.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IDataRepository<Author, AuthorDto> _dataRepository;

        public AuthorsController(IDataRepository<Author, AuthorDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Authors
        [HttpGet]
        public IActionResult Get()
        {
            var authors = _dataRepository.GetAll();
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult Get(int id)
        {
            var author = _dataRepository.GetDto(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }

            return Ok(author);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Author author)
        {
            if (author is null)
            {
                return BadRequest("Author is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(author);
            return CreatedAtRoute("GetAuthor", new { Id = author.AuthorID }, null);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest("Author is null.");
            }

            var authorToUpdate = _dataRepository.Get(id);
            if (authorToUpdate == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(authorToUpdate, author);
            return NoContent();
        }
        //public void Put(int id, [FromQuery]string value)
        ////public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Author = this._dataRepository.Get(id);

            if (null == Author)
            {
                return NotFound("The AUthor Record couldn't be found.");
            }

            this._dataRepository.Delete(Author);

            return NoContent();
        }
    }
}
