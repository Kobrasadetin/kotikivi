﻿using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using Resources;
using UnityEngine;

namespace Graph
{
    public class GraphNode
    {
        public float Height = 0;
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

        public void Tick()
        {
            //TODO operate all streams
            ResourceInteractions.ForEach(x => x.Consume(Resources));
            ResourceInteractions.ForEach(x => x.Spawn(Neighbors));
        }
    }
}
