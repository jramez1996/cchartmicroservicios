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
                            person.id = (int)reader["id"];
                            person.nombre = (string)reader["nombre"];
                            person.edad = (int)reader["edad"];

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
                    command.Parameters.AddWithValue("@nombre", persona.nombre);
                    command.Parameters.AddWithValue("@edad", persona.edad);
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
                command.Parameters.AddWithValue("@Nombre", persona.nombre);
                command.Parameters.AddWithValue("@Edad", persona.edad);
                command.Parameters.AddWithValue("@Id", persona.id);
                command.ExecuteNonQuery();
            }
        }
    }

}