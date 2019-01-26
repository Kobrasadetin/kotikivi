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
        public float Magnitude;
        [FormerlySerializedAs("LibraryEntries")] [HideInInspector]
        public List<LibraryEntry> Dependencies;
    }
}