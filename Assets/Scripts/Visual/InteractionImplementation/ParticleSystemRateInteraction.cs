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
            base.SetValue();
            var emission = Particles.emission;
            emission.rate = new ParticleSystem.MinMaxCurve(0.0f, InitialRate * Value);
        }
    }
}