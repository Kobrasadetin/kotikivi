using UnityEngine;
using UnityEditor;
using PowerLines;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Graph {
    public class NeighborList<T> : IEnumerable<T>
    {
        private T[] neighbors;

        public NeighborList()
        {
            neighbors = new T[6];
        }

        public void SetNeighbor(StreamAngle angle, T neighbor)
        {
            neighbors[(int)angle] = neighbor;
        }

        public T GetNeigbor(StreamAngle angle)
        {
            return neighbors[(int)angle];
        }

        public bool Exists(System.Predicate<T> predicate)
        {
            return this.ToList().Exists(predicate);
        }
        public void ForEach(System.Action<T> action)
        {
            this.ToList().ForEach(action);
        }


        //IEnumerable implementation

        public IEnumerator<T> GetEnumerator()
        {
            return new GraphNodeEnumerator(neighbors);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class GraphNodeEnumerator : IEnumerator<T>
        {
            private int index;
            private T[] nodes;
            public GraphNodeEnumerator(T[] nodes)
            {
                this.nodes = nodes;
                this.index = -1;
            }

            public T Current {
                get
                {
                    return nodes[index];
                }
            }

            object IEnumerator.Current {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                while (++index < 6 && nodes[index] == null);
                return index < 6;
            }

            public void Reset()
            {
                index=-1;
            }
        }
    }
}