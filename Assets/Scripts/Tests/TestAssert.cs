using UnityEngine;
using UnityEditor;
namespace Tests
{
    public class TestAssert : ScriptableObject
    {
        public static void Assert(bool assertion)
        {
            if (!assertion)
            {
                throw new System.Exception("Assertion failed");
            }
        }
    }
}