using Microsoft.AspNetCore.Mvc;
using STGenetics_API.Contracts;
using STGenetics_API.Dto;
using STGenetics_API.Entities;

namespace STGenetics_API.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : Controller
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalsController(IAnimalRepository animalRepository) => _animalRepository = animalRepository;

        [HttpGet("get-list")]
        public async Task<IActionResult> GetList() 
        {
            var animals = await _animalRepository.GetList();
            
            return Ok(animals);
        }

        [HttpGet("animals-by-filters")]
        public async Task<ActionResult<AnimalByFiltersRequest>> AnimalsByFilters([FromQuery] AnimalByFiltersRequest filters) 
        {
            var animals = await _animalRepository.GetAnimalByFilters(filters.Animal_Id, filters.Name, filters.Sex, filters.Status);
            if (animals is null)
                return NotFound();

            return Ok(animals);
        }


        [HttpPost("create")]
        public async Task<ActionResult<Animal>> Create([FromBody] AnimalDto animal)
        {
            var newAnimal = await _animalRepository.Create(animal);

            return Ok(newAnimal);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] AnimalDto animal) 
        {
            var animalDb = await _animalRepository.Get(id);
            if (animalDb is null)
                return NotFound();

            await _animalRepository.Update(id, animal);

            return Ok(animalDb.Animal_Id);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id) 
        {
            var animalDb = await _animalRepository.Get(id);
            if (animalDb is null)
                return NotFound();

            await _animalRepository.Delete(id);

            return NoContent();
        }
    }
}
