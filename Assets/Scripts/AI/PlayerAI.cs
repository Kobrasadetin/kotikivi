using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PlayerAI
    {
        public static PlayerAI Instance;
        private Vector3 target;
        private Vector3 privateTarget;
        private Action currentAction = Action.IDLE;
        private Dynamic.PlayerCharacter playerCharacter;
        private Vector3 direction;
        private float boredness = 0f;

        public float goalDistance = 0.4f;
        public float maxDistance = 1.2f;
		private ControlMode controlMode;

		public ControlMode GetControlMode()
		{
			return controlMode;
		}

		public void SetControlMode(ControlMode value)
		{
			controlMode = value;
		}

		public enum ControlMode
		{
			PLAYER,
			AI
		}

        public enum Action
        {
            IDLE,
            RUN,
            PICKUP,
            CARRY,
            DROP
        }

        public PlayerAI(Dynamic.PlayerCharacter playerCharacter)
        {
            Instance = this;
            this.playerCharacter = playerCharacter;

        }
        public void SetTarget(Vector3 worldCoordinate)
        {
            this.target = worldCoordinate;
            if(distance2D(privateTarget, playerCharacter.GetPosition()) > maxDistance)
            {
                this.privateTarget = this.target + new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
            }
        }

        private float distance2D(Vector3 a, Vector3 b)
        {
            var planeTarget = a;
            planeTarget.y = 0;
            var planePos = b;
            planePos.y = 0;
            return (planeTarget - planePos).magnitude;
        }

        public void Update()
        {
            if (boredness > 0.6f)
            {
                privateTarget = target + new Vector3(Random.Range(maxDistance, maxDistance), 0f, Random.Range(maxDistance, maxDistance));
                AlterBoredness(-0.2f);
                Debug.Log("Bored cat.");
            }
            //Debug.Log("taget: " + target.ToString() + ", pos " + playerCharacter.GetPosition());
            var planeTarget = privateTarget;
            planeTarget.y = 0;
            var planePos = playerCharacter.GetPosition();
            planePos.y = 0;            
            if (distance2D(privateTarget, playerCharacter.GetPosition()) > goalDistance)
            {
                direction = (this.privateTarget - playerCharacter.GetPosition());
                direction.Scale(new Vector3(1, 0, 1));
                direction.Normalize();
                currentAction = Action.RUN;
                AlterBoredness(-0.05f);
            } else {
                currentAction = Action.IDLE;
                AlterBoredness(0.005f);
            }
            playerCharacter.DynamicUpdate();
        }

        public Action GetAction()
        {
            return currentAction;
        }

        public Vector3 GetDirection()
        {
            return direction;
        }

        private void AlterBoredness(float val)
        {
            boredness = Mathf.Clamp(boredness + val, 0f, 1f);
        }
    }
}