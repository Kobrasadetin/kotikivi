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

        public GameObject prefab;

        public bool GetHeldByPlayer()
        {
            return heldByPlayer;
        }

        public void SetHeldByPlayer(bool value)
        {
            heldByPlayer = value;
        }

        public DynamicObject(Vector3 position)
        {
            this.position = position;
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

    }
}
