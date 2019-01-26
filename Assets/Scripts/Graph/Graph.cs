using System.Collections.Generic;
using Resources;

namespace Graph
{
    /**
     * Nodes are linear list in two dimensions, but are rendered and interact in a hex grid
     */
    public class Graph
    {
        public List<GraphNode> Nodes = new List<GraphNode>();
        public List<ResourceStream> Streams = new List<ResourceStream>();

        public void Tick()
        {
            Streams.ForEach(x => x.Tick());
            Nodes.ForEach(x => x.Tick());
        }
    }
}