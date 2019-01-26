using System.Collections.Generic;
using System.Linq;
using Graph;
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

        public float MaxFlowRate => Sinks.Sum(x => x.Amount) + Sources.Sum(y => y.Amount);
        public float CurrentFlowRate { get; private set; }

        public void Consume(List<Resource> resources)
        {
            //TODO consume resources
            //TODO add resources
            //TODO set flow rate
            CurrentFlowRate = MaxFlowRate;
        }

        public void Spawn(List<GraphNode> neighbors)
        {
            //TODO spawn interaction to neighbor nodes according to current flow rate
        }

        public ResourceInteraction()
        {
        }

        public ResourceInteraction(LibraryEntry libraryEntry)
        {
            Type = libraryEntry.Type;
            libraryEntry.Sinks.ForEach(x => Sinks.Add(x));
            libraryEntry.Sources.ForEach(x => Sources.Add(x));
            libraryEntry.Spawns.ForEach(x => Spawns.Add(x));
            Age = 0;
        }

    }
}