using STGenetics_API.Dto;
using STGenetics_API.Entities;

namespace STGenetics_API.Contracts
{
    public interface IAnimalRepository
    {
        public Task<IEnumerable<Animal>> GetList();
        public Task<Animal> Get(int id);
        public Task<IEnumerable<Animal>> GetAnimalByFilters(int id, string name, string sex, string status);
        public Task<Animal> Create(AnimalDto animal);
        public Task Update(int id, AnimalDto animal);
        public Task Delete(int id);
    }   
}
