using Graph;
using System.Collections;
using System.Collections.Generic;
using Dynamic;
using PowerLines;
using UnityEngine;

namespace Visual
{
    public class VisualRoot : MonoBehaviour
    {
        public VisualizationLibrary visualizationLibrary;

        private Graph.Graph graph;
        private BasicPrefabEvaluator evaluator;
        private AI.PlayerAI ai;

        public static Dynamic.PowerRune[] PowerRunes = new PowerRune[4];

        // Start is called before the first frame update
        void Start()
        {
            evaluator = new BasicPrefabEvaluator(visualizationLibrary);
            graph = Graph.GraphSingleton.Instance.Graph;

            Vector2 homePos = evaluator.calculatePosition(graph.HomeNode.Coordinate);
            Global.GlobalVariables.SetHomeNodePosition(new Vector3(homePos.x, 0, homePos.y));

            graph.Nodes.ForEach(node =>
               {
                   VisualInstanceFactory.CreateTile(this.transform, node, evaluator);
               }
            );

            var ohMyGodHomeIsTheThemeHomeNode = graph.HomeNode;
            var homeposition = evaluator.calculatePositionWithHeight(ohMyGodHomeIsTheThemeHomeNode);

            CreatePowerRune(homeposition, new Vector3(.75f, 0f, 0f), PowerRuneType.GREEN);
            CreatePowerRune(homeposition, new Vector3(-.75f, 0f, 0f), PowerRuneType.BLUE);
            CreatePowerRune(homeposition, new Vector3(0f, 0f, .75f), PowerRuneType.RED);
            CreatePowerRune(homeposition, new Vector3(0f, 0f, -.75f), PowerRuneType.WHITE);

            VisualInstanceFactory.CreatePlayerCharacter(this.transform, homeposition);

            transform.gameObject.AddComponent<UI.UIClickAction>();
        }

        private void CreatePowerRune(Vector3 homeposition, Vector3 offset, PowerRuneType type)
        {
            Dynamic.PowerRune rune = new Dynamic.PowerRune(
                homeposition + offset,
                Graph.GraphSingleton.Instance.Graph, type);
            VisualInstanceFactory.CreateDynamicObject(this.transform, rune);
            PowerRunes[(int) type] = rune;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}