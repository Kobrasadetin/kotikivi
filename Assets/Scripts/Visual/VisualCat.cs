using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCat : VisualDynamicObject
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AI.PlayerAI ai = AI.PlayerAI.Instance;

        if (ai != null)
        {
            Vector3 v = UI.UIClickAction.GetCameraPos();
            ai.SetTarget(v);
            ai.Update();
           
        }
        else
        {
            Debug.Log("no AI");
        }

        var visualPosition = calculateCoordinates(dynamicObject.GetPosition());
        transform.position = visualPosition;
        transform.rotation = dynamicObject.Orientation;
        
    }
}
