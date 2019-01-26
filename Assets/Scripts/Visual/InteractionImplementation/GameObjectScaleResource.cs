using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class GameObjectScaleResource : VisualResource
    {
        public Transform[] Transforms;
        private Vector3[] MaxScale;

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
            for (int i=0; i<Transforms.Length; i++)
            {
                Transforms[i].localScale = MaxScale[i] * Value;
            }
        }
    }
}