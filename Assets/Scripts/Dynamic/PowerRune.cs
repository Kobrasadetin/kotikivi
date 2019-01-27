using System;
using System.Collections;
using System.Collections.Generic;
using PowerLines;
using UnityEngine;

namespace Dynamic
{
    public class PowerRune : DynamicObject
    {
        public PowerRuneType RuneType;
        public Action<PowerRune> OnDroppedAction;

        public PowerRune(Vector3 position, Graph.Graph graph, PowerRuneType type) :base(position, graph)
        {
            this.prefab = GetPrefab(type);
            this.RuneType = type;
        }

        private GameObject GetPrefab(PowerRuneType runeType)
        {
            switch (runeType)
            {
                case PowerRuneType.RED:
                    return UnityEngine.Resources.Load<GameObject>("RuneStone_Red");
                case PowerRuneType.BLUE:
                    return UnityEngine.Resources.Load<GameObject>("RuneStone_Blue");
                default:
                case PowerRuneType.GREEN:
                    return UnityEngine.Resources.Load<GameObject>("RuneStone_Green");
                case PowerRuneType.WHITE:
                    return UnityEngine.Resources.Load<GameObject>("RuneStone_White");
            }
        }

        override public DynamicObject PickUpObject()
        {
            if (this.GetHeldByPlayer() == false)
            {
                this.SetHeldByPlayer(true);
                return this;
            }
            return null;
        }

        public override bool DropObject()
        {
            //go to center of hex
            if (this.GetHeldByPlayer())
            {
                this.SetHeldByPlayer(false);
                Vector2Int nearest = Visual.BasicPrefabEvaluator.s_nearestGraphCoordinate(GetPosition());
                Graph.GraphNode nearestNode = findNode(nearest);
                if(nearestNode != null){
                    Vector2 pos2d = Visual.BasicPrefabEvaluator.s_calculatePosition(nearest);
                    Vector3 pos = new Vector3(pos2d.x, nearestNode.Height, pos2d.y);
                    this.SetPosition(pos);
                    this.SetGraphCoordinates(nearest);
                }

                if (OnDroppedAction != null)
                {
                    OnDroppedAction.Invoke(this);
                }
            }
            return false;
        }

        private Graph.GraphNode findNode(Vector2Int coordinates)
        {
            return graph.Nodes.Find(x => x.Coordinate == coordinates);
        }
    }
}
