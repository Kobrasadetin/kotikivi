using System;
using System.Collections.Generic;
using Graph;
using Interactions;
using Resources;
using UnityEngine;

namespace PowerLines
{
    [Serializable]
    public enum StreamAngle
    {
        LEFT,
        RIGHT,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT,
    }

    public class PowerStream
    {
        public GraphNode Home;
        public List<ResourceStream> Generators { get; private set; } = new List<ResourceStream>();

        public PowerStream(Graph.Graph graph, InteractionLibrary library, GraphNode home, PowerRuneType a, PowerRuneType b, StreamAngle angle, float power, float gradient)
        {
            Home = home;
            List<ResourceType> resourceTypes = new List<ResourceType>();
            resourceTypes.Add(PowerRuneMixer.GetResourceType(a));
            resourceTypes.Add(PowerRuneMixer.GetResourceType(b));
            InterActionType interActionType = PowerRuneMixer.GetMix(new PowerRuneType[] {a, b});
            List<LibraryEntry> dependencies;
            LibraryEntry spawnType = library.GetRandomInteractionOfType(interActionType, out dependencies);
            // iterate to a direction
            float mainPower = power;
            GraphNode iter = Home.GetNeighborInDirection(angle);
            while (iter != null && mainPower > 0)
            {
                // create resourcestream
                var stream = new ResourceStream();
                stream.Transfers.Add(new ResourceTransfer() { Type = resourceTypes[0], Amount = mainPower});
                stream.Transfers.Add(new ResourceTransfer() { Type = resourceTypes[1], Amount = mainPower});
                stream.Interactions.Add(new ResourceInteraction(spawnType, dependencies));
                stream.Target = iter;
                graph.Streams.Add(stream);

                // gradient power decay
                mainPower = Mathf.Max(0f, mainPower - gradient);
                iter = iter.GetNeighborInDirection(angle);
            }
            // iterate to b direction
            mainPower = power;
            iter = Home.GetNeighborInDirection(GetOppositeAngle(angle));
            while (iter != null && mainPower > 0)
            {
                // drop resourcestream
                var stream = new ResourceStream();
                stream.Transfers.Add(new ResourceTransfer() { Type = resourceTypes[0], Amount = mainPower});
                stream.Transfers.Add(new ResourceTransfer() { Type = resourceTypes[1], Amount = mainPower});
                stream.Interactions.Add(new ResourceInteraction(spawnType, dependencies));
                stream.Target = iter;
                graph.Streams.Add(stream);

                // gradient power decay
                mainPower = Mathf.Max(0f, mainPower - gradient);
                iter = iter.GetNeighborInDirection(GetOppositeAngle(angle));
            }
        }

        public PowerStream(Graph graph, InteractionLibrary library, GraphNode home, PowerRuneType a, PowerRuneType b, PowerRuneType c, StreamAngle angle, float power, float gradient)
        {
            Home = home;
        }

        private static StreamAngle GetOppositeAngle(StreamAngle angle)
        {
            switch (angle)
            {
                case StreamAngle.LEFT:
                    return StreamAngle.RIGHT;
                case StreamAngle.RIGHT:
                    return StreamAngle.LEFT;
                case StreamAngle.UPLEFT:
                    return StreamAngle.DOWNRIGHT;
                case StreamAngle.UPRIGHT:
                    return StreamAngle.DOWNLEFT;
                case StreamAngle.DOWNLEFT:
                    return StreamAngle.UPRIGHT;
                case StreamAngle.DOWNRIGHT:
                    return StreamAngle.UPLEFT;
                default:
                    throw new ArgumentOutOfRangeException(nameof(angle), angle, null);
            }
        }

    }
}