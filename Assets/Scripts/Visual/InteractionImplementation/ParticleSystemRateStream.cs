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
            var emission = Particles.emission;
            emission.rate = new ParticleSystem.MinMaxCurve(0.0f, InitialRate * Value);
        }
    }
}