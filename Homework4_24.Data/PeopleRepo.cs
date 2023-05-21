using System.Data.SqlClient;

namespace Homework4_24.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PeopleRepo
    {
        private string _connectionString;

        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var list = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                });
            }

            return list;
        }


        public void AddPerson(Person person)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO People(FirstName, LastName, Age) VALUES (@firstName, @lastName, @age);";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();

        }

        public void DeletePersonById(int personId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "Delete from People Where Id = @id";
            command.Parameters.AddWithValue("@id", personId);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE Id = @id; ";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            command.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            command.ExecuteNonQuery();

        }

        public Person GetPersonById(int personId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "Select * from People where id = @id";
            command.Parameters.AddWithValue("@id", personId);
            connection.Open();
            var reader = command.ExecuteReader();
            reader.Read();
            return new Person
            {
                Id = (int)reader["id"],
                FirstName = (string)reader["firstName"],
                LastName = (string)reader["lastName"],
                Age = (int)reader["age"]
            };

        }
        

    }
}