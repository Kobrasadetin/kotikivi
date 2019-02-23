using System.Collections.Generic;
using System.Linq;
using Dynamic;
using Graph;
using Interactions;
using UnityEngine;
using Visual;

namespace PowerLines
{
    public enum RuneLocation
    {
        NONE,
        MIDDLE,
		UPLEFT,
		UPRIGHT,
		RIGHT,
		DOWNRIGHT,
		DOWNLEFT,
		LEFT
	}

    public struct StreamConfiguration
    {
        public StreamAngle Angle;
        public PowerRuneType[] RuneTypes;
    }

    /**
     * Object lives in homebase and checks the rune positions to manage streams
     */
    public class PowerStreamResolver : MonoBehaviour
    {
        public float Power;
        public float Gradient;
        public InteractionLibrary InteractionLibrary;

        private Vector2Int _midNodeCoords = new Vector2Int();
        private Vector2Int[] _neighborCoords = new Vector2Int[6];
        private RuneLocation[] _runeLocations = new RuneLocation[4];

        void Start()
        {
            foreach (PowerRune powerRune in VisualRoot.PowerRunes)
            {
                powerRune.OnDroppedAction += OnPowerRuneDropped;
            }

            _midNodeCoords = GraphSingleton.Instance.MiddleNode.Coordinate;
            for (int i = 0; i < 6; i++)
            {
                _neighborCoords[i] = GraphSingleton.Instance.MiddleNode.GetNeighborInDirection((StreamAngle) i).Coordinate;
            }

            for (int i = 0; i < 4; i++)
            {
                _runeLocations[i] = RuneLocation.NONE;
            }
        }

        void OnPowerRuneDropped(PowerRune powerRune)
        {
            var graphCoordinates = powerRune.GetGraphCoordinates();

            //Debug.Log($"{powerRune.RuneType} dropped to {graphCoordinates}");
            if (graphCoordinates == _midNodeCoords)
            {
                Debug.Log($"Power rune drop to {RuneLocation.MIDDLE}");
                _runeLocations[(int) powerRune.RuneType] = RuneLocation.MIDDLE;
                ResolveAndActivateStreams();
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                if (_neighborCoords[i] == graphCoordinates)
                {
                    var runeLocation = (RuneLocation)(i + 2);
                    Debug.Log($"Power rune drop to {runeLocation}");
                    _runeLocations[(int) powerRune.RuneType] = runeLocation;
                    ResolveAndActivateStreams();
                    return;
                }
            }

            Debug.Log($"Power rune drop outside effective area");
            _runeLocations[(int) powerRune.RuneType] = RuneLocation.NONE;
            ResolveAndActivateStreams();
        }

        private void ResolveAndActivateStreams()
        {
            List<StreamConfiguration> streams = ResolveStreams();

            Graph.Graph graph = GraphSingleton.Instance.Graph;
            graph.Streams.ForEach(x => x.Target.Streams.Clear());
            graph.Streams.Clear();

            foreach (var stream in streams)
            {
                if (stream.RuneTypes.Length == 2)
                {
                    new PowerStream(graph, InteractionLibrary, stream.RuneTypes[0], stream.RuneTypes[1], stream.Angle, Power, Gradient);
                }
                else
                {
                    new PowerStream(graph, InteractionLibrary, stream.RuneTypes[0], stream.RuneTypes[1], stream.RuneTypes[2], stream.Angle, Power, Gradient);
                }
            }
        }

