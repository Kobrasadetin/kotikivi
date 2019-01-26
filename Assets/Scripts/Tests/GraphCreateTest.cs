using System.Linq;
using Graph;
using UnityEngine;

namespace Tests
{
    public class GraphCreateTest : MonoBehaviour
    {
        void Start()
        {
            GraphNode midNode;
            var graph = GraphFactory.CreateGraph(5, out midNode);

            Debug.Assert(graph.Nodes.Count == 5 * 5);
            Debug.Assert(midNode != null);
            Debug.Assert(graph.Nodes[0].Neighbors.Count == 2);
            Debug.Assert(midNode.Neighbors.Count == 6);

            Debug.Log("Graph create test completed");
        }
    }
}