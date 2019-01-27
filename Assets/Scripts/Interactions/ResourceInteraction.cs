using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Graph;
using Resources;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactions
{
    [Serializable]
    public class ResourceInteraction
    {
        public string Id;
        public InterActionType Type;
        public List<ResourceTransfer> Sinks = new List<ResourceTransfer>();
        public List<ResourceTransfer> Sources = new List<ResourceTransfer>();
        public List<InteractionSpawn> Spawns = new List<InteractionSpawn>();
        public float Age;
        private float _spawnRate;
        private float _spawnCounter;

        public float MaxFlowRate => Sinks.Sum(x => x.Amount) + Sources.Sum(y => y.Amount);
        public float CurrentFlowRate { get; private set; } = 0f;
        public bool IsDead => CurrentFlowRate <= 0.01f;

        private const float _flowRateMaxDelta = 0.1f;

        public void Consume(List<Resource> resources)
        {
            // resolve resource availability
            float availability = 1f;
            Sinks.ForEach(x =>
            {
                if (!resources.Exists(y => y.Type == x.Type))
                {
                    availability = 0;
                }
                else
                {
                    float sourceAmount = resources.First(y => y.Type == x.Type).Amount;
                    float thisAvailability = Mathf.Min(1f, sourceAmount / x.Amount);
                    availability = Mathf.Min(availability, thisAvailability);
                }
            });
            if (availability <= 0)
            {
                CurrentFlowRate = 0;
                return;
            }

            // smooth consume increase
            availability = Mathf.Min(availability, CurrentFlowRate + _flowRateMaxDelta);

            // consume resources
            Sinks.ForEach(x =>
            {
                var resource = resources.First(y => y.Type == x.Type);
                resource.Sink(x.Amount * availability);
            });

            // add resources
            Sources.ForEach(x =>
            {
                if (!resources.Exists(y => y.Type == x.Type))
                {
                    resources.Add(new Resource() { Amount = x.Amount, Type = x.Type });
                }
                else
                {
                    resources.First(y => y.Type == x.Type).Source(x.Amount * availability);
                }
            });

            // set flow rate
            CurrentFlowRate = availability;
        }

        public void Spawn(List<GraphNode> neighbors)
        {
            // spawn interaction to random neighbor node
            if (_spawnCounter >= _spawnRate)
            {
                int neighborIndex = Mathf.FloorToInt(Random.Range(0, neighbors.Count - 0.001f));
                Spawns.ForEach(spawn =>
                {
                    if (!neighbors[neighborIndex].Interactions.Exists(x => x.Id == spawn.Id))
                    {
                        neighbors[neighborIndex].Interactions.Add(
                            new ResourceInteraction(
                                spawn.Dependencies.First(y => y.Id == spawn.Id), spawn.Dependencies));
                    };
                });
                _spawnCounter -= _spawnRate;
            }
        }


        public void Tick()
        {
            Age++;
            _spawnCounter += CurrentFlowRate;
        }

        public ResourceInteraction()
        {
        }

        public ResourceInteraction(LibraryEntry libraryEntry, List<LibraryEntry> dependencies)
        {
            Id = libraryEntry.Id;
            Type = libraryEntry.Type;
            libraryEntry.Sinks.ForEach(x => Sinks.Add(x));
            libraryEntry.Sources.ForEach(x => Sources.Add(x));
            libraryEntry.Spawns.ForEach(x =>
            {
                Spawns.Add(x);
                Spawns[Spawns.Count - 1].Dependencies = dependencies;
                Spawns[Spawns.Count - 1].Threshold = x.Threshold;
                _spawnRate = Mathf.Max(x.Threshold, _spawnRate);
            });
            Age = 0;
        }

    }
}