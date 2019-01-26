using System;
using System.Collections.Generic;
using System.Linq;
using Resources;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
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

        public LibraryEntry GetRandomInteraction(out List<LibraryEntry> dependencies)
        {
            var randomInteraction = Entries[Mathf.FloorToInt(Random.Range(0.001f, Entries.Count - 0.001f))];
            dependencies = GetDependencies(randomInteraction.Id);
            return randomInteraction;
        }

        public LibraryEntry GetRandomInteractionOfType(InterActionType type, out List<LibraryEntry> dependencies)
        {
            var randomInteraction = Entries[Mathf.FloorToInt(Random.Range(0.001f, Entries.Count - 0.001f))];
            dependencies = GetDependencies(randomInteraction.Id);
            return randomInteraction;
        }

        public List<LibraryEntry> GetDependencies(string id)
        {
            List<LibraryEntry> results = new List<LibraryEntry>();
            GetById(id).Spawns.ForEach(x => results.Add(GetById(x.Id)));
            return results;
        }

        private LibraryEntry GetById(string id)
        {
            return Entries.First(x => x.Id == id);
        }

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