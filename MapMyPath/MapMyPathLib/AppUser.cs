using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathLib
{
    public class AppUser
    {
        public Guid Id { get; set; }
        [DisplayName("First name:")]
        public string FirstName { get; set; }
        [DisplayName("Last name:")]
        public string LastName { get; set; }
        [DisplayName("Username:")]
        public string UserName { get; set; }
        [DisplayName("Password:")]
        public string Password { get; set; }
      
        public int IsDeleted { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
    }
}
