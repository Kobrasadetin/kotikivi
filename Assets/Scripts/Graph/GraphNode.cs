using System.Collections;
using System.Collections.Generic;
using Resources;
using UnityEngine;

namespace Graph
{

    public class GraphNode
    {
        public List<GraphNode> Neighbors;
        public List<Resource> Resources;
        public List<ResourceStream> ResourceStreams;
    }

}
