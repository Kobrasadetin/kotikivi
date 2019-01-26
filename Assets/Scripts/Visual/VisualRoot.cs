using Graph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
    public class VisualRoot : MonoBehaviour
    {
        public VisualizationLibrary visualizationLibrary;
        public GameObject runePrefab;

        private Graph.Graph graph;
        private BasicPrefabEvaluator evaluator;

        // Start is called before the first frame update
        void Start()
        {
            evaluator = new BasicPrefabEvaluator(visualizationLibrary);
            graph = Graph.GraphSingleton.Instance.Graph;
            graph.Nodes.ForEach(node =>
               {
                   VisualInstanceFactory.CreateTile(this.transform, node, evaluator);
               }
            );
          
            var ohMyGodHomeIsTheThemeHomeNode = graph.HomeNode;
            Dynamic.PowerRune rune = new Dynamic.PowerRune(
                evaluator.calculatePositionWithHeight(ohMyGodHomeIsTheThemeHomeNode));
            VisualInstanceFactory.CreateDynamicObject(this.transform, rune);

            transform.gameObject.AddComponent<UIClickAction>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}