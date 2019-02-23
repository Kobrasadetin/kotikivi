using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class ParticleSystemRateInteraction : VisualInteraction
    {
        public ParticleSystem Particles;
        private float InitialRate;

        void Start()
        {
            InitialRate = Particles.emission.rate.constantMax;
        }

        public override void SetValue()
        {
			UnityEngine.Profiling.Profiler.BeginSample("ParticleSystemRateInteraction Update");
			if (Value > 0.01f)
            {
                if (!Particles.isPlaying)
                {
                    Particles.Play();
                }
				var emission = Particles.emission;
				emission.rateOverTime = new ParticleSystem.MinMaxCurve(0.0f, InitialRate * Value);
			}
            else
            {
                if (Particles.isPlaying)
                {
                    Particles.Stop();
                }
            }
			UnityEngine.Profiling.Profiler.EndSample();

		}
    }
}