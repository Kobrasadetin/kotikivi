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
			UnityEngine.Profiling.Profiler.BeginSample("AudioVolumeInteraction Update");
			if (!Source.enabled) return;

            bool shouldPlay = Value > 0.01f;

            if (shouldPlay)
            {
                if (!Source.isPlaying) Source.Play();
                Source.volume = Value * InitialVolume;
            }
            else
            {
                if (Source.isPlaying) Source.Stop();
            }
			UnityEngine.Profiling.Profiler.EndSample();

		}
    }
}