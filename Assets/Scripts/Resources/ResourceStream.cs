using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Transfers.ForEach(x =>
            {
                Target.Resources.First(y => y.Type == x.Type).Source(x.Amount);
            });
            Interactions.ForEach(x =>
            {
                if (!Target.Interactions.Exists(y => y.Id == x.Id))
                {
                    Target.Interactions.Add(x);
                }
            });
            Age += 1;
        }
    }
}