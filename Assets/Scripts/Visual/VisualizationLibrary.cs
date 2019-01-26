using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Visual
{
    [Serializable]
    public class LibraryEntry
    {
        public Interactions.InterActionType Type;
        public string Id;
        public GameObject gameObject;
        
    }

    [Serializable]
    public class VisualizationLibrary : ScriptableObject
    {
        public List<LibraryEntry> Entries;

        public LibraryEntry findEntryForType(Interactions.InterActionType type)
        {
            return Entries.First(x => x.Type == type);
        }
    


#if UNITY_EDITOR
        [MenuItem("Kotikivi/Create Visualization Library")]
        private static void CreateScriptableObject()
        {
            VisualizationLibrary library = CreateInstance<VisualizationLibrary>();
            AssetDatabase.CreateAsset(library, "Assets/VisualizationLibrary.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }

}