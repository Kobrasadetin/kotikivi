using System.Collections;
using System.Collections.Generic;
using Graph;
using UnityEngine;

namespace Visual
{
    [System.Serializable]
    public class BasicPrefabEvaluator : PrefabChangeEvaluator
    {
        public GameObject prefabObject;
        public override void UpdatePrefab(VisualNode visualNode, GraphNode node)
        {
            float newHeight = node.Height;
            //we avoid unnecessary geometry recalculations
            if (newHeight != visualNode.SimulationHeight)
            {
                visualNode.SimulationHeight = newHeight;
            }
        }
    }
}
