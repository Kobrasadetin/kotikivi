using System.Collections;
using System.Collections.Generic;
using Graph;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GraphTests
    {

        [Test]
        public void GraphCreationAndNeighbors()
        {
            GraphNode middleNode;
            Graph.Graph graph = GraphFactory.CreateGraph(5, 0, 0, 0, false, out middleNode);
            graph.Nodes.ForEach(node =>
            {
                foreach (PowerLines.StreamAngle angle in (PowerLines.StreamAngle[])PowerLines.StreamAngle.GetValues(typeof(PowerLines.StreamAngle)))
                {
                    #pragma warning disable 612, 618
                    Assert.AreEqual(node.GetNeighborInDirection(angle), node.GetNeighborInDirectionDeprecated(angle));
                    #pragma warning restore 612, 618
                };

            });
        }
    }
}
