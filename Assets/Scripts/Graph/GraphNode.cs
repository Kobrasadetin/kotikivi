using System;
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
        [NonSerialized]
        public List<GraphNode> Neighbors = new List<GraphNode>();
        public List<Resource> Resources = new List<Resource>();
        [NonSerialized]
        public List<ResourceInteraction> Interactions = new List<ResourceInteraction>();
        [NonSerialized]
        public List<ResourceStream> Streams = new List<ResourceStream>();

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
            AddLight();
        }

        private void AddLight()
        {
            if (Resources.Exists(x => x.Type == ResourceType.LIGTH))
            {
                Resources.First(x => x.Type == ResourceType.LIGTH).Source(0.2f);
            }
        }

        private void AddGroundWater()
        {
            if (Resources.Exists(x => x.Type == ResourceType.WATER))
            {
                Resources.First(x => x.Type == ResourceType.WATER).Source(0.1f);
            }
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
