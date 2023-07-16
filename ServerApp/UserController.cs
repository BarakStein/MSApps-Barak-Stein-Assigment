using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using ServerApp.Model;

namespace ServerApp
{
    //User controller class to preform all the CRUD functions
    public class UserController
    {
        //Partial object of DBService class that get the value of the connection string
        private static DBService _db = DBService.GetDBService();


        //Function that returns a list of all users from the DB
        public List<User> GetAllUsersFromDB(int id = 0)
        {
            string query = "SELECT * FROM Users";
            if (id != 0)
            {
                query += $" Where ID = {id}";
            }

            MySqlCommand cmd = _db.CreateCommand(query);
            DataTable dt = _db.Select(cmd);
            return _db.ConvertDataTable<User>(dt);
        }

        //Function to add new user to the DB
        public static void AddNewUser(User user)
        {
            string query = $@"Insert Into Users (Name, Email, Password)
                              Values (@Name, @Email, @Password)";
            MySqlCommand cmd = _db.CreateCommand(query);

            //Inserting the values from the object
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            _db.Insert(cmd);
        }

        //Function to update user details identify by his ID
        public static int UpdateUserDetails(int id, User user)
        {
            string query = $@"Update Users Set Name = @Name,
                                             Email = @Email,
                                             Password = @Password
                            Where ID = {id}";

            MySqlCommand cmd = _db.CreateCommand(query);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            return _db.Update(cmd);
        }

        //Function to delete a user from the DB identify by his ID
        public static int DeleteUser(int id)
        {
            string query = $@"Delete From Users Where ID = {id}";
            MySqlCommand cmd = _db.CreateCommand(query);
            return _db.Delete(cmd);
        }
    }
}
