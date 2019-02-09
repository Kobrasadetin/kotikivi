using System.Collections;
using System.Collections.Generic;
using Graph;
using UnityEngine;
using Resources;
using System;

namespace Visual
{
	[System.Serializable]
	public class BasicPrefabEvaluator : PrefabChangeEvaluator
	{
		private VisualizationLibrary library;
		static Color green = new Color(0.117f, 0.730f, 0.229f);
		static Color brown = new Color(0.510f, 0.199f, 0.187f);
		bool[] grassiness_resources;

		public BasicPrefabEvaluator(VisualizationLibrary library)
        {
            this.library = library;
			grassiness_resources = ResourcesAsValues(new ResourceType[]{ ResourceType.NUTRIENT, ResourceType.WARMTH });

		}

		static bool[] ResourcesAsValues(ResourceType[] resources)
		{
			bool[] values = new bool[Enum.GetNames(typeof(ResourceType)).Length];
			foreach (ResourceType resource in resources){
				values[(int)resource] = true;
			}
			return values;
		}

		public GameObject prefabObject;
        public override void UpdatePrefab(VisualNode visualNode, GraphNode node)
        {
			UnityEngine.Profiling.Profiler.BeginSample("UpdatePrefab geometry");
			//update geometry
			float newHeight = node.Height;
            //avoid unnecessary recalculations
            if (newHeight != visualNode.SimulationHeight)
            {
                visualNode.SimulationHeight = newHeight;
            }
			UnityEngine.Profiling.Profiler.EndSample();
			UnityEngine.Profiling.Profiler.BeginSample("UpdatePrefab visualizations");
			//update visualizations
			visualNode.ResetInteractionTargets();
            node.Interactions.ForEach(x => visualNode.SetInteractionValue(x.Id, x.CurrentFlowRate));
            visualNode.ResetResourceVisualizationTargets();
            node.Resources.ForEach(x => visualNode.SetResourceVisualizaionValue(x.Type, x.Amount));
            visualNode.ResetStreamVisualizationTargets();
            node.Streams.ForEach(x =>
            {
                //x.Interactions.ForEach(y => visualNode.SetStreamVisualizaionValue(y.Type, y.CurrentFlowRate));
                x.Transfers.ForEach(y => visualNode.SetStreamVisualizaionValue(y.Type, y.Amount));
            });
			UnityEngine.Profiling.Profiler.EndSample();
			UnityEngine.Profiling.Profiler.BeginSample("UpdatePrefab color");
			//grassiness
			float grassiness = getMedian(node, grassiness_resources);
            Color newColor = Color.Lerp(brown, green, grassiness);
            visualNode.SetGroundColor(newColor);
			UnityEngine.Profiling.Profiler.EndSample();
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
        private float getMedian(GraphNode node, bool[] types)
        {
            float sum = 0;
            int count = 0;
            foreach (Resource r in node.Resources)
            {
				if (types[(int)r.Type])
				{
					sum += r.Amount;
					count++;
				}
            }
            return sum/count;
        }

    }
}
