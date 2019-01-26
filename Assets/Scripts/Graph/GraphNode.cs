using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
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

        public void InitRandomResources()
        {
            for (int i=0; i<Enum.GetValues(typeof(ResourceType)).Length; i++)
            {
                Resources.Add(Resource.GetRandom((ResourceType)i));
            }
        }
    }
}
