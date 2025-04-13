using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Configuration;

namespace ActorsFilmListUsingADOnet
{
    internal class ConnectionHandler
    {
        public string? ConnectionString { get; }
        public ConnectionHandler()
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            ConnectionString = configuration.GetConnectionString("Sakila");
            Menu menu = new Menu(this);
        }
        public void ListActors()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT first_name, last_name " +
                    "FROM actor", connection);
                var receiver = command.ExecuteReader();
                while (receiver.Read()) Console.WriteLine($"{receiver[0]} {receiver[1]}");
            }
        }
        public void ListMovies(string firstName, string lastName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT film.title " +
                    "FROM actor " +
                    "INNER JOIN film_actor on film_actor.actor_id=actor.actor_id " +
                    "INNER JOIN film on film.film_id=film_actor.film_id " +
                    "WHERE first_name = @firstName and last_name = @lastName", connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                var receiver = command.ExecuteReader();
                while (receiver.Read()) Console.WriteLine(receiver[0]);
            }
        }
    }
}
