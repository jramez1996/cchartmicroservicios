using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplicationPortafolio.Models;

namespace WebApplicationPortafolio.DataAccessLayer
{
    public class PersonDataAccess
    {
        public List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();

            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                string query = "SELECT id, nombre, edad FROM persona";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Person person = new Person();
                            person.Id = (int)reader["id"];
                            person.Nombre = (string)reader["nombre"];
                            person.Edad = (int)reader["edad"];

                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }

        public void InsertarPersona(Person persona)
        {
            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                string query = "INSERT INTO persona (nombre, edad) VALUES (@nombre, @edad)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@edad", persona.Edad);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarPersona(Person persona)
        {
            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                string query = "UPDATE Persona SET Nombre = @Nombre, Edad = @Edad WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Edad", persona.Edad);
                command.Parameters.AddWithValue("@Id", persona.Id);
                command.ExecuteNonQuery();
            }
        }
    }

}