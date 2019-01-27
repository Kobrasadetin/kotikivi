using UnityEngine;

namespace Visual.InteractionImplementations
{
    public class AudioVolumeInteraction : VisualInteraction
    {
        public AudioSource Source;
        private float InitialVolume;

        private void Start()
        {
            InitialVolume = Source.volume;
            if (Random.Range(0f, 1f) > 0.25f)
            {
                Source.enabled = false;
            }
        }

        public override void SetValue()
        {
            Source.gameObject.SetActive(Value > 0.01f);
            if (Value > 0.01f && Source.enabled && !Source.isPlaying)
            {
                Source.Play();
            }
            Source.volume = Value * InitialVolume;
        }
    }
}