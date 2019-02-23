using UnityEngine;
using UnityEditor;
using PowerLines;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Collections.ObjectModel;

namespace Graph {
    public class NeighborList<T> : IEnumerable<T>
    {
        private T[] neighbors;
        private int count = 0;

        public NeighborList()
        {
            neighbors = new T[6];
        }

        public void SetNeighbor(StreamAngle angle, T neighbor)
        {
            T oldValue = neighbors[(int)angle];
            count -= oldValue == null ? 0 : 1;
            count += neighbor == null ? 0 : 1;
            neighbors[(int)angle] = neighbor;
        }

        public T GetNeigbor(StreamAngle angle)
        {
            return neighbors[(int)angle];
        }

        public T GetNeigbor(int angle)
        {
            return neighbors[angle];
        }

        public StreamAngle GetNeighborDirection(T neighborObject)
        {
            for (int i = 0; i < 6; i++)
            {
                if (neighbors[i] != null && neighbors[i].Equals(neighborObject))
                {
                    return (StreamAngle)i;
                }
            }
            throw new KeyNotFoundException();
        }

        public List<KeyValuePair<StreamAngle, T>> GetAnglesAndValues()
        {
            List<KeyValuePair<StreamAngle, T>> result = new List<KeyValuePair<StreamAngle, T>>();
            int index = 0;
            foreach (T obj in neighbors)
            {
                if (obj != null)
                {
                    result.Add(new KeyValuePair<StreamAngle, T>((StreamAngle)index, obj));
                }
                index++;
            }
            return result;
        }


        public int Count
        {
            get
            {
                int count = 0;
                foreach (T obj in neighbors)
                {
                    if (obj != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public bool Exists(System.Predicate<T> predicate)
        {
            foreach (T obj in neighbors)
            {
                if (obj != null && predicate.Invoke(obj))
                {
                    return true;
                }
            }
            return false;
        }

        public void ForEach(System.Action<T> action)
        {
            foreach (T obj in neighbors)
            {
                if (obj != null)
                {
                    action.Invoke(obj);
                }
            }
        }

        private int getIndex(T neighbor)
        {
            return System.Array.IndexOf(neighbors, neighbor);
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