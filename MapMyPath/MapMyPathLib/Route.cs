using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathLib
{
    public class Route
    {
        [Key]
        public Guid IdRoute { get; set; }
        public Guid UserId { get; set; }

    }
}
