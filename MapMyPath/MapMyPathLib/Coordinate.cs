using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathLib
{
    public class Coordinate
    {
        [Key]
        public Guid IdCoordinate { get; set; }
        public virtual Route Route { get; set; }
        public Guid RouteId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int StoppingOrder { get; set; }

    }
}
