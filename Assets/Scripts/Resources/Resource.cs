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

        public void Source(float amount)
        {
            Amount = Math.Min(ResourceCap.Get(Type), Amount + amount);
        }

        public void Sink(float amount)
        {
            Amount = Math.Max(0f, Amount - amount);
        }
    }
}