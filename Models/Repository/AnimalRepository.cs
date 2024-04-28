namespace AnimalAPBD.Models.Repository;

public class AnimalRepository : IAnimalRepository
    {
        public AnimalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;
        public List<Animal> GetAnimals(string orderBy){
            List<Animal> animals = new List<Animal>();

            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Animal ORDER BY " +orderBy +" ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Animal animal = new Animal
                            {
                                Id = Convert.ToInt32(reader["IdAnimal"]),
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                Description = reader["Description"].ToString(),
                                Area = reader["Area"].ToString()
                            };
                            animals.Add(animal);
                        }
                    }
                }
            }
            return animals;
        }
        public void AddAnimal(Animal newAnimal){
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Animal (Name, Category, Description, Area) VALUES (@Name, @Category, @Description, @Area)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", newAnimal.Name);
                    command.Parameters.AddWithValue("@Category", newAnimal.Category);
                    command.Parameters.AddWithValue("@Description", newAnimal.Description);
                    command.Parameters.AddWithValue("@Area", newAnimal.Area);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateAnimal(Animal updatedAnimal){
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Animal SET Name = @Name, Category = @Category, Description = @Description, Area = @Area WHERE IdAnimal = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", updatedAnimal.Id);
                    command.Parameters.AddWithValue("@Name", updatedAnimal.Name);
                    command.Parameters.AddWithValue("@Category", updatedAnimal.Category);
                    command.Parameters.AddWithValue("@Description", updatedAnimal.Description);
                    command.Parameters.AddWithValue("@Area", updatedAnimal.Area);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public Animal GetAnimalById(int id){
            Animal animal = null;
            
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Animal WHERE IdAnimal = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animal = new Animal
                            {
                                Id = Convert.ToInt32(reader["IdAnimal"]),
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                Description = reader["Description"].ToString(),
                                Area = reader["Area"].ToString()
                            };
                        }
                    }
                }
            }
            return animal;
        }
        public void DeleteAnimalById(int id) {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Animal WHERE IdAnimal = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}