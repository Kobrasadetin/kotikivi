using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamic
{
    public class PlayerCharacter : DynamicObject
    {
        public float RunSpeed = 0.035f;
        public bool jumping = false;
        public float jumpLerp = 0f;
        private float jstart;
        private float jend;
        private Vector3 foresightPosition; 

        public PlayerCharacter(Vector3 position, Graph.Graph graph) : base(position, graph)
        { 
            this.prefab = UnityEngine.Resources.Load<GameObject>("Cat/Player");
            foresightPosition = position;
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
                Vector3 fPos = ForesightPathPos(ai);
                if (jumping == false) {
                    Vector2Int c = Visual.BasicPrefabEvaluator.s_nearestGraphCoordinate(GetPosition());
                    Vector2Int p = Visual.BasicPrefabEvaluator.s_nearestGraphCoordinate(fPos);
                    float curHeight = graph.Nodes.Find(x => x.Coordinate == c).Height;
                    float nextHeight = graph.Nodes.Find(x => x.Coordinate == p).Height;
                    if (Mathf.Abs(curHeight - nextHeight) > 0.03f)
                    {
                        jumping = true;
                        jumpLerp = 0f;
                        jstart = curHeight;
                        jend = nextHeight;
                    }
                } else
                {
                    jumpLerp += 0.05f;
                    if (jumpLerp >= 1.0f)
                    {
                        jumping = false;
                    }
                    else
                    {
                        float linearHeight = Mathf.Lerp(jstart, jend, jumpLerp);
                        float pHeight = linearHeight + (0.5f - Mathf.Abs(jumpLerp - 0.5f))*0.4f;
                        Vector3 pos = GetPosition();
                        SetPosition(new Vector3(pos.x, pHeight, pos.z));
                    }
                }
            }

           
        }
        private Vector3 ForesightPathPos(AI.PlayerAI ai)
        {
            Quaternion foreOrient = Quaternion.LookRotation(ai.GetDirection(), Vector3.up);
            Vector3 forePosition = GetPosition();

            for (int frame=0; frame < 8; frame++)
            {
                Quaternion targetOrient = Quaternion.LookRotation(ai.GetDirection(), Vector3.up);
                Orientation = Quaternion.Lerp(Orientation, targetOrient, 0.5f);
                forePosition = this.GetPosition() + Orientation * Vector3.forward * RunSpeed;
            }
            return forePosition;

        }
    }
}
