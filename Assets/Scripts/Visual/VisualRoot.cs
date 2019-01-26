using Graph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
    public class VisualRoot : MonoBehaviour
    {
        public VisualizationLibrary visualizationLibrary;

        private Graph.Graph graph;
        private BasicPrefabEvaluator evaluator;

        // Start is called before the first frame update
        void Start()
        {
            evaluator = new BasicPrefabEvaluator(visualizationLibrary);
            graph = Graph.GraphSingleton.Instance.Graph;
            graph.Nodes.ForEach(node =>
               {
                   VisualNodeFactory.CreateTile(this.transform, node, evaluator);
               }
            );
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}