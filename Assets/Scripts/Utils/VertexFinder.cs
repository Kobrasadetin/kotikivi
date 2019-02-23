using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexFinder : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public int index = 0;

    private Vector3[] original;
    private GameObject groundMeshGO;
    private Mesh groundMesh;
    private MeshRenderer groundMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        this.groundMeshGO = this.transform.gameObject;
        this.groundMesh = groundMeshGO.GetComponent<MeshFilter>().mesh;
        this.groundMeshRenderer = this.groundMeshGO.GetComponent<MeshRenderer>();
        original = groundMesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] v = (Vector3[])original.Clone();
        v[index] = original[index] + offset;
        groundMesh.vertices = v;
    }
}
