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
            Graph.Graph graph = GraphFactory.CreateGraph(5, 0, 0, out middleNode);
            graph.Nodes.ForEach(node =>
            {
                foreach (PowerLines.StreamAngle angle in (PowerLines.StreamAngle[])PowerLines.StreamAngle.GetValues(typeof(PowerLines.StreamAngle)))
                {
                    Assert.AreEqual(node.GetNeighborInDirection(angle), node.GetNeighborInDirectionDeprecated(angle));
                };

            });
        }
    }
}
