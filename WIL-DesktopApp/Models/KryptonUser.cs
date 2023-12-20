using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIL_DesktopApp.Models
{
    public class KryptonUser
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public KryptonUser(string username, string firstName, string lastName, string email, int type)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Type = type;
        }
    }
}
