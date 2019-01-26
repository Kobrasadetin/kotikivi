using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dynamic;

public class VisualDynamicObject : MonoBehaviour
{
    private DynamicObject dynamicObject;
    private Vector3 visualPosition = Vector3.zero;

    public DynamicObject GetDynamicObject() {
        return dynamicObject;
    }

    public void Initialize(DynamicObject dynamicObject)
    {
        this.dynamicObject = dynamicObject;
        this.visualPosition = calculateCoordinates(dynamicObject.GetPosition());
    }

    private Vector3 calculateCoordinates(Vector3 worldPos)
    {
        return new Vector3(worldPos.x, worldPos.y * 3, worldPos.z);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!dynamicObject.GetHeldByPlayer())
        {
            visualPosition = calculateCoordinates(dynamicObject.GetPosition());
            transform.position = visualPosition;
        }
        else
        {
            //player dragging
            var targetPosition = calculateCoordinates(dynamicObject.GetPosition()) + new Vector3(0f,0.5f,0f);
            float heightLerp = Mathf.Lerp(visualPosition.y, targetPosition.y, 0.08f);
            Vector3 planeLerp = Vector3.Lerp(visualPosition, targetPosition, 0.4f);
            visualPosition = new Vector3(planeLerp.x, heightLerp, planeLerp.z);
            transform.position = visualPosition;

        }
        
    }

}
