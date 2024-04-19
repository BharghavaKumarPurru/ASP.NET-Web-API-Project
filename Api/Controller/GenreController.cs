using Api.Data;
using Api.DTOs;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDb _db;
        public GenreController(ApplicationDb db)
        {
            _db = db;
        }


        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var genres=_db.Genres.ToList();
            var toReturn = new List<GenreDTOs>();
            foreach (var genre in genres)
            {
                var genreDto = new GenreDTOs
                {
                    Id = genre.Id,
                    Name = genre.Name,
                };
                toReturn.Add(genreDto);
            }
            return Ok(toReturn);
        }

        [HttpGet("get-one/{id}")]
        public IActionResult GetOneid(int id)
        {
            var genre = _db.Genres.Find(id);
            if(genre == null)
            {
                return NotFound();
            }
            var toReturn=new GenreDTOs 
            { 
                Id = id,
                Name = genre.Name,
            };
            return Ok(toReturn);

        }

        [HttpPost("create")]
        public IActionResult Create(GenreAddEditDto model)
        {
            if (GenerateNameExits(model.Name))
            {
                return BadRequest("genere name should be unquew");

            }
            var genreToAdd=new Genre
            { 
                Name = model.Name.ToLower() 
            };
            _db.Genres.Add(genreToAdd);
            _db.SaveChanges();
            return NoContent();
        }

        private bool GenerateNameExits(string name)
        {
            var fetachGenre=_db.Genres.FirstOrDefault(x=>x.Name.ToLower() == name.ToLower());
            if(fetachGenre !=null)
            {
                return true;
            }
            return false;
        }

        [HttpPut("update")]
        public IActionResult Update(GenreAddEditDto model)
        {
            var fetchGenre = _db.Genres.Find(model.Id);
            if(fetchGenre == null)
            {
                return NotFound();
            }
            if (GenerateNameExits(model.Name))
            {
                return BadRequest("genere name should be unquew");

            }
            fetchGenre.Name = model.Name.ToLower();
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var fetchGenre = _db.Genres.Find(id);
            if(fetchGenre == null)
            {
                return NotFound();

            }
            _db.Genres.Remove(fetchGenre);
            _db.SaveChanges();
            return NoContent();

        }

    }
}
