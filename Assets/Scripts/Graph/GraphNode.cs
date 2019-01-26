﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactions;
using PowerLines;
using Resources;
using UnityEngine;

namespace Graph
{
    [Serializable]
    public class GraphNode
    {
        public float Height = 0;
        public Vector2Int Coordinate = Vector2Int.zero;
        public List<GraphNode> Neighbors = new List<GraphNode>();
        public List<Resource> Resources = new List<Resource>();
        public List<ResourceInteraction> Interactions = new List<ResourceInteraction>();

        public void InitRandomResources()
        {
            for (int i=0; i<Enum.GetValues(typeof(ResourceType)).Length; i++)
            {
                Resources.Add(Resource.GetRandom((ResourceType)i));
            }
        }

        public void Tick()
        {
            Interactions.ForEach(x => x.Consume(Resources));
            Interactions.ForEach(x => x.Spawn(Neighbors));
            Interactions.ForEach(x => x.Tick());
        }

        public GraphNode GetNeighborInDirection(StreamAngle direction)
        {
            if (Coordinate.y % 2 == 0)
            {
                // even lines
                switch (direction)
                {
                    case StreamAngle.LEFT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x - 1 && x.Coordinate.y == Coordinate.y);
                    case StreamAngle.RIGHT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x + 1 && x.Coordinate.y == Coordinate.y);
                    case StreamAngle.UPLEFT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x - 1 && x.Coordinate.y == Coordinate.y - 1);
                    case StreamAngle.UPRIGHT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x && x.Coordinate.y == Coordinate.y - 1);
                    case StreamAngle.DOWNLEFT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x - 1 && x.Coordinate.y == Coordinate.y + 1);
                    case StreamAngle.DOWNRIGHT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x && x.Coordinate.y == Coordinate.y + 1);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }
            else
            {
                // odd lines
                switch (direction)
                {
                    case StreamAngle.LEFT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x - 1 && x.Coordinate.y == Coordinate.y);
                    case StreamAngle.RIGHT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x + 1 && x.Coordinate.y == Coordinate.y);
                    case StreamAngle.UPLEFT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x && x.Coordinate.y == Coordinate.y - 1);
                    case StreamAngle.UPRIGHT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x + 1 && x.Coordinate.y == Coordinate.y - 1);
                    case StreamAngle.DOWNLEFT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x && x.Coordinate.y == Coordinate.y + 1);
                    case StreamAngle.DOWNRIGHT:
                        return Neighbors.FirstOrDefault(x =>
                            x.Coordinate.x == Coordinate.x + 1 && x.Coordinate.y == Coordinate.y + 1);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }

            return null;
        }
    }
}
