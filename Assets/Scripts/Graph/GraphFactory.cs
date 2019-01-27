using Interactions;
using Resources;
using UnityEngine;
using Utils;

namespace Graph
{
    public class GraphFactory
    {
        /**
         * Create rectangular graph that represents hexagon grid rendered in 1/2 square offset on each alternating row
         * Height from perlin noise
         */
        public static Graph CreateGraph(int dimension, float noiseScale, float noiseOffset, out GraphNode midNode)
        {
            Graph result = new Graph();
            // Nodes are linear list in two dimensions, but are rendered and interact in a hex grid

            for (int y = 0; y < dimension; y++)
            {
                for (int x = 0; x < dimension; x++)
                {
                    GraphNode thisNode = new GraphNode()
                    {
                        Coordinate = new Vector2Int(x, y)
                    };
                    thisNode.InitRandomResources();
                    thisNode.Height = Perlin.Noise(noiseOffset + noiseScale * x, noiseOffset + noiseScale * y) / 2 + 0.5f;
                    result.Nodes.Add(thisNode);
                }
            }

            for (int y = 0; y < dimension; y++)
            {
                for (int x = 0; x < dimension; x++)
                {
                    int index = y * dimension + x;
                    GraphNode thisNode = result.Nodes[index];
                    if (y % 2 == 0)
                    {
                        // even rows
                        if (y > 0)
                        {
                            if (x > 0)
                            {
                                int topleft = index - dimension - 1;
                                thisNode.Neighbors.Add(result.Nodes[topleft]);
                            }
                            int topright = index - dimension;
                            thisNode.Neighbors.Add(result.Nodes[topright]);
                        }
                        if (x > 0)
                        {
                            int left = index - 1;
                            thisNode.Neighbors.Add(result.Nodes[left]);
                        }
                        if (x < dimension - 1)
                        {
                            int right = index + 1;
                            thisNode.Neighbors.Add(result.Nodes[right]);
                        }
                        if (y < dimension - 1)
                        {
                            if (x > 0)
                            {
                                int bottomleft = index + dimension - 1;
                                thisNode.Neighbors.Add(result.Nodes[bottomleft]);
                            }
                            int bottomright = index + dimension;
                            thisNode.Neighbors.Add(result.Nodes[bottomright]);
                        }
                    }
                    else
                    {
                        // odd rows
                        if (y > 0)
                        {
                            int topleft = index - dimension;
                            thisNode.Neighbors.Add(result.Nodes[topleft]);
                            if (x < dimension - 1)
                            {
                                int topright = index - dimension + 1;
                                thisNode.Neighbors.Add(result.Nodes[topright]);
                            }
                        }
                        if (x > 0)
                        {
                            int left = index - 1;
                            thisNode.Neighbors.Add(result.Nodes[left]);
                        }
                        if (x < dimension - 1)
                        {
                            int right = index + 1;
                            thisNode.Neighbors.Add(result.Nodes[right]);
                        }
                        if (y < dimension - 1)
                        {
                            int bottomleft = index + dimension;
                            thisNode.Neighbors.Add(result.Nodes[bottomleft]);
                            if (x < dimension - 1)
                            {
                                int bottomright = index + dimension + 1;
                                thisNode.Neighbors.Add(result.Nodes[bottomright]);
                            }
                        }
                    }
                }
            }

            // Infinite resource hex is in the middle

            int midNodeIndex = dimension * (dimension / 2) + (dimension / 2) - 1;
            result.Nodes[midNodeIndex].Interactions.Add(new ResourceInteraction()
            {
                Type = InterActionType.HOME,
                Sources =
                {
                    new ResourceTransfer(){ Type = ResourceType.WATER, Amount = 100f},
                    new ResourceTransfer(){ Type = ResourceType.WARMTH, Amount = 100f},
                    new ResourceTransfer(){ Type = ResourceType.NUTRIENT, Amount = 100f},
                    new ResourceTransfer(){ Type = ResourceType.LIGTH, Amount = 100f},
                }
            });

            midNode = result.Nodes[midNodeIndex];
            midNode.Height = 0.55f;
            midNode.Neighbors.ForEach(x => x.Height = 0.55f);
            result.HomeNode = midNode;
            return result;
        }

    }
}