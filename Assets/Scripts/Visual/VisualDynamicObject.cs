using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dynamic;

public class VisualDynamicObject : MonoBehaviour
{
    private DynamicObject dynamicObject;
    public void Initialize(DynamicObject dynamicObject)
    {
        this.dynamicObject = dynamicObject;
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
            var wolrdPos = dynamicObject.GetPosition();
            transform.position = new Vector3(wolrdPos.x, wolrdPos.y * 3, wolrdPos.z);
        }
        else
        {
            var wolrdPos = dynamicObject.GetPosition();
            transform.position = new Vector3(wolrdPos.x, wolrdPos.y * 3 - 1, wolrdPos.z);
        }
        
    }

}
