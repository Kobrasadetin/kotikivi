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
        private AI.PlayerAI ai;

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
            var homeposition = evaluator.calculatePositionWithHeight(ohMyGodHomeIsTheThemeHomeNode);
            Dynamic.PowerRune rune = new Dynamic.PowerRune(
                homeposition + new Vector3(.5f, 0f, 0f), 
                Graph.GraphSingleton.Instance.Graph);
            VisualInstanceFactory.CreateDynamicObject(this.transform, rune);

            VisualInstanceFactory.CreatePlayerCharacter(this.transform, homeposition);

            transform.gameObject.AddComponent<UIClickAction>();           
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}