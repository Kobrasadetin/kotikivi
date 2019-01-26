using System.Collections;
using System.Collections.Generic;
using Graph;
using UnityEngine;
using Resources;

namespace Visual
{
    [System.Serializable]
    public class BasicPrefabEvaluator : PrefabChangeEvaluator
    {
        private VisualizationLibrary library;
        static Color green = new Color(0.117f, 0.760f, 0.219f);
        static Color brown = new Color(0.670f, 0.639f, 0.607f);

        public BasicPrefabEvaluator(VisualizationLibrary library)
        {
            this.library = library;
        }

        public GameObject prefabObject;
        public override void UpdatePrefab(VisualNode visualNode, GraphNode node)
        {
            //update geometry
            float newHeight = node.Height;
            //avoid unnecessary recalculations
            if (newHeight != visualNode.SimulationHeight)
            {
                visualNode.SimulationHeight = newHeight;
            }

            //update visualizations
            visualNode.ResetInteractionTargets();
            node.Interactions.ForEach(x => visualNode.SetInteractionValue(x.Id, x.CurrentFlowRate));
            visualNode.ResetResourceVisualizationTargets();
            node.Resources.ForEach(x => visualNode.SetResourceVisualizaionValue(x.Type, x.Amount));
            visualNode.ResetStreamVisualizationTargets();
            node.Streams.ForEach(x =>
            {
                x.Interactions.ForEach(y => visualNode.SetStreamVisualizaionValue(y.Type, y.CurrentFlowRate));
                x.Transfers.ForEach(y => visualNode.SetStreamVisualizaionValue(y.Type, y.Amount));
            });

            //grassiness
            float grassiness = getMedian(node, new List<ResourceType> { ResourceType.NUTRIENT, ResourceType.WARMTH });
            Color newColor = Color.Lerp(brown, green, grassiness);
            visualNode.SetGroundColor(newColor);
        }

        private float getResource(GraphNode node, ResourceType type)
        {
            var a = node.Resources.Find(x => x.Type == type);
            if (a != null)
            {
                return a.Amount;
            }
            return 0f;
        }
        private float getMedian(GraphNode node, List<Resources.ResourceType> types)
        {
            float sum = 0;
            int count = 0;
            foreach (Resources.Resource r in node.Resources.FindAll(x => types.Contains(x.Type)))
            {
                sum += r.Amount;
                count++;
            }
            return sum/count;
        }

    }
}
