using System.Collections;
using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace Graph
{
    public class GraphNode
    {
        public Vector2Int Coordinate = Vector2Int.zero;
        public List<GraphNode> Neighbors = new List<GraphNode>();
        public List<Resource> Resources = new List<Resource>();
        public List<ResourceStream> ResourceStreams = new List<ResourceStream>();
        public List<ResourceInteraction> ResourceInteractions = new List<ResourceInteraction>();
    }
}
