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
using System.IO;
using System.Text;
using System.Text.Json;

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


        [HttpDelete("populate/{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            try
            {
                Movie ValorEliminar = new Movie();
                ValorEliminar.id = Convert.ToInt32(id);
                if (F.Arbolb.Delete(ValorEliminar))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }



        [HttpPost]
        public async Task<ActionResult> Order([FromBody] Order n)
        {
            try
            {
                if (Convert.ToInt32(n.order) % 2 != 0)
                {
                    F.tamaño = Convert.ToInt32(n.order);
                    F.Arbolb = new ArbolB<Movie>(Convert.ToInt32(n.order), Movie.Compare_id);
                    return Created("", n);
                }
                else
                {
                    return BadRequest("Numero debe ser impar");
                }

            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }



        }
        [HttpPost("populate")]
        public async Task<ActionResult> Create([FromForm] IFormFile file)
        {
            try
            {
                List<Movie> listapeli = new List<Movie>();
                using var memoryst = new MemoryStream();
                await file.CopyToAsync(memoryst);
                var movies = Encoding.ASCII.GetString(memoryst.ToArray());
                listapeli = JsonSerializer.Deserialize<List<Movie>>(movies);
                await Task.Run(() =>
                {
                    foreach (Movie item in listapeli)
                    {
                        item.id = F.id;
                        F.id++;
                        F.Arbolb.Add(item);
                    }
                });
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            //try
            //{

            //    foreach (var item in peli)
            //    {
            //        item.id = F.id;
            //        F.Arbolb.Add(item);
            //        F.id++;
            //    }

            //    return Ok();
            //}
            //catch (Exception error)
            //{
            //    return BadRequest(error.Message);
            //}
        }
        [HttpDelete]
        public IActionResult Delete()
        {

            F.Arbolb = new ArbolB<Movie>(F.tamaño, Movie.Compare_id);
            F.id = 1;
            return Ok();
        }
        // DELETE: api/Movies/5
        //[HttpDelete("{id}")]

    }

}
