using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class GameObjectScaleInteraction : VisualInteraction
    {
        public Transform[] Transforms;
        private Vector3[] MaxScale;
        private bool activeObject = true;

        public void Start()
        {
            MaxScale = new Vector3[Transforms.Length];
            for (int i = 0; i < Transforms.Length; i++)
            {
                MaxScale[i] = Transforms[i].localScale;
            }
        }

        public override void SetValue()
        {
			UnityEngine.Profiling.Profiler.BeginSample("GameObjectScaleInteraction Update");
			bool shouldActivate = (Value > 0.01f);

            if ((!activeObject) && shouldActivate)
            {
                activeObject = true;
                for (int i = 0; i < Transforms.Length; i++)
                {
                    Transforms[i].gameObject.SetActive(true);
                }
            }
            if (activeObject && (!shouldActivate))
            {
                activeObject = false;
                for (int i = 0; i < Transforms.Length; i++)
                {
                    Transforms[i].gameObject.SetActive(false);
                }
            }
            if (activeObject)
            {
                for (int i = 0; i < Transforms.Length; i++)
                {
                    Transforms[i].localScale = MaxScale[i] * Value;
                }
            }
			UnityEngine.Profiling.Profiler.EndSample();
		}
    }
}