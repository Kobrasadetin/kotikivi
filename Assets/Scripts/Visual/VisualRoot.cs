using Graph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
    public class VisualRoot : MonoBehaviour
    {
        Graph.Graph graph;
        // Start is called before the first frame update
        void Start()
        {
            graph = Graph.GraphSingleton.Instance.Graph;
            graph.Nodes.ForEach(node =>
               {
                   VisualNodeFactory.CreateTile(this.transform, node);
               }
            );
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}