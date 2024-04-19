using Api.Data;
using Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly ApplicationDb _db;
        public AlbumController(ApplicationDb db) 
        {
            _db = db;
        }

        /*[HttpPost("create")]
        public async Task<IActionResult> Create(AlbumAddEditDto model)
        {
            if (AlbumNameExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                return BadRequest("album name should be unique");

            }
            if (model.ArtistIds.Count==0)
            {
                return BadRequest("At least one artist is should be selected");
            }
            var albumToAdd = new Models.Album
            {
                Name = model.Name,
                PhotoUrl = model.PhotoUrl,
            };
            _db.Albums.Add(albumToAdd);
            await _db.SaveChangesAsync();


        }*/
        private async Task<bool> AlbumNameExistsAsync(string albumName)
        {
            return await _db.Albums.AnyAsync(x=>x.Name == albumName.ToLower());
        }

        /*private async Task AssignArtistsToAlbumAsync(int albumId,List<int> artistIds)
        {
            artistIds = artistIds.Distinct().ToList();
            foreach (var artistId in artistIds)
            {
                var artist = await _db.Albums.FirstOrDefaultAsync(artistId);
                if(artist != null)
                {

                }

            }
        }*/
    }
}
