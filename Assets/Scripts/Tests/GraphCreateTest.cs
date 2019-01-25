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
        }
    }
}