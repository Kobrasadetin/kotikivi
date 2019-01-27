using Dynamic;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace Visual
{
    public class VisualInstanceFactory
    {
        private static GameObject[] prefabResource = new GameObject[]
        {
            UnityEngine.Resources.Load<GameObject>("GroundNode 1"),
            UnityEngine.Resources.Load<GameObject>("GroundNode 1"),
            UnityEngine.Resources.Load<GameObject>("GroundNode 1"),
            UnityEngine.Resources.Load<GameObject>("GroundNode 1"),
        };

        private static GameObject homeResource = UnityEngine.Resources.Load<GameObject>("GroundNode Home");
        public static VisualNode CreateTile(Transform parentTransform, Graph.GraphNode node, BasicPrefabEvaluator evaluator)
        {
            GameObject newGO;
            if (node.Interactions.Exists(x => x.Type == InterActionType.HOME))
            {
                newGO = Object.Instantiate<GameObject>(homeResource, Vector3.zero, Quaternion.identity);
            }
            else
            {
                int randomPrefabType = Random.Range(0, prefabResource.Length);
                newGO = Object.Instantiate<GameObject>(prefabResource[randomPrefabType], Vector3.zero, Quaternion.identity);
            }
            newGO.transform.SetParent(parentTransform);

            VisualNode visualNode = newGO.GetComponent<VisualNode>();
            visualNode.Initialize(node, evaluator);
            return visualNode;
        }

        public static VisualDynamicObject CreateDynamicObject(Transform parentTransform, DynamicObject dynamicObject)
        {
            GameObject newGO = Object.Instantiate<GameObject>(dynamicObject.prefab, Vector3.zero, Quaternion.identity);
            newGO.transform.SetParent(parentTransform);

            VisualDynamicObject newVisual = newGO.AddComponent<VisualDynamicObject>();
            newVisual.Initialize(dynamicObject);
            return newVisual;

        }

        public static PlayerCharacter CreatePlayerCharacter(Transform parentTransform, Vector3 position)
        {
            Graph.Graph graph = Graph.GraphSingleton.Instance.Graph;
            PlayerCharacter pc = new PlayerCharacter(position, graph);
            GameObject newGO = Object.Instantiate<GameObject>(pc.prefab, Vector3.zero, Quaternion.identity);
            newGO.transform.SetParent(parentTransform);

            VisualDynamicObject newVisual = newGO.AddComponent<VisualCat>();
            newVisual.Initialize(pc);
            AI.PlayerAI ai = new AI.PlayerAI(pc);
            return null;
        }
    }
}