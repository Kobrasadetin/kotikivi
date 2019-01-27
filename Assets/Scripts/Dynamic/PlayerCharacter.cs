using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamic
{
    public class PlayerCharacter : DynamicObject
    {
        public float RunSpeed = 0.035f;

        public PlayerCharacter(Vector3 position, Graph.Graph graph) : base(position, graph)
        { 
            this.prefab = UnityEngine.Resources.Load<GameObject>("Cat/Player");
        }

        public void DynamicUpdate()
        {
            AI.PlayerAI ai = AI.PlayerAI.Instance;
            if (ai.GetAction() == AI.PlayerAI.Action.RUN)
            {
                Quaternion targetOrient = Quaternion.LookRotation(ai.GetDirection(), Vector3.up);
                //todo: hills
                Orientation = Quaternion.Lerp(Orientation, targetOrient, 0.5f);
                Vector3 newPosition = this.GetPosition() + Orientation * Vector3.forward * RunSpeed;
                this.SetPosition(newPosition);
            }
           
        }
    }
}
