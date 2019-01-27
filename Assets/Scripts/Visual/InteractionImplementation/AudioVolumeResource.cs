using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class AudioVolumeResource : VisualResource
    {
        public AudioSource Source;
        private float InitialVolume;

        private void Start()
        {
            InitialVolume = Source.volume;
        }

        public override void SetValue()
        {
            Source.gameObject.SetActive(Value > 0.01f);
            Source.volume = Value * InitialVolume;
        }
    }
}