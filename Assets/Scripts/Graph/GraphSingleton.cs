using UnityEngine;

namespace Graph
{
    public class GraphSingleton : MonoBehaviour
    {
        public static GraphSingleton Instance { get; private set; }

        public int Dimension;
        public float NoiseScale;
        public float NoiseOffset;
        public float ResourceSeepRate = 0.1f;
        public bool RandomResources = true;
        public Graph Graph;
        public GraphNode MiddleNode;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Graph = GraphFactory.CreateGraph(Dimension, NoiseScale, NoiseOffset, ResourceSeepRate, RandomResources, out MiddleNode);
        }
    }
}