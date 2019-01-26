using System;

namespace Resources
{
    [Serializable]
    public enum ResourceType
    {
        WARMTH,
        WATER,
        NUTRIENT,
        LIGTH
    }

    public static class ResourceCap
    {
        public static float Get(ResourceType type)
        {
            return 5;
        }
    }
}