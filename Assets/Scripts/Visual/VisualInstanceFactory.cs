using Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
    public class VisualInstanceFactory
    {
        static GameObject prefabResource = UnityEngine.Resources.Load<GameObject>("GroundNode");
        public static VisualNode CreateTile(Transform parentTransform, Graph.GraphNode node, BasicPrefabEvaluator evaluator)
        {
            GameObject newGO = Object.Instantiate<GameObject>(prefabResource, Vector3.zero, Quaternion.identity);
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
    }
}