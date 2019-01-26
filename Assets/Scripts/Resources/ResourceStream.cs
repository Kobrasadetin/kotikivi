using System.Collections.Generic;
using Graph;

namespace Resources
{
    public class ResourceStream
    {
        public GraphNode Source;
        public GraphNode Target;
        public List<ResourceTransfer> Transfers = new List<ResourceTransfer>();
        public float Age;
        public float Life;

        public bool IsDead => Age > Life;

        public void Tick()
        {
            //TODO
        }
    }
}