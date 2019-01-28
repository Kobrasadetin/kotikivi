using System;
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
        public List<Resource> Resources = new List<Resource>();
        [NonSerialized]
        public NeighborList<GraphNode> Neighbors = new NeighborList<GraphNode>();
        [NonSerialized]
        private NeighborList<GraphNode> AccessibleNeighbors = new NeighborList<GraphNode>();
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
            Interactions.ForEach(x => x.Spawn(Neighbors.ToList()));
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
            return Neighbors.GetNeigbor(direction);
        }

        [Obsolete("Deprecated")]
        public GraphNode GetNeighborInDirectionDeprecated(StreamAngle direction)
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
        }

        public void SetAccessibleNeighbor(StreamAngle direction)
        {
            GraphNode neighbor = Neighbors.GetNeigbor(direction);
            if (neighbor!=null)
            {
                AccessibleNeighbors.SetNeighbor(direction, neighbor);
            }else
            {
                throw new UnassignedReferenceException("no neighbor at " + direction.ToString() + " for node " + this.ToString());
            }
        }
    }
}
