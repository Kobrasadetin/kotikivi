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
        [NonSerialized]
        public List<LibraryEntry> Dependencies;
    }
}