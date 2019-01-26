using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickAction : MonoBehaviour
{
    public float dragSpeed = 2;
    public Vector3 inertia = Vector3.zero;
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
        Camera.main.transform.Translate(inertia, Space.World);
        inertia = inertia.magnitude < 0.01 ? Vector3.zero : inertia * multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            applyInertia(0.4f);
            Debug.Log("enter");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //dragging an object
            if (dynamicObjectInHand != null)
            {
                Debug.Log("dragging");
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
                    Debug.Log("2");
                    return;
                }
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
                if (Input.GetMouseButtonDown(0))
                {
                    cameraDragActive = true;
                    dragOrigin = Input.mousePosition;
                    return;
                }

                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
                Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);
                inertia = (inertia + move) / 2;

                Camera.main.transform.Translate(move, Space.World);
            }

        }
        else
        {
            //mb0 not pressed
            cameraDragActive = false;
            applyInertia(0.9f);
            if (dynamicObjectInHand != null)
            {
                dynamicObjectInHand.SetHeldByPlayer(false);
                dynamicObjectInHand = null;
            }



        }
    }
}
