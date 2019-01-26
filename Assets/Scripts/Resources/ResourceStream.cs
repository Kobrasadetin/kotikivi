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
        public List<InteractionSpawn> Spawns = new List<InteractionSpawn>();
        public float Age = 0;

        public void Tick()
        {
            Transfers.ForEach(x => { Target.Resources.First(y => y.Type == x.Type).Amount += x.Amount; });
            Age += 1;
        }
    }
}