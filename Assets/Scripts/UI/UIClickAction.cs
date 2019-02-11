using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{

    public class UIClickAction : MonoBehaviour
    {
		public enum CameraMode
		{
			FLOATING,
			CAT_TRACKING
		}
		public CameraMode cameraMode = CameraMode.FLOATING;
        public static float KEYS_MOVE_AXIS_SPEED_VERTICAL = .45f;
        public static float KEYS_MOVE_AXIS_SPEED_HORIZONTAL = .27f;
		public static float CAMERA_HIGH_ALTITUDE = 5.78f;
		public static float CAMERA_LOW_ALTITUDE = 1.01f;
		public static float TRACKING_DAMPING_DISTANCE = 2.8f;

		public float dragSpeed = 20;
        public float speedLimit = 0.19f;
        public static Vector3 CameraInertia = Vector3.zero;
        private Vector3 dragOrigin;
        bool cameraDragActive = false;
        public Quaternion RotInertia = Quaternion.identity;
        public Vector2 InputMousePositionOld;
        public bool rotationActive = false;
		private VisualDynamicObject trackedObject;

        PrefabChangeEvaluator prefabCE = new Visual.BasicPrefabEvaluator(null);
        // Start is called before the first frame update
        void Start()
        {
            Camera.main.transform.Translate(Global.GlobalVariables.GetHomeNodePosition(), Space.World);
        }
        Dynamic.DynamicObject dynamicObjectInHand;

        private void applyInertia(float multiplier)
        {
            if (CameraInertia.magnitude > speedLimit)
            {
                CameraInertia *= speedLimit / CameraInertia.magnitude;
            }
            var cameraPos = GetCameraPos();
            var homeDistance = (Global.GlobalVariables.GetHomeNodePosition() - cameraPos).magnitude;
            var homeDirection = (Global.GlobalVariables.GetHomeNodePosition() - cameraPos).normalized;
            homeDirection.Scale(new Vector3(1, 0, 1));

            if (homeDistance > Global.GlobalVariables.GetHomeNodePosition().x - 5f)
            {
                CameraInertia += homeDirection * homeDistance * 0.005f;
            }
            if (homeDistance > Global.GlobalVariables.GetHomeNodePosition().x - 3f)
            {
                CameraInertia += homeDirection * homeDistance * 0.035f;
                cameraPos += homeDirection * 0.05f;
            }

            //keypresses affect CameraInertia
            HandleKeypressActions();

            Camera.main.transform.Translate(CameraInertia, Space.World);
            CameraInertia = CameraInertia.magnitude < 0.01 ? Vector3.zero : CameraInertia * multiplier;
        }

		private void ChangeCameraMode(){
			if (cameraMode == CameraMode.FLOATING){
				cameraMode = CameraMode.CAT_TRACKING;
				AI.PlayerAI.Instance.SetControlMode(AI.PlayerAI.ControlMode.PLAYER);
			} else{
				cameraMode = CameraMode.FLOATING;
				AI.PlayerAI.Instance.SetControlMode(AI.PlayerAI.ControlMode.AI);
			}
		}

        public static Vector3 GetCameraPos()
        {
            //todo: ground mesh hitboxes
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            Plane m_Plane = new Plane(Vector3.up, new Vector3(0, Camera.main.transform.position.y - 2.2f, 0));
            float enter = 0.0f;
            if (m_Plane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                return ray.GetPoint(enter);
            }
            Debug.Log("Camera position not found");
            return Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //dragging an object
                if (dynamicObjectInHand != null)
                {
                    var VPos = dynamicObjectInHand.GetPosition();
                    Plane m_Plane = new Plane(Vector3.up, new Vector3(VPos.x, VPos.y * 3, VPos.z));
                    float enter = 0.0f;
                    if (m_Plane.Raycast(ray, out enter))
                    {
                        //Get the point that is clicked
                        Vector3 p = ray.GetPoint(enter);

                        //Move dynamicObjectInHand to the point
                        var oldPos = dynamicObjectInHand.GetPosition();
                        dynamicObjectInHand.SetPosition(new Vector3(p.x, oldPos.y, p.z));
                    }
                    return;
                }

                //clicking or dragging
                RaycastHit hit;
                if (cameraDragActive == false && Physics.Raycast(ray, out hit))
                {
                    VisualDynamicObject clickedObject = hit.transform.GetComponent<VisualDynamicObject>();
                    if (clickedObject != null)
                    {
						Dynamic.DynamicObject clickTarget = clickedObject.GetDynamicObject();
						if (clickTarget.IsPlayer())
						{
							trackedObject = clickedObject;
							ChangeCameraMode();
						}
						else
						{
							Dynamic.DynamicObject obj = clickedObject.GetDynamicObject().PickUpObject();
							if (obj != null)
							{
								dynamicObjectInHand = obj;
								Debug.Log("Object in hand");
							}
						}
                    } else {
						MouseDragMovement();
						VerticalMovement();
					}
                }
                else
                {
					MouseDragMovement();
					VerticalMovement();
				}

            }
            else
            {
                //mb0 not pressed

                rotationActive = false;
                cameraDragActive = false;
                applyInertia(0.9f);
                RotInertia = Quaternion.Lerp(RotInertia, Quaternion.identity, 0.3f);
                Camera.main.transform.Rotate(RotInertia.eulerAngles, Space.World);

                if (dynamicObjectInHand != null)
                {
                    dynamicObjectInHand.DropObject();
                    dynamicObjectInHand = null;
                }
				VerticalMovement();


			}
        }
        private void HandleKeypressActions()
        {
            Vector3 move = Vector3.zero;          
            move += Camera.main.transform.rotation * Vector3.forward * Input.GetAxis("Vertical") * KEYS_MOVE_AXIS_SPEED_VERTICAL;
            move -= Camera.main.transform.rotation * Vector3.left * Input.GetAxis("Horizontal") * KEYS_MOVE_AXIS_SPEED_HORIZONTAL;
            move.Scale(new Vector3(1, 0, 1));
            CameraInertia = (CameraInertia + move);           
        }

		private void VerticalMovement()
		{
			var targetHeight = CAMERA_HIGH_ALTITUDE;
			Vector3 pos = Camera.main.transform.position;
			if (cameraMode == CameraMode.CAT_TRACKING && trackedObject != null)
			{
				RaycastHit hit;
				Vector3 raypoint = Vector3.zero;
				var ray = new Ray(Camera.main.transform.position, Vector3.down);
				if (Physics.Raycast(ray, out hit))
				{
					raypoint = hit.point;
				}
				targetHeight = Mathf.Min(trackedObject.transform.position.y, raypoint.y) + CAMERA_LOW_ALTITUDE;
			}
			float newHeight = Mathf.Lerp(pos.y, targetHeight, .1f);
			Camera.main.transform.position = new Vector3(pos.x, newHeight, pos.z);
		}

		private void MouseDragMovement()
		{
			//camera drag
			if (cameraDragActive == false)
			{
				cameraDragActive = true;
				dragOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
				return;
			}

			//add turning
			if (Input.mousePosition.y > Screen.height / 2)
			{
				if (rotationActive)
				{
					float multi = Input.mousePosition.y / Screen.height * 0.08f;
					float diff = Input.mousePosition.x - InputMousePositionOld.x;
					Quaternion rot = Quaternion.Euler(0f, diff * multi, 0f);
					RotInertia = RotInertia * rot;
				}
				else
				{
					rotationActive = true;
				}
				InputMousePositionOld.x = Input.mousePosition.x;

			}
			else
			{
				rotationActive = false;
			}

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - dragOrigin;
			Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);
			move = Camera.main.transform.rotation * move;
			move.Scale(new Vector3(1, 0, 1));
			CameraInertia = (CameraInertia + move) / 2;

			Camera.main.transform.Translate((move + CameraInertia) / 2, Space.World);
			dragOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);

			RotInertia = Quaternion.Lerp(RotInertia, Quaternion.identity, 0.3f);
			Camera.main.transform.Rotate(RotInertia.eulerAngles, Space.World);
		}
	}
}
