using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");//Display system erros in English

            int option;
            string idToCheck;
            do
            {
                //Option menu
                Console.WriteLine("Hello! Please choose an option:\n-------------------------------");
                Console.Write(@"1. Print all users
2. Print one user
3. Add new user
4. Update user details
5. Delete user
6. Exit
Your choice: ");
                while (true)
                {
                    try
                    {
                        option = int.Parse(Console.ReadLine());
                        CheckChociceOptions(option);//Function call to check the user choice
                        Console.Clear();
                        break;
                    }
                    catch (OverflowException worngChoiceOptionE)
                    {
                        Console.WriteLine($"\n{worngChoiceOptionE.Message}\n\nPress any key to continue");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nException: {ex.Message}\n\nPress any key to continue");
                    }
                    Console.ReadKey();
                    Console.Clear();
                }

                switch (option)
                {
                    case 1:
                        UserBatchService.PrintAllUsersFromDB();
                        break;
                    case 2:
                        Console.Write("Enter ID number of user you want to see the details: ");
                        idToCheck = Console.ReadLine();
                        Console.WriteLine();
                        while (!IsIdValueValid(idToCheck))
                        {
                            Console.WriteLine(@"Please Enter a valid ID
Enter ID number of user you want to see the details: ");
                            idToCheck = Console.ReadLine();
                            Console.WriteLine();
                        }
                        PrintOneUserFromDB(int.Parse(idToCheck));
                        break;
                    case 3:
                        AddNewUserToDb(UserDetails());
                        Console.WriteLine("User added");
                        break;
                    case 4:
                        Console.Write("Enter ID number of user you want to update: ");
                        idToCheck = Console.ReadLine();
                        Console.WriteLine();
                        while (!IsIdValueValid(idToCheck))
                        {
                            Console.WriteLine(@"Please Enter a valid ID
Enter ID number of user you want to update: ");
                            idToCheck = Console.ReadLine();
                            Console.WriteLine();
                        }
                        int tempId = int.Parse(idToCheck);
                        UpdateUser(tempId, UserDetails(tempId, UserObject(tempId)));
                        break;
                    case 5:
                        Console.Write("Enter ID number of user you want to delete: ");
                        idToCheck = Console.ReadLine();
                        Console.WriteLine();
                        while (!IsIdValueValid(idToCheck))
                        {
                            Console.WriteLine(@"Please Enter a valid ID
Enter ID number of user you want to delete: ");
                            idToCheck = Console.ReadLine();
                        }
                        DeleteUserFromDB(int.Parse(idToCheck));
                        break;
                    case 6:
                        return;
                }
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
                Console.Clear();
            } while (true);
        }

        //Function that get the results from the DB
        //and printing all the data about a specific user
        public static void PrintOneUserFromDB(int id)
        {
            try
            {
                UserController _userCon = new UserController();
                List<User> listOfUsers = _userCon.GetAllUsersFromDB(id);
                foreach (User user in listOfUsers)
                    Console.WriteLine($"ID: {user.ID}, Name: {user.Name}," +
                                      $" Email: {user.Email}," +
                                      $" Password: {user.Password}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to MySQL: " + ex.Message);
            }
        }

        //Function to add new user to the DB
        public static void AddNewUserToDb(User user)
        {
            try
            {
                UserController.AddNewUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to MySQL: " + ex.Message);
            }
        }

        //Function to delete user from DB identify by his ID
        public static void DeleteUserFromDB(int id)
        {
            try
            {
                int rowEffected = UserController.DeleteUser(id);
                if (rowEffected > 0)
                    Console.WriteLine("\nUser deleted successfully");
                else
                    Console.WriteLine($@"The user with ID = {id} was not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to MySQL: " + ex.Message);
            }
        }


        //Function to update user details identify by his ID
        public static void UpdateUser(int id, User user)
        {
            try
            {
                int rowEffected = UserController.UpdateUserDetails(id, user);
                if (rowEffected > 0)
                {
                    Console.WriteLine("\nUser updated successfully");
                    PrintOneUserFromDB(id);

                }
                else
                    Console.WriteLine($@"The user with ID = {id} was not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to MySQL: " + ex.Message);
            }
        }



        //Function to create a new user object or update his information
        public static User UserDetails(int id = 0, User user = null)
        {
            string name, email, password;


            if (id != 0)
            {
                //Vars that stores the current value from the User object
                string nameToUpdate = user.Name, emailToUpdate = user.Email, passwordToUpdate = user.Password;
                Console.Write("Name: ");
                name = Console.ReadLine();

                Console.Write("Email: ");
                email = Console.ReadLine();

                Console.Write("Password: ");
                password = Console.ReadLine();

                if (name != "")
                    nameToUpdate = name;

                if (email != "")
                    emailToUpdate = email;

                if (password != "")
                    passwordToUpdate = password;

                return new User(nameToUpdate, emailToUpdate, passwordToUpdate);
            }
            else
            {
                Console.Write("Name: ");
                name = Console.ReadLine();

                Console.Write("Email: ");
                email = Console.ReadLine();

                Console.Write("Password: ");
                password = Console.ReadLine();

                //Loop to check if the user entered values to the fields
                while (name == "" || email == "" || password == "")
                {
                    if (name == "")
                    {
                        Console.Write("Re Enter name: ");
                        name = Console.ReadLine();
                    }
                    else if (email == "")
                    {
                        Console.Write("Re Enter email: ");
                        email = Console.ReadLine();
                    }
                    else
                    {
                        Console.Write("Re Enter password: ");
                        password = Console.ReadLine();
                    }
                }
                return new User(name, email, password);
            }
        }

        //Function to check if the user choice is valid or not
        public static int CheckChociceOptions(int choiceOption)
        {
            if (!(choiceOption >= 1 && choiceOption <= 9))
            {
                throw new OverflowException("Wrong choice please Enter a number from the menu.");
            }
            return choiceOption;//Returns a valid value
        }


        //Function to check if the user Entered a valid value for the ID
        public static bool IsIdValueValid(string id)
        {
            int number;
            return int.TryParse(id, out number);
        }


        //Function that returns single user object identify by ID
        private static User UserObject(int id)
        {
            User userObject = null;
            UserController _userCon = new UserController();
            List<User> listOfUsers = _userCon.GetAllUsersFromDB(id);
            foreach (User user in listOfUsers)
                userObject = new User(user.Name, user.Email, user.Password);
            return userObject;
        }
    }
}
