using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Data;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SuperHero>> Get(int Id)
        {
            var hero = await _context.SuperHeroes.FindAsync(Id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.Id);

            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                hero.Name = request.Name;
                hero.FirstName = request.FirstName;
                hero.LastName = request.LastName;
                hero.Place = request.Place;
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }
        [HttpDelete("{Id}")]

        public async Task<ActionResult<SuperHero>> DelHero(int Id)
        {
            var hero = await _context.SuperHeroes.FindAsync(Id);

            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                _context.SuperHeroes.Remove(hero);
            }
            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }


    }
}
