using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimalAPBD.Models.Repository;

namespace Animal.Controllers // Poprawiono nazwę przestrzeni nazw
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase // Poprawiono nazwę kontrolera
    {
        private readonly IConfiguration _configuration;
        private readonly IAnimalRepository _animalRepository;

        public AnimalsController(IConfiguration configuration, IAnimalRepository animalRepository)
        {
            _configuration = configuration;
            _animalRepository = animalRepository;
        }

        [HttpGet] // Poprawiono adres URL do /api/animals
        public IActionResult GetAnimals(string orderBy = "Name")
        {
            var animals = _animalRepository.GetAnimals(orderBy);
            return Ok(animals);
        }

        [HttpPost] // Poprawiono adres URL do /api/animals
        public IActionResult AddAnimal([FromBody] Animal newAnimal)
        {
            if (newAnimal == null) 
            {
                return BadRequest("Animal object is null");
            }
        
            _animalRepository.AddAnimal(newAnimal);
            return Ok("Animal added successfully");
        }

        [HttpPut("{idAnimal}")] // Poprawiono adres URL do /api/animals/{idAnimal}
        public IActionResult UpdateAnimal(int idAnimal, [FromBody] Animal updatedAnimal) 
        {
            if (updatedAnimal == null || idAnimal != updatedAnimal.Id)
            {
                return BadRequest("Invalid request data");
            }
            var existingAnimal = _animalRepository.GetAnimalById(idAnimal);

            if (existingAnimal == null)
            {
                return NotFound("Animal not found");
            }

            existingAnimal.Name = updatedAnimal.Name;
            existingAnimal.Category = updatedAnimal.Category;
            existingAnimal.Description = updatedAnimal.Description;
            existingAnimal.Area = updatedAnimal.Area;

            _animalRepository.UpdateAnimal(existingAnimal);

            return Ok("Animal updated successfully");
        }
    
        [HttpDelete("{idAnimal}")] // Poprawiono adres URL do /api/animals/{idAnimal}
        public IActionResult DeleteAnimal(int idAnimal) 
        {
            var existingAnimal = _animalRepository.GetAnimalById(idAnimal);

            if (existingAnimal == null)
            {
                return NotFound("Animal not found");
            }
            _animalRepository.DeleteAnimalById(idAnimal);
            return Ok("Animal deleted successfully");
        }
    }
}
