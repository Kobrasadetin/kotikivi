using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickAction : MonoBehaviour
{
    public float dragSpeed = 20;
    public float speedLimit = 0.19f;
    public static Vector3 CameraInertia = Vector3.zero;
    private Vector3 dragOrigin;
    bool cameraDragActive = false;


    PrefabChangeEvaluator prefabCE = new Visual.BasicPrefabEvaluator(null);
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Dynamic.DynamicObject dynamicObjectInHand;

    private void applyInertia(float multiplier)
    {
        if (CameraInertia.magnitude> speedLimit)
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

        Camera.main.transform.Translate(CameraInertia, Space.World);
        CameraInertia = CameraInertia.magnitude < 0.01 ? Vector3.zero : CameraInertia * multiplier;
    }

    public static Vector3 GetCameraPos()
    {
        //todo: ground mesh hitboxes
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        Plane m_Plane = new Plane(Vector3.up, new Vector3(0, 2.2f, 0));
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
                Plane m_Plane = new Plane(Vector3.up, new Vector3(VPos.x, VPos.y*3, VPos.z));
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
                    Dynamic.DynamicObject obj = clickedObject.GetDynamicObject().PickUpObject();
                    if (obj != null)
                    {
                        dynamicObjectInHand = obj;
                        Debug.Log("Object in hand");
                    }
                }
            }
            else
            {
                //camera drag
                if (cameraDragActive == false)
                {
                    cameraDragActive = true;
                    dragOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    return;
                }

                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - dragOrigin;
                Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);
                CameraInertia = (CameraInertia + move) / 2;

                Camera.main.transform.Translate((move + CameraInertia)/2, Space.World);
                dragOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }

        }
        else
        {
            //mb0 not pressed
            cameraDragActive = false;
            applyInertia(0.9f);
            if (dynamicObjectInHand != null)
            {
                dynamicObjectInHand.DropObject();
                dynamicObjectInHand = null;
            }



        }
    }
}
