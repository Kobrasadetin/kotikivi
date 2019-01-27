﻿using Graph;
using System.Collections.Generic;
using Interactions;
using Resources;
using UnityEngine;
using Visual.InteractionImplementations;

namespace Visual
{
    public class VisualNode : MonoBehaviour
    {
        public PrefabChangeEvaluator prefabChangeEvaluator;
        public GraphNode node;
        public GameObject groundMesh;
        public VisualInteraction[] visualInteractions;
        public VisualResource[] visualResources;
        public VisualStream[] visualStreams;

        private MeshRenderer groundMeshRenderer;
        private bool initialized;
        private bool geometryChanged;
        private float simulationHeight;
        private List<float> neighborHeights;

        public float SimulationHeight
        {
            get => simulationHeight;
            set { simulationHeight = value; geometryChanged = true; }
        }
        public List<float> NeighborHeights
        {
            get => neighborHeights;
            set { neighborHeights = value; geometryChanged = true; }

        }
        public void SetPosition(Vector2 position)
        {
            this.transform.position = new Vector3(position.x, 0, position.y);
        }
        public void SetVisualHeight(float height)
        {
            this.transform.position = new Vector3(transform.position.x, height * 3, transform.position.z);
        }
        public void ReCalculateGeometry()
        {
            this.simulationHeight = node.Height;
            SetVisualHeight(this.simulationHeight);
        }
        public void SetGroundColor(Color color)
        {
            groundMeshRenderer.material.SetColor("_Color", color);
        }
        public void ResetInteractionTargets()
        {
            foreach (var visualInteraction in visualInteractions)
            {
                visualInteraction.Target = 0;
            }
        }
        public void SetInteractionValue(string Id, float value)
        {
            foreach (var visualInteraction in visualInteractions)
            {
                if (visualInteraction.Id == Id)
                {
                    visualInteraction.Target = value;
                }
            }
        }
        public void ResetResourceVisualizationTargets()
        {
            foreach (var visualResource in visualResources)
            {
                visualResource.Target = 0;
            }
        }
        public void SetResourceVisualizaionValue(ResourceType type, float value)
        {
            foreach (var visualResource in visualResources)
            {
                if (visualResource.Type == type)
                {
                    visualResource.Target = value;
                }
            }
        }
        public void ResetStreamVisualizationTargets()
        {
            foreach (var visualStream in visualStreams)
            {
                visualStream.Target = 0;
            }
        }
        public void SetStreamVisualizaionValue(ResourceType type, float value)
        {
            foreach (var visualStream in visualStreams)
            {
                if (visualStream.Type == type)
                {
                    visualStream.Target = value;
                }
            }
        }
//        public void SetStreamVisualizaionValue(InterActionType type, float value)
//        {
//            foreach (var visualStream in visualStreams)
//            {
//                if (visualStream.IType == type)
//                {
//                    visualStream.Target = value;
//                }
//            }
//        }

        public void Initialize(GraphNode node, PrefabChangeEvaluator prefabChangeEvaluator)
        {
            this.node = node;
            this.prefabChangeEvaluator = prefabChangeEvaluator;
            SetPosition(this.prefabChangeEvaluator.calculatePosition(node));
            this.groundMesh = transform.Find("GroundMesh").gameObject;
            this.groundMeshRenderer = this.groundMesh.GetComponent<MeshRenderer>();
            if (groundMesh == null)
            {
                Debug.LogError("No GroundMesh");
            }
            initialized = true;
            geometryChanged = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            Update();
        }

        // Update is called once per frame
        void Update()
        {
            if (initialized && prefabChangeEvaluator != null)
            {
                if (geometryChanged)
                {
                    ReCalculateGeometry();
                }
                prefabChangeEvaluator.UpdatePrefab(this, node);
            }
            else
            {
                Debug.Log("UPDATE No change evaluator assigned for " + this.ToString());
            }
        }
    }
}
