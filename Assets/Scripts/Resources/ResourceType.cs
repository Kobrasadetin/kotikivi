using System;

namespace Resources
{
    [Serializable]
    public enum ResourceType
    {
        WARMTH,
        WATER,
        NUTRIENT,
        LIGTH,
        PLANTS,
        BERRIES,
        CARCASSES,
        SEEDS
    }

    public static class ResourceCap
    {
        public static float Get(ResourceType type)
        {
            return 5.0f;
        }
    }
}