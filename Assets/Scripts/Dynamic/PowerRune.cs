using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamic
{
    public class PowerRune : DynamicObject
    {
        public PowerRune(Vector3 position, Graph.Graph graph) :base(position, graph)
        {
            this.prefab = UnityEngine.Resources.Load<GameObject>("RuneStone_1");
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
