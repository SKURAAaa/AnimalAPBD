namespace AnimalAPBD.Models.Repository;

public interface IAnimalRepository
{
    List<Animal> GetAnimals(string orderBy);
    void AddAnimal(Animal newAnimal);
    void UpdateAnimal(Animal updatedAnimal);
    Animal GetAnimalById(int id);
    void DeleteAnimalById(int id);
}