using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Model
{

    //User Class
    public class User
    {
        public int ID { get; set; }//Getter and Setter for ID attribute

        public string Name { get; set; }//Getter and Setter for Name attribute

        public string Email { get; set; }//Getter and Setter for Email attribute

        public string Password { get; set; }//Getter and Setter for Password attribute

        public string EmailSent { get; set; }//Getter and Setter to check if an email sent

        public User()//Default constructor to inisialize new user
        {

        }

        public User(string Name, string Email, string Password)//Constructor to create a new user
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }
    }
}
