using System.Collections.Generic;
using System.Linq;

namespace Resources
{
    public class ResourceInteraction
    {
        public InterActionType Type;
        public List<ResourceTransfer> Sinks = new List<ResourceTransfer>();
        public List<ResourceTransfer> Sources = new List<ResourceTransfer>();
        public float Age;
        public float Life;

        public float FlowRate => Sinks.Sum(x => x.Amount) + Sources.Sum(y => y.Amount);
        public bool IsDead => Age > Life;
    }
}