using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using ArbolB;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        
        
           
        private readonly Models.Data.Singleton F = Models.Data.Singleton.Instance;
        private readonly ApiContext _context;

        public MoviesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await _context.Movie.ToListAsync();
        }

        //// GET: api/Movies/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Movie>> GetMovie(string id)
        //{
        //    var movie = await _context.Movie.FindAsync(id);

        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return movie;
        //}
        [HttpGet("{tranversal}")]
        public IEnumerable<Movie> Recorridos([FromRoute] string tranversal)
        {
            if (!F.Arbolb.IsEmpty())
            {
                if (tranversal == "preorden")
                {
                    return F.Arbolb.Recorridos(1);
                }
                else if (tranversal == "inorden")
                {
                    return F.Arbolb.Recorridos(2);
                }
                else if (tranversal == "postorden")
                {
                    return F.Arbolb.Recorridos(3);
                }
                return null;
            }
            return null;
        }


        //[HttpPut("{id}")]



        [HttpPost]
        public IActionResult Order([FromBody] Order n)
        {
            try
            {
                F.tamaño = Convert.ToInt32(n);
                F.Arbolb = new ArbolB<Movie>(Convert.ToInt32(n), Movie.Compare_Title);
                return Created("", n);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }



        }
        [HttpPost("populate")]
        public async Task<ActionResult> Create([FromBody] List<Movie> peli)
        {
            try
            {
                foreach (var item in peli)
                {
                    F.Arbolb.Add(item);
                }

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpDelete]
      public IActionResult Delete()
      {
          F.Arbolb = new ArbolB<Movie>(F.tamaño, Movie.Compare_Title);
          return Ok();
      }
        // DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        
    }   

}
