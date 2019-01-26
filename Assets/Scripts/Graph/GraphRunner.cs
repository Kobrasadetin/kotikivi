using System.Linq;
using UnityEngine;

namespace Graph
{
    [RequireComponent(typeof(GraphSingleton))]
    public class GraphRunner : MonoBehaviour
    {
        public float TickRate;

        public static GraphRunner Instance { get; private set; }

        public float CurrentTick;

        private float tickCounter = 0f;

        public void Awake()
        {
            Instance = this;
            CurrentTick = 0f;
        }

        public void Update()
        {
            TickAll();
        }

        private static void RemoveDeadInterActions()
        {
            GraphSingleton.Instance.Graph.Nodes.ForEach(x =>
            {
                for (int i = 0; i < x.Interactions.Count; i++)
                {
                    if (x.Interactions[i].IsDead)
                    {
                        x.Interactions.RemoveAt(i);
                        i--;
                    }
                }
            });
        }

        private void TickAll()
        {
            tickCounter += Time.deltaTime * TickRate;

            int nTicksToRun = Mathf.FloorToInt(tickCounter);
            for (int i = 0; i < nTicksToRun; i++)
            {
                GraphSingleton.Instance.Graph.Tick();
                RemoveDeadInterActions();
                CurrentTick++;
            }

            tickCounter -= nTicksToRun;
        }
    }
}