using System.Collections.Generic;
using System.Linq;
using Resources;

namespace Interactions
{
    public class ResourceInteraction
    {
        public InterActionType Type;
        public List<ResourceTransfer> Sinks = new List<ResourceTransfer>();
        public List<ResourceTransfer> Sources = new List<ResourceTransfer>();
        public List<InteractionSpawn> Spawns = new List<InteractionSpawn>();
        public float Age;
        public float Life;

        public float FlowRate => Sinks.Sum(x => x.Amount) + Sources.Sum(y => y.Amount);
        public bool IsDead => Age > Life;

        public ResourceInteraction()
        {
        }

        public ResourceInteraction(LibraryEntry libraryEntry)
        {
            Type = libraryEntry.Type;
            libraryEntry.Sinks.ForEach(x => Sinks.Add(x));
            libraryEntry.Sources.ForEach(x => Sources.Add(x));
            libraryEntry.Spawns.ForEach(x => Spawns.Add(x));
            Life = libraryEntry.LifeTime;
            Age = 0;
        }

    }
}