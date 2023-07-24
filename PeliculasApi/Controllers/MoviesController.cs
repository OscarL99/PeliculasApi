using PeliculasApi.Models;
using PeliculasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesService _moviesService;
        public MoviesController(MoviesService moviesService) => _moviesService = moviesService;

        [HttpGet]
        public async Task<List<Movie>> Get() => await _moviesService.GetAsync();
        
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Movie>>Get(string id)
        {
            var movie=await _moviesService.GetAsync(id);

            if (movie is null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult>Post(Movie newmovie)
        {
            await _moviesService.CreateAsync(newmovie);

            return CreatedAtAction(nameof(Get),new {id=newmovie.Id},newmovie);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult>Update(string id,Movie updateMovie)
        {
            var movie = await _moviesService.GetAsync(id);
            if(movie is null)
            {
                return NotFound();
            }
            updateMovie.Id=movie.Id;
            await _moviesService.UpdateAsync(id,updateMovie);
            return NoContent(); 
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult>Delete(string id)
        {
            var movie = await _moviesService.GetAsync(id);
            if(movie is null)
            {
                return NotFound();
            }
            await _moviesService.RemoveAsync(id);
            return NoContent(); 
        }

    }
}
