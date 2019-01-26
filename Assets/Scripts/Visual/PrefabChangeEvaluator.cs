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
        float oddOffset = (node.Coordinate.y % 2) * (sideLength);
        float xposition = node.Coordinate.x * sideLength *2  + oddOffset;
        float yposition = node.Coordinate.y * 1.5f;
        return new Vector2(xposition, yposition);
    }
}
