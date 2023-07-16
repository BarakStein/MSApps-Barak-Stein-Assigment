using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ServerApp.Model;

namespace ServerApp
{

    //Class to interact with the DB
    public class UserDAL
    {
        ////Private partial DBService object
        //private DBService _db = DBService.GetDBService();

        ////Function to get all users from the DB order by their ID in ascending order
        //public List<User> GetAllUsers()
        //{
        //    string mySql = @"Select * From Users order by ID asc";

        //    MySqlCommand cmd = _db.CreateCommand(mySql);
        //    MySqlDataReader reader = cmd.ExecuteReader();
        //    List<User> users = new List<User>();
        //    while (reader.Read())
        //    {
        //        users.Add(reader.GetString(1));
        //    }
        //    reader.Close();
        //    return users;

        //}





    }
}
