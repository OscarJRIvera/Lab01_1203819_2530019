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
        public string Get()
        {
            string xd = "Lab01";

            return xd;
        }

     
        [HttpGet("{tranversal}")]
        public IEnumerable<Movie> Recorridos([FromRoute] string tranversal)
        {
            if (!F.Arbolb.IsEmpty())
            {
                if (tranversal == "preorder")
                {
                    return F.Arbolb.Recorridos(1);
                }
                else if (tranversal == "inorder")
                {
                    return F.Arbolb.Recorridos(2);
                }
                else if (tranversal == "postorder")
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
                    F.id=1;
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
        [HttpPost("import")] // para mandar archivo json
        public async Task<ActionResult> Create([FromForm] IFormFile File)
        {
            try
            {
                List<Movie> listapeli = new List<Movie>();
                using var memoryst = new MemoryStream();
                await File.CopyToAsync(memoryst);
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
           
        }
        [HttpPost("populate")] //para mandar text json
        public async Task<ActionResult> Create([FromBody] List<Movie> peli)
        {
            try
            {
                foreach (var item in peli)
                {
                    item.id = F.id;
                    F.Arbolb.Add(item);
                    F.id++;
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

          F.Arbolb = new ArbolB<Movie>(F.tamaño, Movie.Compare_id);
          F.id = 1;
          return Ok();
      }
        // DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        
    }   

}
