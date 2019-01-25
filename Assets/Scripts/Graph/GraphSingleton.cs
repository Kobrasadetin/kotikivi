using UnityEngine;

namespace Graph
{
    public class GraphSingleton : MonoBehaviour
    {
        public static GraphSingleton Instance { get; private set; }

        public int Dimension;
        public Graph Graph;
        public GraphNode MiddleNode;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Graph = GraphFactory.CreateGraph(Dimension, out MiddleNode);
        }
    }
}