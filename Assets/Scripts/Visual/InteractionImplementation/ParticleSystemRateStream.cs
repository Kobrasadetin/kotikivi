using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class ParticleSystemRateStream : VisualStream
    {
        public ParticleSystem Particles;
        private float InitialRate;

        void Start()
        {
            InitialRate = Particles.emission.rate.constantMax;
        }

        public override void SetValue()
        {
            if (Value > 0.01f)
            {
                if (!Particles.isPlaying)
                {
                    Particles.Play();
                }
            }
            else
            {
                if (Particles.isPlaying)
                {
                    Particles.Stop();
                }
            }
            var emission = Particles.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(0.0f, InitialRate * Value);
        }
    }
}