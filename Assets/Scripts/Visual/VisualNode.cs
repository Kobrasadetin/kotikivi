using Graph;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
    public class VisualNode : MonoBehaviour
    {
        public PrefabChangeEvaluator prefabChangeEvaluator;
        public GraphNode node;
        public GameObject groundMesh;
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
