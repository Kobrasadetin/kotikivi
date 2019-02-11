using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamic
{
    public abstract class DynamicObject
    {
        private Vector2Int graphCoordinates;
        private Vector3 position;
        private bool heldByPlayer;
        protected Graph.Graph graph;

        public Quaternion Orientation = Quaternion.identity;
        public GameObject prefab;

		public virtual bool IsPlayer(){
			return false;
		}

        public DynamicObject(Vector3 position, Graph.Graph graph)
        {
            this.position = position;
            this.graph = graph;
        }


        public virtual DynamicObject PickUpObject()
        {
            return null;
        }

        public virtual bool DropObject()
        {
            return false;
        }

        public bool GetHeldByPlayer()
        {
            return heldByPlayer;
        }

        public void SetHeldByPlayer(bool value)
        {
            heldByPlayer = value;
        }

        public Vector2Int GetGraphCoordinates()
        {
            return graphCoordinates;
        }

        protected void SetGraphCoordinates(Vector2Int value)
        {
            graphCoordinates = value;
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector3 value)
        {

            position = value;
        }

        public virtual void DynamicUpdate()
        {

        }

    }
}
