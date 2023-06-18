using Dapper;
using STGenetics_API.Context;
using STGenetics_API.Contracts;
using STGenetics_API.Dto;
using STGenetics_API.Entities;
using System.Xml.Linq;

namespace STGenetics_API.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly DapperContext _context;

        public AnimalRepository(DapperContext context) => _context = context;

        public async Task<Animal> Create(AnimalDto animal)
        {
            var query = @"INSERT INTO animals (name, breed, birth_date, sex, price, status)
                        VALUES (@name, @breed, @birth_date, @sex, @price, @status);
                        SELECT SCOPE_IDENTITY() AS AnimalID;";


            var parameters = new DynamicParameters();
            parameters.Add("@name", animal.Name);
            parameters.Add("@breed", animal.Breed);
            parameters.Add("@birth_date", animal.BirthDate);
            parameters.Add("@sex", animal.Sex);
            parameters.Add("@price", animal.Price);
            parameters.Add("@status", animal.Status);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdAnimal = new Animal
                {
                    Animal_Id = id,
                    Name = animal.Name,
                    Breed = animal.Breed,   
                    BirthDate = animal.BirthDate,   
                    Sex = animal.Sex,   
                    Price = animal.Price,
                    Status = animal.Status
                };
                
                return createdAnimal;
            }
        }

        public async Task Delete(int animal_id)
        {
            var query = @"DELETE FROM animals WHERE animal_id = @animal_id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { animal_id });
            }
        }

        public async Task<Animal> Get(int animal_id)
        {
            var query = @"SELECT * FROM animals WHERE animal_id = @animal_id";

            using (var connection = _context.CreateConnection())
            {
                var animal = await connection.QuerySingleOrDefaultAsync<Animal>(query, new { animal_id });

                return animal;
            }
        }

        public async Task<IEnumerable<Animal>> GetAnimalByFilters(int id, string name, string sex, string status)
        {
            var query = @"SELECT * FROM animals WHERE animal_id = @animal_id OR name = @name OR sex = @sex OR status = @status;";

            var parameters = new DynamicParameters();
            parameters.Add("@animal_id", id);
            parameters.Add("@name", name);
            parameters.Add("@sex", sex);
            parameters.Add("@status", status);

            using (var connection = _context.CreateConnection())
            {
                var animals = await connection.QueryAsync<Animal>(query, parameters);

                return animals.ToList();
            }
        }

        public async Task<IEnumerable<Animal>> GetList()
        {
            var query = @"SELECT * FROM animals";

            using (var connection = _context.CreateConnection())
            {
                var animals = await connection.QueryAsync<Animal>(query);

                return animals.ToList();
            }
        }

        public async Task Update(int id, AnimalDto animal)
        {
            var query = @"UPDATE animals
                        SET name = @name, breed = @breed, birth_date = @birth_date, sex = @sex, price = @price, status = @status
                        WHERE animal_id = @animal_id;";

            var parameters = new DynamicParameters();
            parameters.Add("@animal_id", id);
            parameters.Add("@name", animal.Name);
            parameters.Add("@breed", animal.Breed);
            parameters.Add("@birth_date", animal.BirthDate);
            parameters.Add("@sex", animal.Sex);
            parameters.Add("@price", animal.Price);
            parameters.Add("@status", animal.Status);

            using (var connection = _context.CreateConnection()) 
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
