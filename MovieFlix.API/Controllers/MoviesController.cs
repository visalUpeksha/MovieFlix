using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieFlix.Application.Interfaces;
using MovieFlix.Domain.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieFlix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }
        // GET: api/<MoviesController>
        [HttpGet]
        [Authorize]
        public ActionResult<List<Movie>> Get()
        {
            var moviesFromService = _service.GetAllMovies();
            return Ok(moviesFromService);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Movie> Post(Movie movie)
        {
            var moviesFromService = _service.CreateMovie(movie);
            return Ok(moviesFromService);
        }

    }
}
