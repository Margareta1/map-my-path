using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathLib
{
    public class Route
    {
        public Guid IdRoute { get; set; }
        public Guid UserId { get; set; }
        public virtual Coordinate StartPoint { get; set; }
        public Guid StartPointId { get; set; }
        public virtual Coordinate EndPoint { get; set; }
        public Guid EndPointId { get; set; }
        public virtual List<Coordinate> StoppingPoints { get; set; }
        public Route(Guid userId, Coordinate startPoint, Guid startPointId, Coordinate endPoint, Guid endPointId, List<Coordinate> stoppingPoints)
        {
            UserId = userId;
            StartPoint = startPoint;
            StartPointId = startPointId;
            EndPoint = endPoint;
            EndPointId = endPointId;
            StoppingPoints = stoppingPoints;
        }
    }
}
