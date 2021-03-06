using System.Collections.Generic;
using Graph;
using Interactions;
using UnityEngine;

namespace OldTests
{
    public class GraphDebug : MonoBehaviour
    {
        public GraphNode Node;
    }

    public class GraphRunnerTest : MonoBehaviour
    {
        public GraphSingleton GraphSingleton;
        public InteractionLibrary InteractionLibrary;
        public bool GenerateDebugNodes;

        void Start()
        {
            if (GenerateDebugNodes)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i));
                }
            }
            // drop random interactions
            GraphSingleton.Graph.Nodes.ForEach(x =>
            {
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    List<LibraryEntry> dependencies = new List<LibraryEntry>();
                    var randomInteraction = InteractionLibrary.GetRandomInteraction(out dependencies);
                    var resourceInteraction = new ResourceInteraction(randomInteraction, dependencies);
                    x.Interactions.Add(resourceInteraction);
                }

                if (GenerateDebugNodes)
                {
                    var debug = new GameObject();
                    var debugComponent = debug.AddComponent<GraphDebug>();
                    debugComponent.Node = x;
                    debug.transform.SetParent(transform);
                }
            });
        }

        private void Update()
        {
            int i = 2;
        }
    }
}