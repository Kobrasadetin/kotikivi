using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class LightIntensityResource : VisualResource
    {
        public Light Lght;

        private float initialIntensity;

        private void Start()
        {
            initialIntensity = Lght.intensity;
        }

        public override void SetValue()
        {
            Lght.intensity = Value * initialIntensity;
        }
    }
}