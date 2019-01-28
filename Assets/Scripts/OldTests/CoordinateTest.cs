using System.Linq;
using Graph;
using UnityEngine;

namespace OldTests
{
    public class CoordinateTest : MonoBehaviour
    {
        void Start()
        {
            PrefabChangeEvaluator ev = new Visual.BasicPrefabEvaluator(null);
            for (float x = -100.0f; x < 120.0; x += 0.721f)
            {
                for (float y = -100.0f; y < 120.0; y += 0.721f)
                {
                    Vector3 coordinate = new Vector3(x, 0, y);
                    Vector2Int closest = ev.nearestGraphCoordinate(coordinate);
                    Vector2 nearCoord = ev.calculatePosition(closest);
                    Vector2 nextCoord = ev.calculatePosition(closest + new Vector2Int(1, 0));
                    Vector3 wc = new Vector3(nearCoord.x, 0, nearCoord.y);
                    TestAssert.Assert((wc - coordinate).magnitude <= 1.0f);
                }
            }
            Debug.Log("CoordinateTest done");
        }
        
    }
}