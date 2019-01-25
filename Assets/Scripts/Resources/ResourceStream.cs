using System.Collections.Generic;
using Graph;

namespace Resources
{
    public class ResourceStream
    {
        public GraphNode Source;
        public GraphNode Target;
        public List<ResourceTransfer> Transfers;
    }
}