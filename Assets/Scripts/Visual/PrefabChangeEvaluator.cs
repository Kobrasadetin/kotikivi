using Graph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PrefabChangeEvaluator
{

    public static readonly float sideLength = (float)System.Math.Cos(System.Math.PI / 6.0);

    public abstract void UpdatePrefab(Visual.VisualNode visualNode, GraphNode node);

    public Vector2 calculatePosition(GraphNode node)
    {
        return calculatePosition(node.Coordinate);
    }

    public Vector3 calculatePositionWithHeight(GraphNode node)
    {
        Vector2 v = calculatePosition(node.Coordinate);
        return new Vector3(v.x, node.Height, v.y);
    }

    public Vector2 calculatePosition(Vector2Int coordinates)
    {
        float oddOffset = (coordinates.y % 2) * (sideLength);
        float xposition = coordinates.x * sideLength * 2 + oddOffset;
        float yposition = coordinates.y * 1.5f;
        return new Vector2(xposition, yposition);
    }

    public Vector2Int nearestGraphCoordinate(Vector3 worldCoordinate)
    {
        Vector2 w = new Vector2(worldCoordinate.x, worldCoordinate.z);

        int yposition1 = Mathf.RoundToInt(worldCoordinate.z / 1.5f - 0.70f);
        int xposition1 = Mathf.RoundToInt((worldCoordinate.x - sideLength) / sideLength  / 2);
        Vector2Int closest = new Vector2Int(0, 0);
        float dist = 10f;
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                Vector2Int p = new Vector2Int(x + xposition1, y + yposition1);
                var ndist = (calculatePosition(p) - w).magnitude;
                if (ndist < dist)
                {
                    closest = p;
                    dist = ndist;
                }
            }
        }
        return closest;
    }
}
