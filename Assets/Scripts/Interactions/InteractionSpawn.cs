using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactions
{
    [Serializable]
    public class InteractionSpawn
    {
        public string Id;
        [FormerlySerializedAs("Magnitude")] public float Threshold;
        [FormerlySerializedAs("LibraryEntries")] [HideInInspector]
        public List<LibraryEntry> Dependencies;
    }
}