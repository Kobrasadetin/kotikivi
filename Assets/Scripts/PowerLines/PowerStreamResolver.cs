using Dynamic;
using Graph;
using UnityEngine;
using Visual;

namespace PowerLines
{
    public enum RuneLocation
    {
        NONE,
        MIDDLE,
        LEFT,
        RIGHT,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT,
    }

    /**
     * Object lives in homebase and checks the rune positions to manage streams
     */
    public class PowerStreamResolver : MonoBehaviour
    {
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
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                if (_neighborCoords[i] == graphCoordinates)
                {
                    var runeLocation = (RuneLocation)(i + 2);
                    Debug.Log($"Power rune drop to {runeLocation}");
                    _runeLocations[(int) powerRune.RuneType] = runeLocation;
                    return;
                }
            }

            Debug.Log($"Power rune drop outside effective area");
            _runeLocations[(int) powerRune.RuneType] = RuneLocation.NONE;
        }


    }
}