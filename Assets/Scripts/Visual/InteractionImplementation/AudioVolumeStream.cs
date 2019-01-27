using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class AudioVolumeStream : VisualStream
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
            if (Value > 0.01f && !Source.isPlaying)
            {
                Source.Play();
            }
            Source.volume = Value * InitialVolume;
        }
    }
}