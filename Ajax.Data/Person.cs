using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ajax.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PersonDb
    {
        private readonly string _connectionString;

        public PersonDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Person person)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES " +
                             "(@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                conn.Open();
                person.Id = (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public List<Person> GetAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People";
                List<Person> ppl = new List<Person>();
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ppl.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }

                return ppl;
            }
        }
        public void Delete (int personId)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "delete from people where id = @id";
                cmd.Parameters.AddWithValue("@id", personId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Edit (int id, string firstName, string lastName, int age)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "update People set firstname=@firstName, lastname= @lastName, age=@age where Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@age", age);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
