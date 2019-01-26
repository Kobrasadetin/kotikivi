using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
    public class VisualNodeFactory
    {
        static BasicPrefabEvaluator prefabEvaluator = new BasicPrefabEvaluator();
        static GameObject prefabResource = UnityEngine.Resources.Load<GameObject>("GroundNode");
        public static VisualNode CreateTile(Transform parentTransform, Graph.GraphNode node)
        {
            GameObject newGO = Object.Instantiate<GameObject>(prefabResource, Vector3.zero, Quaternion.identity);
            newGO.transform.SetParent(parentTransform);

            VisualNode visualNode = newGO.GetComponent<VisualNode>();
            visualNode.Initialize(node, prefabEvaluator);
            return visualNode;
        }
    }
}
