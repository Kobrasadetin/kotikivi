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
			UnityEngine.Profiling.Profiler.BeginSample("AudioVolumeStream Update");
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