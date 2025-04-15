using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ActorsFilmListUsingADOnet
{
    internal class Menu
    {
        public ConnectionHandler MyConnection { get; }
        public Menu(ConnectionHandler connection)
        {
            MyConnection = connection;
            do
            {
                MainMenu();
            }
            while (Choice());

        }
        private void MainMenu()
        {
            Console.WriteLine("Welcome to Sakila database");
            Console.WriteLine("1. List Actors");
            Console.WriteLine("2. Choose actor to list movies");
            Console.WriteLine("3. Quit");

        }
        private bool Choice()
        {
            int choice;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Input integer please");
                return true;
            }
            switch (choice)
            {
                case 1:
                    Console.WriteLine(MyConnection.ListActors());
                    return true;
                case 2:
                    ChooseActor();
                    return true;
                case 3:
                    return false;
                default:
                    break;
            }
            return true;
        }
        private void ChooseActor()
        {
            Console.WriteLine("Write name of actor:");
            try
            {
                string? fullName = Console.ReadLine();
                string[] splitName = fullName.Split(' ');
                Console.WriteLine(MyConnection.ListMovies(splitName[0], splitName[1]));
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
        }
    }
}
