using System;
using Random = UnityEngine.Random;

namespace Resources
{
    [Serializable]
    public class Resource
    {
        public ResourceType Type;
        public float Amount;

        public static Resource GetRandom(ResourceType type)
        {
            Resource result = new Resource()
            {
                Type = type,
                Amount = Random.Range(0f, 1f)
            };
            return result;
        }
    }
}