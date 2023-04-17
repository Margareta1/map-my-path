using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MapMyPathCore.Areas.Identity.Data;

public class MapMyPathCoreUser : IdentityUser
{
    public string Password { get; set; }
    public int IsDeleted { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
}

