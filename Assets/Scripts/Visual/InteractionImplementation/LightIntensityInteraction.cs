using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class LightIntensityInteraction : VisualInteraction
    {
        public Light Lght;

        private float initialIntensity;

        private void Start()
        {
            initialIntensity = Lght.intensity;
        }

        public override void SetValue()
        {
			UnityEngine.Profiling.Profiler.BeginSample("LightIntensityInteraction Update");
			Lght.enabled = Value > 0.01f;
            Lght.intensity = Value * initialIntensity;
			UnityEngine.Profiling.Profiler.EndSample();
		}
    }
}