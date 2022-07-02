using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.Application.GenreOperations.Command.UpdateGenreCommand;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_dbContext, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreCommand.CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenreModel)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = id;
            command.Model = updateGenreModel;


            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
