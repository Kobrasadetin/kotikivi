using System.Collections.Generic;
using Graph;
using Interactions;
using UnityEngine;

namespace Tests
{
    public class GraphRunnerTest : MonoBehaviour
    {
        public GraphSingleton GraphSingleton;
        public InteractionLibrary InteractionLibrary;

        void Start()
        {
            // drop random interactions
            GraphSingleton.Graph.Nodes.ForEach(x =>
            {
                if (Random.Range(0f, 1f) < 0.25f)
                {
                    List<LibraryEntry> dependencies = new List<LibraryEntry>();
                    var randomInteraction = InteractionLibrary.GetRandomInteraction(out dependencies);
                    var resourceInteraction = new ResourceInteraction(randomInteraction, dependencies);
                    x.Interactions.Add(resourceInteraction);
                }
            });
        }

        private void Update()
        {
            int i = 2;
        }
    }
}