using System.Collections.Generic;
using System.Linq;
using Graph;
using Interactions;

namespace Resources
{
    public class ResourceStream
    {
        public GraphNode Target;
        public List<ResourceTransfer> Transfers = new List<ResourceTransfer>();
        public List<ResourceInteraction> Interactions = new List<ResourceInteraction>();
        public float Age = 0;

        public void Tick()
        {
            Transfers.ForEach(x => { Target.Resources.First(y => y.Type == x.Type).Amount += x.Amount; });
            // TODO add interaction if not here
            Age += 1;
        }
    }
}