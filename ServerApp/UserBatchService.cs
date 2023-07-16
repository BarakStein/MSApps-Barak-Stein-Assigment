using MySql.Data.MySqlClient;
using ServerApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ServerApp
{
    public static class UserBatchService
    {
        //Partial object of DBService class that get the value of the connection string
        private static DBService _db = DBService.GetDBService();

        //Function that get the results from the DB
        //and printing all the data about the user for each user
        public static void PrintAllUsersFromDB()
        {
            try
            {
                UserController _userCon = new UserController();
                List<User> listOfUsers = _userCon.GetAllUsersFromDB();

                foreach (User user in listOfUsers)
                    Console.WriteLine($"ID: {user.ID}, Name: {user.Name}," +
                                      $" Email: {user.Email}," +
                                      $" Password: {user.Password}," +
                                      $" Sent Email: {user.EmailSent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to MySQL: " + ex.Message);
            }
        }
    }
}
