using UnityEngine;

namespace Resources
{
    public class Resource
    {
        public ResourceType Type;
        public float Amount;

        public static Resource GetRandom(ResourceType type)
        {
            Resource result = new Resource()
            {
                Type = type,
                Amount = Random.Range(0, 1)
            };
            return result;
        }
    }
}