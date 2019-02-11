using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCat : VisualDynamicObject
{
	private Animator animator;
	private string[] IDLE_ANIMATIONS = { "Idle_A", "Idle_B" };
	private int current_idle_animation  = 0;
	private int animationTimer = 60;
    // Start is called before the first frame update
    void Start()
    {
		this.animator = gameObject.GetComponentInChildren<Animator>();
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
			AI.PlayerAI.Action action = ai.GetAction();
			if (action == AI.PlayerAI.Action.RUN){
				animator.SetFloat("runSpeed", 1.5f);
			}else{
				animator.SetFloat("runSpeed", 0);
			}
			if (animationTimer-- <= 0){
				current_idle_animation = (current_idle_animation + 1) % IDLE_ANIMATIONS.Length;
				animator.SetInteger("idleState", current_idle_animation);
				animationTimer = Random.Range(30, 250);
			}


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
