using System;
using System.Collections.Generic;
using Resources;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Animal
{
    [Serializable]
    public enum AnimalFamily
    {
        WALKING,
        FLYING,
        AQUATIC
    }

    [Serializable]
    public enum AnimalDiet
    {
        VEGETARIAN,
        OMNIVOROUS,
        CARNIVOROUS
    }

    [Serializable]
    public class AnimalResourceInteraction
    {
        public ResourceType Type;
        public float Amount;
    }

    [Serializable]
    public class AnimalActivityChainEntry
    {
        public float Weight;
        public AnimalActivity Activity;
        public AnimalActivity[] OnlyAllowedNext;
    }

    [Serializable]
    public class AnimalEntry
    {
        public string Id;
        public UnityEngine.Object Visualization;
        public AnimalFamily Family;
        public AnimalDiet Diet;
        public AnimalResourceInteraction[] Sinks;
        public AnimalResourceInteraction[] Sources;
        public float Mass;
        public float Speed;
        public AnimalActivityChainEntry[] ActivityLoop;
        public string[] EatsOnly;
    }

    [Serializable]
    public class AnimalLibrary : ScriptableObject
    {
        public List<AnimalEntry> Entries;

#if UNITY_EDITOR
    [MenuItem("Kotikivi/Create Animal Library")]
    private static void CreateScriptableObject()
    {
        AnimalLibrary library = CreateInstance<AnimalLibrary>();
        AssetDatabase.CreateAsset(library, "Assets/AnimalLibrary.asset");
        AssetDatabase.SaveAssets();
    }
#endif
    }

}