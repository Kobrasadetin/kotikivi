using UnityEngine;
using UnityEditor;
namespace Global
{
    public class GlobalVariables
    {
        private static Vector3 homeNodePosition;

        public static Vector3 GetHomeNodePosition()
        {
            if (homeNodePosition == null)
            {
                Debug.LogError("GLOBAL home not set");
            }
            return homeNodePosition;
        }

        public static void SetHomeNodePosition(Vector3 value)
        {
            homeNodePosition = value;
        }
    }
}