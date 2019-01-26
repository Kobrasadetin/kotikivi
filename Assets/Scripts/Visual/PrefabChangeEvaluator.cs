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
}
