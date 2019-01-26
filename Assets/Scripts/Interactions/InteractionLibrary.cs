using System;
using System.Collections.Generic;
using Resources;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Interactions
{
    [Serializable]
    public class LibraryEntry
    {
        public string Id;
        public Object Visualization;
        public InterActionType Type;
        public List<ResourceTransfer> Sinks = new List<ResourceTransfer>();
        public List<ResourceTransfer> Sources = new List<ResourceTransfer>();
        public List<InteractionSpawn> Spawns = new List<InteractionSpawn>();
    }

    [Serializable]
    public class InteractionLibrary : ScriptableObject
    {
        public List<LibraryEntry> Entries;

#if UNITY_EDITOR
        [MenuItem("Kotikivi/Create Interaction Library")]
        private static void CreateScriptableObject()
        {
            InteractionLibrary library = CreateInstance<InteractionLibrary>();
            AssetDatabase.CreateAsset(library, "Assets/InteractionLibrary.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }

}