        private List<StreamConfiguration> ResolveStreams()
        {
            List<StreamConfiguration> results = new List<StreamConfiguration>();

            Dictionary<RuneLocation, PowerRuneType> occupied = new Dictionary<RuneLocation, PowerRuneType>();
            for (int i = 0; i < 4; i++)
            {
                if (!occupied.ContainsKey(_runeLocations[i]))
                {
                    occupied.Add(_runeLocations[i], (PowerRuneType)i);
                }
            }

            // 3-stone configurations

            if (occupied.Keys.Contains(RuneLocation.LEFT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE) &&
                occupied.Keys.Contains(RuneLocation.RIGHT))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.RIGHT,
                    RuneTypes = new PowerRuneType[3]
                    {
                        occupied[RuneLocation.LEFT],
                        occupied[RuneLocation.MIDDLE],
                        occupied[RuneLocation.RIGHT]
                    }
                });
                // dont' handle edge stones again
                occupied.Remove(RuneLocation.LEFT);
                occupied.Remove(RuneLocation.RIGHT);
            }

            if (occupied.Keys.Contains(RuneLocation.DOWNLEFT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE) &&
                occupied.Keys.Contains(RuneLocation.UPRIGHT))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.UPRIGHT,
                    RuneTypes = new PowerRuneType[3]
                    {
                        occupied[RuneLocation.DOWNLEFT],
                        occupied[RuneLocation.MIDDLE],
                        occupied[RuneLocation.UPRIGHT]
                    }
                });
                // dont' handle edge stones again
                occupied.Remove(RuneLocation.DOWNLEFT);
                occupied.Remove(RuneLocation.UPRIGHT);
            }

            if (occupied.Keys.Contains(RuneLocation.DOWNRIGHT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE) &&
                occupied.Keys.Contains(RuneLocation.UPLEFT))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.UPLEFT,
                    RuneTypes = new PowerRuneType[3]
                    {
                        occupied[RuneLocation.DOWNRIGHT],
                        occupied[RuneLocation.MIDDLE],
                        occupied[RuneLocation.UPLEFT]
                    }
                });
                // dont' handle edge stones again
                occupied.Remove(RuneLocation.DOWNRIGHT);
                occupied.Remove(RuneLocation.UPLEFT);
            }

            // 2-stone edge configurations

            if (occupied.Keys.Contains(RuneLocation.LEFT) &&
                occupied.Keys.Contains(RuneLocation.RIGHT))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.RIGHT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.LEFT],
                        occupied[RuneLocation.RIGHT]
                    }
                });
            }

            if (occupied.Keys.Contains(RuneLocation.UPLEFT) &&
                occupied.Keys.Contains(RuneLocation.DOWNRIGHT))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.DOWNRIGHT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.UPLEFT],
                        occupied[RuneLocation.DOWNRIGHT]
                    }
                });
            }

            if (occupied.Keys.Contains(RuneLocation.DOWNLEFT) &&
                occupied.Keys.Contains(RuneLocation.UPRIGHT))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.UPRIGHT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.DOWNLEFT],
                        occupied[RuneLocation.UPRIGHT]
                    }
                });
            }

            // 2-stone with middle configurations

            if (occupied.Keys.Contains(RuneLocation.LEFT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.RIGHT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.LEFT],
                        occupied[RuneLocation.MIDDLE]
                    }
                });
            }

            if (occupied.Keys.Contains(RuneLocation.RIGHT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.LEFT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.RIGHT],
                        occupied[RuneLocation.MIDDLE]
                    }
                });
            }

            if (occupied.Keys.Contains(RuneLocation.UPLEFT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.DOWNRIGHT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.UPLEFT],
                        occupied[RuneLocation.MIDDLE]
                    }
                });
            }

             if (occupied.Keys.Contains(RuneLocation.DOWNRIGHT) &&
                 occupied.Keys.Contains(RuneLocation.MIDDLE))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.UPLEFT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.DOWNRIGHT],
                        occupied[RuneLocation.MIDDLE]
                    }
                });
            }

            if (occupied.Keys.Contains(RuneLocation.DOWNLEFT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.UPRIGHT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.DOWNLEFT],
                        occupied[RuneLocation.MIDDLE]
                    }
                });
            }

           if (occupied.Keys.Contains(RuneLocation.UPRIGHT) &&
                occupied.Keys.Contains(RuneLocation.MIDDLE))
            {
                results.Add(new StreamConfiguration()
                {
                    Angle = StreamAngle.DOWNLEFT,
                    RuneTypes = new PowerRuneType[2]
                    {
                        occupied[RuneLocation.UPRIGHT],
                        occupied[RuneLocation.MIDDLE]
                    }
                });
            }

            return results;
        }

    }
}