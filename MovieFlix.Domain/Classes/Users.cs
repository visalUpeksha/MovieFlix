using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFlix.Domain.Classes
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }

}
