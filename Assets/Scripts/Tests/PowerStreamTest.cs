using Graph;
using Interactions;
using PowerLines;
using Resources;
using UnityEngine;

namespace Tests
{
    public class PowerStreamTest : MonoBehaviour
    {
        public InteractionLibrary Library;
        public PowerRuneType[] Types;
        public StreamAngle Angle;
        public float Power;
        public float Gradient;

        [ContextMenu("Reset")]
        void Reset()
        {
            Graph.Graph graph = GraphSingleton.Instance.Graph;
            graph.Streams.Clear();

            if (Types.Length < 2 || Types.Length > 3)
            {
                return;
            }

            if (Types.Length == 2)
            {
                new PowerStream(graph, Library, Types[0], Types[1], Angle, Power, Gradient);
            }
            else
            {
                new PowerStream(graph, Library, Types[0], Types[1], Types[2], Angle, Power, Gradient);
            }
        }

    }
}