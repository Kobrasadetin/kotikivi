using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

namespace Visual
{
    public class VisualNodeFactory
    {
        private static GameObject[] prefabResource = new GameObject[]
        {
            UnityEngine.Resources.Load<GameObject>("GroundNode"),
            UnityEngine.Resources.Load<GameObject>("GroundNode"),
            UnityEngine.Resources.Load<GameObject>("GroundNode"),
            UnityEngine.Resources.Load<GameObject>("GroundNode"),
        };

        private static GameObject homeResource = UnityEngine.Resources.Load<GameObject>("GroundNode");
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
    }
}