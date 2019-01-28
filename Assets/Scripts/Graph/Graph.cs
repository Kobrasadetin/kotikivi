using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace Graph
{
    /**
     * Nodes are linear list in two dimensions, but are rendered and interact in a hex grid
     */
    public class Graph
    {
        public GraphNode HomeNode;
        public List<GraphNode> Nodes = new List<GraphNode>();
        public Dictionary<Vector2Int, GraphNode> NodeAtCoordinates = new Dictionary<Vector2Int, GraphNode>();
        public List<ResourceStream> Streams = new List<ResourceStream>();

        public void Tick()
        {
            Streams.ForEach(x => x.Tick());
            Nodes.ForEach(x => x.Tick());
        }
    }
}